﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Core.Entities
{
    [Table("course")]
    public class Course : BaseEntity<Course>
    {
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
        [Column("intCreatedBy")]
        public int CreatedById { get; private set; }
        public virtual Player CreatedBy { get; private set; }
        [Column("dtmCreated")]
        public DateTime Created { get; private set; }
        
        [Column("dtmScoreReset")]
        public DateTime? ScoreReset { get; set; }
        
        
        public virtual ICollection<Round> Rounds { get; set; }

        private Course()
        {

        }               

        internal Course(Player player)
        {
            Created = DateTime.Now;
            CreatedById = player.Id;
            CreatedBy = player;
        }     

        public Round CreateRound()
        {
            return new Round(this);
        }
              
    }
}
