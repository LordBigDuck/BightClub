using System;
using System.Text.RegularExpressions;

namespace NightClub.Core.Domains
{
    public class IdentityCard
    {
        private const string NissPattern = @"^\d{3}.\d{2}.\d{2}-\d{3}-\d{2}$";

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public string NationalRegisterNumber { get; set; }
        public DateTime ValidityDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CardNumber { get; set; }


        public static bool IsValidNiss(string niss)
        {
            var regexMatch = Regex.Match(niss, NissPattern);
            return regexMatch.Success;
        }

        public bool IsDateValid()
        {
            var comparison = DateTime.Compare(ValidityDate, ExpirationDate);
            return comparison < 0;
        }

        public int CalculateAge()
        {
            var today = DateTime.Today;
            var birthday = BirthDate.Date;
            var age = today.Year - birthday.Year;
            // Leap year
            if (birthday.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}
