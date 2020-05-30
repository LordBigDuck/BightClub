using System.Collections.Generic;

namespace NightClub.Core.Domains
{
    public class Member
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public ICollection<IdentityCard> IdentityCards { get; set; } = new List<IdentityCard>();
        public ICollection<MemberCard> MemberCards { get; set; } = new List<MemberCard>();
        public ICollection<Blacklist> Blacklists { get; set; } = new List<Blacklist>();
    }
}
