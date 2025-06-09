
using Test2C.Exceptions;
using Microsoft.EntityFrameworkCore;
using Test2C.Data;
using Test2C.DTOs;
using Test2C.Models;

namespace Test2C.Services;

public class DbService(DatabaseContext ctx) : IDbService
{
    public async Task<RacerParticipationsDto> GetRacerParticipations(int racerId)
    {
        var racer = await ctx.Racers
            .Include(r => r.Participations)
                .ThenInclude(p => p.TrackRace)
                    .ThenInclude(tr => tr.Race)
            .Include(r => r.Participations)
                .ThenInclude(p => p.TrackRace)
                    .ThenInclude(tr => tr.Track)
            .SingleOrDefaultAsync(r => r.RacerId == racerId);

        if (racer == null)
            throw new NotFoundException($"Racer with id={racerId} not found");

        var parts = racer.Participations.Select(p => new ParticipationDto(
            new RaceDto(p.TrackRace.Race.Name, p.TrackRace.Race.Location, p.TrackRace.Race.Date),
            new TrackDto(p.TrackRace.Track.Name, p.TrackRace.Track.LengthInKm),
            p.Laps,
            p.FinishTimeInSeconds,
            p.Position
        ));

        return new RacerParticipationsDto(racer.RacerId, racer.FirstName, racer.LastName, parts);
    }

    public async Task AddTrackRaceParticipations(TrackRaceParticipationCreateDto dto)
    {
        await using var transaction = await ctx.Database.BeginTransactionAsync();
        try
        {
            var race = await ctx.Races
                .FirstOrDefaultAsync(r => r.Name == dto.RaceName);
            if (race == null)
                throw new NotFoundException($"Race '{dto.RaceName}' not found");

            var track = await ctx.Tracks
                .FirstOrDefaultAsync(t => t.Name == dto.TrackName);
            if (track == null)
                throw new NotFoundException($"Track '{dto.TrackName}' not found");

            var trackRace = await ctx.TrackRaces
                .FirstOrDefaultAsync(tr =>
                    tr.RaceId  == race.RaceId &&
                    tr.TrackId == track.TrackId);
            if (trackRace == null)
                throw new NotFoundException(
                    $"TrackRace for '{dto.RaceName}' on '{dto.TrackName}' not found");

            foreach (var p in dto.Participations)
            {
                if (!await ctx.Racers.AnyAsync(r => r.RacerId == p.RacerId))
                    throw new NotFoundException($"Racer {p.RacerId} not found");

                var participation = new Participation
                {
                    RacerId             = p.RacerId,
                    RaceId              = race.RaceId,
                    TrackId             = track.TrackId,
                    Laps                = 0,
                    FinishTimeInSeconds = p.FinishTimeInSeconds,
                    Position            = p.Position
                };

                ctx.Participations.Add(participation);

                if (p.FinishTimeInSeconds < trackRace.BestTimeInSeconds)
                    trackRace.BestTimeInSeconds = p.FinishTimeInSeconds;
            }

            await ctx.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    public async Task<int> AddRacerAsync(RacerCreateDto dto)
    {
        if (await ctx.Racers.AnyAsync(r =>
                r.FirstName == dto.FirstName &&
                r.LastName  == dto.LastName))
        {
            throw new ConflictException(
                $"Racer '{dto.FirstName} {dto.LastName}' already exists");
        }

        var racer = new Racer
        {
            FirstName = dto.FirstName,
            LastName  = dto.LastName
        };

        ctx.Racers.Add(racer);
        await ctx.SaveChangesAsync();

        return racer.RacerId;
    }
}