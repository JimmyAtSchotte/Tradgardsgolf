using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using Tradgardsgolf.Api.Shared;
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
            _scorecardService.Add(new AddScorecardCommand(request));

            return Ok(await Task.FromResult(new CourseScorecardAddResponse()));
        }
    }

    public class AddScorecardCommand : IAddScorecardCommand
    {
        private readonly CourseScorecardAddRequest _request;

        public AddScorecardCommand(CourseScorecardAddRequest request)
        {
            _request = request;
        }

        public int CourseId => _request.Id;
        public IEnumerable<IPlayerScoreCommand> PlayerScores => _request.PlayerScores.Select(x => new PlayerScoreCommand(x));
    }

    public class PlayerScoreCommand : IPlayerScoreCommand
    {
        private readonly PlayerScoreModel _playerScoreRequest;

        public PlayerScoreCommand(PlayerScoreModel playerScoreRequest)
        {
            _playerScoreRequest = playerScoreRequest;
        }

        public string Name => _playerScoreRequest.Player.Name;
        public int[] Scores => _playerScoreRequest.Scores
            .OrderBy(x => x.Hole)
            .Select(x => x.Score.GetValueOrDefault())
            .ToArray();
    }

    public class CourseScorecardAddResponse
    {
        
    }

    public class CourseScorecardAddRequest
    {
        [FromRoute]
        public int Id { get; set; }

        public IEnumerable<PlayerScoreModel> PlayerScores { get; set; }
    }
}