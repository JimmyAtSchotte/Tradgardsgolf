using MediatR;

namespace Tradgardsgolf.Contracts.Tournament;

public class QueryTournamentResultsCommand : IRequest<TournamentResultResponse[]>;
