using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Core.Entities
{
    [Table("course")]
    public class Course : BaseEntity<Course>
    {
        private ICollection<Scorecard> _rounds;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Holes { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Created { get; private set; }
        public DateTime? ScoreReset { get; set; }
        public string Image { get; set; }
        public Guid OwnerGuid { get; set; }

        public ICollection<Scorecard> Scorecards
        {
            get => _rounds ??= new List<Scorecard>();
            set => _rounds = value;
        }

        private Course()
        {

        }               

        private Course(Guid ownerGuid)
        {
            Created = DateTime.Now;
            OwnerGuid = ownerGuid;
        }     

        public static Course Create(Guid ownerEmail, Action<Course> properties = null)
        {
            var course = new Course(ownerEmail);
            properties?.Invoke(course);

            return course;
        }
        
        public Scorecard CreateScorecard()
        {
            var round = Scorecard.Create(this);
            Scorecards.Add(round);
            return round;
        }
              
    }
}
