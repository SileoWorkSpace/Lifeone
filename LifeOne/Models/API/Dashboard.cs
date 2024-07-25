using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class Dashboard
    {
    }

    public class DashboardModel
    {
        public string imageurl { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }
    public class jsonDashboardResponse
    {
        public string response { get; set; }
        public List<DashboardModel> shopping { get; set; }
        public List<DashboardModel> travel { get; set; }
        public List<DashboardModel> utilities { get; set; }
    }
    public class DashboardCategoryResponse
    {
        public string response { get; set; }
        public List<DashboardCategoryMaster> category { get; set; }
    }
    public class DashboardCategoryMaster
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string imgurl { get; set; }        
        public List<DashboardCategory> CategoryList { get; set; }
    }
    public class DashboardCategory
    {
        public string categoryId { get; set; }
        public string name { get; set; }
        public string imageurl { get; set; }
        public string link { get; set; }
    }
}