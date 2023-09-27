using API.Models.SurveyOptions;
using API.Models.Surveys;
using API.Models.DiscordUsers;
using Microsoft.EntityFrameworkCore;
using Models.Fights;
using Models.Players;
using Newtonsoft.Json;

namespace API.Port.Database
{
    public class DiscordContext : DbContext
    {
        public DiscordContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DiscordUser> DiscordUsers { get; set; }
        public DbSet<Fight> Fights { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyOption> SurveyOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fight>()
                        .Property(fight => fight.Summary)
                        .HasColumnName("Summary")
                        .HasConversion(
                            summary => JsonConvert.SerializeObject(summary),
                            summary => JsonConvert.DeserializeObject<List<string>>(summary));

            modelBuilder.Entity<Fight>()
                        .Navigation(fight => fight.Player)
                        .AutoInclude();

            modelBuilder.Entity<Survey>()
            .Navigation(survey => survey.SurveyOptions)
            .AutoInclude();
        }
    }
}