﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Infrastructure.Context
{
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
        public int CreatedById { get; set; }
        public virtual Player CreatedBy { get; set; }
        [Column("dtmCreated")]
        public DateTime Created { get; set; }
              
    }
}
