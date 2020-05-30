using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NightClub.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace NightClub.Infrastructure.Contexts
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().HasData(new
            {
                Id = 1,
                Email = "john.doe@gmail.com",
                IsActive = true
            });

            modelBuilder.Entity<IdentityCard>().HasData(new
            {
                Id = 1,
                CardNumber = 1000000001,
                BirthDate = new DateTime(1994, 4, 26),
                Firstname = "John",
                Lastname = "Doe",
                NationalRegisterNumber = "548.65.84-654-56",
                ValidityDate = new DateTime(2017, 11, 18),
                ExpirationDate = new DateTime(2022, 11, 18),
                MemberId = 1
            });

            modelBuilder.Entity<MemberCard>().HasData(new
            {
                Id = 1,
                IsActive = false,
                Code = Guid.NewGuid().ToString(),
                MemberId = 1
            },
            new
            {
                Id = 2,
                IsActive = true,
                Code = Guid.NewGuid().ToString(),
                MemberId = 1
            });

            modelBuilder.Entity<Blacklist>().HasData(new
            {
                Id = 1,
                StartDate = new DateTime(2019, 6, 1),
                EndDate = new DateTime(2019, 8, 1),
                MemberId = 1
            });
        }
    }
}
