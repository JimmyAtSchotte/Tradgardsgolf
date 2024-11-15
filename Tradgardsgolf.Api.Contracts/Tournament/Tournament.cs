using System;

namespace Tradgardsgolf.Contracts.Tournament;

public record Tournament
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
}