using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradgardsgolf.Core.Enums;
using Tradgardsgolf.Core.Services.Authentication;

namespace Tradgardsgolf.Api.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse> Index([FromBody] CredentialsModel credentials)
        {
            var authenticationModelResult = _authenticationService.CredentialsAuthentication(credentials);

            if (authenticationModelResult.Status != AuthenticationStatus.Success)
                return Unauthorized();

            return Ok(new AuthenticationResponse(authenticationModelResult));
        }
    }
}