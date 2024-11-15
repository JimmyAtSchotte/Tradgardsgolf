using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Tournament;

namespace Tradgardsgolf.Api.RequestHandling.Tournament;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class QueryTodaysTournamentsHandler(IRepository repository)
    : IRequestHandler<QueryTodaysTournamentsCommand, IEnumerable<Contracts.Tournament.Tournament>>
{
    public async Task<IEnumerable<Contracts.Tournament.Tournament>> Handle(QueryTodaysTournamentsCommand request,
        CancellationToken cancellationToken)
    {
        var list = await repository.ListAsync(Specs.Tournament.ByCourseAndDate(request.CourseId, DateTime.Today), cancellationToken);

        return list.Select(x => new Contracts.Tournament.Tournament
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
}