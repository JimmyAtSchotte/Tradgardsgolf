using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using Tradgardsgolf.Core.Services.Scorecard;

namespace Tradgardsgolf.Api.Endpoints.Courses.Scorecards
{
    public class Add : BaseAsyncEndpoint<CourseScorecardAddRequest, CourseScorecardAddResponse>
    {
        private readonly IScorecardService _scorecardService;

        public Add(IScorecardService scorecardService)
        {
            _scorecardService = scorecardService;
        }

        [HttpPost("Courses/{Id}/Scorecards")]
        [SwaggerOperation(
            OperationId = "Course.Scorecards.Add",
            Tags = new[] { "Courses" })
        ]
        public override async Task<ActionResult<CourseScorecardAddResponse>> HandleAsync([FromBody] CourseScorecardAddRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            _scorecardService.Add(new ScorecardModel(request));

            return Ok(await Task.FromResult(new CourseScorecardAddResponse()));
        }
    }

    public class ScorecardModel : IScorecardModel
    {
        private readonly CourseScorecardAddRequest _request;

        public ScorecardModel(CourseScorecardAddRequest request)
        {
            _request = request;
        }

        public int CourseId => _request.Id;
        public IEnumerable<IPlayerScoreModel> PlayerScores => _request.PlayerScores.Select(x => new PlayerScoreModel(x));
    }

    public class PlayerScoreModel : IPlayerScoreModel
    {
        private readonly PlayerScoreRequest _playerScoreRequest;

        public PlayerScoreModel(PlayerScoreRequest playerScoreRequest)
        {
            _playerScoreRequest = playerScoreRequest;
        }

        public string Name => _playerScoreRequest.Name;
        public int[] Scores => _playerScoreRequest.Scores;
    }

    public class CourseScorecardAddResponse
    {
    }

    public class CourseScorecardAddRequest
    {
        [FromRoute]
        public int Id { get; set; }

        public IEnumerable<PlayerScoreRequest> PlayerScores { get; set; }
    }

    public class PlayerScoreRequest
    {
        public string Name { get; set; }
        public int[] Scores { get; set; }
    }
}