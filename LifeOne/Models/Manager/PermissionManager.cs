using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.Manager
{
    public static class PermissionManager
    {
        public static bool IsTabPermit(string FormType)
        {
            using (var sm = new SessionManager())
            {
                if (SessionManager.Fk_UserTypeId == 1 || SessionManager.Fk_UserTypeId == 3)
                {
                    return true;
                }
                else
                {
                    if (SessionManager.UserPermissionDt != null && SessionManager.UserPermissionDt.Rows.Count > 0)
                    {
                        return SessionManager.UserPermissionDt.Select("FormType='" + FormType + "' and FormView=1").Length > 0;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static bool IsViewPermit(string FormName)
        {
            using (var sm = new SessionManager())
            {
                if (SessionManager.Fk_UserTypeId == 1 || SessionManager.Fk_UserTypeId == 3)
                {
                    return true;
                }
                else
                {
                    if (SessionManager.UserPermissionDt != null && SessionManager.UserPermissionDt.Rows.Count > 0)
                    {
                        return SessionManager.UserPermissionDt.Select("FormName='" + FormName + "' and FormView=1").Length > 0;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static bool IsActionPermit(string FormName, ViewOptions vo)
        {
            using (var sm = new SessionManager())
            {
                //if (SessionManager.UserPermissionDt != null && SessionManager.UserPermissionDt.Rows.Count > 0)
                //{
                    if (SessionManager.Fk_UserTypeId == 1 || SessionManager.Fk_UserTypeId == 3)
                    {
                        return true;
                    }
                    else
                    {
                        if (ViewOptions.FormView == vo)
                        {
                            return SessionManager.UserPermissionDt.Select("FormName='" + FormName + "' and FormView=1").Length > 0;
                        }
                        else if (ViewOptions.FormSave == vo)
                        {
                            return SessionManager.UserPermissionDt.Select("FormName='" + FormName + "' and FormSave=1").Length > 0;
                        }
                        else if (ViewOptions.FormUpdate == vo)
                        {
                            return SessionManager.UserPermissionDt.Select("FormName='" + FormName + "' and FormUpdate=1").Length > 0;
                        }
                        else
                        {
                            return SessionManager.UserPermissionDt.Select("FormName='" + FormName + "' and FormDelete=1").Length > 0;
                        }
                    }
                //}
                //else
                //{
                //    return false;
                //}
            }
        }

    }
    public enum ViewOptions { FormView, FormSave, FormUpdate, FormDelete };
}