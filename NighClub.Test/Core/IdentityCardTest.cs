using Newtonsoft.Json.Bson;
using NightClub.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NighClub.Test.Core
{
    public class IdentityCardTest
    {
        [Fact]
        public void IsValidNiss_BadFormat_ReturnFalse()
        {
            var niss = "565.455.56-456-44";

            var isNissValid = IdentityCard.IsValidNiss(niss);

            Assert.False(isNissValid);
        }

        [Fact]
        public void IsValidNiss_FirstNumberBadFormat_ReturnFalse()
        {
            var niss = "5565.45.56-456-44";

            var isNissValid = IdentityCard.IsValidNiss(niss);

            Assert.False(isNissValid);
        }

        [Fact]
        public void IsValidNiss_GoodFormat_ReturnTrue()
        {
            var niss = "456.45.56-456-44";

            var isNissValid = IdentityCard.IsValidNiss(niss);

            Assert.True(isNissValid);
        }

        [Fact]
        public void IsDateValid_ValidityEarlierThanExpiration_ReturnTrue()
        {
            var idCard = new IdentityCard
            {
                Id = 1,
                NationalRegisterNumber = "654.23.45-456-78",
                BirthDate = new DateTime(1994, 02, 24),
                CardNumber = 1234567891,
                Firstname = "John",
                Lastname = "Doe",
                ValidityDate = new DateTime(2015, 11, 18),
                ExpirationDate = new DateTime(2016, 11, 18)
            };

            var isDateValid = idCard.IsDateValid();

            Assert.True(isDateValid);
        }

        [Fact]
        public void IsDateValid_ValidityEqualsToExpiration_ReturnFalse()
        {
            var idCard = new IdentityCard
            {
                Id = 1,
                NationalRegisterNumber = "654.23.45-456-78",
                BirthDate = new DateTime(1994, 02, 24),
                CardNumber = 1234567891,
                Firstname = "John",
                Lastname = "Doe",
                ValidityDate = new DateTime(2016, 11, 18),
                ExpirationDate = new DateTime(2015, 11, 18)
            };

            var isDateValid = idCard.IsDateValid();

            Assert.False(isDateValid);
        }

        [Fact]
        public void IsDateValid_LaterThanExpiration_ReturnFalse()
        {
            var idCard = new IdentityCard
            {
                Id = 1,
                NationalRegisterNumber = "654.23.45-456-78",
                BirthDate = new DateTime(1994, 02, 24),
                CardNumber = 1234567891,
                Firstname = "John",
                Lastname = "Doe",
                ValidityDate = new DateTime(2016, 11, 18),
                ExpirationDate = new DateTime(2016, 11, 18)
            };

            var isDateValid = idCard.IsDateValid();

            Assert.False(isDateValid);
        }

        [Fact]
        public void CalculateAge_Return26()
        {
            var idCard = new IdentityCard
            {
                Id = 1,
                NationalRegisterNumber = "654.23.45-456-78",
                BirthDate = new DateTime(1994, 02, 24),
                CardNumber = 1234567891,
                Firstname = "John",
                Lastname = "Doe",
                ValidityDate = new DateTime(2016, 11, 18),
                ExpirationDate = new DateTime(2016, 11, 18)
            };

            var age = idCard.CalculateAge(new DateTime(2020, 3, 29));

            Assert.Equal(26, age);
        }

        [Fact]
        public void CalculateAge_LeapYear()
        {
            var idCard = new IdentityCard
            {
                Id = 1,
                NationalRegisterNumber = "654.23.45-456-78",
                BirthDate = new DateTime(2016, 2, 29),
                CardNumber = 1234567891,
                Firstname = "John",
                Lastname = "Doe",
                ValidityDate = new DateTime(2016, 11, 18),
                ExpirationDate = new DateTime(2016, 11, 18)
            };

            var age = idCard.CalculateAge(new DateTime(2020, 2, 29));

            Assert.Equal(4, age);
        }
    }
}
