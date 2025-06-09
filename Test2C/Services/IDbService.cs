using Test2C.DTOs;

namespace Test2C.Services;

public interface IDbService
{
    Task<RacerParticipationsDto> GetRacerParticipations(int racerId);
    Task AddTrackRaceParticipations(TrackRaceParticipationCreateDto dto);
    Task<int> AddRacerAsync(RacerCreateDto dto);
}