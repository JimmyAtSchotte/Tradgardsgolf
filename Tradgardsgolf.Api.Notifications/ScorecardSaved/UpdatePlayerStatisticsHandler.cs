using MediatR;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.PlayerStatistic;

namespace Tradgardsgolf.Api.RequestHandling.PlayerStatistic;

public class UpdatePlayerStatisticsHandler(IRepository repository) : INotificationHandler<ScorecardSavedNotification>
{
    public async Task Handle(ScorecardSavedNotification notification, CancellationToken cancellationToken)
    {
        foreach (var player in notification.Scorecard.Scores.Keys)
        {
            var playerStatistic = await repository.FirstOrDefaultAsync(Specs.PlayerStatistic.ByCoursePlayer(notification.Scorecard.CourseId, notification.Scorecard.CourseRevision, player), cancellationToken);

            if (playerStatistic is null)
                playerStatistic = Core.Entities.PlayerStatistic.Create(notification.Scorecard.CourseId, notification.Scorecard.CourseRevision, player);
            
            playerStatistic.Add(notification.Scorecard);
            
            if(playerStatistic.Id == Guid.Empty)
                await repository.AddAsync(playerStatistic, cancellationToken);
            else
                await repository.UpdateAsync(playerStatistic, cancellationToken);
        }

    }
}