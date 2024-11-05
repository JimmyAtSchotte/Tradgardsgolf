using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Core.Entities;

public class Scorecard : BaseEntity
{
    private IDictionary<string, int[]> _scores;

    private Scorecard() { }

    private Scorecard(Course course)
    {
        CourseId = course.Id;
        Course = course;
        Date = DateTime.Now;
    }

    public Guid CourseId { get; private set; }
    public DateTime Date { get; set; }
    public Course Course { get; private set; }

    public IDictionary<string, int[]> Scores
    {
        get => _scores ??= new Dictionary<string, int[]>();
        set => _scores = value;
    }

    public Guid TournamentId { get; set; }

    public void AddPlayerScores(string player, params int[] scores)
    {
        Scores.Add(player, scores);
    }

    public static Scorecard Create(Course course)
    {
        var scorecard = new Scorecard(course);
        return scorecard;
    }

    public bool ReplaceName(string oldnName, string newName)
    {
        if(!Scores.ContainsKey(oldnName))
            return false;
        
        Scores.Add(newName, _scores[oldnName]);
        Scores.Remove(oldnName);

        return true;
    }
}