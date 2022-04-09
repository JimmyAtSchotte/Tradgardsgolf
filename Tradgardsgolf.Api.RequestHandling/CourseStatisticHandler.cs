using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Statistics;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Tasks
{
    public class CourseStatisticHandler : IRequestHandler<CourseStatisticCommand, CourseStatistic>
    {
        private readonly IRoundRepository _roundRepository;

        public CourseStatisticHandler(IRoundRepository roundRepository)
        {
            _roundRepository = roundRepository;
        }


        public async Task<CourseStatistic> Handle(CourseStatisticCommand request, CancellationToken cancellationToken)
        {
            var rounds =  await _roundRepository.ListAsync(new AllRoundsByCourse(request.CourseId));
           
            return new CourseStatistic()
            {
                Rounds = rounds.Select(round => new RoundModel()
                {
                    Date = round.Date,
                    Scores = round.RoundScores.Select(score => new RoundScoreModel()
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

