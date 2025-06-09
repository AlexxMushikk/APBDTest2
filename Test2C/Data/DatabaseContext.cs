using Microsoft.EntityFrameworkCore;
using Test2C.Models;

namespace Test2C.Data
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<Racer> Racers { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<TrackRace> TrackRaces { get; set; }
        public DbSet<Participation> Participations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Racer>().HasData(
                new Racer { RacerId = 1, FirstName = "Lewis", LastName = "Hamilton" },
                new Racer { RacerId = 2, FirstName = "Raamon",  LastName = "Make" }
            );

            modelBuilder.Entity<Race>().HasData(
                new Race { RaceId = 1, Name = "British Grand Prix", Location = "Silverstone, UK", Date = new DateTime(2025,7,14) },
                new Race { RaceId = 2, Name = "Monaco Grand Prix",  Location = "Monte Carlo, Monaco", Date = new DateTime(2025,5,25) }
            );

            modelBuilder.Entity<Track>().HasData(
                new Track { TrackId = 1, Name = "Silverstone Circuit", LengthInKm = 5.89 },
                new Track { TrackId = 2, Name = "Monaco Circuit",     LengthInKm = 3.34 }
            );

            modelBuilder.Entity<TrackRace>().HasData(
                new TrackRace { RaceId = 1, TrackId = 1, BestTimeInSeconds = 5460 },
                new TrackRace { RaceId = 2, TrackId = 2, BestTimeInSeconds = 6300 }
            );

            modelBuilder.Entity<Participation>().HasData(
                new Participation {
                    ParticipationId      = 1,
                    RacerId              = 1,
                    RaceId               = 1,
                    TrackId              = 1,
                    Laps                 = 52,
                    FinishTimeInSeconds  = 5460,
                    Position             = 1
                },
                new Participation {
                    ParticipationId      = 2,
                    RacerId              = 1,
                    RaceId               = 2,
                    TrackId              = 2,
                    Laps                 = 78,
                    FinishTimeInSeconds  = 6300,
                    Position             = 2
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}