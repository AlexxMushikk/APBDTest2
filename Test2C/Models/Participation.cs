using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2C.Models;

[Table("Participation")]
public class Participation
{
    [Key]
    public int ParticipationId { get; set; }
    [ForeignKey(nameof(Racer))]
    public int RacerId { get; set; }
    public int RaceId  { get; set; }
    public int TrackId { get; set; }

    public int Laps { get; set; }
    public int FinishTimeInSeconds { get; set; }
    public int Position { get; set; }

    public Racer Racer { get; set; } = null!;
    
    [ForeignKey("RaceId,TrackId")]
    public TrackRace TrackRace { get; set; } = null!;
}