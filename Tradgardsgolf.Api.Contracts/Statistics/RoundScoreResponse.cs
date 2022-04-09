namespace Tradgardsgolf.Contracts.Statistics
{
    public record RoundScoreResponse
    {
        public string Player { get; init; }
        public int Hole { get; init; }
        public int Score { get; init; }
    }
}