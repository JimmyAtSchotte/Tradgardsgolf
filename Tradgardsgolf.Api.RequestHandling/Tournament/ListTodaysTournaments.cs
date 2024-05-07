﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Api.RequestHandling.Tournament
{
    public class ListTodaysTournaments(IRepository<Core.Entities.Tournament> repository) : IRequestHandler<ListTodaysTournamentsCommand, IEnumerable<Contracts.Tournament.Tournament>>
    {
        public async Task<IEnumerable<Contracts.Tournament.Tournament>> Handle(ListTodaysTournamentsCommand request, CancellationToken cancellationToken)
        {
            var tournments = (await repository.ListAsync(cancellationToken)).Where(x => x.TournamentCourseDates.Any(c => c.CourseId == request.CourseId && c.Date == DateTime.Today));
            
            return tournments.Select(x => new Contracts.Tournament.Tournament()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}