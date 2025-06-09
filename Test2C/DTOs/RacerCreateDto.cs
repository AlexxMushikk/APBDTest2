using System.ComponentModel.DataAnnotations;

namespace Test2C.DTOs;

public class RacerCreateDto
{
    [Required, MaxLength(50)]
    public string FirstName { get; init; } = null!;

    [Required, MaxLength(50)]
    public string LastName { get; init; } = null!;
}