
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Exceptions;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Services;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;
using Tradgardsgolf.Core.Specifications.CourseSeason;
using Tradgardsgolf.Core.Specifications.PlayerStatistic;

namespace Tradgardsgolf.Api.RequestHandling.Course;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class RecalculateStatisticsHandler(IRepository repository, IAuthenticationService authenticationService)
    : IRequestHandler<RecalculateStatisticsCommand, Unit>
{

    public async Task<Unit> Handle(RecalculateStatisticsCommand request, CancellationToken cancellationToken)
    {
        var user = authenticationService.RequireAuthenticatedUser();
        
        var course = await repository.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(request.CourseId), cancellationToken);

        if (course.OwnerGuid != user.UserId)
            throw new ForbiddenException();
        
        var scorecards = await repository.ListAsync(Specs.Scorecard.ByCourse(course.Id), cancellationToken);
        var playerStats = await repository.ListAsync(Specs.PlayerStatistic.ByCourse(course.Id), cancellationToken);
        var courseSeasons = await repository.ListAsync(Specs.CourseSeason.ByCourse(course.Id), cancellationToken);
        
        var courseStatisticService =  new CourseStatisticService(course, scorecards, playerStats.ToList(), courseSeasons.ToList());

        await MigrateCourseToRevision(courseStatisticService, cancellationToken);
        await MigrateScorecardsToRevision(courseStatisticService, cancellationToken);
        await GeneratePlayerStatistics(courseStatisticService, cancellationToken);
        await GenerateCourseSeasons(courseStatisticService, cancellationToken);
        
        return Unit.Value;
    }

    private async Task MigrateCourseToRevision(CourseStatisticService courseStatisticService, CancellationToken cancellationToken)
    {
        if (courseStatisticService.ShouldMigrateCourseToRevision())
            await repository.UpdateAsync(courseStatisticService.MigrateCourseToRevision(), cancellationToken);
    }

    private async Task MigrateScorecardsToRevision(CourseStatisticService courseStatisticService, CancellationToken cancellationToken)
    {
        var scorecards = courseStatisticService.MigrateScorecardsToRevision().ToList();

        if(scorecards.Count != 0)
            await repository.UpdateRangeAsync(scorecards.ToArray(), cancellationToken);
    }
    
    private async Task GeneratePlayerStatistics(CourseStatisticService courseStatisticService, CancellationToken cancellationToken)
    {
        var playerStatistics = courseStatisticService.GeneratePlayerStatistics().ToList();
        var addPlayerStatistics = playerStatistics.Where(x => x.Id == Guid.Empty).ToArray();
        var updatePlayerStatistics = playerStatistics.Where(x => x.Id != Guid.Empty).ToArray();

        if (addPlayerStatistics.Length != 0)
            await repository.AddRangeAsync(addPlayerStatistics, cancellationToken);

        if(updatePlayerStatistics.Length != 0)
            await repository.UpdateRangeAsync(updatePlayerStatistics, cancellationToken);
    }
    
    private async Task GenerateCourseSeasons(CourseStatisticService courseStatisticService, CancellationToken cancellationToken)
    {
        var courseSeasons = courseStatisticService.GenerateCourseSeasons().ToList();
        var addCourseSeasons = courseSeasons.Where(x => x.Id == Guid.Empty).ToArray();
        var updateCourseSeasons = courseSeasons.Where(x => x.Id != Guid.Empty).ToArray();

        if (addCourseSeasons.Length != 0)
           await repository.AddRangeAsync(addCourseSeasons, cancellationToken);

        if(updateCourseSeasons.Length != 0)
            await repository.UpdateRangeAsync(updateCourseSeasons, cancellationToken);
    }
}