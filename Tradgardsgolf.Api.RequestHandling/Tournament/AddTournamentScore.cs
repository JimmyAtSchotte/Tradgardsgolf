﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api.RequestHandling.Tournament
{
    public class AddTournamentRoundScore(IRepository<TournamentRound> repository)
        : IRequestHandler<AddTournamentRoundScoreCommand>
    {
        public async Task Handle(AddTournamentRoundScoreCommand request, CancellationToken cancellationToken)
        {
            await repository.AddAsync(new TournamentRound()
            {
                TournamentId = request.TournamentId,
                RoundId = request.RoundId
            }, cancellationToken);
            
        }
    }
}