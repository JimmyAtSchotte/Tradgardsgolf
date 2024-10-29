using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Core.Entities;

[Table("course")]
public class Course : BaseEntity<Course>
{
    private ICollection<Scorecard> _rounds;

    private Course() { }

    private Course(Guid ownerGuid)
    {
        Created = DateTime.Now;
        OwnerGuid = ownerGuid;
    }

    public string Name { get; set; }
    public int Holes { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public DateTime Created { get; set; }
    public DateTime? ScoreReset { get; set; }
    public string Image { get; set; }
    public Guid OwnerGuid { get; set; }

    public ICollection<Scorecard> Scorecards
    {
        get => _rounds ??= new List<Scorecard>();
        set => _rounds = value;
    }

    public static Course Create(Guid ownerId, Action<Course> properties = null)
    {
        var course = new Course(ownerId);
        properties?.Invoke(course);

        return course;
    }

    public Scorecard CreateScorecard()
    {
        var scorecard = Scorecard.Create(this);
        Scorecards.Add(scorecard);
        return scorecard;
    }
}