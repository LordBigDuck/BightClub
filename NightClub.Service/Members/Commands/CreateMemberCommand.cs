using System;
using System.Collections.Generic;
using System.Text;

namespace NightClub.Service.Members.Commands
{
    public class CreateMemberCommand
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IdentityCard IdCard { get; set; }

        public class IdentityCard
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public DateTime BirthDate { get; set; }
            public string NationalRegisterNumber { get; set; }
            public DateTime ValidityDate { get; set; }
            public DateTime ExpirationDate { get; set; }
            public int CardNumber { get; set; }
        }
    }
}
