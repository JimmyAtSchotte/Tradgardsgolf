namespace Tradgardsgolf.Contracts.Players
{
    public record PlayerResponse
    {
        public string Name { get; init; }
        public int Id { get; init; }
    }
}