using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Infrastructure.Entities;

namespace Tradgardsgolf.Infrastructure
{
    public class TradgardsgolfContext : DbContext
    {
        public DbSet<Course> Course { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Round> Round { get; set; }
        public DbSet<RoundScore> RoundScore { get; set; }


        public TradgardsgolfContext(DbContextOptions<TradgardsgolfContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoundScore>().HasNoKey();
        }
    }
}
