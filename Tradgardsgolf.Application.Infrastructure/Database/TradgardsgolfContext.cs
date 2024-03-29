﻿using Microsoft.EntityFrameworkCore;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Infrastructure.Database
{
    public class TradgardsgolfContext(DbContextOptions<TradgardsgolfContext> options) : DbContext(options)
    {
        public DbSet<Course> Course { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Round> Round { get; set; }
        public DbSet<RoundScore> RoundScore { get; set; }
        public DbSet<Tournament> Tournament { get; set; }
        public DbSet<TournamentRound> TournamentRound { get; set; }
        public DbSet<TournamentCourseDate> TournamentCourseDate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TournamentRound>()
                .HasOne(tournamentRound => tournamentRound.Round)
                .WithOne()
                .HasForeignKey<TournamentRound>(tournamentRound => tournamentRound.RoundId)
                .OnDelete(DeleteBehavior.Cascade);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
