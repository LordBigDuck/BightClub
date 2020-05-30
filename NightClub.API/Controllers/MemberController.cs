using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NightClub.Service.Members;
using NightClub.Service.Members.Commands;

namespace NightClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        // TODO define view model
        private readonly IMemberService _memberService;

        public MemberController(
            IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Object>>> GetAllMembers()
        {
            var members = await _memberService.GetAllMembers();

            return Ok(members);
        }
        
        [HttpPost]
        public async Task<ActionResult<object>> CreateMember([FromBody] CreateMemberCommand command)
        {
            var member = await _memberService.CreateMember(command);

            return Ok(member);
        }
    }
}
