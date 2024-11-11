using System;
using System.Collections.Generic;
using System.Linq;

namespace Tradgardsgolf.Core.Entities;

public class CourseSeason : BaseEntity
{
    private Dictionary<string, List<int>> _players;
    
    public Guid CourseId { get; }
    public int Season { get; }

    public Dictionary<string, List<int>> Players
    {
        get => _players ??= new Dictionary<string, List<int>>();
        set => _players = value;
    }

    private CourseSeason(Guid courseId, int season)
    {
        CourseId = courseId;
        Season = season;
    }

    public static CourseSeason Create(Guid courseId, int season)
    {
        return new CourseSeason(courseId, season);
    }

    public void Add(Scorecard scorecard)
    {
        foreach (var score in scorecard.Scores)
        {
            if (!Players.TryGetValue(score.Key, out var scores))
            {
                scores = new List<int>();
                Players.Add(score.Key, scores);
            }
            
            scores.Add(score.Value.Sum());
        }
    }
}