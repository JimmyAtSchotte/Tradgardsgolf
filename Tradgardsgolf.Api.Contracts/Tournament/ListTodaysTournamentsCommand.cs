using System;
using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Tournament
{
    public class ListTodaysTournamentsCommand : IRequest<IEnumerable<Tournament>>
    {
        public Guid CourseId { get; set; }
        
    }
}