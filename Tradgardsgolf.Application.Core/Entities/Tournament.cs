using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Core.Entities
{
    public class Tournament : BaseEntity<Tournament>
    {
        private ICollection<TournamentCourseDate> _tournamentCourseDates;
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<TournamentCourseDate> TournamentCourseDates
        {
            get => _tournamentCourseDates ??= new List<TournamentCourseDate>();
            set => _tournamentCourseDates = value;
        }
    }
}