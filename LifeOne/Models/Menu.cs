
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using LifeOne.Models.API.DAL;
using Newtonsoft.Json;

namespace LifeOne.Models
{
    public class Menu
    {

        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string SubMenuName { get; set; }
        public int  PermissionId { get; set; }
        public string Fk_EmpId { get; set; }
        public string Fk_MenuId { get; set; }
        public int SubMenuId { get; set; }
        public string Url { get; set; }
        public string Class { get; set; }
        public string SubMenuIcon { get; set; }
        public string Icon { get; set; }
        public string IsDropdown { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string Fk_MemId { get; set; }
        //public string? MobileNo { get; set; }
        public List<Menu> menuList { get; set; }
        public List<Menu> subMenuList { get; set; }

        public DataSet GetMenuDetails()
        {
            try
            {
                SqlParameter[] para =
                {
                  new SqlParameter("@Fk_MemId", Fk_MemId)
                 

                };
                DataSet ds = DBHelper.ExecuteQuery("BindMenuMaster", para);
                return ds;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }


    

}
