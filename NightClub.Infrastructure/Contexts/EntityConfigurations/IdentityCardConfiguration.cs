using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NightClub.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace NightClub.Infrastructure.Contexts.EntityConfigurations
{
    public class IdentityCardConfiguration : IEntityTypeConfiguration<IdentityCard>
    {
        public void Configure(EntityTypeBuilder<IdentityCard> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CardNumber).IsRequired();

            builder
                .HasIndex(e => e.CardNumber)
                .IsUnique();
        }
    }
}
