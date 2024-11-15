using System.Diagnostics.CodeAnalysis;
using MediatR;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.CourseSeason;

namespace Tradgardsgolf.Api.Notifications.ScorecardSaved;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class UpdateCourseSeasonHandler(IRepository repository) : INotificationHandler<ScorecardSavedNotification>
{
    public async Task Handle(ScorecardSavedNotification notification, CancellationToken cancellationToken)
    {
        var courseSeason = await repository.FirstOrDefaultAsync(Specs.CourseSeason.ByCourseSeason(notification.Scorecard.CourseId, notification.Scorecard.GetSeason()), cancellationToken) ??
                           Core.Entities.CourseSeason.Create(notification.Scorecard.CourseId, notification.Scorecard.GetSeason());

        courseSeason.Add(notification.Scorecard);
        
        if(courseSeason.Id == Guid.Empty)
            await repository.AddAsync(courseSeason, cancellationToken);
        else
            await repository.UpdateAsync(courseSeason, cancellationToken);
    }
}