using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
//using iTextSharp.text;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.exceptions;
using iTextSharp.tool.xml.css.apply;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.end;

namespace LifeOne.Models.TravelModel
{

    public class Authentication
    {

        public string LoginId { get; set; }

        public string Password { get; set; }



    }

    public class Common
    {
        public class AgentCreditBalance
        {
            public Authentication Authentication { get; set; }
        }
        public class MobileAPIDetails
        {
            // public static string APIURL = "http://demo11.mlmsoftindia.com/Home";
            public static string APIURL = "http://115.248.39.80/HermesMobAPI/HermesMobile.svc/jsonservice";
            //public static string LoginId = "RIGHT1234";
            //public static string Password = "apiRIGHT1234";
            public static string CompanyId = "10003";
            public static string TransType = "Recharge";
            ////public static string APIURL = "http://api.hermes-it.in/mobile/hermesmobile.svc/JSONService";
            public static string LoginId = "RIGHT1234";
            public static string Password = "apiRIGHT1234";
        }
        public class FlightAPIDetails
        {
            // public static string APIURL = "http://demo11.mlmsoftindia.com/Home";
            public static string APIURL = "http://api.hermes-it.in/DOM_Airlines/DomesticAir.svc/JsonService";
            //public static string APIURL = "http://api.hermes-it.in/DOM_Airlines/DomesticAir.svc/JsonService";
            //public static string GStno = "22AAAAA0000A1Z5";
            //public static string EmailId = "test@test.com";
            //public static string CompanyName = "Hermes";
            //public static string ContactNumber = "Cashway@12367890";
            //public static string Address = "Guindy";
            //public static string FirstName = "Vel";
            //public static string LastName = "Murugan";
            //public static string CityJsonUrl = "http://demo11.mlmsoftindia.com/airlinecode.json";
            //public static string RootUrl = "http://travelportal.udlifecare.com";

        }
        public class BusAPIDetails
        {
            public static string APIURL = "http://api.hermes-it.in/BUS_V1/Bus.svc/JSONService";
            public Authentication Authentication { get; set; }
            public static string CompanyId = "10002";
            public static string TransType = "Bus Booking";


        }
        public class GetCompanyBalance
        {
            //public static string APIURL = "http://115.248.39.80/hermesBusV1/Bus.svc/JSONService";
            public static string APIURL = "http://demo11.mlmsoftindia.com/api/WebAPI";
            public static string LoginId = "RIGHT1234";
            public static string Password = "apiRIGHT1234";

            //public static string APIURL = "http://api.hermes-it.in/mobile/hermesmobile.svc/JSONService";
            //public static string LoginId = "RIGHT1234";
            //public static string Password = "apiRIGHT1234";
        }
        public class ProcedureName
        {
            public static string MemberLogin = "MemberLogin";
            public static string BookingDetails = "BookingDetails";
            public static string TicketDetails = "GetTicketDetails";
            public static string CancelTicketInsert = "CancelTicketInsert";
            public static string SeatConfirmationInsert = "SeatConfirmationInsert";
            public static string FlightRequestResponseInsert = "FlightRequestResponseInsert";
            public static string MobileResponse = "MobileRechargeInsert";
            public static string FailureResponse = "FailureResponse";
            public static string BusBooking = "BusPassengerDetail";
        }


        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string DateString = "";
            // DateTime Dt;

            string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);

            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];

                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Month + "/" + Day + "/" + Year;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }

            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }

        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        [ScriptMethod()]
        [WebMethod]
        public static string SendOTPMail(string TransportPNR, string BookedDate, string TransportName, string Address1, string Address2, string Address3, string CityNamePinCode, string ContactNumber, string EmailId, string Website, string BusName, string TravelDate, string DepartureTime, string Origin, string Destination, string TicketNumber, string SeatNo, string SeatType, string PassengerName, string Gender, string Age, string Fare, string Place, string Time, string Address, string Telephone, string ReportingTime, string StatusFor, string CancellationPolicy, string Boarding, string TotalAmount)
        {
            string body = PopulateRegisTemplate(TransportPNR, BookedDate, TransportName, Address1, Address2, Address3, CityNamePinCode, ContactNumber, EmailId, Website, BusName, TravelDate, DepartureTime, Origin, Destination, TicketNumber, SeatNo, SeatType, PassengerName, Gender, Age, Fare, Place, Time, Address, Telephone, ReportingTime, StatusFor, CancellationPolicy, Boarding, TotalAmount);
            string result = SendMailUsingGmail1(EmailId, "LifeOne", body);

            return result;
        }

        public static string SendMailUsingGmail1(string EmailId, string type, string message)
        {
            try
            {

                string to = EmailId.ToString();
                //string from = "donotreply@kreazlife.com";
                //string authPass = "Mona@1981";
                string from = "admin@aarcledia.com";
                string authPass = "aarcpay123@";
                string subject = type;
                string body = message;
                string FileName = EmailId + '_' + DateTime.Now.ToString("ddMMyyhhmmss");

                //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                using (MailMessage mm = new MailMessage(from, to))
                {
                    mm.Subject = subject;
                    mm.Body = body;

                    //    using (MemoryStream memoryStream = new MemoryStream())
                    //    {
                    //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    //        pdfDoc.Open();
                    //        htmlparser.Parse(sr);
                    //        pdfDoc.Close();
                    //        byte[] bytes = memoryStream.ToArray();
                    //        memoryStream.Close();
                    //        mm.Subject = subject;
                    //        mm.Body = body;
                    //        mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "Invoice.pdf"));                      

                    //    }

                    Document pdfDoc = new Document(PageSize.A4);
                    //--------------------------For Image------------------------
                    var tagProcessors = (DefaultTagProcessorFactory)Tags.GetHtmlTagProcessorFactory();
                    tagProcessors.RemoveProcessor(HTML.Tag.IMG); // remove the default processor
                    tagProcessors.AddProcessor(HTML.Tag.IMG, new CustomImageTagProcessor()); // use our new processor
                                                                                             //------------------------------end----------------------------        
                    var hpc = new HtmlPipelineContext(new CssAppliersImpl(new XMLWorkerFontProvider()));
                    hpc.SetAcceptUnknown(true).AutoBookmark(true).SetTagFactory(tagProcessors); // inject the tagProcessors
                    ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(true);
                    StringReader sr = new StringReader(message.ToString());
                    using (MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        pdfDoc.Open();
                        IPipeline pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(hpc, new PdfWriterPipeline(pdfDoc, writer)));
                        XMLWorker worker = new XMLWorker(pipeline, true);
                        XMLParser p = new XMLParser(true, worker, Encoding.UTF8);
                        p.Parse(sr);
                        pdfDoc.Close();
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();


                        //if (fuAttachment.HasFile)
                        //{
                        //    foreach (HttpPostedFile file in fuAttachment.PostedFiles)
                        //    {
                        //        string fileName = Path.GetFileName(file.FileName);
                        //        file.SaveAs(Server.MapPath("~/Content/Uploads/") + fileName);
                        //        mm.Attachments.Add(new Attachment(file.InputStream, ""));
                        //    }
                        //}

                        if (message != null)
                        {
                            string path = "~/KYCDocument/BusTicket/";
                            bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(path));
                            if (!folderExists)
                            {
                                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                            }
                            string fname = FileName + ".pdf";
                            string filename = Path.Combine(path, fname);
                            File.WriteAllBytes(HttpContext.Current.Server.MapPath(filename), bytes);
                            mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "BusTicket.pdf"));
                        }
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(from, authPass);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        //smtp.Port = 25;
                        smtp.Send(mm);
                        return "Success";
                        // ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Email sent.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public static string PopulateRegisTemplate(string TransportPNR, string BookedDate, string TransportName, string Address1, string Address2, string Address3, string CityNamePinCode, string ContactNumber, string EmailId, string Website, string BusName, string TravelDate, string DepartureTime, string Origin, string Destination, string TicketNumber, string SeatNo, string SeatType, string PassengerName, string Gender, string Age, string Fare, string Place, string Time, string Address, string Telephone, string ReportingTime, string StatusFor, string CancellationPolicy, string Boarding, string TotalAmount)
        {
            string body = string.Empty;
            string template = HttpContext.Current.Server.MapPath("~/BusTicket.html");
            using (StreamReader reader = new StreamReader(template.ToString()))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{TransportPNR}", TransportPNR);
            body = body.Replace("{BookedDate}", Convert.ToDateTime(BookedDate).ToString("MMM-dd-yyyy :: hh:mm tt"));
            body = body.Replace("{TransportName}", TransportName);
            body = body.Replace("{Address1}", Address1);
            body = body.Replace("{Address2}", Address2);
            body = body.Replace("{Address3}", Address3);
            body = body.Replace("{CityNamePinCode}", CityNamePinCode);
            body = body.Replace("{ContactNumber}", ContactNumber);
            body = body.Replace("{EmailId}", EmailId);
            body = body.Replace("{Website}", Website);
            body = body.Replace("{BusName}", BusName);
            body = body.Replace("{TravelDate}", TravelDate);
            body = body.Replace("{DepartureTime}", DepartureTime);
            body = body.Replace("{Origin}", Origin);
            body = body.Replace("{Destination}", Destination);
            body = body.Replace("{TicketNumber}", TicketNumber);
            body = body.Replace("{SeatNo}", SeatNo);
            body = body.Replace("{SeatType}", SeatType);
            body = body.Replace("{PassengerName}", PassengerName);
            body = body.Replace("{Gender}", Gender);
            body = body.Replace("{Age}", Age);
            body = body.Replace("{Fare}", Fare);
            body = body.Replace("{Place}", Place);
            body = body.Replace("{Time}", Time);
            body = body.Replace("{Address}", Address);
            body = body.Replace("{Telephone}", Telephone);
            body = body.Replace("{ReportingTime}", ReportingTime);
            body = body.Replace("{StatusFor}", StatusFor);
            body = body.Replace("{CancellationPolicy}", CancellationPolicy);
            body = body.Replace("{Boarding}", Boarding);
            body = body.Replace("{TotalAmount}", TotalAmount);
            return body;
        }

    }

    public class CustomImageTagProcessor : iTextSharp.tool.xml.html.Image
    {
        public override IList<IElement> End(IWorkerContext ctx, Tag tag, IList<IElement> currentContent)
        {
            IDictionary<string, string> attributes = tag.Attributes;
            string src;
            if (!attributes.TryGetValue(HTML.Attribute.SRC, out src))
                return new List<IElement>(1);

            if (string.IsNullOrEmpty(src))
                return new List<IElement>(1);

            if (src.StartsWith("data:image/", StringComparison.InvariantCultureIgnoreCase))
            {
                // data:[<MIME-type>][;charset=<encoding>][;base64],<data>
                var base64Data = src.Substring(src.IndexOf(",") + 1);
                var imagedata = Convert.FromBase64String(base64Data);
                var image = iTextSharp.text.Image.GetInstance(imagedata);

                var list = new List<IElement>();
                var htmlPipelineContext = GetHtmlPipelineContext(ctx);
                list.Add(GetCssAppliers().Apply(new Chunk((iTextSharp.text.Image)GetCssAppliers().Apply(image, tag, htmlPipelineContext), 0, 0, true), tag, htmlPipelineContext));
                return list;
            }
            else
            {
                return base.End(ctx, tag, currentContent);
            }
        }
    }

}