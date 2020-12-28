using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using Tradgardsgolf.Api.Shared;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Services.Course;
using Tradgardsgolf.Core.Services.Round;
using Round = Tradgardsgolf.Api.Shared.Round;
using RoundScore = Tradgardsgolf.Api.Shared.RoundScore;

namespace Tradgardsgolf.Api.Endpoints.Courses
{
    public class Statistics : BaseAsyncEndpoint<CourseStatisticsRequest, CourseStatistic>
    {
        private readonly ICourseService _courseService;

        public Statistics(ICourseService roundService)
        {
            _courseService = roundService;
        }
        
        [HttpGet("Courses/{Id}/Statistics")]
        [SwaggerOperation(
            OperationId = "Course.Statistics",
            Tags = new[] { "Courses" })
        ]
        public override async Task<ActionResult<CourseStatistic>> HandleAsync([FromRoute] CourseStatisticsRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
           var rounds = await _courseService.ListAllRounds(request.Id);
           
           var courseStatistic = new CourseStatistic()
           {
               Rounds = rounds.Select(round => new Round()
               {
                   Date = round.Date,
                   Scores = round.RoundScores.Select(score => new RoundScore()
                   {
                        Player = score.Player.Name,
                        Hole = score.Hole,
                        Score = score.Score
                   })
               })
           };

           return Ok(courseStatistic);
        }
    }

    public class CourseStatisticsRequest
    {
        public int Id { get; set; }
    }
}