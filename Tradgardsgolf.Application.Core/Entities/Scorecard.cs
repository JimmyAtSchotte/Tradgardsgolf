using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Core.Entities;

public class Scorecard : BaseEntity
{
    private IDictionary<string, int[]> _scores;
    
    
    public int CourseRevision { get; set; }
    public Guid CourseId { get; private set; }
    public DateTime Date { get; set; }
    public IDictionary<string, int[]> Scores
    {
        get => _scores ??= new Dictionary<string, int[]>();
        set => _scores = value;
    }
    public Guid TournamentId { get; set; }

    private Scorecard() { }
    
    private Scorecard(Guid courseId, int courseRevision)
    {
        CourseId = courseId;
        CourseRevision = courseRevision;
        Date = DateTime.Now;
    }

    public void AddPlayerScores(string player, params int[] scores)
    {
        Scores.Add(player, scores);
    }

    public bool ReplaceName(string oldnName, string newName)
    {
        if(!Scores.ContainsKey(oldnName))
            return false;
        
        Scores.Add(newName, _scores[oldnName]);
        Scores.Remove(oldnName);

        return true;
    }

    public static Scorecard Create(Guid courseId, int courseRevision)
    {
        var scorecard = new Scorecard(courseId, courseRevision);
        return scorecard;
    }

    public string GetSeason() => Date.Year.ToString();
    
}