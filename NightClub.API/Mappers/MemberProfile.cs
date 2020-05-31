using AutoMapper;
using NightClub.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NightClub.API.Mappers
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Core.Domains.Member, GetMemberViewModel>();
            CreateMap<Core.Domains.IdentityCard, GetMemberViewModel.IdentityCard>();
            CreateMap<Core.Domains.MemberCard, GetMemberViewModel.MemberCard>();
            CreateMap<Core.Domains.Blacklist, GetMemberViewModel.Blacklist>();
        }
    }
}
