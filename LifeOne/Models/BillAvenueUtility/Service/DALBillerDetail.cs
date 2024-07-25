using LifeOne.Models.BillAvenueUtility.DAL;
using LifeOne.Models.BillAvenueUtility.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace LifeOne.Models.BillAvenueUtility.Service
{
    public class DALBillerDetail
    {
        public static ResponseModel GetCustomBillerDetail(RequestModel _obj)
        {


            var sralizeData = new List<CustomeFieldDataModel>();
            BABillerCustomFieldDetailModel slctCustomfldDetail = new BABillerCustomFieldDetailModel();
            DALCommon common = new DALCommon();
            ResponseModel responseModel = new ResponseModel();
            try
            {
                BABillerDetail model = new BABillerDetail();
                // RequestModel _biilDetails = DALCommon.GetGenericRechareAPIResponseWithDecryptList<RequestModel>(_obj.body);
                RequestModel _biilDetails = DALCommon.CustomDeserializeObjectWithDecryptString<RequestModel>(_obj.body);

                DataSet dataSet = common.GetCustomBillerDetail(_biilDetails);
                BABillerCustomFieldDetailModel bABillerDetail = new BABillerCustomFieldDetailModel();
                List<BABillerCustomFieldDetailModel> bABillerCustomFieldDetailModels = new List<BABillerCustomFieldDetailModel>();
                BABillerMasterModel bABillerMasterModel = new BABillerMasterModel();
                bABillerMasterModel.billerName = dataSet.Tables[0].Rows[0]["billerName"].ToString();
                bABillerMasterModel.billerId = dataSet.Tables[0].Rows[0]["billerId"].ToString();
                bABillerMasterModel.billerCategory = dataSet.Tables[0].Rows[0]["billerCategory"].ToString();
                bABillerMasterModel.billerInputParams = dataSet.Tables[0].Rows[0]["billerInputParams"].ToString();
                bABillerMasterModel.billerFetchRequiremet = dataSet.Tables[0].Rows[0]["billerFetchRequiremet"].ToString();
                bABillerMasterModel.billerSupportBillValidation = dataSet.Tables[0].Rows[0]["billerSupportBillValidation"].ToString();
                model.BillerMasterDetail = bABillerMasterModel;

                for (int i = 0; i <= dataSet.Tables[1].Rows.Count - 1; i++)
                {
                    BABillerCustomFieldDetailModel bABiller = new BABillerCustomFieldDetailModel();
                    bABiller.Id = int.Parse(dataSet.Tables[1].Rows[i]["Id"].ToString());
                    bABiller.BIllerCategory = dataSet.Tables[1].Rows[i]["BIllerCategory"].ToString();
                    bABiller.paramName = dataSet.Tables[1].Rows[i]["paramName"].ToString();
                    bABiller.dataType = dataSet.Tables[1].Rows[i]["dataType"].ToString();
                    bABiller.regEx = dataSet.Tables[1].Rows[i]["regEx"].ToString();
                    bABiller.RMTName = dataSet.Tables[1].Rows[i]["RMTName"].ToString();
                    bABiller.isOptional = bool.Parse(dataSet.Tables[1].Rows[i]["isOptional"].ToString());
                    bABiller.minLength = string.IsNullOrEmpty(dataSet.Tables[1].Rows[i]["minLength"].ToString()) ? 0 : int.Parse(dataSet.Tables[1].Rows[i]["minLength"].ToString());
                    bABiller.maxLength = string.IsNullOrEmpty(dataSet.Tables[1].Rows[i]["maxLength"].ToString()) ? 0 : int.Parse(dataSet.Tables[1].Rows[i]["maxLength"].ToString());
                    bABiller.Act = string.IsNullOrEmpty(dataSet.Tables[1].Rows[i]["Act"].ToString()) ? false : bool.Parse(dataSet.Tables[1].Rows[i]["Act"].ToString());
                    bABiller.Crdt = string.IsNullOrEmpty(dataSet.Tables[1].Rows[i]["Crdt"].ToString()) ? null : (dataSet.Tables[1].Rows[i]["Crdt"].ToString());
                    bABillerCustomFieldDetailModels.Add(bABiller);
                }
                model.BillerCustomFieldDetailList = bABillerCustomFieldDetailModels;
                var datareslist = new BillerParamsInfoDetailModel();
                var datares = new BillerParamsInfoModel();
                dynamic data = JsonConvert.DeserializeObject<dynamic>(model.BillerMasterDetail.billerInputParams);
                if (data.billerInputParams.paramInfo != null && data.billerInputParams.paramInfo.Count > 0)
                    datareslist = JsonConvert.DeserializeObject<BillerParamsInfoDetailModel>(model.BillerMasterDetail.billerInputParams);
                else
                    datares = JsonConvert.DeserializeObject<BillerParamsInfoModel>(model.BillerMasterDetail.billerInputParams);

                sralizeData = model.BillerCustomFieldDetailList.GroupBy(x => x.RMTName).Select(x => new CustomeFieldDataModel
                {
                    RMTName = x.FirstOrDefault().RMTName,
                }).ToList();

                sralizeData.ForEach(a =>
                {
                    model.BillerCustomFieldDetailList.ForEach(b =>
                    {
                        if (datareslist.billerInputParams != null)
                        {
                            if (a.RMTName == b.RMTName && b.paramName == datareslist.billerInputParams.paramInfo.Where(x => x.paramName.Contains(b.paramName)).Select(x => x.paramName).FirstOrDefault())
                            {
                                a.ParamName = b.paramName;
                                a.IsOptional = datareslist.billerInputParams.paramInfo.FirstOrDefault(x => x.paramName.Contains(b.paramName)).isOptional;
                                a.RegEx = datareslist.billerInputParams.paramInfo.FirstOrDefault(x => x.paramName.Contains(b.paramName)).regEx;
                                a.Active = true;
                                a.billerFetchRequiremet = model.BillerMasterDetail.billerFetchRequiremet;
                                a.billerSupportBillValidation = model.BillerMasterDetail.billerSupportBillValidation;
                            }
                        }

                        else if (datares.billerInputParams != null)
                        {
                            if (a.RMTName == b.RMTName && b.paramName == datares.billerInputParams.paramInfo.paramName)
                            {
                                a.ParamName = b.paramName;
                                a.IsOptional = datares.billerInputParams.paramInfo.isOptional;
                                a.RegEx = datares.billerInputParams.paramInfo.regEx;
                                a.Active = true;
                                a.billerFetchRequiremet = model.BillerMasterDetail.billerFetchRequiremet;
                                a.billerSupportBillValidation = model.BillerMasterDetail.billerSupportBillValidation;
                            }

                        }
                    });
                });
                responseModel.body = DALCommon.CustomSerializeObjectWithEncryptList<CustomeFieldDataModel>(sralizeData); // on success we will be added records on
            }
            catch (Exception ex)
            {
            }
            return responseModel;
        }


    }


}