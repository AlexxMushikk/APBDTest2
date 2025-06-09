namespace Test2C.DTOs;

public record RacerParticipationsDto(
    int RacerId,
    string FirstName,
    string LastName,
    IEnumerable<ParticipationDto> Participations
);