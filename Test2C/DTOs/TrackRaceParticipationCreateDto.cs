using System.ComponentModel.DataAnnotations;

namespace Test2C.DTOs;

public class TrackRaceParticipationCreateDto
{
    [Required]
    public string RaceName { get; init; } = null!;

    [Required]
    public string TrackName { get; init; } = null!;

    [Required]
    public List<SingleParticipationCreateDto> Participations { get; init; } = null!;
}