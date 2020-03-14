﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Infrastructure.Context
{
    public class Round : BaseEntity<Round>
    {
        [Key]
        public int Id { get; set; }

        [Column("intCourseId")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Column("dtmDate")]
        public DateTime Date { get; set; }    
  


    }
}
