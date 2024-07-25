using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.BillAvenueUtility.Entity
{
    public class BABillerMasterModel
    {
        public string billerId { get; set; }
        public string billerName { get; set; }
        public string billerCategory { get; set; }
        public string billerInputParams { get; set; }

        public string billerFetchRequiremet { get; set; }
        public string billerSupportBillValidation { get; set; }
        
    }

    public class BABillerCustomFieldDetailModel
    {
        public int Id { get; set; }
        public int minLength { get; set; }
        public int maxLength { get; set; }
        public string BIllerCategory { get; set; }
        public string RMTName { get; set; }
        public string paramName { get; set; }
        public string dataType { get; set; }
        public string regEx { get; set; }
        public bool isOptional { get; set; }
        public bool IsActive { get; set; }
        public bool? Act { get; set; }
        public string Crdt { get; set; }
    }

    public class BABillerDetail
    {
        public BABillerMasterModel BillerMasterDetail { get; set; }
        public List<BABillerCustomFieldDetailModel> BillerCustomFieldDetailList { get; set; }
    }

    public class ParamInfo
    {

        public string paramName { get; set; }
        public string dataType { get; set; }
        public Nullable<bool> isOptional { get; set; }
        public string minLength { get; set; }
        public string maxLength { get; set; }
        public string regEx { get; set; }
    }

    public class BillerInputParamsList
    {
        public List<ParamInfo> paramInfo { get; set; }
    }

    public class BillerInputParams
    {
        public ParamInfo paramInfo { get; set; }
    }
    public class BillerParamsInfoDetailModel
    {
        public BillerInputParamsList billerInputParams { get; set; }
    }
    public class BillerParamsInfoModel
    {
        public BillerInputParams billerInputParams { get; set; }
    }


    //public class CustomeFieldDataModelBillerWise
    //{
    //    public string billerFetchRequiremet { get; set; }
    //    public string billerSupportBillValidation { get; set; }

    //    public List<CustomeFieldDataModel> sralizeData { get; set; }
    //}
    public class CustomeFieldDataModel
    {
        public string billerFetchRequiremet { get; set; }
        public string billerSupportBillValidation { get; set; }
        public string RMTName { get; set; }
        public string ParamName { get; set; }
        public string RegEx { get; set; }
        public Nullable<bool> IsOptional { get; set; }
        public bool Active { get; set; }

    }
}