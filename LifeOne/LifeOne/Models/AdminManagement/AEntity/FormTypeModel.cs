using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class FormTypeModel
    {
        public string PK_FormTypeId { get; set; } 
        [Required]
        public string FormType { get; set; }
        public string CreatedBy { get; set; }  
        public string UpdatedBy { get; set; } 
        public string DeletedBy { get; set; }    
        public List<FormTypeModel> FormTypeList { get; set; }
    }
}