using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Statistics;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Tasks
{
    public class CourseStatisticHandler : IRequestHandler<CourseStatisticCommand, CourseStatisticResponse>
    {
        private readonly IRepository<Round> _roundRepository;

        public CourseStatisticHandler(IRepository<Round> roundRepository)
        {
            _roundRepository = roundRepository;
        }


        public async Task<CourseStatisticResponse> Handle(CourseStatisticCommand request, CancellationToken cancellationToken)
        {
            var rounds =  await _roundRepository.ListAsync(new AllRoundsByCourse(request.CourseId));
           
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

