using System;
using System.Collections.Generic;
using System.Text;

namespace NightClub.Core.Domains
{
    public class Blacklist
    {
        public int Id { get; set; }
        public Member Member { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //public Blacklist(Member member, DateTime startDate, DateTime endDate)
        //{
        //    // TODO use custom exception
        //    if (member == null)
        //    {
        //        throw new Exception();
        //    }

        //    var dateComparison = DateTime.Compare(startDate, endDate);
        //    if(dateComparison >= 0)
        //    {
        //        throw new Exception();
        //    }

        //    Member = member;
        //    StartDate = startDate;
        //    EndDate = endDate;
        //}
    }
}
