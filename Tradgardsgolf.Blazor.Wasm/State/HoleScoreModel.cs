namespace Tradgardsgolf.BlazorWasm.State;

public class HoleScoreModel
{
    public HoleScoreModel() { }

    private HoleScoreModel(int hole)
    {
        Hole = hole;
        Score = default;
    }

    public int Hole { get; set; }
    public int? Score { get; set; }

    public static HoleScoreModel Create(int hole)
    {
        return new HoleScoreModel(hole);
    }

    public override string ToString()
    {
        if (Score.HasValue)
            return Score.ToString();

        return "-";
    }
}