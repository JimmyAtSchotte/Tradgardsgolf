namespace Tradgardsgolf.Contracts.Players
{
    public record Player
    {
        public string Name { get; init; }
        public int Id { get; init; }
    }
}