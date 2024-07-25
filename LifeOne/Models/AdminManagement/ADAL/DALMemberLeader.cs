using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALMemberLeader
    {
        public static List<MFranchiseLeaders> MemberLeaderList(MFranchiseLeaders model)
        {
            try
            {                
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Page", model.Page == null ? 1 : model.Page);
                queryParameters.Add("@Size", SessionManager.Size);
                queryParameters.Add("@LoginId", model.LoginId);
                queryParameters.Add("@Name", model.FirstName);
                queryParameters.Add("@Mobile", model.Phone);
                model.FranchiseLeaderList = DBHelperDapper.DAAddAndReturnModelList<MFranchiseLeaders>("GetFranchiseLeaders", queryParameters);
                return model.FranchiseLeaderList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}