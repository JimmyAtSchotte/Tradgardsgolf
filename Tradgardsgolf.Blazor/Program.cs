using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tradgardsgolf.Core.Services.Course;
using Tradgardsgolf.Core.Services.CreateLogin;

namespace Tradgardsgolf.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var createLoginService = host.Services.GetService(typeof(ICreateLoginService)) as ICreateLoginService;

            createLoginService.CreateLogin(new CreateLoginModel("vaugh@hotmail.se", "1234"));
            createLoginService.CreateLogin(new CreateLoginModel("patrik@hotmail.se", "1234"));
            createLoginService.CreateLogin(new CreateLoginModel("conny@hotmail.se", "1234"));

            var courseService = host.Services.GetService(typeof(ICourseService)) as ICourseService;

            courseService.Add(new CourseAddModel("Kumhof", 6, 17.052026, 59.605530, 1));
            courseService.Add(new CourseAddModel("Törnehof", 6, 17.063828, 59.630690, 2));
            courseService.Add(new CourseAddModel("Sördeby karlsäng", 6, 18.437326, 60.292925, 3));

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
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
