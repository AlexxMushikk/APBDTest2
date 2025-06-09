namespace Test2C.DTOs;

public record ParticipationDto(
    RaceDto Race,
    TrackDto Track,
    int Laps,
    int FinishTimeInSeconds,
    int Position
);