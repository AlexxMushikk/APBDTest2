using System.ComponentModel.DataAnnotations;

namespace Test2C.DTOs;

public class SingleParticipationCreateDto
{
    [Required]
    public int RacerId { get; init; }

    [Range(1, int.MaxValue)]
    public int Position { get; init; }

    [Range(1, int.MaxValue)]
    public int FinishTimeInSeconds { get; init; }
}