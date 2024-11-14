using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tradgardsgolf.Contracts.Course;

namespace Tradgardsgolf.BlazorWasm.State;

public class ScorecardState : BaseState
{
    private ScorecardState(CourseResponse courseResponse, IEnumerable<PlayerScores> playerScores)
    {
        CourseResponse = courseResponse;
        PlayerScores = playerScores.ToList();
    }

    [JsonPropertyName("courseResponse")] public CourseResponse CourseResponse { get; init; }

    [JsonPropertyName("playerScores")] public List<PlayerScores> PlayerScores { get; init; }

    public static ScorecardState Create(CourseResponse courseResponseModel, params PlayerScores[] playerScores)
    {
        return new ScorecardState(courseResponseModel, playerScores);
    }

    public Task AddPlayer(ComponentBase source, string name)
    {
        var player = State.PlayerScores.Create(name, CourseResponse.Holes);
        PlayerScores.Add(player);
        base.NotifyStateChange(source, nameof(PlayerScores));
        return Task.CompletedTask;
    }

    public Task RemovePlayer(ComponentBase source, string name)
    {
        PlayerScores.RemoveAll(x => x.PlayerResponse.Name == name);
        base.NotifyStateChange(source, nameof(PlayerScores));
        return Task.CompletedTask;
    }

    public Task ResetScores(ComponentBase source)
    {
        foreach (var score in PlayerScores.SelectMany(player => player.Scores))
            score.Score = null;
        
        base.NotifyStateChange(source, nameof(PlayerScores));
        return Task.CompletedTask;
    }

    public async Task SetScore(ComponentBase source, string name, int hole, int score)
    {
        var player = PlayerScores.FirstOrDefault(x => x.PlayerResponse.Name == name);
        if (player == null)
            return;

        player.Scores[hole].Score = score;
        await Task.Delay(1);
        base.NotifyStateChange(source, nameof(PlayerScores));
    }
}