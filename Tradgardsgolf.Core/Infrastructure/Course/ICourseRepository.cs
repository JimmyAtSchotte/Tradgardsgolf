﻿using System.Collections.Generic;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Infrastructure.Course
{
    public interface ICourseRepository : IRepositoryBase<Entities.Course>
    {
        IEnumerable<ICourseDtoResult> ListAll();

        ICourseDtoResult Add(ICourseAddDto dto);

        IEnumerable<ICoursePlayerDtoResult> Players(ICoursePlayerDto dto);
    }

    public interface ICoursePlayerDtoResult
    {
        string Name { get; }
    }

    public interface ICoursePlayerDto
    {
        int Id { get; }
    }

    public interface ICourseAddDto
    {
        string Name { get; }
        int Holes { get; }
        double Longitude { get; }
        double Latitude { get; }
        int CreatedBy { get; }
    }
}
