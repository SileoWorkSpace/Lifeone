using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class MemberLeaderService
    {
        public static List<MFranchiseLeaders> MemberLeaderList(MFranchiseLeaders _model)
        {
            return DALMemberLeader.MemberLeaderList(_model);
        }
    }
}