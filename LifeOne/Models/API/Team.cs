using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class Team
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public long MemberId { get; set; }
        public string joiningDate { get; set; }

        public long Totalteam { get; set; }
        public int totalCount { get; set; }

        public long Totalrefferal { get; set; }
    }
    public class TeamResponse : CommonJsonReponse
    {

      public List<Team> result { get; set; }
    }
    public class DownlineTeam :  DownLineJsonReponse
    {

        public List<DownlineTeamData> result { get; set; }
    }

    public class DownlineTeamData
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public long MemberId { get; set; }
        public string joiningDate { get; set; }
        public long TotalDirect { get; set; }
        public decimal TotalBusiness { get; set; }

        public long totalCount { get; set; }
    }

    public class GetBusinessDetailsResponse: CommonJsonReponse
    {
       public GetBusinessDetails result { get; set; }
    }
    public class GetBusinessDetails 
    {
        public long TotalTeam { get; set; }
        public long TotalRefferal { get; set; }
        public decimal Business { get; set; }

    }

    public class TodayReferal
    {
        public string MemberName { get; set; }
      
        public string Mobile { get; set; }
        public string LoginId { get; set; }
        public string InvitedBy { get; set; }
        public int TotalReferal { get; set; }     
    }

    public class TodaysReferalResponse: CommonJsonReponse
    {
        public List<TodayReferal> Data { get; set; }
    }
}