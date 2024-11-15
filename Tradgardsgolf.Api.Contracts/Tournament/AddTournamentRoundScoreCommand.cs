using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Tournament;

public class AddTournamentRoundScoreCommand : IRequest
{
    public Guid TournamentId { get; init; }
    public Guid ScorecardId { get; init; }
}