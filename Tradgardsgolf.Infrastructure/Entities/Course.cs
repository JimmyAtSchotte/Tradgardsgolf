using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tradgardsgolf.Infrastructure.EntityBuilder;
using Tradgardsgolf.Infrastructure.SharedKernel;

namespace Tradgardsgolf.Infrastructure.Entities
{
    public class Course : BaseEntity<Course>
    {
        [Key]
        public int Id { get; internal set; }
        [Column("strName")]
        public String Name { get; internal set; }
        [Column("intHoles")]
        public int Holes { get; internal set; }
        [Column("dblLongitude")]
        public double Longitude { get; internal set; }
        [Column("dblLatitude")]
        public double Latitude { get; internal set; }
        [Column("intCreatedBy")]
        public int CreatedById { get; internal set; }
        public virtual Player CreatedBy { get; internal set; }
        [Column("dtmCreated")]
        public DateTime Created { get; internal set; }

        [NotMapped]
        public bool HasPlayedOnCourse { get; set; }

        /// <summary>
        /// Distance in meters
        /// </summary>
        [NotMapped]
        public double Distance { get; set; }

        public IReadOnlyCollection<Round> Rounds { get; set; }

        private Course()
        {
            Created = DateTime.Now;
        }

        public static Course Create(Action<CourseBuilder> options = null)
        {
            var course = new Course();
            course.SetOptions(options);

            return course;
        }
    }
}
