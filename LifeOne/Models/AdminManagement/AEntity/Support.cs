using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class Support
    {
        public int PK_SupportId { get; set; }
        public int UserType { get; set; }
        public long EmpFk_MemID { get; set; }
        public string SupportName { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public int ProcId { get; set; }
        public string TicketNumber { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public string Mobile { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
        public int ReplyBy { get; set; }
        public string SendDate { get; set; }
        public string RepliedOn { get; set; }
        public string Subject { get; set; }
        public string DeviceId { get; set; }
        public int id { get; set; }
        public string ReplyMessage { get; set; }
        public string response { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalRecord { get; set; }
        public bool IsClosed { get; set; }
        public Pager Pager { get; set; }
        public long TotalRecords { get; set; }
        public List<Support> SupportList { get; set; }
    }
    public class SupportResponse
    {
        public int Flag { get; set; }
        public string response { get; set; }
        public string Msg { get; set; }
    }
    public class SupportRequest
    {
        public int Fk_SupportId { get; set; }
        public long Fk_MemId { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string DocumentPath { get; set; }
    }


    public class SupportHistoryModel
    {
        public int SupportId { get; set; }
        public int HistoryID { get; set; }
        public int MsgBy { get; set; }
        public int Flag { get; set; }
        public long Fk_MemId { get; set; }
        public string Msg { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string RevertBy { get; set; }
        public string Remark { get; set; }
        public string LoginId { get; set; }
        public string Fk_MemLoginId { get; set; }
        public string MemberProfile { get; set; }
        public string SupportProfile { get; set; }
        public string SupportName { get; set; }
        public string CreateDate { get; set; }
        public string DeviceId { get; set; }
        public bool IsClosed { get; set; }
    }

    public class Support_EmployeeListModel
    {
        public long FK_MemId { get; set; }
        public string EmpName { get; set; }
        public List<Support_EmployeeListModel> EmployeeList { get; set; }
        public List<Support> SupportList { get; set; }
    }
    public class EmployeeModel
    {
        public long EmpId { get; set; }
        public string EmpName { get; set; }
    }
}