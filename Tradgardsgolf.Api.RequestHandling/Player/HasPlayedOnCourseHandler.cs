﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Players;
using Tradgardsgolf.Core.Infrastructure;
using HasPlayedOnCourse = Tradgardsgolf.Core.Specifications.HasPlayedOnCourse;

namespace Tradgardsgolf.Api.RequestHandling.Player
{
    public class HasPlayedOnCourseHandler(IRepository<Core.Entities.Player> playerRepository) : IRequestHandler<HasPlayedOnCourseCommand, IEnumerable<PlayerResponse>>
    {
        public async Task<IEnumerable<PlayerResponse>> Handle(HasPlayedOnCourseCommand request, CancellationToken cancellationToken)
        {
            return (await playerRepository.ListAsync(new HasPlayedOnCourse(request.CourseId), cancellationToken))
                    .Select(player => new PlayerResponse()
                    {
                        Name = player.Name
                    }).ToList();
        }
    }
}