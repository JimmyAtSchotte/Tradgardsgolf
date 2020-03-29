using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradgardsgolf.Core.Services.Authentication;
using Tradgardsgolf.Core.Services.CreateLogin;

namespace Tradgardsgolf.Api.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ICreateLoginService _createLoginService;

        public LoginController(ICreateLoginService createLoginService)
        {
            _createLoginService = createLoginService;
        }

        [HttpPost("Create")]
        public ActionResult Create([FromBody] CreateLoginModel login)
        {
            var loginResult = _createLoginService.CreateLogin(login);

            if (loginResult.Status != Core.Enums.CreateLoginStatus.Success)
                return BadRequest();

            return Ok();
        }
    }
}