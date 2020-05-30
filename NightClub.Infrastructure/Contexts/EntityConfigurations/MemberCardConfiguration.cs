using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NightClub.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace NightClub.Infrastructure.Contexts.EntityConfigurations
{
    public class MemberCardConfiguration : IEntityTypeConfiguration<MemberCard>
    {
        public void Configure(EntityTypeBuilder<MemberCard> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Member)
                .WithMany(m => m.MemberCards);
        }
    }
}
