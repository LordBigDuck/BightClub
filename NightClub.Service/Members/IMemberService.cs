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
        Task<Member> CreateMember(CreateMemberCommand command);
        Task<List<Member>> GetAllMembers();
    }
}
