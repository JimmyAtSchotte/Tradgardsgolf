﻿// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StatsMigration;
using Tradgardsgolf.Core.Services;
using Tradgardsgolf.Infrastructure.Database;


var services = new ServiceCollection();

var ruTracker = new CosmosRUTracker();

services.AddDbContext<TradgardsgolfContext>((_, dbContextOptionsBuilder) =>
{
    const string connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
    dbContextOptionsBuilder.UseCosmos(connectionString, "tradgardsgolf-db");
    
    
    dbContextOptionsBuilder.LogTo(log =>
    {
        ruTracker.Log(log, "PROGRAM");
    });
});

var provider = services.BuildServiceProvider();
var context = provider.GetRequiredService<TradgardsgolfContext>();

context.Database.EnsureCreated();

var courseMigrators = GetCourseMigrators(context).ToList();

foreach (var courseMigrator in courseMigrators)
{
    if (courseMigrator.ShouldMigrateCourseToRevision())
        context.Courses.Update(courseMigrator.MigrateCourseToRevision());

    var scorecards = courseMigrator.MigrateScorecardsToRevision().ToList();

    if(scorecards.Count != 0)
        context.Scorecards.UpdateRange(scorecards);

    var playerStatistics = courseMigrator.GeneratePlayerStatistics().ToList();
    var addPlayerStatistics = playerStatistics.Where(x => x.Id == Guid.Empty).ToList();
    var updatePlayerStatistics = playerStatistics.Where(x => x.Id != Guid.Empty).ToList();

    if (addPlayerStatistics.Count != 0)
        context.PlayerStatistic.AddRange(addPlayerStatistics);

    if(updatePlayerStatistics.Count != 0)
        context.PlayerStatistic.UpdateRange(updatePlayerStatistics);

    var courseSeasons = courseMigrator.GenerateCourseSeasons().ToList();
    var addCourseSeasons = courseSeasons.Where(x => x.Id == Guid.Empty).ToList();
    var updateCourseSeasons = courseSeasons.Where(x => x.Id != Guid.Empty).ToList();

    if (addCourseSeasons.Count != 0)
        context.CourseSeason.AddRange(addCourseSeasons);

    if(updateCourseSeasons.Count != 0)
        context.CourseSeason.UpdateRange(updateCourseSeasons);

    await context.SaveChangesAsync();
}

var ruUsages = ruTracker.TotalCharge("PROGRAM").ToList();

foreach (var ruUsage in ruUsages)
{
    Console.WriteLine($"[{ruUsage.DateTime:yyyy-MM-dd HH:mm:ss}] {ruUsage.Ru} RU");
}

Console.WriteLine($"Total: {ruUsages.Sum(x => x.Ru)} RU");

return;


static IEnumerable<CourseStatisticService> GetCourseMigrators(TradgardsgolfContext context)
{
    var courses = context.Courses.ToList();

    foreach (var course in courses)
    {
        var scorecards =  context.Scorecards.Where(x => x.CourseId == course.Id).ToList();
        var playerStats = context.PlayerStatistic.Where(x => x.CourseId == course.Id).ToList();
        var courseSeasons = context.CourseSeason.Where(x => x.CourseId == course.Id).ToList();

        yield return new CourseStatisticService(course, scorecards, playerStats, courseSeasons);
    }
}