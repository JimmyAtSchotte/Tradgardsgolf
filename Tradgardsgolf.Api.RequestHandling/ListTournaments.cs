using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tournament = Tradgardsgolf.Contracts.Tournament.Tournament;

namespace Tradgardsgolf.Tasks
{
    public class ListTournaments : IRequestHandler<ListTournamentsCommand, IEnumerable<Tournament>>
    {
        private readonly IRepository<TournamentCourseDate> _repository;

        public ListTournaments(IRepository<TournamentCourseDate> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Tournament>> Handle(ListTournamentsCommand request, CancellationToken cancellationToken)
        {
            var tournments = await _repository.ListAsync(new TournamentsOnCourse(request.CourseId, DateTime.Today));
            
            return tournments.Select(x => new Tournament()
            {
                Id = x.Tournament.Id,
                Name = x.Tournament.Name
            });
        }
    }
}