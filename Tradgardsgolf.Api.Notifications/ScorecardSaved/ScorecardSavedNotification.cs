using MediatR;

namespace Tradgardsgolf.Api.RequestHandling;

public class ScorecardSavedNotification : INotification
{
    public Core.Entities.Scorecard Scorecard { get; }

    public ScorecardSavedNotification(Core.Entities.Scorecard scorecard)
    {
        Scorecard = scorecard;
    }
}