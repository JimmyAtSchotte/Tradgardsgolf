namespace Tradgardsgolf.Blazor.Wasm.Data
{
    public class HoleScore
    {
        public int Hole { get; set; }
        public int? Score { get; set; }

        public HoleScore()
        {

        }

        private HoleScore(int hole)
        {
            Hole = hole;
            Score = default;
        }

        public static HoleScore Create(int hole)
        {
            return new HoleScore(hole);
        }

        public override string ToString()
        {
            if (Score.HasValue)
                return Score.ToString();

            return "-";
        }
    }
}
