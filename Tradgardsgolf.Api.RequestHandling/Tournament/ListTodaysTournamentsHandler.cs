using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications.Tournament;

namespace Tradgardsgolf.Api.RequestHandling.Tournament;

public class ListTodaysTournamentsHandler(IRepository<Core.Entities.Tournament> tournaments)
    : IRequestHandler<ListTodaysTournamentsCommand, IEnumerable<Contracts.Tournament.Tournament>>
{
    public async Task<IEnumerable<Contracts.Tournament.Tournament>> Handle(ListTodaysTournamentsCommand request,
        CancellationToken cancellationToken)
    {
        var list = await tournaments.ListAsync(new TournamentsOnCourse(request.CourseId, DateTime.Today), cancellationToken);

        return list.Select(x => new Contracts.Tournament.Tournament
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
}