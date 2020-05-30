using Microsoft.EntityFrameworkCore;
using NightClub.Core.Domains;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NightClub.Infrastructure.Contexts
{
    public class NightClubContext : DbContext
    {
        public DbSet<IdentityCard> IdentityCards { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberCard> MemberCards { get; set; }
        public DbSet<Blacklist> Blacklists { get; set; }

        public NightClubContext(DbContextOptions<NightClubContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
