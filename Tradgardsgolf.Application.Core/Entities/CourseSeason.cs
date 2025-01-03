﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Tradgardsgolf.Core.Entities;

[SuppressMessage("ReSharper", "UnusedMember.Local")]
public class CourseSeason : BaseEntity
{
    private Dictionary<string, List<int>> _players;
    
    public Guid CourseId { get; private set;  }
    public string Season { get; private set; }

    public Dictionary<string, List<int>> Players
    {
        get => _players ??= new Dictionary<string, List<int>>();
        init => _players = value;
    }

    
    private CourseSeason()
    {
    }
    
    private CourseSeason(Guid courseId, string season)
    {
        CourseId = courseId;
        Season = season;
    }

    public static CourseSeason Create(Guid courseId, string season)
    {
        return new CourseSeason(courseId, season);
    }

    public void Add(Scorecard scorecard)
    {
        foreach (var score in scorecard.Scores)
        {
            if (!Players.TryGetValue(score.Key, out var scores))
            {
                scores = [];
                Players.Add(score.Key, scores);
            }
            
            scores.Add(score.Value.Sum());
        }
    }

    public void Reset()
    {
        Players.Clear();
    }
}