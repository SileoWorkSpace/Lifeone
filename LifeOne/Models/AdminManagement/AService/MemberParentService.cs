using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class MemberParentService
    {
        public MemberParent BindMemberParentDetailByLoginID(MemberParent obj)
        {
            return DALMemberParent.BindMemberParentDetailByLoginID(obj);
        }
        
        public MemberParent UpdateMemberParentDetail(MemberParent obj)
        {
            return DALMemberParent.UpdateMemberParentDetail(obj);
        }
        
        public MemberParent AddMiddleMemberByLoginID(MemberParent obj)
        {
            return DALMemberParent.AddMiddleMemberByLoginID(obj);
        }


    }
}