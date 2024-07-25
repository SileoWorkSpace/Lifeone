using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class DynamicUtilityModel
    {
        public int Id { get; set; }
        public string UtilityName { get; set; }
        public string Status { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateStr { get; set; }
        public string CreatedBy { get; set; }
        public string CampaignStartDate { get; set; }
        public string _CampaignStartDate { get; set; }
        public bool isDashboardIcon { get; set; }
    }
    public class DynamicUtilityResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string response { get; set; }
        public List<DynamicUtilityModel> Data { get; set; }
    }
}