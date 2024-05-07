using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Statistics;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Course
{
    public class CourseStatisticHandler(IRepository<Core.Entities.Scorecard> roundRepository) : IRequestHandler<CourseStatisticCommand, CourseStatisticResponse>
    {
        public async Task<CourseStatisticResponse> Handle(CourseStatisticCommand request, CancellationToken cancellationToken)
        {
            var rounds =  await roundRepository.ListAsync(new ByCourse(request.CourseId), cancellationToken);
           
            return new CourseStatisticResponse()
            {
                Rounds = rounds.Select(round => new RoundResponse()
                {
                    Date = round.Date,
                    Scores = round.Scores.SelectMany(keyValuePair => keyValuePair.Value.Select((score, hole) => new RoundScoreResponse()
                    {
                        Player = keyValuePair.Key,
                        Hole = hole + 1,
                        Score = score
                    }))
                })
            };
        }
    }
}

