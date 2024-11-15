using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tradgardsgolf.Contracts.Course;

namespace Tradgardsgolf.BlazorWasm.State;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class ScorecardState : BaseState
{
    [JsonConstructor]
    private ScorecardState()
    {
        
    }
    
    private ScorecardState(CourseResponse courseResponse, IEnumerable<PlayerScores> playerScores)
    {
        CourseResponse = courseResponse;
        PlayerScores = playerScores.ToList();
    }

    [JsonPropertyName("courseResponse")] public CourseResponse CourseResponse { get; set; }

    [JsonPropertyName("playerScores")] public List<PlayerScores> PlayerScores { get; set; }

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
        foreach (var playerScores in PlayerScores)
            playerScores.ResetScores();
        
        base.NotifyStateChange(source, nameof(PlayerScores));
        return Task.CompletedTask;
    }

    public async Task SetScore(ComponentBase source, string name, int hole, int score)
    {
        var player = PlayerScores.FirstOrDefault(x => x.PlayerResponse.Name == name);
        if (player == null)
            return;

        player.SetScore(hole, score);
        await Task.Delay(1);
        base.NotifyStateChange(source, nameof(PlayerScores));
    }
}