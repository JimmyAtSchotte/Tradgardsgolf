using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Tradgardsgolf.Contracts.Players;

namespace Tradgardsgolf.BlazorWasm.State;

public class PlayerScores
{
    [JsonConstructor]
    private PlayerScores()
    {
        
    }
    
    private PlayerScores(PlayerResponse playerResponse, List<HoleScoreModel> scores)
    {
        PlayerResponse = playerResponse;
        Scores = scores;
    }

    public PlayerResponse PlayerResponse { get; set; }

    public List<HoleScoreModel> Scores { get; set; }

    public static PlayerScores Create(string name, int holes)
    {
        var scores = new List<HoleScoreModel>();

        for (var hole = 1; hole <= holes; hole++)
            scores.Add(HoleScoreModel.Create(hole));

        return new PlayerScores(new PlayerResponse
        {
            Name = name
        }, scores);
    }

    public int Total()
    {
        return  Scores.Sum(x => x.Score.GetValueOrDefault(0));
    }

    public bool MissingScores()
    {
        return Scores.Any(x => x.Score.GetValueOrDefault(0) <= 0);
    }
    
    public void SetScore(int hole, int? score)
    {
        var holeScore = Scores.FirstOrDefault(x => x.Hole == hole);

        if (holeScore is null)
        {
            holeScore = HoleScoreModel.Create(hole);
            Scores.Add(holeScore);
        }
        
        holeScore.Score = score;
    }
    
    public HoleScoreModel? GetHoleScore(int hole)
    {
        return Scores.FirstOrDefault(x => x.Hole == hole);
        
    }

    public void ResetScores()
    {
        foreach (var score in Scores)
            score.Score = null;
    }
}