using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using Tradgardsgolf.Api.Shared;
using Tradgardsgolf.Core.Services.Player;

namespace Tradgardsgolf.Api.Endpoints.Courses
{
    public class Players : BaseAsyncEndpoint<CoursePlayerRequest, IEnumerable<PlayerModel> >
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
        public override async Task<ActionResult<IEnumerable<PlayerModel>>> HandleAsync([FromRoute] CoursePlayerRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            var players = await _playerService.ListPlayersThatHasPlayedOnCourseAsync(request.Id);

            var models = players.Select(x => new PlayerModel()
            {
                Id = x.Id,
                Name = x.Name
            });

            return Ok(models);
        }
    }
    
    public class CoursePlayerRequest
    {
        public int Id { get; set; }
    }
}