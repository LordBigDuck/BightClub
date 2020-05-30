using System;

namespace NightClub.Core.Domains
{
    public class MemberCard
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Member Member { get; set; }
        public bool IsActive { get; set; }

        public static MemberCard GenerateMemberCard()
        {
            return new MemberCard
            {
                Code = Guid.NewGuid().ToString(),
                IsActive = true
            };
        }
    }
}
