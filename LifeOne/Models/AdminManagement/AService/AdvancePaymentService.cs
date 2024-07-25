using LifeOne.Controllers;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class AdvancePaymentService
    {
        public AdvancePaymentReportModel GetAllAdvancePaymentRequest(CommonRequestModel req)
        {

            AdvancePaymentReportModel result = new AdvancePaymentReportModel();
            try
            {
                result.DataList = AdvancePaymentDAL.GetAllAdvancePaymentRequest(req);
                int totalRecords = 0;
                if (result.DataList != null && result.DataList.Count() > 0)
                {
                    totalRecords = Convert.ToInt32(result.DataList[0].TotalRecords);
                    var pager = new Common.Pager(totalRecords, req.Page, SessionManager.Size);
                    result.Pager = pager;
                }
                else
                {
                    result.DataList = null;
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public AddUpdateResponseResponseModel ApproveRequest(string Action, string Narration, int ReqId = 0)
        {
            AddUpdateResponseResponseModel result = new AddUpdateResponseResponseModel();
            try
            {
                result = AdvancePaymentDAL.ApproveRequest(Action, Narration, ReqId);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public MSimpleString SaveAdvancePaymentDetails(AdvancePaymentApprovalDetails _data)
        {
            try
            {
                MSimpleString _Response = new MSimpleString();
                string FilePath = string.Empty;
                if (_data.Doc != null && _data.Doc.ContentLength > 0)
                {
                    var extension = Path.GetExtension(_data.Doc.FileName);
                    var filesize = Math.Round(Convert.ToDouble(_data.Doc.ContentLength / 1024));
                    if (filesize > 20)
                    {
                        _Response.flag = 0;
                        _Response.Message = "File is too Big. Please select a file less then 20mb !";
                        return _Response;
                    }
                    else if (extension.ToUpper() == ".PNG" || extension.ToUpper() == ".JPG" || extension.ToUpper() == ".JPEG" || extension.ToUpper() == ".BMP" || extension.ToUpper() == ".GIF" || extension.ToUpper() == ".PPT" || extension.ToUpper() == ".PPTX" || extension.ToUpper() == ".CGI" || extension.ToUpper() == ".XLX" || extension.ToUpper() == ".XLSX" || extension.ToUpper() == ".PDF" || extension.ToUpper() == ".EX4" || extension.ToUpper() == ".DOC" || extension.ToUpper() == ".DOCX" || extension.ToUpper() == ".TXT")
                    {
                        string fileName = _data.Doc.FileName;
                        string fileExtension = _data.Doc.ContentType;
                        fileExtension = Path.GetExtension(fileName);
                        fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + fileName;
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            string path = HttpContext.Current.Server.MapPath("~/ReadWriteData/PaymentSlip/") + fileName;
                            _data.Doc.SaveAs(path);
                            FilePath = "/ReadWriteData/PaymentSlip/" + fileName;
                        }
                    }
                    else
                    {
                        _Response.flag = 0;
                        _Response.Message = "Invalid file extension!!!";

                    }
                }
                _data.PaymentSlip = FilePath;
                _Response = AdvancePaymentDAL.SaveAdvancePaymentDetails(_data);
                _Response.Code = _Response.flag;
                return _Response;
            }
            catch (Exception e)
            {
                return new MSimpleString { flag = 0, Message = "#Error" };
            }
        }
        public AdvancePaymenthistory ViewAdvancePaymenthistory(int ReqId = 0)
        {
          return  AdvancePaymentDAL.ViewAdvancePaymenthistory(ReqId);
        }
    }
}