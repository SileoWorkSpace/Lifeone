using LifeOne.Models.AdminManagement.AEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LifeOne.Models.Manager
{
    
    public class SessionManager : IDisposable
    {
        public static int Fk_UserTypeId
        {
            get
            {
                if (HttpContext.Current.Session["Fk_UserTypeId"] == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(HttpContext.Current.Session["Fk_UserTypeId"]);
                }
            }
            set
            {
                HttpContext.Current.Session["Fk_UserTypeId"] = value;
            }
        }
        public static DataTable UserPermissionDt
        {
            get
            {
                if (HttpContext.Current.Session["Permissions"] == null)
                {
                    return null;
                }
                else
                {
                    return (DataTable)HttpContext.Current.Session["Permissions"];
                }
            }
            set
            {
                HttpContext.Current.Session["Permissions"] = value;
            }
        }
        public static List<UserPermissionModel> _UserPermissionList
        {
            get
            {
                if (HttpContext.Current.Session["MenuPermissions"] == null)
                {
                    return null;
                }
                else
                {
                    return (List<UserPermissionModel>)HttpContext.Current.Session["MenuPermissions"];
                }
            }
            set
            {
                HttpContext.Current.Session["MenuPermissions"] = value;
            }
        }
        public static string Usertype
        {
            get
            {
                if (HttpContext.Current.Session["Usertype"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Session["Usertype"].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session["Usertype"] = value;
            }
        }
        public static string Name
        {
            get
            {
                if (HttpContext.Current.Session["Name"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Session["Name"].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session["Name"] = value;
            }
        }



        public static string IsBusinessId
        {
            get
            {
                if (HttpContext.Current.Session["IsBusinessId"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Session["IsBusinessId"].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session["IsBusinessId"] = value;
            }
        }


        public static string ProfilePic
        {
            get
            {
                if (HttpContext.Current.Session["ProfilePic"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Session["ProfilePic"].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session["ProfilePic"] = value;
            }
        }
        
        public static string RecognitionPic
        {
            get
            {
                if (HttpContext.Current.Session["RecognitionPic"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Session["RecognitionPic"].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session["RecognitionPic"] = value;
            }
        }

        public static string PerformanceLevel
        {
            get
            {
                if (HttpContext.Current.Session["PerformanceLevel"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Session["PerformanceLevel"].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session["PerformanceLevel"] = value;
            }
        }

        public static string PerformanceLevelId
        {
            get
            {
                if (HttpContext.Current.Session["PerformanceLevelId"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Session["PerformanceLevelId"].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session["PerformanceLevelId"] = value;
            }
        }

        public static string Mobile
        {
            get
            {
                if (HttpContext.Current.Session["Mobile"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Session["Mobile"].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session["Mobile"] = value;
            }
        }
        public static long Fk_MemId
        {
            get
            {
                if (HttpContext.Current.Session["Fk_MemId"] == null)
                {
                    return 0;
                }
                else
                {
                    return long.Parse(HttpContext.Current.Session["Fk_MemId"].ToString());
                }
            }
            set
            {
                HttpContext.Current.Session["Fk_MemId"] = value;
            }
        }
        public static int? Quantity
        {
            get
            {
                if (HttpContext.Current.Session["Quantity"] == null)
                {
                    return 0;
                }
                else
                {
                    return int.Parse(HttpContext.Current.Session["Quantity"].ToString());
                }
            }
            set
            {
                HttpContext.Current.Session["Quantity"] = value;
            }
        }

        //public static long AssociateFk_MemId
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session["AssociateFk_MemId"] == null)
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            return long.Parse(HttpContext.Current.Session["AssociateFk_MemId"].ToString());
        //        }
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session["AssociateFk_MemId"] = value;
        //    }
        //}

        public static long AssociateFk_MemId
        {
            get            
            {
                if (HttpContext.Current.Session["AssociateFk_MemId"] == null)
                {
                    return 0;
                }
                long result;
                if (long.TryParse(HttpContext.Current.Session["AssociateFk_MemId"].ToString(), out result))
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["AssociateFk_MemId"] = value;
            }
        }

        public static long FranchiseFk_MemId
        {
            get
            {
                if (HttpContext.Current.Session["FranchiseFk_MemId"] == null)
                {
                    return 0;
                }
                else
                {
                    return long.Parse(HttpContext.Current.Session["FranchiseFk_MemId"].ToString());
                }
            }
            set
            {
                HttpContext.Current.Session["FranchiseFk_MemId"] = value;
            }
        }

        public static int RequestId
        {
            get
            {
                if (HttpContext.Current.Session["RequestId"] == null)
                {
                    return 0;
                }
                else
                {
                    return int.Parse(HttpContext.Current.Session["RequestId"].ToString());
                }
            }
            set
            {
                HttpContext.Current.Session["RequestId"] = value;
            }
        }
        public static string LoginId
        {
            get
            {
                if (HttpContext.Current.Session["LoginId"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Session["LoginId"].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session["LoginId"] = value;
            }
        }
        public static string TopupLevel
        {
            get
            {
                if (HttpContext.Current.Session["TopupLevel"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Session["TopupLevel"].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session["TopupLevel"] = value;
            }
        }

        public static long Fk_FranchiseId
        {
            get
            {
                if (HttpContext.Current.Session["Fk_FranchiseId"] == null)
                {
                    return 0;
                }
                else
                {
                    return long.Parse(HttpContext.Current.Session["Fk_FranchiseId"].ToString());
                }
            }
            set
            {
                HttpContext.Current.Session["Fk_FranchiseId"] = value;
            }
        }

        public static long SponsorFk_MemId
        {
            get
            {
                if (HttpContext.Current.Session["SponsorFk_MemId"] == null)
                {
                    return 0;
                }
                else
                {
                    return long.Parse(HttpContext.Current.Session["SponsorFk_MemId"].ToString());
                }
            }
            set
            {
                HttpContext.Current.Session["SponsorFk_MemId"] = value;
            }
        }
        public static string Recognition
        {
            get
            {
                if (HttpContext.Current.Session["Recognition"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Session["Recognition"].ToString();
                }
            }
            set
            {
                HttpContext.Current.Session["Recognition"] = value;
            }
        }

        public static int TotalItems
        {
            get
            {
                if (HttpContext.Current.Session["TotalItems"] == null)
                {
                    return 0;
                }
                else
                {
                    return int.Parse(HttpContext.Current.Session["TotalItems"].ToString());
                }
            }
            set
            {
                HttpContext.Current.Session["TotalItems"] = value;
            }
        }
        public static string TokenNo
        {
            get
            {
                if (HttpContext.Current.Session["TokenNo"] == null)
                {
                    return "";
                }
                else
                {
                    return (HttpContext.Current.Session["TokenNo"].ToString());
                }
            }
            set
            {
                HttpContext.Current.Session["TokenNo"] = value;
            }
        }


        public static int Size => 30;

        public void Dispose()
        {
           
        }
        public static int ProductCount
        {
            get
            {
                if (HttpContext.Current.Session["ProductCount"] == null)
                {
                    return 0;
                }
                else
                {
                    return int.Parse(HttpContext.Current.Session["ProductCount"].ToString());
                }
            }
            set
            {
                HttpContext.Current.Session["ProductCount"] = value;
            }
        }

    }
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this HttpSessionStateBase session, string key, object value)
        {
            session[key] = JsonConvert.SerializeObject(value);
        }

        public static T GetObjectFromJson<T>(this HttpSessionStateBase session, string key)
        {
            var value = session[key] as string;
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
