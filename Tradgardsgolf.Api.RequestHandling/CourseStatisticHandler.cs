using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Statistics;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Api.RequestHandling
{
    public class CourseStatisticHandler(IRepository<Round> roundRepository) : IRequestHandler<CourseStatisticCommand, CourseStatisticResponse>
    {
        public async Task<CourseStatisticResponse> Handle(CourseStatisticCommand request, CancellationToken cancellationToken)
        {
            var rounds =  await roundRepository.ListAsync(new AllRoundsByCourse(request.CourseId), cancellationToken);
           
            return new CourseStatisticResponse()
            {
                Rounds = rounds.Select(round => new RoundResponse()
                {
                    Date = round.Date,
                    Scores = round.RoundScores.Select(score => new RoundScoreResponse()
                    {
                        Player = score.Player.Name,
                        Hole = score.Hole,
                        Score = score.Score
                    })
                })
            };
        }
    }
}

