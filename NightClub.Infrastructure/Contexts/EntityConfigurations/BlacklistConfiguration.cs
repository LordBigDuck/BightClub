using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NightClub.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace NightClub.Infrastructure.Contexts.EntityConfigurations
{
    public class BlacklistConfiguration : IEntityTypeConfiguration<Blacklist>
    {
        public void Configure(EntityTypeBuilder<Blacklist> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
