using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace LifeOne.Models.Common
{
    public class SendNotification
    {

        public static string SendNotificationForMobile(string DeviceId,string Title, string message)
        {
            string sResponseFromServer = string.Empty;
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAARnqcB-w:APA91bEIWsnGUde4qyZ8B_Ik3eAehlZyEggacU8NlsohN_B6MshVOhoItTQ3Z5WAIlgIFOm3yeYIq0hD01mx470gf8H8Pk3ls3-iuW-75qhODgQGSqoqrDeX5EdXOzqH6sPIgWWJa9hT"));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", "302704756716"));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = DeviceId,
                priority = "high",
                content_available = true,
                notification = new
                {
                    title= Title,
                    message = message,
                    type = "",
                    image = "",
                    body=message
                },
                data = new
                {
                    title = Title,
                    message = message,
                    type = "",
                    image="",
                    body = message
                }
            };

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }
            return sResponseFromServer;
        }

    }
}