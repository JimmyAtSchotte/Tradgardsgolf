using System;
using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Tournament;

public class QueryTournamentScores : IRequest<IEnumerable<TournamentScore>>
{
    public Guid TournamentId { get; init; }
}