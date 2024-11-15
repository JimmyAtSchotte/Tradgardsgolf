using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Tournament;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class QueryTournamentResultsHandler : IRequestHandler<QueryTournamentResultsCommand, TournamentResultResponse[]>
{
    private readonly IRepository _repository;
    
    public QueryTournamentResultsHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<TournamentResultResponse[]> Handle(QueryTournamentResultsCommand request, CancellationToken cancellationToken)
    {
        var tournaments = await _repository.ListAsync<Core.Entities.Tournament>(cancellationToken);
        var results = new List<TournamentResultResponse>();
        
        foreach (var tournament in tournaments.OrderByDescending(x => x.TournamentCourseDates.Min(dates => dates.Date)))
        {
            var result = new TournamentResultResponse
            {
                Name = tournament.Name
            };

            var scorecards = await _repository.ListAsync(Specs.Scorecard.ByTournament(tournament.Id), cancellationToken);
            
            result.PlayerTournamentScores = scorecards.SelectMany(x => x.Scores)
                .GroupBy(x => x.Key)
                .Select(x => new PlayerTournamentScore
                {
                    Name = x.Key,
                    Results = x.Select(s => s.Value.Sum()).ToList(),
                    Total =  x.SelectMany(s => s.Value).Sum()
                })
                .OrderByDescending(x => x.Results.Count())
                .ThenBy(x => x.Total).ToList();
            
            results.Add(result);
        }

        return results.ToArray();
    }
}