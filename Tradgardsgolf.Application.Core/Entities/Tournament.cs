using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Core.Entities
{
    public class Tournament : BaseEntity<Tournament>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TournamentCourseDate> TournamentCourseDates { get; set; }
        
        public void AddCourseDate(Course course, DateTime date)
        {
            if (TournamentCourseDates is null)
                TournamentCourseDates = new List<TournamentCourseDate>();
            
            TournamentCourseDates.Add(TournamentCourseDate.Create(course, date));
        }

        private Tournament()
        {
            
        }

        private Tournament(string name)
        {
            Name = name;
        }

        public static Tournament Create(string name)
        {
            return new Tournament(name);
        }
    }
}