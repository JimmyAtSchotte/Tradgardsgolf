using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api.RequestHandling.Tournament;

public class ListTodaysTournaments(IRepository<Core.Entities.Tournament> tournaments)
    : IRequestHandler<ListTodaysTournamentsCommand, IEnumerable<Contracts.Tournament.Tournament>>
{
    public async Task<IEnumerable<Contracts.Tournament.Tournament>> Handle(ListTodaysTournamentsCommand request,
        CancellationToken cancellationToken)
    {
        var list = (await tournaments.ListAsync(cancellationToken)).Where(x =>
            x.TournamentCourseDates.Any(c => c.CourseId == request.CourseId && c.Date == DateTime.Today));

        return list.Select(x => new Contracts.Tournament.Tournament
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
}