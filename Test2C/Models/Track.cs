using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test2C.Models;

[Table("Track")]
public class Track
{
    [Key]
    public int TrackId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;
    [Precision(5,2)]
    public double LengthInKm { get; set; }
    
    public ICollection<TrackRace> TrackRaces { get; set; } = null!;
}