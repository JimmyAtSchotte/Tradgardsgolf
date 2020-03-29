using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tradgardsgolf.Api.Authorization
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        [Authorize]
        [HttpGet("IsAuthorized")]
        public ActionResult IsAuthorized()
        {
            return Ok();
        }
    }
}