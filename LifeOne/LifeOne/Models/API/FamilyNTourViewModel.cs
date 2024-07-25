using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class FamilyNTourViewModel
    {
        public int FK_MemId { get; set; }
        public string Location { get; set; }
        public string From_Dt { get; set; }
        public string To_Dt { get; set; }
        public string Hotel_Category { get; set; }
        public string TravelType { get; set; }
        public string IsReturn { get; set; }
        public string Your_Budget { get; set; }
        public int TotalPerson { get; set; }
        public int Adults { get; set; }
        public int Child_below_12Years { get; set; }        
        public int Child_above_12Years { get; set; }        
        public int TotalDays { get; set; }        
        public int TotalNights { get; set; }        
    }

    public class TourDestination
    {

        public int DestinationId { get; set; }
        public string Destination { get; set; }


    }
    public class TourDestinationResponse
    {
        public string Flag { get; set; }
        public string Remark { get; set; }
        public string Response { get; set; }
        public List<TourDestination> TourDestinationList { get; set; }
    }
}