﻿using System.Text.Json.Serialization;

namespace Tradgardsgolf.BlazorWasm.State;

public class HoleScoreModel
{
    [JsonConstructor]
    private HoleScoreModel()
    {
        
    }
    
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
        return Score.HasValue ? Score.Value.ToString() : "-";
    }
}