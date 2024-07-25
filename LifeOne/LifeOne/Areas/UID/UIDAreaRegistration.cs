using System.Web.Mvc;

namespace LifeOne.Areas.UID
{
    public class UIDAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "UID";
            }
        }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "UID_default",
                "UID/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
             "UIDDashboard_default",
             "UID-Dashboard/{action}/{id}",
             new
             {
                 area = "UID",
                 controller = "UIDDashboard",
                 action = "Dashboard",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.UID.Controllers" });


            context.MapRoute(
                 "UID_Welcomeletter",
                 "UID-Welcome-letter/{action}/{id}",
                 new
                 {
                     area = "UID",
                     controller = "UIDViewProfile",
                     action = "Welcomeletter",
                     id = UrlParameter.Optional
                 },
                 new[] { "LifeOne.Areas.UID.Controllers" });

            context.MapRoute(
                 "UID_ViewProfile",
                 "UID-View-Profile/{action}/{id}",
                 new
                 {
                     area = "UID",
                     controller = "UIDViewProfile",
                     action = "ViewProfile",
                     id = UrlParameter.Optional
                 },
                 new[] { "LifeOne.Areas.UID.Controllers" });

            context.MapRoute(
                 "UID_ChangePassword",
                 "UID-Change-Password/{action}/{id}",
                 new
                 {
                     area = "UID",
                     controller = "UIDViewProfile",
                     action = "ChangePassword",
                     id = UrlParameter.Optional
                 },
                 new[] { "LifeOne.Areas.UID.Controllers" });

            context.MapRoute(
                "UID_Associate",
                "UID-RequestDetail/{action}/{id}",
                new
                {
                    area = "UID",
                    controller = "UIDViewProfile",
                    action = "RequestDetailAssociate",
                    id = UrlParameter.Optional
                },
                new[] { "LifeOne.Areas.UID.Controllers" });
            context.MapRoute(
               "UIDKYC_",
               "UID-KYC/{action}/{id}",
               new
               {
                   area = "UID",
                   controller = "UIDViewProfile",
                   action = "KYC",
                   id = UrlParameter.Optional
               },
               new[] { "LifeOne.Areas.UID.Controllers" });

            context.MapRoute(
               "UIDMy_Direct",
               "UID-My-Direct/{action}/{id}",
               new
               {
                   area = "UID",
                   controller = "UIDTeam",
                   action = "MyDirect",
                   id = UrlParameter.Optional
               },
               new[] { "LifeOne.Areas.UID.Controllers" });

            context.MapRoute(
              "UIDMyDownline",
              "UID-My-Downline/{action}/{id}",
              new
              {
                  area = "UID",
                  controller = "UIDTeam",
                  action = "MyDownline",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.UID.Controllers" });


            context.MapRoute(
             "EWalletRequestDetailsList",
             "UID-EWallet-RequestDetailsList/{action}/{id}",
             new
             {
                 area = "UID",
                 controller = "UIDEWalletRequest",
                 action = "EWalletRequestDetailsList",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.UID.Controllers" });




            context.MapRoute(
              "UIDPayout_Leadger",
              "UID-Payout-Leadger/{action}/{id}",
              new
              {
                  area = "UID",
                  controller = "UIDReport",
                  action = "PayoutLeadger",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.UID.Controllers" });

            context.MapRoute(
              "UIDBusiness_Summary",
              "UID-Business-Summary/{action}/{id}",
              new
              {
                  area = "UID",
                  controller = "UIDReport",
                  action = "BusinessSummary",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.UID.Controllers" });

            context.MapRoute(
             "UIDStatistics_",
             "UID-Statistics/{action}/{id}",
             new
             {
                 area = "UID",
                 controller = "AssociateDashboard",
                 action = "Statistics",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.UID.Controllers" });

            context.MapRoute(
             "UIDAddRequestForStock",
             "UID-Order-Stock/{action}/{id}",
             new
             {
                 area = "UID",
                 controller = "UIDStockRequest",
                 action = "AddRequestForStock",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.UID.Controllers" });

            context.MapRoute(
            "UIDTrackPurchaseStock",
            "UID-Track-Purchase-Stock/{action}/{id}",
            new
            {
                area = "UID",
                controller = "UIDStockRequest",
                action = "TrackPurchaseStock",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.UID.Controllers" });

            context.MapRoute(
           "UIDSupport",
           "UID-Associate-Support/{action}/{id}",
           new
           {
               area = "UID",
               controller = "UIDReport",
               action = "AssociateSupport",
               id = UrlParameter.Optional
           },
           new[] { "LifeOne.Areas.UID.Controllers" });



        }
    }
}