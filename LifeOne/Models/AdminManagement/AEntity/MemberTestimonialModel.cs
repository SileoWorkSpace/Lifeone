using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MemberTestimonialModel
    {
        public int? Page { get; set; }
        public Pager Pager { get; set; }
        public int Id { get; set; }
        public int TotalRecords { get; set; }
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string CropCategory { get; set; }
        public string Crop { get; set; }
        public string Remark { get; set; }
        public bool IsApprove { get; set; }
        public bool IsDecline { get; set; }
        public List<MemberTestimonialModel> MemberTestimonialList { get; set; }
    }
}