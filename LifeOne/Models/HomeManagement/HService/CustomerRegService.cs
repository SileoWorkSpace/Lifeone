using LifeOne.Models.HomeManagement.HDAL;
using LifeOne.Models.HomeManagement.HEntity;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using LifeOne.Models.API;

namespace LifeOne.Models.HomeManagement.HService
{
    public class CustomerRegService
    {
        public static MCustomerRegistration InvokeGetSponserDetails(string Fk_SponserId)
        {
            MCustomerRegistration obj = DALCustomerRegistration.GetSponserDetailByReferalCode(Fk_SponserId);
            return obj;
        }
        public static MplaceUnderId InvokePlaceHolderId(string Fk_SponserId)
        {
            MplaceUnderId obj = DALCustomerRegistration.InvokePlaceHolderId(Fk_SponserId);
            return obj;
        }
        public static MplaceUnderId InvokeGetPlaceholderWithPArentid(string Fk_SponserId,string Fk_PlaceId)
        {
            MplaceUnderId obj = DALCustomerRegistration.InvokePlaceHolderIdWithParent(Fk_SponserId, Fk_PlaceId);
            return obj;
        }


        public static MCustomerRegistration InvokeGetDistrictDetails(string Pincode)
        {
            MCustomerRegistration obj = DALCustomerRegistration.DistrictDetails(Pincode);
            return obj;
        }

        public static MSimpleString InvokeCustomerPrimaryDetails(MCustomerRegistration _model)
        {
            MSimpleString _Response = new MSimpleString();
            if (_model.AttachmentFile != null && _model.AttachmentFile.ContentLength > 0)
            {
                var extension = Path.GetExtension(_model.AttachmentFile.FileName);
                var filesize = Math.Round(Convert.ToDouble(_model.AttachmentFile.ContentLength / 1024));
                if (filesize > 1024)
                {
                    _Response.flag = 0;
                    _Response.Remark = "File is too Big. Please select a file less then 1MB !";
                    return _Response;
                }
                else if (extension.ToUpper() == ".PNG" || extension.ToUpper() == ".JPG" || extension.ToUpper() == ".JPEG" || extension.ToUpper() == ".BMP" || extension.ToUpper() == ".GIF" || extension.ToUpper() == ".PDF")
                {
                    string fileName = _model.AttachmentFile.FileName;
                    string fileExtension = _model.AttachmentFile.ContentType;
                    fileExtension = Path.GetExtension(fileName);
                    fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + _model.DocType + "_" + fileExtension;
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        string path = HttpContext.Current.Server.MapPath("~/Images/Users/Voterid/") + fileName;
                        _model.AttachmentFile.SaveAs(path);
                        _model.FilePath = "/Images/Users/Voterid/" + fileName;
                    }

                }
                else
                {
                    _Response.flag = 0;
                    _Response.Remark = "Invalid file extension!!!";
                    return _Response;
                }
            }
            _Response = DALCustomerRegistration.CustomerPrimaryDetails(_model);
            return _Response;
        }
        //Edit 
        public static MSimpleString InvokeUpdateCustomerPrimaryDetails(MCustomerRegistration _model)
        {
            MSimpleString obj = DALCustomerRegistration.CustomerUpdatePrimaryDetails(_model);
            return obj;
        }

        public static MSimpleString InvokeCustomerAddressDetails(MCustomerRegistration _model)
        {
            MSimpleString obj = DALCustomerRegistration.CustomerAddressDetails(_model);
            return obj;
        }
        public static MSimpleString InvokeCustomerFamilyDetails(MCustomerRegistration _model, string XmlData)
        {
            MSimpleString obj = DALCustomerRegistration.CustomerFamilyDetails(_model, XmlData);
            return obj;
        }

        public static MSimpleString InvokeCustomerFarmDetails(MCustomerRegistration _model)
        {
            MSimpleString obj = DALCustomerRegistration.CustomerFarmDetails(_model);
            return obj;
        }

        public static MCustomerRegistration InvokeCustomerDetailsById(int? Fk_MemId)
        {
            MCustomerRegistration obj = DALCustomerRegistration.GetCustomerDetailsById(Fk_MemId);
            return obj;
        }
        public static List<ChildDetails> InvokeChildDetailsById(int? Fk_MemId)
        {
            List<ChildDetails> obj = DALCustomerRegistration.GetChildDetailsById(Fk_MemId);
            return obj;
        }

        public static FranchiseRegistrationResponse SaveFranchiseApplicationForm(FranchiseApplicationFormModel req)
        {   try
            {
                FranchiseRegistrationResponse _Response = new FranchiseRegistrationResponse();
                string DocXML = string.Empty;
                string BankXML = string.Empty;
                if (req.FranchiseRequestDocumentList != null && req.FranchiseRequestDocumentList.Count() > 0)
                {
                    DocXML += "<result>";
                    foreach (var x in req.FranchiseRequestDocumentList)
                    {
                        if (x.DocFile != null && x.DocFile.ContentLength > 0)
                        {
                            var extension = Path.GetExtension(x.DocFile.FileName);
                            var filesize = Math.Round(Convert.ToDouble(x.DocFile.ContentLength / 1024));
                            if (filesize > 1024)
                            {
                                _Response.Flag = 0;
                                _Response.Remark = "File is too Big. Please select a file less then 1MB !";
                                return _Response;
                            }
                            else if (extension.ToUpper() == ".PNG" || extension.ToUpper() == ".JPG" || extension.ToUpper() == ".JPEG" || extension.ToUpper() == ".BMP" || extension.ToUpper() == ".GIF" || extension.ToUpper() == ".PDF")
                            {
                                string fileName = x.DocFile.FileName;
                                string fileExtension = x.DocFile.ContentType;
                                fileExtension = Path.GetExtension(fileName);
                                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + x.DocType + "_" + fileExtension;
                                if (!string.IsNullOrEmpty(fileName))
                                {
                                    string path = HttpContext.Current.Server.MapPath("~/Images/Users/Voterid/") + fileName;
                                    x.DocFile.SaveAs(path);
                                    x.DocImage = "/Images/Users/Voterid/" + fileName;
                                }                           

                            }
                            else
                            {
                                _Response.Flag = 0;
                                _Response.Remark = "Invalid file extension!!!";
                                return _Response;
                            }
                        }
                        DocXML += "<data> <DocType>" + x.DocType + " </DocType> <DocNumber> " + x.DocNumber + "</DocNumber> <DocImage>" + x.DocImage + " </DocImage></data>";
                    }
                    DocXML += "</result>";
                }
                if (req.FranchiseRequestBankList != null && req.FranchiseRequestBankList.Count() > 0)
                {
                    BankXML += "<result>";
                    req.FranchiseRequestBankList.ForEach(x =>
                    {
                        BankXML += "<data> <AccountNo>" + x.AccountNo + " </AccountNo> <BankName> " + x.BankName + "</BankName> <Branch>" + x.Branch + " </Branch> <IFSC_CODE>" + x.IFSC_CODE + " </IFSC_CODE><GSTNo>" + x.GSTNo + " </GSTNo></data>";
                    });
                    BankXML += "</result>";
                }
                req.DocXml = DocXML;
                req.BankXML = BankXML;
                _Response = DALCustomerRegistration.SaveFranchiseApplicationForm(req);
                return _Response;
            }
            catch (Exception e)
            {
                return new FranchiseRegistrationResponse {Flag=0, Remark = e.Message };
            }
           
        }
        public static MemberDetailsForFranchiseModel GetMemberDetailsForFranchiseApplicationForm(string BID)
        {
            MemberDetailsForFranchiseModel _response = DALCustomerRegistration.GetMemberDetailsForFranchiseApplicationForm(BID);
            return _response;
        }

        public static FranchiseApplicationFormReportModel BindFranchiseApplicationForm(FranchiseApplicationFormReportModel model)
        {
            return DALCustomerRegistration.BindFranchiseApplicationForm(model);
        }
        public static FranchiseApprovalResponseModel ApproveFranchiseApproval(int? KeyId, int? Status, string Remark)
        {
            try
            {
                FranchiseApprovalResponseModel _Response = new FranchiseApprovalResponseModel();
                _Response = DALCustomerRegistration.ApproveFranchiseApproval(KeyId, Status, Remark);
              
                return _Response;
            }
            catch (Exception e)
            {
                return new FranchiseApprovalResponseModel { Status = false, Message = "#Error" };
            }
        }
        public static FranchiseApplicationFormReportModel GetOtherFranchiseApprovalDetails(int KeyId)
        {
            return DALCustomerRegistration.GetOtherFranchiseApprovalDetails(KeyId);
        }
        public static FranchiseApplicationFormReportModel CheckFranchiseApprovalByPincode(int? pincode)
        {
            return DALCustomerRegistration.CheckFranchiseApprovalByPincode(pincode);
        }
        public static FranchiseRegistrationResponse SaveFranchiseApplicationForm_V1(FranchiseApplicationFormModel req)
        {
            try
            {
                FranchiseRegistrationResponse _Response = new FranchiseRegistrationResponse();
                string DocXML = string.Empty;
                string BankXML = string.Empty;
                if (req.FranchiseRequestDocumentList != null && req.FranchiseRequestDocumentList.Count() > 0)
                {
                    DocXML += "<result>";
                    foreach (var x in req.FranchiseRequestDocumentList)
                    {
                        if (x.DocFile != null && x.DocFile.ContentLength > 0)
                        {
                            var extension = Path.GetExtension(x.DocFile.FileName);
                            var filesize = Math.Round(Convert.ToDouble(x.DocFile.ContentLength / 1024));
                            if (filesize > 1024)
                            {
                                _Response.Flag = 0;
                                _Response.Remark = "File is too Big. Please select a file less then 1MB !";
                                return _Response;
                            }
                            else if (extension.ToUpper() == ".PNG" || extension.ToUpper() == ".JPG" || extension.ToUpper() == ".JPEG" || extension.ToUpper() == ".BMP" || extension.ToUpper() == ".GIF" || extension.ToUpper() == ".PDF")
                            {
                                string fileName = x.DocFile.FileName;
                                string fileExtension = x.DocFile.ContentType;
                                fileExtension = Path.GetExtension(fileName);
                                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + x.DocType + "_" + fileExtension;
                                if (!string.IsNullOrEmpty(fileName))
                                {
                                    string path = HttpContext.Current.Server.MapPath("~/Images/Users/Voterid/") + fileName;
                                    x.DocFile.SaveAs(path);
                                    x.DocImage = "/Images/Users/Voterid/" + fileName;
                                }

                            }
                            else
                            {
                                _Response.Flag = 0;
                                _Response.Remark = "Invalid file extension!!!";
                                return _Response;
                            }
                        }
                        DocXML += "<data> <DocType>" + x.DocType + " </DocType> <DocNumber> " + x.DocNumber + "</DocNumber> <DocImage>" + x.DocImage + " </DocImage></data>";
                    }
                    DocXML += "</result>";
                }
                if (req.FranchiseRequestBankList != null && req.FranchiseRequestBankList.Count() > 0)
                {
                    BankXML += "<result>";
                    req.FranchiseRequestBankList.ForEach(x =>
                    {
                        BankXML += "<data> <AccountNo>" + x.AccountNo + " </AccountNo> <BankName> " + x.BankName + "</BankName> <Branch>" + x.Branch + " </Branch> <IFSC_CODE>" + x.IFSC_CODE + " </IFSC_CODE><GSTNo>" + x.GSTNo + " </GSTNo></data>";
                    });
                    BankXML += "</result>";
                }
                req.DocXml = DocXML;
                req.BankXML = BankXML;
                _Response = DALCustomerRegistration.SaveFranchiseApplicationForm_V1(req);
                return _Response;
            }
            catch (Exception e)
            {
                return new FranchiseRegistrationResponse { Flag = 0, Remark = e.Message };
            }

        }

    }
}