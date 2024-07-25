using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class TourDetailViewModel : MPaging
    {
        public int Tour_PKid { get; set; }
        public int TotalPassenger { get; set; }
        public int TotalDays { get; set; }
        public int TotalNights { get; set; }
        public int Flag { get; set; }
        public string Fk_MemId { get; set; }
        public string TourID { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string TourType { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Your_Budget { get; set; }
        public string Approval_Remark { get; set; }
        public string From_Dt { get; set; }
        public string To_Dt { get; set; }
        public string PassengerName { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string OrderNo { get; set; }
        public string message { get; set; }
        public string PassengerType { get; set; }
        public string Remark { get; set; }
        public string Response { get; set; }
        public Nullable<bool> IsApprove { get; set; }
        public decimal BookingAmount { get; set; }
        //public DateTime FromDt { get; set; }
        //public DateTime ToDt { get; set; }

        public string TourStatus { get; set; }
        public string CompleteUpComing { get; set; }
        public string HotelCategory { get; set; }
        public string PaymentStatus { get; set; }
        public string ReturnAirTour { get; set; }
        public string IsPaid { get; set; }


        public List<TourDetailViewModel> TourDetailList { get; set; }
    }

    public class AddTourDetailViewModel
    {
        public int FK_MemId { get; set; }
        public int TotalPassenger { get; set; }
        public int TotalDays { get; set; }
        public int TotalNights { get; set; }
        public int Flag { get; set; }
        public string Source_Place { get; set; }
        public string Destination { get; set; }
        public string Hotel_Category { get; set; }
        public string TourType { get; set; }
        public string Return_Air_tour { get; set; }
        public string Adults { get; set; }
        public string Infants { get; set; }
        public string Children { get; set; }
        public string Your_Budget { get; set; }
        public string Remark { get; set; }
        public string From_Dt { get; set; }
        public string To_Dt { get; set; }
        public List<PassengerDetailModel> PassengerList { get; set; }
    }

    public class PassengerDetailModel
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string PassengerType { get; set; }
    }

    public class TourResponse
    {
        public List<APITour_PassengerDetailViewModel> TourList { get; set; }
        public string Response { get; set; }
        public string Remark { get; set; }
        public string Flag { get; set; }


    }
    public class Tour_PassengerDetailViewModel
    {
        public int Tour_PKid { get; set; }
        public int TotalPassenger { get; set; }
        public int TotalDays { get; set; }
        public int TotalNights { get; set; }
        public int Flag { get; set; }
        public string TourID { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string TourType { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Your_Budget { get; set; }
        public string PassengerName { get; set; }
        public string Remark { get; set; }
        public decimal BookingAmount { get; set; }
        public string TourStatus { get; set; }
        public string IsPaid { get; set; }
        public string CompleteUpComing { get; set; }
        public string From_Dt { get; set; }
        public string To_Dt { get; set; }
        public DateTime FromDt { get; set; }
        public DateTime ToDt { get; set; }
        public List<PassengerDetailModel> PassengerList { get; set; }
        public string HotelCategory { get; set; }
        public string ReturnAirTour { get; set; }               
    }
    public class APITour_PassengerDetailViewModel
    {
        public int Tour_PKid { get; set; }
        public int TotalPassenger { get; set; }
        public int TotalDays { get; set; }
        public int TotalNights { get; set; }
        public int Flag { get; set; }
        public string TourID { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string TourType { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Your_Budget { get; set; }
        public string PassengerName { get; set; }
        public string Remark { get; set; }
        public decimal BookingAmount { get; set; }
        public string TourStatus { get; set; }
        public string CompleteUpComing { get; set; }
        public string PaymentStatus { get; set; }
        public string From_Dt { get; set; }
        public string To_Dt { get; set; }
        public List<PassengerDetailModel> PassengerList { get; set; }
        public string HotelCategory { get; set; }
        public string ReturnAirTour { get; set; }
    }
    public class TourOnlinePayment
    {
        public int TourId { get; set; }
        public int Fk_MemId { get; set; }
        public decimal PaidAmount { get; set; }
        public string TransactionNo { get; set; }
        public string PaymentId { get; set; }
        public string Status { get; set; }
        public string PaymentDate { get; set; }
        public string Response { get; set; }
        public string Remark { get; set; }



    }
}