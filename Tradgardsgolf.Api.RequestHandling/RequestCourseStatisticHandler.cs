using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Players;
using Tradgardsgolf.Contracts.Statistics;
using Tradgardsgolf.Core.Services.Course;

namespace Tradgardsgolf.Tasks
{
    public class RequestCourseStatisticHandler : IRequestHandler<RequestCourseStatistic, CourseStatistic>
    {
        private readonly ICourseService _courseService;

        public RequestCourseStatisticHandler(ICourseService courseService)
        {
            _courseService = courseService;
        }


        public async Task<CourseStatistic> Handle(RequestCourseStatistic request, CancellationToken cancellationToken)
        {
            var rounds = await _courseService.ListAllRounds(request.CourseId);
           
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

