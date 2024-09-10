using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALAdminImageUpload
    {        
        public static ResponseUploadImage ImageUpload(MAdminImageUpload obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Id", obj.Id);                          
                queryParameters.Add("@ImageUrl", obj.ImageUrl);                          
                queryParameters.Add("@Createdby", obj.CreatedBy);                          
                queryParameters.Add("@OpCode", obj.OpCode);
                ResponseUploadImage _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseUploadImage>("UploadeWebsiteImage", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MAdminImageUpload> GetImageList(MAdminImageUpload obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Id", obj.Id);
                List<MAdminImageUpload> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminImageUpload>("WebsiteImageList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ResponseUploadImage VideoUpload(MAdminImageUpload obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Id", obj.Id);                              
                queryParameters.Add("@Videofile", obj.Videolink);
                queryParameters.Add("@Createdby", obj.CreatedBy);
                queryParameters.Add("@OpCode", obj.OpCode);
                ResponseUploadImage _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseUploadImage>("UploadeWebsiteVideo", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MAdminImageUpload> GetVideoDetails(MAdminImageUpload obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Id", obj.Id);
                List<MAdminImageUpload> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminImageUpload>("WebsiteVideoList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}