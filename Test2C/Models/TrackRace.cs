using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test2C.Models;

[Table("Track_Race")]
[PrimaryKey(nameof(RaceId), nameof(TrackId))]
public class TrackRace
{
    public int RaceId { get; set; }
    public int TrackId { get; set; }
    public int? BestTimeInSeconds { get; set; }
    public Race Race { get; set; } = null!;
    public Track Track { get; set; } = null!;
    
    public ICollection<Participation> Participations { get; set; } = null!;
}