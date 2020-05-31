using NightClub.Core.Domains;
using NightClub.Service.Members.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NightClub.Service.Members
{
    public interface IMemberService
    {
        Task<Member> BlacklistMember(BlacklistMemberCommand command);
        Task<Member> CreateMember(CreateMemberCommand command);
        Task<List<Member>> GetAllMembers();
        Task<Member> GetMemberByEmail(GetMemberByEmailCommand command);
        Task<Member> GetMemberByPhoneNumber(GetMemberByPhoneNumberCommand command);
        Task<Member> GetMemberById(int id);
        Task<Member> GenerateNewMemberCard(int memberId);
        Task<Member> UpdateIdCard(UpdateIdCardCommand command);
    }
}
