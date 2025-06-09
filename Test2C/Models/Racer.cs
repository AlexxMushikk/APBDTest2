using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2C.Models;

[Table("Racer")]
public class Racer
{
    [Key]
    public int RacerId { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(50)]
    public string LastName { get; set; } = null!;
    public ICollection<Participation> Participations { get; set; } = null!;
}