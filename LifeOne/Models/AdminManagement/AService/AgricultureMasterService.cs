using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;

namespace LifeOne.Models.AdminManagement.AService
{
    public class AgricultureMasterService
    {
        public List<ProductMasterModel> BindProductMster()
        {
            List<ProductMasterModel> result = new List<ProductMasterModel>();
            try
            {
                result = AgricultureMasterDAL.BindProductMster();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public AddUpdateResponseResponseModel AddMaster(MAgricultureMaster model)
        {
            try
            {
                if (model != null && model._objDayList != null && model._objDayList.Count > 0)
                {
                    string xml = string.Empty;
                    xml += "<result>";
                    model._objDayWiseProductList.Where(x => x.ProductId > 0).ToList().ForEach(x =>
                    {
                        var crop = model._objDayList.Where(p => p.CommonId == x._ChildCommonId).FirstOrDefault();
                        xml += "<data><CropType>" + model.CropType + "</CropType><Days>" + crop.Day + "</Days><ToDays>" + crop.ToDay + "</ToDays><Area>" + crop.Area + "</Area><AreaType>" + crop.AreaType + "</AreaType><ProductId>" + x.ProductId + "</ProductId><ProductQty>" + x.Quantity + "</ProductQty><ProductUnit>" + null + "</ProductUnit><Description>" + x.Description + "</Description></data>";
                    });
                    xml += "</result>";
                    string LanguageXml = string.Empty;
                    LanguageXml += "N'<result>";
                    LanguageXml += "<data><MethodOfApplication>" + model.MethodOfApplication + "</MethodOfApplication><CropType>" + model.CropType + "</CropType> <FK_LanguageId>" + 2 + "</FK_LanguageId><CropName>" + model.CropName + "</CropName><TotalDays>" + model.TotalDaysOfCrop + "</TotalDays><CropCategory>" + model.CropCategory + "</CropCategory><Crop_ProductId>" + model.Crop_ProductId + "</Crop_ProductId></data>";
                    model.LanguageList = AgricultureMasterDAL.BindAllLanguageMster();
                    model.LanguageList.Where(x => x.Language_Code != "en").ToList().ForEach(x =>
                     {
                         string Transl =  TranslateText(model.CropName, x.Language_Code);
                         string TransMethodMode = TranslateText(model.MethodOfApplication, x.Language_Code);
                         LanguageXml += "<data> <MethodOfApplication>" + TransMethodMode + "</MethodOfApplication><CropType>" + model.CropType + "</CropType> <FK_LanguageId>" + x.Language_Id + "</FK_LanguageId><CropName>" + Transl + "</CropName><TotalDays>" + model.TotalDaysOfCrop + "</TotalDays><CropCategory>" + model.CropCategory + "</CropCategory><Crop_ProductId>" + model.Crop_ProductId + "</Crop_ProductId></data>";
                     });
                    LanguageXml += "</result>'";
                    model.CropName = null;
                    model.MethodOfApplication = null;
                    return AgricultureMasterDAL.AddMaster(xml, LanguageXml);
                }
                else
                {
                    return new AddUpdateResponseResponseModel { Status = false, Message = "Invalid Request!!!" };
                }
            }
            catch (Exception e)
            {
                return new AddUpdateResponseResponseModel { Status = false, Message = "#Error" };
            }
        }



        public List<_AgricultureMasterModel> GetCropDetailsByCropCodeReport(string CropCode, string CropType)
        {
            return AgricultureMasterDAL._GetCropDetailsByCropCodeReport(CropCode, CropType);
        }
        public MAgricultureMasterModel GetCropDetailsByCropCode(string CropCode, string CropType)
        {
            return AgricultureMasterDAL.GetCropDetailsByCropCode(CropCode, CropType);
        }
        public List<_AgricultureMasterModel> _GetCropDetailsByCropCode(string CropCode, int? FKMemid, int PK_OrderId, string LanguageCode)
        {
            return AgricultureMasterDAL._GetCropDetailsByCropCode(CropCode, FKMemid, PK_OrderId, LanguageCode);
        }

        public AgricultureMasterReportModel BindAgricultureMasterReport(AgricultureMasterFilter req)
        {
            AgricultureMasterReportModel Response = new AgricultureMasterReportModel();
            List<AgricultureMasterModel> result = AgricultureMasterDAL.BindAgricultureMasterReport(req);
            int totalRecords = 0;
            if (result != null && result.Count() > 0)
            {
                Response.AgricultureMasterList = result;
                totalRecords = Convert.ToInt32(result[0].TotalRecords);
                var pager = new Common.Pager(totalRecords, req.Page, SessionManager.Size);
                Response.Pager = pager;
            }
            else
            {
                Response.AgricultureMasterList = null;
            }
            return Response;


        }
        public AddUpdateResponseResponseModel UpdateMaster(MAgricultureMaster model)
        {
            try
            {
                if (model != null && model._objDayList != null && model._objDayList.Count > 0)
                {
                    string xml = string.Empty;
                    xml += "<result>";
                    model._objDayWiseProductList.Where(x => x.ProductId > 0).ToList().ForEach(x =>
                    {
                        var crop = model._objDayList.Where(p => p.CommonId == x._ChildCommonId).FirstOrDefault();
                        xml += "<data><CropType>" + model.CropType + "</CropType><Days>" + crop.Day + "</Days><ToDays>" + crop.ToDay + "</ToDays><Area>" + crop.Area + "</Area><AreaType>" + crop.AreaType + "</AreaType><ProductId>" + x.ProductId + "</ProductId><ProductQty>" + x.Quantity + "</ProductQty><ProductUnit>" + null + "</ProductUnit><Description>" + x.Description + "</Description></data>";
                    });
                    xml += "</result>";
                    return AgricultureMasterDAL.UpdateCropMaster(xml, model.CropCode, model.CropType);
                }
                else
                {
                    return new AddUpdateResponseResponseModel { Status = false, Message = "Invalid Request!!!" };
                }
            }
            catch (Exception e)
            {
                return new AddUpdateResponseResponseModel { Status = false, Message = "#Error" };
            }
        }

        public string TranslateText(string input, string target)
        {
            try
            {
                string url = "https://translation.googleapis.com/language/translate/v2?key=AIzaSyC8Y_Jg_YuJCrMoWfKaP4CPb06d-ueedp4"; // Paid API
                url += "&source=en";
                url += "&target=" + target;
                url += "&q=" + input.Trim();
                HttpClient httpClient = new HttpClient();
                string result = httpClient.GetStringAsync(url).Result;
                return (new JavaScriptSerializer()).Deserialize<JsonData_>(result).Data.Translations[0].TranslatedText;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public AddUpdateResponseResponseModel DeleteCrop(string CropCode, string CropType)
        {
            return AgricultureMasterDAL.DeleteCrop(CropCode, CropType);
        }

        public List<CropCategoryModel> BindCropCategoryList()
        {
            return AgricultureMasterDAL.BindCropCategoryList();
        }
        public List<CropProductModel> BindCropProductByCategoyId(int CategoryId)
        {
            return AgricultureMasterDAL.BindCropProductByCategoyId(CategoryId);
        }

    }
    public class JsonData_
    {
        public Data Data { get; set; }
    }
    public class Data
    {
        public List<Translation> Translations { get; set; }
    }
    public class Translation
    {
        public string TranslatedText { get; set; }
    }
}