using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class TestimonialsModel
    {
        public string LoginId { get; set; }
        public long FK_MemId { get; set; }
        public int CropCategoryId { get; set; }
        public int CropId { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string Profile { get; set; }
        public string Name { get; set; }
        public string MemberType { get; set; }
        public TestimonialsResponseModel AddTestimonials()
        {
            try
            {
                var QuaryParameter = new DynamicParameters();
                QuaryParameter.Add("@LoginId", LoginId);
                QuaryParameter.Add("@FK_MemId", FK_MemId);
                QuaryParameter.Add("@CropCategoryId", CropCategoryId);
                QuaryParameter.Add("@CropId", CropId);
                QuaryParameter.Add("@Description", Description);
                QuaryParameter.Add("@FilePath", FilePath);
                return DBHelperDapper.DAAddAndReturnModel<TestimonialsResponseModel>("ProAddTestimonials", QuaryParameter);
            }
            catch (Exception e)
            {
                return new TestimonialsResponseModel { Status = false, Flag = 0, response = "failed", Message = e.Message };          
            }
        }

        public TestimonialsResponseModel BindTestimonials()
        {
            try
            {
                TestimonialsResponseModel _obj = new TestimonialsResponseModel();
                var QuaryParameter = new DynamicParameters();
                List<TestimonialsModel> _response = DBHelperDapper.DAAddAndReturnModelList<TestimonialsModel>("ProBindTestimonials", QuaryParameter);
                if (_response != null && _response.Count() > 0)
                {
                    _obj.Status = true;
                    _obj.Data = _response;
                }
                else
                {
                    _obj.Status = false;
                    _obj.Data = null;
                }
                return _obj;
            }
            catch (Exception e)
            {
                return new TestimonialsResponseModel { Status = false, Flag = 0, response = "failed", Message = e.Message };
            }
        }

        public TestimonialsResponseModel BindTestimonialsWithCat(int Categoryid)
        {
            try
            {
                TestimonialsResponseModel _obj = new TestimonialsResponseModel();
                var QuaryParameter = new DynamicParameters();
                QuaryParameter.Add("@CategoryID", Categoryid);
                List<TestimonialsModel> _response = DBHelperDapper.DAAddAndReturnModelList<TestimonialsModel>("ProBindTestimonialsWithCat", QuaryParameter);
                if (_response != null && _response.Count() > 0)
                {
                    _obj.Status = true;
                    _obj.Data = _response;
                }
                else
                {
                    _obj.Status = false;
                    _obj.Data = null;
                }
                return _obj;
            }
            catch (Exception e)
            {
                return new TestimonialsResponseModel { Status = false, Flag = 0, response = "failed", Message = e.Message };
            }
        }
    }
    public class TestimonialsResponseModel
    {
        public bool Status { get; set; }
        public int Flag { get; set; }
        public string response { get; set; }
        public string Message { get; set; }
        public List<TestimonialsModel> Data { get; set; }
    }
}