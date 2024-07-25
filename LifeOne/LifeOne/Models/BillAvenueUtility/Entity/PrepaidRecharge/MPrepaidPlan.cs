using System;
using System.Xml.Serialization;
using System.Collections.Generic;




namespace LifeOne.Models.BillAvenueUtility.Entity.PrepaidRecharge
{
    public class MPrepaidPlan
    {


    }

	[XmlRoot(ElementName = "rechargePlanRequest")]
	public class RechargePlanRequest:MCommon
	{
		[XmlElement(ElementName = "billerId")]
		public string BillerId { get; set; }
		[XmlElement(ElementName = "circle")]
		public string Circle { get; set; }
	}

	[XmlRoot(ElementName = "rechargePlanResponse")]
	public class RechargePlanResponse: MError
	{
		[XmlElement(ElementName = "responseCode")]
		public string responseCode { get; set; }
		[XmlElement(ElementName = "rechargePlan")]
		public RechargePlan RechargePlan { get; set; }
	}

	[XmlRoot(ElementName = "rechargePlansDetails")]
	public class RechargePlansDetails
	{
		[XmlElement(ElementName = "amount")]
		public string Amount { get; set; }
		[XmlElement(ElementName = "description")]
		public string Description { get; set; }
		[XmlElement(ElementName = "locationName")]
		public string LocationName { get; set; }
		[XmlElement(ElementName = "planName")]
		public string PlanName { get; set; }
		[XmlElement(ElementName = "serviceProviderName")]
		public string ServiceProviderName { get; set; }
		[XmlElement(ElementName = "talktime")]
		public string Talktime { get; set; }
		[XmlElement(ElementName = "validity")]
		public string Validity { get; set; }
	}

	[XmlRoot(ElementName = "rechargePlan")]
	public class RechargePlan
	{
		[XmlElement(ElementName = "rechargePlansDetails")]
		public List<RechargePlansDetails> RechargePlansDetails { get; set; }
	}

	
}