using System;
using System.Collections.Generic;
using MediatR;

namespace Tradgardsgolf.Contracts.Tournament;

public class QueryTodaysTournamentsCommand : IRequest<IEnumerable<Tournament>>
{
    public Guid CourseId { get; init; }
}