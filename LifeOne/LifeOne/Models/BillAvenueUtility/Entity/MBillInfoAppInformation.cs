using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.BillAvenueUtility.Entity
{
    public class MBillInfoAppInformation
    {

        public static string accessCode
        {
            get { return accessCode; }
            set { accessCode = "AVDF54HV34NP93OLCN"; }
        }
        public static string requestId
        {
            get { return requestId; }
            set { requestId = GenerateRequestId.RandomString(35); }
        }

        public string encRequest { get; set; } //35
        public static string ver
        {
            get { return ver; }
            set { ver = "1.0"; }
        }
        public static string instituteId
        {
            get { return instituteId; }
            set { instituteId = "KK11"; }
        }
    }

    //add new class

    public class GenerateRequestId
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            try
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetYDDDHHMM()
        {
            string Yr = DateTime.Now.Year.ToString().Substring(3);
            int Year = DateTime.Now.Year;

            DateTime startTime = DateTime.Now;






            var beginOfYear = new DateTime(Year, 01, 01, 0, 0, 0, DateTimeKind.Local);

            var Hrs = DateTime.Now.ToString("HH");
            var mm = DateTime.Now.ToString("mm");


            DateTime startDate = beginOfYear;
            DateTime endDate = DateTime.Now;
            int days = 0;
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {

                days++;

                startDate = startDate.AddDays(1);
            }
            var Yddhhmm = (Yr + days + Hrs + mm);
            return Yddhhmm;
        }
    }
}