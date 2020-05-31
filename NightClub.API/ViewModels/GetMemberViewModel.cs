using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NightClub.API.ViewModels
{
    public class GetMemberViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<MemberCard> MemberCards { get; set; }
        public ICollection<IdentityCard> IdentityCards { get; set; }
        public ICollection<Blacklist> Blacklists { get; set; }

        public class MemberCard
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public bool IsActive { get; set; }
        }

        public class IdentityCard
        {
            public int Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public DateTime BirthDate { get; set; }
            public string NationalRegisterNumber { get; set; }
            public DateTime ValidityDate { get; set; }
            public DateTime ExpirationDate { get; set; }
            public int CardNumber { get; set; }
        }

        public class Blacklist
        {
            public int Id { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
    }
}
