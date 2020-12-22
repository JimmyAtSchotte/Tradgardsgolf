using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Services.Player;

namespace Tradgardsgolf.Api.Endpoints.Courses
{
    public class Players : BaseAsyncEndpoint<CoursePlayerRequest, IEnumerable<CoursePlayerResponse> >
    {
        private readonly IPlayerService _playerService;

        public Players(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        
        [HttpGet("Courses/{Id}/Players")]
        [SwaggerOperation(
            OperationId = "Course.Players",
            Tags = new[] { "Courses" })
        ]
        public override async Task<ActionResult<IEnumerable<CoursePlayerResponse>>> HandleAsync([FromRoute] CoursePlayerRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            var players = await _playerService.ListPlayersThatHasPlayedOnCourseAsync(request.Id);

            return Ok(players.Select(x => new CoursePlayerResponse(x)));
        }
    }

    public class CoursePlayerResponse
    {
        private readonly Player _player;

        public int Id => _player.Id;
        public string Name => _player.Name;
        public CoursePlayerResponse(Player player)
        {
            _player = player;
        }
    }

    public class CoursePlayerRequest
    {
        public int Id { get; set; }
    }
}