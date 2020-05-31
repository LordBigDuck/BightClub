using System;
using System.Collections.Generic;
using System.Text;

namespace NightClub.Core.Exceptions
{
    public static class ExceptionMessage
    {
        public static string BadEmailFormat = "Email format is not valid";
        public static string BlacklistInvalidEndDate = "EndDate of blacklist can not be earlier than today";
        public static string BlacklistInvalidDate = "EndDate of blacklist can not be earlier than StartDate";
        public static string IdCardExpired = "ID card is already expired";
        public static string IdCardInvalidDate = "Expiration date must be later than Validity date";
        public static string IdCardInvalidNiss = "NISS format is not valid";
        public static string IdCardNull = "ID card can not be null";
        public static string MemberEmptyEmailAndPhone = "Member must have an email and/or a phone number";
        public static string MemberInvalidAge = "Member must have 18 to register";
        public static string MemberIdNotFound = "Member with specified ID not found";
        public static string NullCommand = "Request can not be null";
    }
}
