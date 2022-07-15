using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Tournament
{
    public class ListTournamentsCommand : IRequest<IEnumerable<Tournament>>
    {
        public int CourseId { get; set; }
        
    }
}