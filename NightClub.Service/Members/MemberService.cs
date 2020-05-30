using Microsoft.EntityFrameworkCore;
using NightClub.Core.Domains;
using NightClub.Core.Exceptions;
using NightClub.Infrastructure.Contexts;
using NightClub.Service.Members.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NightClub.Service.Members
{
    public class MemberService : IMemberService
    {
        private const int MinimumAge = 18;

        private readonly NightClubContext _context;

        public MemberService(NightClubContext context)
        {
            _context = context;
        }

        public Task<List<Member>> GetAllMembers()
        {
            return _context.Members
                .Include(m => m.IdentityCards)
                .Include(m => m.Blacklists)
                .Include(m => m.MemberCards)
                .Where(m => m.IsActive)
                .ToListAsync();
        }

        public async Task<Member> CreateMember(CreateMemberCommand command)
        {
            // TODO add exception message from Core.Exceptions ExceptionMessage
            if (String.IsNullOrEmpty(command.PhoneNumber) && String.IsNullOrEmpty(command.Email))
            {
                throw new CustomBadRequestException();
            }

            if (command.IdCard == null)
            {
                throw new CustomBadRequestException();
            }

            if (String.IsNullOrEmpty(command.Email))
            {
                if (!IsEmailValid(command.Email))
                {
                    throw new CustomBadRequestException();
                }
            }

            var idCard = new IdentityCard
            {
                Firstname = command.IdCard.Firstname,
                Lastname = command.IdCard.Lastname,
                BirthDate = command.IdCard.BirthDate,
                CardNumber = command.IdCard.CardNumber,
                ExpirationDate = command.IdCard.ExpirationDate,
                ValidityDate = command.IdCard.ValidityDate,
                NationalRegisterNumber = command.IdCard.NationalRegisterNumber
            };
            ValidateIdCard(idCard);

            var memberCard = MemberCard.GenerateMemberCard();

            var member = new Member
            {
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                IdentityCards = new List<IdentityCard>() { idCard },
                MemberCards = new List<MemberCard>() { memberCard },
                IsActive = true
            };

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return member;
        }

        private void ValidateIdCard(IdentityCard idCard)
        {
            if (!idCard.IsDateValid())
            {
                throw new CustomBadRequestException();
            }

            if (DateTime.Compare(idCard.ExpirationDate, DateTime.Today) < 0)
            {
                throw new CustomBadRequestException();
            }

            if (!IdentityCard.IsValidNiss(idCard.NationalRegisterNumber))
            {
                throw new CustomBadRequestException();
            }

            if (idCard.CalculateAge() < MinimumAge)
            {
                throw new CustomBadRequestException();
            }
        }

        private bool IsEmailValid(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
