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
        private ICollection<Round> _rounds;

        [Key]
        public int Id { get; set; }
        [Column("strName")]
        public string Name { get; set; }
        [Column("intHoles")]
        public int Holes { get; set; }
        [Column("dblLongitude")]
        public double Longitude { get; set; }
        [Column("dblLatitude")]
        public double Latitude { get; set; }

        [Column("dtmCreated")]
        public DateTime Created { get; private set; }
        
        [Column("dtmScoreReset")]
        public DateTime? ScoreReset { get; set; }
        
        [Column("strImage")]
        public string Image { get; set; }
        
        [Column("strOwnerGuid")]
        public Guid OwnerGuid { get; set; }

        public virtual ICollection<Round> Rounds
        {
            get => _rounds ??= new List<Round>();
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
        
        public Round CreateRound()
        {
            var round = Round.Create(this);
            Rounds.Add(round);
            return round;
        }
              
    }
}
