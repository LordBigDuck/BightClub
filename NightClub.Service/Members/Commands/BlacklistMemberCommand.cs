using System;
using System.Collections.Generic;
using System.Text;

namespace NightClub.Service.Members.Commands
{
    public class BlacklistMemberCommand
    {
        public int MemberId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
