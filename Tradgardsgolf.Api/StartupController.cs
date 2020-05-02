using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tradgardsgolf.Core.Services.Course;
using Tradgardsgolf.Core.Services.CreateLogin;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tradgardsgolf.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartupController : ControllerBase
    {
        private readonly ICreateLoginService _createLoginService;
        private readonly ICourseService _courseService;

        public StartupController(ICreateLoginService createLoginService, ICourseService courseService)
        {
            _createLoginService = createLoginService;
            _courseService = courseService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _createLoginService.CreateLogin(new CreateLoginModel("vaugh@hotmail.se", "1234"));
            _createLoginService.CreateLogin(new CreateLoginModel("patrik@hotmail.se", "1234"));
            _createLoginService.CreateLogin(new CreateLoginModel("conny@hotmail.se", "1234"));
            _courseService.Add(new CourseAddModel("Kumhof", 6, 17.052026, 59.605530, 1));
            _courseService.Add(new CourseAddModel("Törnehof", 6, 17.063828, 59.630690, 2));
            _courseService.Add(new CourseAddModel("Sördeby karlsäng", 6, 18.437326, 60.292925, 3));

            return Ok();
        }
    }

    public class CreateLoginModel : ICreateLoginModel
    {      

        public string Email { get; }
        public string Password { get; }

        public CreateLoginModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }


    public class CourseAddModel : ICourseAddModel
    {      
        public string Name { get; }
        public int Holes { get; }
        public double Longitude { get; }
        public double Latitude { get; }
        public int CreatedBy { get; }

        public CourseAddModel(string name, int holes, double longitude, double latitude, int createdBy)
        {
            Name = name;
            Holes = holes;
            Longitude = longitude;
            Latitude = latitude;
            CreatedBy = createdBy;
        }

    }
}
