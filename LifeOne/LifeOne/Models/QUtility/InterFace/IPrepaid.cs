using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeOne.Models.QUtility.InterFace
{
    public interface IPrepaid
    {
        ResponseModel CallPrepaidAndOtherRechargeService(RequestModel _obj, string _url, string _ActionType, string _RechargeType);
    }
}
