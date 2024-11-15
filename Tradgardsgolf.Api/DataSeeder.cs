using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Services;
using Tradgardsgolf.Infrastructure.Database;

namespace Tradgardsgolf.Api;

public class DataSeeder
{
    private readonly TradgardsgolfContext _context;
    private List<Course> _courses;
    private List<Scorecard> _scorecards;
    

    private DataSeeder(TradgardsgolfContext context)
    {
        _context = context;
        _courses = [];
        _scorecards = [];
    }

    public static DataSeeder Create(TradgardsgolfContext context) => new DataSeeder(context);

    public async Task SeedDataAsync()
    {
        await SeedCoursesAsync();
        await SeedTournamentsAsync();
        await GenerateStatisticsAsync();
        await _context.SaveChangesAsync();
    }

    private async Task SeedCoursesAsync()
    {
        var kumhof = Course.Create(Guid.Empty, p =>
        {
            p.Holes = 6;
            p.Name = "Kumhof (Local)";
            p.Latitude = 59.331181;
            p.Longitude = 18.040736;
            p.ScoreReset = DateTime.Today.AddYears(-2).AddDays(-1);
        });

        var tornehof = Course.Create(Guid.Empty, p =>
        {
            p.Holes = 6;
            p.Name = "Törnehof (Local)";
            p.Latitude = 59.5751198688695;
            p.Longitude = 17.120523690349092;
        });

        var berlin = Course.Create(Guid.Empty, p =>
        {
            p.Holes = 6;
            p.Name = "Berlin (Local)";
            p.Latitude = 52.520007;
            p.Longitude = 13.404954;
        });

        await _context.AddAsync(kumhof);
        await _context.AddAsync(tornehof);
        await _context.AddAsync(berlin);
        
        _courses.Add(kumhof);
        _courses.Add(tornehof);
        _courses.Add(berlin);

        await SeedScorecardsForCoursesAsync(kumhof, tornehof, berlin);
    }

    private async Task SeedScorecardsForCoursesAsync(Course kumhof, Course tornehof, Course berlin)
    {
        var players = new[] { "Jimmy", "Patrik", "Amanda", "Hanna" };

        for (var r = 0; r < 15; r++)
            await AddScorecardAsync(kumhof, DateTime.Today.AddYears(-2), players);

        for (var r = 0; r < 14; r++)
            await AddScorecardAsync(kumhof, DateTime.Today.AddYears(-1), players);

        for (var r = 0; r < 16; r++)
            await AddScorecardAsync(kumhof, DateTime.Today, players);

        for (var r = 0; r < 12; r++)
            await AddScorecardAsync(tornehof, DateTime.Today, players);

        for (var r = 0; r < 4; r++)
            await AddScorecardAsync(berlin, DateTime.Today, players);
    }

    private async Task SeedTournamentsAsync()
    {
        for (var i = 1; i < 5; i++)
        {
            var previousTournament = Tournament.Create($"Touren {DateTime.Today.AddYears(-i).Year}");
            await AddTournamentCoursesAndScorecardsAsync(previousTournament, i);
        }

        var todaysTournament = Tournament.Create($"Touren {DateTime.Today.Year}");
        todaysTournament.AddCourseDate(Guid.Empty, DateTime.Today); // Replace with course IDs as needed
        await _context.AddAsync(todaysTournament);
    }

    private async Task AddTournamentCoursesAndScorecardsAsync(Tournament tournament, int yearsAgo)
    {
        var date = DateTime.Today.AddYears(-yearsAgo);
        await _context.AddAsync(tournament);

        foreach (var course in _courses)
        {
            tournament.AddCourseDate(course.Id, date);

            for (var r = 0; r < 2; r++)
                await AddScorecardAsync(course, date, new[] { "Jimmy", "Patrik", "Amanda", "Hanna" }, tournament);

            for (var r = 0; r < 2; r++)
                await AddScorecardAsync(course, date, new[] { "Kalle", "Bengt" }, tournament);
        }
    }

    private async Task GenerateStatisticsAsync()
    {
        foreach (var course in _courses)
        {
            var scorecards = _scorecards.Where(x => x.CourseId == course.Id).ToList();
            var courseStatisticService = new CourseStatisticService(course, scorecards, [], []);

            var playerStatistics = courseStatisticService.GeneratePlayerStatistics().ToList();
            await _context.AddRangeAsync(playerStatistics);

            var courseSeasons = courseStatisticService.GenerateCourseSeasons().ToList();
            await _context.AddRangeAsync(courseSeasons);
        }
    }

    private async Task AddScorecardAsync(Course course, DateTime date, string[] players, Tournament tournament = null)
    {
        var scorecard = Scorecard.Create(course.Id, course.GetRevision());
        scorecard.TournamentId = tournament?.Id ?? Guid.Empty;
        scorecard.Date = date;

        foreach (var player in players)
        {
            var scores = Enumerable.Repeat(0, course.Holes).Select(_ => GenerateRandomScore()).ToArray();
            scorecard.AddPlayerScores(player, scores);
        }

        await _context.AddAsync(scorecard);
        
        _scorecards.Add(scorecard);
    }

    private static int GenerateRandomScore()
    {
        var score = Random.Shared.Next(0, 100);
        return score switch
        {
            >= 95 => 1,
            >= 85 => 2,
            >= 50 => 3,
            >= 30 => 4,
            >= 15 => 5,
            _ => 6
        };
    }
}