using System;
using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Scorecard;

public record SaveScorecardCommand : IRequest<ScorecardResponse>
{
    public IEnumerable<PlayerScore> PlayerScores { get; init; } = new List<PlayerScore>();
    public Guid CourseId { get; init; }
    
    public int Revision { get; init; }
}