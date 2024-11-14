using System;
using System.Diagnostics.CodeAnalysis;

namespace Tradgardsgolf.Core.Entities;

[SuppressMessage("ReSharper", "UnusedMember.Local")]
public class Course : BaseEntity
{

    public string Name { get; set; }
    public int Holes { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public DateTime Created { get; init; }
    public DateTime? ScoreReset { get; set; }
    public string Image { get; set; }
    public Guid OwnerGuid { get; set; }
    public int Revision { get; set; }
    
    private Course() { }

    private Course(Guid ownerGuid)
    {
        Created = DateTime.Now;
        OwnerGuid = ownerGuid;
    }
    
    public static Course Create(Guid ownerId, Action<Course> properties = null)
    {
        var course = new Course(ownerId);
        properties?.Invoke(course);

        return course;
    }

    public void ResetScore(DateTime date)
    {
        ScoreReset = date;
        Revision++;
    }
}