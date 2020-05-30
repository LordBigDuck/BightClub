using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NightClub.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace NightClub.Infrastructure.Contexts.EntityConfigurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.IdentityCards)
                .WithOne();

            builder.HasMany(e => e.MemberCards)
                .WithOne(mc => mc.Member);

            builder.HasMany(e => e.Blacklists)
                .WithOne(b => b.Member);
        }
    }
}
