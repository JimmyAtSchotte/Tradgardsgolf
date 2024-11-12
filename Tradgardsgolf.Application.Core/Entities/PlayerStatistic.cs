using System;
using System.Collections.Generic;
using System.Linq;

namespace Tradgardsgolf.Core.Entities;

public class PlayerStatistic : BaseEntity
{
    private BestScore _bestScore;
    private ICollection<HoleStatistic> _holeStatistics;

    public string Name { get; private set; }
    public Guid CourseId { get; private set; }
    public double AverageScore { get; set; }
    public int TimesPlayed { get; set; }
    public int CourseRevision { get; private set; }

    public ICollection<HoleStatistic> HoleStatistics
    {
        get => _holeStatistics ??= new List<HoleStatistic>();
        set => _holeStatistics = value;
    }

    public BestScore BestScore
    {
        get => _bestScore ??= new BestScore();
        set => _bestScore = value;
    }
    

    private PlayerStatistic(Guid courseId, int courseRevision, string name)
    {
        CourseId = courseId;
        CourseRevision = courseRevision;
        Name = name;
    }

    private PlayerStatistic()
    {
        
    }


    public static PlayerStatistic Create(Guid courseId, int courseRevision, string name)
    {
        return new PlayerStatistic(courseId, courseRevision, name);
    }

    public void Add(Scorecard scorecard)
    {
        if(!scorecard.Scores.ContainsKey(Name))
            return;
        
        if(scorecard.CourseRevision != CourseRevision)
            return;
            
        var scores = scorecard.Scores[Name];
        var sum = scores.Sum();
        
        UpdateAverageScore(sum);
        UpdateHoleStatistics(scores);
        UpdateBestScore(scorecard, sum);

        TimesPlayed += 1;
    }

 

    private void UpdateBestScore(Scorecard scorecard, int sum)
    {
        if (BestScore.Score != 0 && sum >= BestScore.Score) 
            return;
        
        BestScore.Score = sum;
        BestScore.Date = scorecard.Date;
    }

    private void UpdateHoleStatistics(int[] scores)
    {
        while (HoleStatistics.Count < scores.Length)
            HoleStatistics.Add(new HoleStatistic()); 
        
        for (var i = 0; i < scores.Length; i++)
        {
            var holeStatistic = HoleStatistics.ElementAt(i);
        
            holeStatistic.AverageScore = ((holeStatistic.AverageScore * TimesPlayed) + scores[i]) / (TimesPlayed + 1);

            if (scores[i] == 1)
                holeStatistic.HoleInOnes++;
        }
    }

    private void UpdateAverageScore(int sum)
    {
        AverageScore = ((AverageScore * TimesPlayed) + sum) / (TimesPlayed + 1);
    }

    public void Reset()
    {
        AverageScore = 0;
        TimesPlayed = 0;
        BestScore = new BestScore();
        HoleStatistics = new List<HoleStatistic>();
    }
}

public class BestScore
{
    public int Score { get; set; }
    public DateTime Date { get; set; }
}

public class HoleStatistic
{
    public double AverageScore { get; set; }
    public int HoleInOnes { get; set; }
}