
using LifeOne.Filters;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API;
using LifeOne.Models.API.DAL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LifeOne.Controllers
{
    // [AuthorizeFilter]
    // [ValidCustomer]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImageController : ApiController
    {
        CustomerDb db = new CustomerDb();
        [HttpPost]
        public async Task<Imagelist> ImageUpload(string Id)
        {
            try
            {
                Random rn = new Random();
                string Results = string.Empty;
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                Imagelist imgs = new Imagelist();
                var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
                //access form data  
                NameValueCollection formData = provider.FormData;
                //access files  
                IList<HttpContent> files = provider.Files;
                HttpContent file1 = files[0];
                var thisFileName = rn.Next(10, 100) + "_" + formData["Action"] + "_" + Id + "_" + file1.Headers.ContentDisposition.FileName.Trim('\"');

                ////-------------------------------------For testing----------------------------------  
                string filename = String.Empty;
                Stream input = await file1.ReadAsStreamAsync();
                string directoryName = String.Empty;
                string URL = String.Empty;
                string Action = String.Empty;

                var path = HttpRuntime.AppDomainAppPath;
                var UniqueId = formData["UniqueNo"].ToString();
                if (formData["Action"] == "Profile")
                {
                    directoryName = System.IO.Path.Combine(path, "Images/Users/ProfilePic");
                }
                else if (formData["Action"] == "Pan")
                {
                    directoryName = System.IO.Path.Combine(path, "Images/Users/Pan");
                }
                else if (formData["Action"] == "VoterId")
                {
                    directoryName = System.IO.Path.Combine(path, "Images/Users/VoterId");
                }


                else if (formData["Action"] == "Bank")
                {
                    directoryName = System.IO.Path.Combine(path, "Images/Users/Bank");
                }

                else if (formData["Action"] == "AdharFront")
                {
                    directoryName = System.IO.Path.Combine(path, "Images/Users/Aadhar");
                }
                else if (formData["Action"] == "AdharBack")
                {
                    directoryName = System.IO.Path.Combine(path, "Images/Users/Aadhar");
                }
                else if (formData["Action"] == "AddressFront")
                {
                    directoryName = System.IO.Path.Combine(path, "Images/Users/Aadhar");
                }
                else if (formData["Action"] == "AddressBack")
                {
                    directoryName = System.IO.Path.Combine(path, "Images/Users/Aadhar");
                }
                else if (formData["Action"] == "SlipUpload")
                {
                    directoryName = System.IO.Path.Combine(path, "Images/Users/Bank");
                }
                else if (formData["Action"] == "FranSlipUpload")
                {
                    directoryName = System.IO.Path.Combine(path, "Images/Users/FranchisePaymentSlip");
                }
                else if (formData["Action"] == "Support")
                {
                    directoryName = System.IO.Path.Combine(path, "Images/Users/Support");
                }
                else if (formData["Action"] == "Popup")
                {
                    directoryName = System.IO.Path.Combine(path, "Images/Users/Popup");
                }
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                filename = System.IO.Path.Combine(directoryName, thisFileName);
                //Deletion exists file  
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                string DocsPath = "/";
                URL = DocsPath + thisFileName;

                if (formData["Action"] == "Pan" || formData["Action"] == "Profile" || formData["Action"] == "Bank" || formData["Action"] == "SlipUpload" || formData["Action"] == "AddressFront" || formData["Action"] == "AddressBack" || formData["Action"] == "AdharFront" || formData["Action"] == "AdharBack" || formData["Action"] == "Support" || formData["Action"] == "VoterId" || formData["Action"] == "FranSlipUpload" || formData["Action"] == "Popup")
                {
                    if (formData["Action"] == "Profile")
                    {
                        imgs.fk_MemId = Id;
                        imgs.uniqueNo = UniqueId;
                        imgs.imageUrl = "~/Images/Users/ProfilePic" + URL;
                        imgs.action = formData["Action"].ToString();
                    }
                    else if (formData["Action"] == "Pan")
                    {
                        imgs.fk_MemId = Id;
                        imgs.uniqueNo = UniqueId;
                        imgs.imageUrl = "~/Images/Users/Pan" + URL;
                        imgs.action = formData["Action"].ToString();
                    }
                    else if (formData["Action"] == "VoterId")
                    {
                        imgs.fk_MemId = Id;
                        imgs.uniqueNo = UniqueId;
                        imgs.imageUrl = "~/Images/Users/VoterId" + URL;
                        imgs.action = formData["Action"].ToString();
                    }

                    else if (formData["Action"] == "Bank")
                    {
                        imgs.fk_MemId = Id;
                        imgs.uniqueNo = UniqueId;
                        imgs.imageUrl = "~/Images/Users/Bank" + URL;
                        imgs.action = formData["Action"].ToString();
                    }
                    else if (formData["Action"] == "SlipUpload")
                    {
                        imgs.fk_MemId = Id;
                        imgs.uniqueNo = UniqueId;
                        imgs.imageUrl = "~/Images/Users/Bank" + URL;
                        imgs.action = formData["Action"].ToString();
                    }
                    else if (formData["Action"] == "AddressFront")
                    {
                        imgs.fk_MemId = Id;
                        imgs.uniqueNo = UniqueId;
                        imgs.imageUrl = "~/Images/Users/Aadhar" + URL;
                        imgs.action = formData["Action"].ToString();
                        // imgs.type = formData["Type"].ToString();
                        // imgs.uniqueNo = formData["UniqueNo"].ToString();
                    }
                    else if (formData["Action"] == "AddressBack")
                    {
                        imgs.fk_MemId = Id;
                        imgs.uniqueNo = UniqueId;
                        imgs.imageUrl = "~/Images/Users/Aadhar" + URL;
                        imgs.action = formData["Action"].ToString();
                        // imgs.type = formData["Type"].ToString();
                        //imgs.uniqueNo = formData["UniqueNo"].ToString();
                    }
                    else if (formData["Action"] == "AdharFront")
                    {
                        imgs.fk_MemId = Id;
                        imgs.uniqueNo = UniqueId;
                        imgs.imageUrl = "~/Images/Users/Aadhar" + URL;
                        imgs.action = formData["Action"].ToString();
                        // imgs.type = formData["Type"].ToString();
                        //  imgs.uniqueNo = formData["UniqueNo"].ToString();
                    }
                    else if (formData["Action"] == "AdharBack")
                    {
                        imgs.fk_MemId = Id;
                        imgs.uniqueNo = UniqueId;
                        imgs.imageUrl = "~/Images/Users/Aadhar" + URL;
                        imgs.action = formData["Action"].ToString();
                        // imgs.type = formData["Type"].ToString();
                        //imgs.uniqueNo = formData["UniqueNo"].ToString();
                    }
                    else if (formData["Action"] == "Support")
                    {
                        imgs.fk_MemId = Id;
                        imgs.uniqueNo = UniqueId;
                        imgs.imageUrl = "~/Images/Users/Support" + URL;
                        imgs.action = formData["Action"].ToString();
                        //imgs.type = formData["Type"].ToString();
                        //  imgs.uniqueNo = formData["UniqueNo"].ToString();
                    }
                    else if (formData["Action"] == "FranSlipUpload")
                    {
                        imgs.fk_MemId = Id;
                        imgs.uniqueNo = UniqueId;
                        imgs.imageUrl = "~/Images/Users/FranchisePaymentSlip" + URL;
                        imgs.action = formData["Action"].ToString();
                        //imgs.type = formData["Type"].ToString();
                        //  imgs.uniqueNo = formData["UniqueNo"].ToString();
                    }
                    else if (formData["Action"] == "Popup")
                    {
                        imgs.fk_MemId = Id;
                        imgs.uniqueNo = UniqueId;
                        imgs.imageUrl = "~/Images/Users/Popup" + URL;
                        imgs.action = formData["Action"].ToString();
                        
                    }
                }
                Results = AddImages(imgs);
                using (Stream file = File.OpenWrite(filename))
                {
                    input.CopyTo(file);
                    file.Close();
                }
                if (Results == "Record Saved Successfully")
                {

                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    imgs.response = "success";
                    imgs.message = "success";
                    //imgs.imageUrl = "~/Images/Users/FranchisePaymentSlip" + URL;
                    return imgs;
                }
                else
                {
                    imgs.response = "error";
                    imgs.message = "error";
                    return imgs;
                }
            }
            catch (Exception ex)
            {
                Imagelist imgs = new Imagelist();
                imgs.response = "error";
                imgs.message = ex.Message;
                return imgs;
            }
        }
        public string AddImages(Imagelist imgList)
        {
            var response = db.AddImages(imgList);
            if (response.Flag == 1)
            {
                return response.Msg;
            }
            else
            {
                return "Record Not Saved";
            }
        }

        [HttpPost]
        public KYC UploadKyc()
        {
            KYC model = new KYC();
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(new HttpResponseMessage((HttpStatusCode.UnsupportedMediaType)));
            }
            var context = HttpContext.Current.Request;
            var ImagePath = "";

            if (context.Files.Count > 0)
            {
                string ImageType = context.Params["documentType"].ToString();
                if (ImageType == "AADHARFRONT")
                {
                    ImagePath = "~/Images/Users/Aadhar/";
                }
                if (ImageType == "AADHARBACK")
                {
                    ImagePath = "~/Images/Users/Aadhar/";
                }
                if (ImageType == "PAN")
                {
                    ImagePath = "~/Images/Users/Pan/";
                }
                if (ImageType == "CHEQUE")
                {
                    ImagePath = "~/Images/Users/Cheque/";
                }
                if (ImageType == "PROFILE")
                {
                    ImagePath = "~/Images/Users/Profile/";
                }
                if (ImageType == "Slip")
                {
                    ImagePath = "~/Images/Users/Slip/";
                }
                if (ImageType == "Support")
                {
                    ImagePath = "~/Images/Users/Support/";
                }
                if (ImageType == "FranchisePaySlip")
                {
                    ImagePath = "~/Images/Users/FranchisePaySlip/";
                }
                if (ImageType == "CropImage")
                {
                    ImagePath = "~/Images/CropImage/";
                }
                if (ImageType == "CropSubCategoryImage")
                {
                    ImagePath = "~/Images/CropSubCategoryImages/";
                }

                HttpPostedFile file = context.Files[0];
                var fileName = Path.GetFileName(file.FileName);
                string filePath = string.Empty;
                var extension = Path.GetExtension(file.FileName);
                if (extension.ToUpper() == ".PDF" || extension.ToUpper() == ".JPG" || extension.ToUpper() == ".JPEG" || extension.ToUpper() == ".PNG")
                {
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(ImagePath)))
                    {
                        Directory.CreateDirectory((HttpContext.Current.Server.MapPath(ImagePath)));
                    }

                    var thisFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + fileName;
                    filePath = System.IO.Path.Combine(ImagePath, thisFileName);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    var path = HttpContext.Current.Server.MapPath(filePath);
                    file.SaveAs(path);
                    model.response = "success";
                    model.filepath = filePath;
                    model.msg = "file upload successfully!";
                }
                else
                {

                }
            }
            else
            {
                model.response = "error";
                model.msg = "Please select file to upload!";
            }
            return model;
        }
        [HttpPost]
        public KYC UploadFranchiseeKyc()
        {
            KYC model = new KYC();
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(new HttpResponseMessage((HttpStatusCode.UnsupportedMediaType)));
            }
            var context = HttpContext.Current.Request;
            var ImagePath = "";

            if (context.Files.Count > 0)
            {
                string ImageType = context.Params["documentType"].ToString();
                if (ImageType == "AADHARFRONT")
                {
                    ImagePath = "~/Images/FranchiseeKYCDocuments/Aadhar/";
                }
                if (ImageType == "AADHARBACK")
                {
                    ImagePath = "~/Images/FranchiseeKYCDocuments/Aadhar/";
                }
                if (ImageType == "PAN")
                {
                    ImagePath = "~/Images/FranchiseeKYCDocuments/Pan/";
                }
                if (ImageType == "BANK")
                {
                    ImagePath = "~/Images/FranchiseeKYCDocuments/Bank/";
                }

                HttpPostedFile file = context.Files[0];
                var fileName = Path.GetFileName(file.FileName);
                string filePath = string.Empty;
                var extension = Path.GetExtension(file.FileName);
                if (extension.ToUpper() == ".PDF" || extension.ToUpper() == ".JPG" || extension.ToUpper() == ".JPEG" || extension.ToUpper() == ".PNG")
                {
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(ImagePath)))
                    {
                        Directory.CreateDirectory((HttpContext.Current.Server.MapPath(ImagePath)));
                    }

                    var thisFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + fileName;
                    filePath = System.IO.Path.Combine(ImagePath, thisFileName);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    var path = HttpContext.Current.Server.MapPath(filePath);
                    file.SaveAs(path);
                    model.response = "success";
                    model.filepath = filePath;
                    model.msg = "file upload successfully!";
                }
                else
                {

                }
            }
            else
            {
                model.response = "error";
                model.msg = "Please select file to upload!";
            }
            return model;
        }

        [HttpPost]
        public FileUploadResponse UploadVideoFile()
        {
            FileUploadResponse model = new FileUploadResponse();
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(new HttpResponseMessage((HttpStatusCode.UnsupportedMediaType)));
            }
            var context = HttpContext.Current.Request;
            var FilePath = @"/ReadWriteData/TestimonialsVideo/";
            if (context.Files.Count > 0)
            {
                HttpPostedFile file = context.Files[0];
                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                if (extension.ToUpper() == ".MP4" || extension.ToUpper() == ".AVI" || extension.ToUpper() == ".MKV" || extension.ToUpper() == ".WEBM " || extension.ToUpper() == ".FLV")
                {
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(FilePath)))
                    {
                        Directory.CreateDirectory((HttpContext.Current.Server.MapPath(FilePath)));
                    }
                    var thisFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + fileName;
                    var filename = System.IO.Path.Combine(FilePath, thisFileName);
                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                    }
                    var path = HttpContext.Current.Server.MapPath(FilePath + fileName);
                    file.SaveAs(path);
                    model.response = "success";
                    model.filepath = FilePath + fileName;
                    model.msg = "file uploaded successfully!";
                }
                else
                {
                    model.response = "error";
                    model.filepath = string.Empty;
                    model.msg = "Invalid video file extensions please upload files of allowed type (ex. MP4,AVI,MKV,WEBM,FLV)!";
                }
            }
            else
            {
                model.response = "error";
                model.filepath = string.Empty;
                model.msg = "Please select file to upload!";
            }
            return model;
        }


    }
}
