using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NightClub.Core.Domains;
using NightClub.Core.Exceptions;
using NightClub.Infrastructure.Contexts;
using NightClub.Service.Members;
using NightClub.Service.Members.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NighClub.Test.Services
{
    public class MemberServiceTest : TestingContext<MemberService>
    {
        public MemberServiceTest()
        {
            base.Setup();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        private NightClubContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<NightClubContext>()
                .UseInMemoryDatabase(databaseName: "NightClub")
                .Options;

            var context = new NightClubContext(options);
            context.Database.EnsureDeleted();
            SeedDatabase(context);
            return context;
        }

        private void SeedDatabase(NightClubContext context)
        {
            context.Members.Add(new Member
            {
                Email = "john.doe@gmail.com",
                IsActive = true,
                IdentityCards = new List<IdentityCard> {
                    new IdentityCard
                    {
                        CardNumber = 1000000001,
                        BirthDate = new DateTime(1994, 4, 26),
                        Firstname = "John",
                        Lastname = "Doe",
                        NationalRegisterNumber = "548.65.84-654-56",
                        ValidityDate = new DateTime(2017, 11, 18),
                        ExpirationDate = new DateTime(2022, 11, 18)
                    }
                },
                MemberCards = new List<MemberCard>
                {
                    new MemberCard
                    {
                        IsActive = false,
                        Code = "0f8fad5b-d9cb-469f-a165-70867728950e",
                    }
                },
                Blacklists = new List<Blacklist>
                {
                    new Blacklist
                    {
                        StartDate = new DateTime(2019, 6, 1),
                        EndDate = new DateTime(2019, 8, 1),
                    }
                }
            });
            context.SaveChanges();
        }

        [Fact]
        public async Task CreateMember_EmptyEmailAndPhone_ThrowException()
        {
            InjectClassFor(InitializeContext());
            var command = new CreateMemberCommand();

            var exception = await Assert.ThrowsAsync<CustomBadRequestException>(() => ClassUnderTest.CreateMember(command));
            Assert.Equal(ExceptionMessage.MemberEmptyEmailAndPhone, exception.Message);
        }

        [Fact]
        public async Task CreateMember_EmptyIdCard_ThrowException()
        {
            InjectClassFor(InitializeContext());
            var command = new CreateMemberCommand
            {
                Email = "test@email.com"
            };

            var exception = await Assert.ThrowsAsync<CustomBadRequestException>(() => ClassUnderTest.CreateMember(command));
            Assert.Equal(ExceptionMessage.IdCardNull, exception.Message);
        }

        [Fact]
        public async Task CreateMember_InvalidEmailFormat_ThrowException()
        {
            InjectClassFor(InitializeContext());
            var command = new CreateMemberCommand
            {
                Email = "testemail.com",
                IdCard = new CreateMemberCommand.IdentityCard
                {
                    BirthDate = new DateTime(2000, 5, 30),
                    CardNumber = 100000012,
                    Firstname = "Test",
                    Lastname = "Jean",
                    NationalRegisterNumber = "456.51.51-456-78",
                    ExpirationDate = new DateTime(2021, 6, 11),
                    ValidityDate = new DateTime(2016, 6, 11)
                }
            };

            var exception = await Assert.ThrowsAsync<CustomBadRequestException>(() => ClassUnderTest.CreateMember(command));
            Assert.Equal(ExceptionMessage.BadEmailFormat, exception.Message);
        }

        [Fact]
        public async Task CreateMember_NotRequiredAge_ThrowException()
        {
            InjectClassFor(InitializeContext());
            var command = new CreateMemberCommand
            {
                PhoneNumber = "+32495414243",
                IdCard = new CreateMemberCommand.IdentityCard
                {
                    BirthDate = new DateTime(2004, 5, 30),
                    CardNumber = 100000012,
                    Firstname = "Test",
                    Lastname = "Jean",
                    NationalRegisterNumber = "456.51.51-456-78",
                    ExpirationDate = new DateTime(2021, 6, 11),
                    ValidityDate = new DateTime(2016, 6, 11)
                }
            };

            var exception = await Assert.ThrowsAsync<CustomBadRequestException>(() => ClassUnderTest.CreateMember(command));
            Assert.Equal(ExceptionMessage.MemberInvalidAge, exception.Message);
        }

        [Fact]
        public async Task CreateMember_IdExpirationOlderThanValidity_ThrowException()
        {
            InjectClassFor(InitializeContext());
            var command = new CreateMemberCommand
            {
                PhoneNumber = "+32495414243",
                IdCard = new CreateMemberCommand.IdentityCard
                {
                    BirthDate = new DateTime(2000, 5, 30),
                    CardNumber = 100000012,
                    Firstname = "Test",
                    Lastname = "Jean",
                    NationalRegisterNumber = "456.51.51-456-78",
                    ExpirationDate = new DateTime(2016, 6, 11),
                    ValidityDate = new DateTime(2021, 6, 11)
                }
            };

            var exception = await Assert.ThrowsAsync<CustomBadRequestException>(() => ClassUnderTest.CreateMember(command));
            Assert.Equal(ExceptionMessage.IdCardInvalidDate, exception.Message);
        }

        [Fact]
        public async Task CreateMember_IdExpirationOlderThanToday_ThrowException()
        {
            InjectClassFor(InitializeContext());
            var command = new CreateMemberCommand
            {
                PhoneNumber = "+32495414243",
                IdCard = new CreateMemberCommand.IdentityCard
                {
                    BirthDate = new DateTime(2000, 5, 30),
                    CardNumber = 100000012,
                    Firstname = "Test",
                    Lastname = "Jean",
                    NationalRegisterNumber = "456.51.51-456-78",
                    ExpirationDate = new DateTime(2016, 6, 11),
                    ValidityDate = new DateTime(2014, 6, 11)
                }
            };

            var exception = await Assert.ThrowsAsync<CustomBadRequestException>(() => ClassUnderTest.CreateMember(command));
            Assert.Equal(ExceptionMessage.IdCardExpired, exception.Message);
        }


        // EF Core in memory does not check for DB constraint like Unique
        //[Fact]
        //public async Task CreateMember_IdCardNotUnique_ThrowException()
        //{
        //    InjectClassFor(InitializeContext());
        //    var command = new CreateMemberCommand
        //    {
        //        PhoneNumber = "+32495414243",
        //        IdCard = new CreateMemberCommand.IdentityCard
        //        {
        //            BirthDate = new DateTime(2000, 5, 30),
        //            CardNumber = 1000000001,
        //            Firstname = "Test",
        //            Lastname = "Jean",
        //            NationalRegisterNumber = "456.51.51-456-78",
        //            ExpirationDate = new DateTime(2026, 6, 11),
        //            ValidityDate = new DateTime(2016, 6, 11)
        //        }
        //    };

        //    await Assert.ThrowsAsync<Exception>(() => ClassUnderTest.CreateMember(command));
        //}

        [Fact]
        public async Task CreateMember_ValidCommand_ReturnCreatedMember()
        {
            InjectClassFor(InitializeContext());
            var command = new CreateMemberCommand
            {
                PhoneNumber = "+32495414243",
                IdCard = new CreateMemberCommand.IdentityCard
                {
                    BirthDate = new DateTime(2000, 5, 30),
                    CardNumber = 100000024,
                    Firstname = "Test",
                    Lastname = "Jean",
                    NationalRegisterNumber = "456.51.51-456-78",
                    ExpirationDate = new DateTime(2026, 6, 11),
                    ValidityDate = new DateTime(2016, 6, 11)
                }
            };

            var member = await ClassUnderTest.CreateMember(command);
            Assert.Equal("+32495414243", member.PhoneNumber);
            Assert.Equal("456.51.51-456-78", member.IdentityCards.FirstOrDefault().NationalRegisterNumber);
            Assert.NotNull(member.MemberCards.FirstOrDefault());
        }
    }
}
