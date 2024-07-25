using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace LifeOne.Models.AdminManagement.AEntity
{
    public class FormMasterModel
    {
        public string PK_FormId { get; set; }
        public string FK_FormTypeId { get; set; }
        public string FormTypeName { get; set; }
        [Required]
        [MaxLength(50)]
        public string FormName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public List<FormMasterModel> FormMasterList { get; set; }
    }
}