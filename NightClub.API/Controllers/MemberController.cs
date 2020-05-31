using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NightClub.API.ViewModels;
using NightClub.Core.Domains;
using NightClub.Core.Exceptions;
using NightClub.Service.Members;
using NightClub.Service.Members.Commands;

namespace NightClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public MemberController(
            IMemberService memberService,
            IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetMemberViewModel>>> GetAllMembers()
        {
            var members = await _memberService.GetAllMembers();

            var memberViewModels = _mapper.Map<List<GetMemberViewModel>>(members);

            return Ok(memberViewModels);
        }

        [HttpGet("{memberId}")]
        public async Task<ActionResult<GetMemberViewModel>> GetById(int memberId)
        {
            var member = await _memberService.GetMemberById(memberId);

            var memberViewModel = _mapper.Map<GetMemberViewModel>(member);

            return Ok(memberViewModel);
        }

        [HttpGet("email")]
        public async Task<ActionResult<GetMemberViewModel>> GetByEmail(GetMemberByEmailCommand command)
        {
            var member = await _memberService.GetMemberByEmail(command);

            var memberViewModel = _mapper.Map<GetMemberViewModel>(member);

            return Ok(memberViewModel);
        }

        [HttpGet("phonenumber")]
        public async Task<ActionResult<GetMemberViewModel>> GetByPhoneNumber(GetMemberByPhoneNumberCommand command)
        {
            var member = await _memberService.GetMemberByPhoneNumber(command);

            var memberViewModel = _mapper.Map<GetMemberViewModel>(member);

            return Ok(memberViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<GetMemberViewModel>> CreateMember([FromBody] CreateMemberCommand command)
        {
            var member = await _memberService.CreateMember(command);

            var memberViewModel = _mapper.Map<GetMemberViewModel>(member);

            return CreatedAtAction(nameof(GetById), new { memberId = memberViewModel.Id }, memberViewModel);
        }

        [HttpPost("{memberId}/blacklist")]
        public async Task<ActionResult<GetMemberViewModel>> BlacklistMember(int memberId, [FromBody] BlacklistMemberCommand command)
        {
            command.MemberId = memberId;
            var member = await _memberService.BlacklistMember(command);

            var memberViewModel = _mapper.Map<GetMemberViewModel>(member);

            return CreatedAtAction(nameof(GetById), new { memberId = memberViewModel.Id }, memberViewModel);
        }

        [HttpPost("{memberId}/generateMemberCard")]
        public async Task<ActionResult<GetMemberViewModel>> GenerateNewMemberCard(int memberId)
        {
            var member = await _memberService.GenerateNewMemberCard(memberId);

            var memberViewModel = _mapper.Map<GetMemberViewModel>(member);

            return CreatedAtAction(nameof(GetById), new { memberId = memberViewModel.Id }, memberViewModel);
        }

        [HttpPost("{memberId}/IdCard")]
        public async Task<ActionResult<GetMemberViewModel>> RenewIdCard(int memberId, UpdateIdCardCommand command)
        {
            command.MemberId = memberId;

            var member = await _memberService.UpdateIdCard(command);

            var memberViewModel = _mapper.Map<GetMemberViewModel>(member);

            return CreatedAtAction(nameof(GetById), new { memberId = memberViewModel.Id }, memberViewModel);
        }

        [HttpPatch("{memberId}")]
        public async Task<ActionResult<GetMemberViewModel>> UpdateMemberInformation(int memberId, [FromBody] JsonPatchDocument<Member> patchDoc)
        {
            var member = await _memberService.GetMemberById(memberId);

            if (member == null)
            {
                throw new CustomNotFoundException(ExceptionMessage.MemberIdNotFound);
            }

            patchDoc.ApplyTo(member, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            member = await _memberService.UpdateMember(member);
            return Ok(member);
        }
    }
}
