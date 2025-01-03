﻿using System;
using Tradgardsgolf.Contracts.Types;

namespace Tradgardsgolf.Contracts.Course;

public record CourseResponse : IResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Holes { get; init; }
    public double Longitude { get; init; }
    public double Latitude { get; init; }
    public DateTime Created { get; init; }
    public DateTime? ScoreReset { get; init; }
    public ImageReference? ImageReference { get; init; }
    public int SeasonTableRounds => 6;
    public Guid OwnerGuid { get; init; }
    public int Revision { get; init; }
}