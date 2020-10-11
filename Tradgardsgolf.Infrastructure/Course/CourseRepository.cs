using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tradgardsgolf.Core.Infrastructure.Course;
using Tradgardsgolf.Infrastructure.Context;

namespace Tradgardsgolf.Infrastructure.Course
{

    public class CourseRepository : BaseRepository, ICourseRepository
    {
        public CourseRepository(TradgardsgolfContext db) : base(db)
        {

        }      

        public IEnumerable<ICourseDtoResult> ListAll()
        {
            return db.Course.Include(x => x.CreatedBy).Select(x => new CourseDtoResult(x));
        }

        public ICourseDtoResult Add(ICourseAddDto dto)
        {
            var player = db.Player.Find(dto.CreatedBy);

           var course = player.CreateCourse(course =>
            {
                course.Name = dto.Name;
                course.Longitude = dto.Longitude;
                course.Latitude = dto.Latitude;
                course.Holes = dto.Holes;
            });

            db.Course.Add(course);
            db.SaveChanges();

            return new CourseDtoResult(course);
        }

        public IEnumerable<ICoursePlayerDtoResult> Players(ICoursePlayerDto dto)
        {
            return (from roundscore in db.RoundScore
                    join round in db.Round on roundscore.RoundId equals round.Id
                    join player in db.Player on roundscore.PlayerId equals player.Id
                    where round.CourseId == dto.Id
                    select new CoursePlayerDtoResult()
                    {
                        Name = player.Name
                    }).Distinct().ToList();
        }
    }

    public class CoursePlayerDtoResult : ICoursePlayerDtoResult
    {
        public string Name { get; set; }
    }

    public class CourseDtoResult : ICourseDtoResult
    {
        private readonly Context.Course _course;


        public int Id => _course.Id;
        public string Name => _course.Name;
        public int Holes => _course.Holes;
        public double Longitude => _course.Longitude;
        public double Latitude => _course.Latitude;
        public ICourseCreatedByDtoResult CreatedBy { get; }
        public DateTime Created => _course.Created;

        public CourseDtoResult(Context.Course course)
        {
            _course = course;
            CreatedBy = new CourseCreatedByDtoResult(course.CreatedBy);
        }
    }

    public class CourseCreatedByDtoResult : ICourseCreatedByDtoResult
    {
        private readonly Context.Player _player;

        public int Id => _player.Id;
        public string Name => _player.Name;

        public CourseCreatedByDtoResult(Context.Player player)
        {
            _player = player;
        }
    }
}
