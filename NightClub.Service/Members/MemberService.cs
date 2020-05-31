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

        public Task<Member> GetMemberById(int id)
        {
            return _context.Members
                .Include(m => m.IdentityCards)
                .Include(m => m.Blacklists)
                .Include(m => m.MemberCards)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public Task<Member> GetMemberByPhoneNumber(GetMemberByPhoneNumberCommand command)
        {
            if (command == null)
            {
                throw new CustomBadRequestException(ExceptionMessage.NullCommand);
            }

            return _context.Members
                .Include(m => m.IdentityCards)
                .Include(m => m.Blacklists)
                .Include(m => m.MemberCards)
                .SingleOrDefaultAsync(m => m.PhoneNumber == command.PhoneNumber);
        }

        public Task<Member> GetMemberByEmail(GetMemberByEmailCommand command)
        {
            if (command == null)
            {
                throw new CustomBadRequestException(ExceptionMessage.NullCommand);
            }

            if (!IsEmailValid(command.Email))
            {
                throw new CustomBadRequestException(ExceptionMessage.BadEmailFormat);
            }

            return _context.Members
                .Include(m => m.IdentityCards)
                .Include(m => m.Blacklists)
                .Include(m => m.MemberCards)
                .SingleOrDefaultAsync(m => m.Email == command.Email);
        }

        public async Task<Member> CreateMember(CreateMemberCommand command)
        {
            if (command == null)
            {
                throw new CustomBadRequestException(ExceptionMessage.NullCommand);
            }

            if (String.IsNullOrEmpty(command.PhoneNumber) && String.IsNullOrEmpty(command.Email))
            {
                throw new CustomBadRequestException(ExceptionMessage.MemberEmptyEmailAndPhone);
            }

            if (command.IdCard == null)
            {
                throw new CustomBadRequestException(ExceptionMessage.IdCardNull);
            }

            if (!String.IsNullOrEmpty(command.Email))
            {
                if (!IsEmailValid(command.Email))
                {
                    throw new CustomBadRequestException(ExceptionMessage.BadEmailFormat);
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

        public async Task<Member> BlacklistMember(BlacklistMemberCommand command)
        {
            if (command == null)
            {
                throw new CustomBadRequestException(ExceptionMessage.NullCommand);
            }

            if (DateTime.Compare(command.StartDate, command.EndDate) > 0)
            {
                throw new CustomBadRequestException(ExceptionMessage.NullCommand);
            }

            if (DateTime.Compare(command.EndDate, DateTime.Today) < 0)
            {
                throw new CustomBadRequestException(ExceptionMessage.BlacklistInvalidEndDate);
            }

            var member = _context.Members
                .Include(m => m.Blacklists)
                .SingleOrDefault(m => m.Id == command.MemberId);

            if (member == null)
            {
                throw new CustomNotFoundException(ExceptionMessage.MemberIdNotFound);
            }

            member.Blacklists.Add(new Blacklist
            {
                EndDate = command.EndDate,
                StartDate = command.StartDate
            });
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<Member> GenerateNewMemberCard(int memberId)
        {
            var member = _context.Members
                .Include(m => m.MemberCards)
                .SingleOrDefault(m => m.Id == memberId);

            if (member == null)
            {
                throw new CustomNotFoundException(ExceptionMessage.MemberIdNotFound);
            }

            foreach(var memberCard in member.MemberCards)
            {
                if (memberCard.IsActive)
                {
                    memberCard.IsActive = false;
                }
            }

            member.MemberCards.Add(MemberCard.GenerateMemberCard());
            await _context.SaveChangesAsync();
            return member;
        }


        public async Task<Member> UpdateIdCard(UpdateIdCardCommand command)
        {
            if (command == null)
            {
                throw new CustomBadRequestException(ExceptionMessage.NullCommand);
            }

            var member = _context.Members
                .Include(m => m.IdentityCards)
                .SingleOrDefault(m => m.Id == command.MemberId);

            if (member == null)
            {
                throw new CustomNotFoundException(ExceptionMessage.MemberIdNotFound);
            }

            var idCard = new IdentityCard
            {
                Firstname = command.Firstname,
                Lastname = command.Lastname,
                BirthDate = command.BirthDate,
                CardNumber = command.CardNumber,
                ExpirationDate = command.ExpirationDate,
                ValidityDate = command.ValidityDate,
                NationalRegisterNumber = command.NationalRegisterNumber
            };
            ValidateIdCard(idCard);

            member.IdentityCards.Add(idCard);
            await _context.SaveChangesAsync();
            return member;
        }

        private void ValidateIdCard(IdentityCard idCard)
        {
            if (!idCard.IsDateValid())
            {
                throw new CustomBadRequestException(ExceptionMessage.IdCardInvalidDate);
            }

            if (DateTime.Compare(idCard.ExpirationDate, DateTime.Today) < 0)
            {
                throw new CustomBadRequestException(ExceptionMessage.IdCardExpired);
            }

            if (!IdentityCard.IsValidNiss(idCard.NationalRegisterNumber))
            {
                throw new CustomBadRequestException(ExceptionMessage.IdCardInvalidNiss);
            }

            if (idCard.CalculateAge(DateTime.Today) < MinimumAge)
            {
                throw new CustomBadRequestException(ExceptionMessage.MemberInvalidAge);
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
