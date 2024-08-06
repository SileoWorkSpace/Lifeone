using System.Web.Mvc;

namespace LifeOne.Areas.Associate
{
    public class AssociateAreaRegistration : AreaRegistration
    {


        public override string AreaName
        {
            get
            {
                return "Associate";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                "Associate_default",
                "Associate/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Associate_Dashboard_default",
            "Associate-Dashboard/{action}/{id}",
            new
            {
                area = "Associate",
                controller = "AssociateDashboard",
                action = "Dashboard",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Associate.Controllers" });


            context.MapRoute(
            "Associate-Package_default",
            "Update-Package/{action}/{id}",
            new
            {
                area = "Associate",
                controller = "BuyPackage",
                action = "purchaseNow",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Associate.Controllers" });



            context.MapRoute(
                 "Associate_Welcomeletter",
                 "Welcome-letter/{action}/{id}",
                 new
                 {
                     area = "Associate",
                     controller = "AssociateViewProfile",
                     action = "Welcomeletter",
                     id = UrlParameter.Optional
                 },
                 new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
                 "Associate_ViewProfile",
                 "View-Profile/{action}/{id}",
                 new
                 {
                     area = "Associate",
                     controller = "AssociateViewProfile",
                     action = "ViewProfile",
                     id = UrlParameter.Optional
                 },
                 new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
                 "Associate_ChangePassword",
                 "Change-Password/{action}/{id}",
                 new
                 {
                     area = "Associate",
                     controller = "AssociateViewProfile",
                     action = "ChangePassword",
                     id = UrlParameter.Optional
                 },
                 new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
                "RequestDetail_Associate",
                "RequestDetail-Associate/{action}/{id}",
                new
                {
                    area = "Associate",
                    controller = "AssociateViewProfile",
                    action = "RequestDetailAssociate",
                    id = UrlParameter.Optional
                },
                new[] { "LifeOne.Areas.Associate.Controllers" });
            context.MapRoute(
               "KYC_",
               "KYC/{action}/{id}",
               new
               {
                   area = "Associate",
                   controller = "AssociateViewProfile",
                   action = "KYC",
                   id = UrlParameter.Optional
               },
               new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
               "My_Direct",
               "My-Direct/{action}/{id}",
               new
               {
                   area = "Associate",
                   controller = "AssociateTeam",
                   action = "MyDirect",
                   id = UrlParameter.Optional
               },
               new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
              "MyDownline",
              "My-Downline/{action}/{id}",
              new
              {
                  area = "Associate",
                  controller = "AssociateTeam",
                  action = "MyDownline",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
              "IncomeDetails",
              "Associate-IncomeDetails/{action}/{id}",
              new
              {
                  area = "Associate",
                  controller = "AssociatePlanIncome",
                  action = "IncomeDetails",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Associate.Controllers" });

            

                 context.MapRoute(
              "Associate-PayoutIncomeDetails",
              "Associate-PayoutIncomeDetails/{action}/{id}",
              new
              {
                  area = "Associate",
                  controller = "AssociateReport",
                  action = "AssociatePayoutReport",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
              "StatementDetails",
              "StatementDetails/{action}/{id}",
              new
              {
                  area = "Associate",
                  controller = "AssociateReport",
                  action = "StatementDetails",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
              "Payout_Report",
              "Payout-Report/{action}/{id}",
              new
              {
                  area = "Associate",
                  controller = "AssociateReport",
                  action = "PayoutReport",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
              "Payout_Leadger",
              "Payout-Leadger/{action}/{id}",
              new
              {
                  area = "Associate",
                  controller = "AssociateReport",
                  action = "PayoutLeadger",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
              "Business_Summary",
              "Business-Summary/{action}/{id}",
              new
              {
                  area = "Associate",
                  controller = "AssociateReport",
                  action = "BusinessSummary",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
             "BusinessStatistics_Associate",
             "Business-Statistics/{action}/{id}",
             new
             {
                 area = "Associate",
                 controller = "AssociateDashboard",
                 action = "Statistics",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
             "AddRequestForStock",
             "Order-Product/{action}/{id}",
             new
             {
                 area = "Associate",
                 controller = "AssociateStockRequest",
                 action = "AddRequestForStock",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
            "TrackPurchaseStock",
            "Track-Purchase-Stock/{action}/{id}",
            new
            {
                area = "Associate",
                controller = "AssociateStockRequest",
                action = "TrackPurchaseStock",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
           "AssociateSupport",
           "Associate-Support/{action}/{id}",
           new
           {
               area = "Associate",
               controller = "AssociateReport",
               action = "AssociateSupport",
               id = UrlParameter.Optional
           },
           new[] { "LifeOne.Areas.Associate.Controllers" });


          context.MapRoute(
          "AssociateAdvancePayment",
          "Associate-Advance-Payment/{action}/{id}",
          new
          {
              area = "Associate",
              controller = "AssociateAdvancePayment",
              action = "AdvancePaymentRequest",
              id = UrlParameter.Optional
          },
          new[] { "LifeOne.Areas.Associate.Controllers" });


            context.MapRoute(
         "AssociateRegistration",
         "Associate-Registration/{action}/{id}",
         new
         {
             area = "Associate",
             controller = "AssociateRegistration",
             action = "AssociateRegistration",
             id = UrlParameter.Optional
         },
         new[] { "LifeOne.Areas.Associate.Controllers" });


            context.MapRoute(
        "PackagePurchaseHistory",
        "Associate-PackagePurchaseHistory/{action}/{id}",
        new
        {
            area = "Associate",
            controller = "BuyPackage",
            action = "PackagePurchaseHistory",
            id = UrlParameter.Optional
        },
        new[] { "LifeOne.Areas.Associate.Controllers" });


            context.MapRoute(
       "Rewards",
       "Associate-Rewards/{action}/{id}",
       new
       {
           area = "Associate",
           controller = "AssociateRewards",
           action = "Rewards",
           id = UrlParameter.Optional
       },
       new[] { "LifeOne.Areas.Associate.Controllers" });


            context.MapRoute(
      "Distributor-Details",
      "Distributor-Details/{action}/{id}",
      new
      {
          area = "Associate",
          controller = "AssociateTeam",
          action = "AssociateDetails",
          id = UrlParameter.Optional
      },
      new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
      "Associate-RepurchaseBonus",
      "Associate-RepurchaseBonus/{action}/{id}",
      new
      {
          area = "Associate",
          controller = "AssociateReport",
          action = "AssociateRepurchaseBonus",
          id = UrlParameter.Optional
      },
      new[] { "LifeOne.Areas.Associate.Controllers" });


            context.MapRoute(
     "Associate-RepurchaseBonusIncome",
     "Associate-RepurchaseBonusIncome/{action}/{id}",
     new
     {
         area = "Associate",
         controller = "AssociateReport",
         action = "AssociateRepurchaseBonusIncome",
         id = UrlParameter.Optional
     },
     new[] { "LifeOne.Areas.Associate.Controllers" });

            

                context.MapRoute(
     "Associate-Bonanza",
     "Associate-Bonanza/{action}/{id}",
     new
     {
         area = "Associate",
         controller = "AssociateRewards",
         action = "AssociateBonanza",
         id = UrlParameter.Optional
     },
     new[] { "LifeOne.Areas.Associate.Controllers" });


            context.MapRoute(
    "Associate-Utility",
    "Associate-Utility/{action}/{id}",
    new
    {
        area = "Associate",
        controller = "AssociateReport",
        action = "AssociateUtility",
        id = UrlParameter.Optional
    },
    new[] { "LifeOne.Areas.Associate.Controllers" });


            context.MapRoute(
               "premimum-club-member",
               "premimum-club-member/{action}/{id}",
               new
               {
                   area = "Associate",
                   controller = "Premimumclub",
                   action = "Index",
                   id = UrlParameter.Optional
               },
               new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
              "premimum-club-qualifier",
              "premimum-club-qualifier/{action}/{id}",
              new
              {
                  area = "Associate",
                  controller = "Premimumclub",
                  action = "PremimumclubQualifier",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
             "Monthly-Bonanza",
             "Monthly-Bonanza/{action}/{id}",
             new
             {
                 area = "Associate",
                 controller = "AssociateReport",
                 action = "GetBonanzaReward",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Associate.Controllers" });
            
            context.MapRoute(
             "MyPackage",
             "MyPackage/{action}/{id}",
             new
             {
                 area = "Associate",
                 controller = "AssociateReport",
                 action = "GetActiveMembers",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
            "BusinessSummary",
            "BusinessSummary/{action}/{id}",
            new
            {
                area = "Associate",
                controller = "AssociateReport",
                action = "GetBusinessStatistics",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Associate.Controllers" });


            context.MapRoute(
     "Associate-CartList",
     "Associate-CartList/{action}/{id}",
     new
     {
         area = "Associate",
         controller = "AssociateReport",
         action = "AssociateCartList",
         id = UrlParameter.Optional
     },
     new[] { "LifeOne.Areas.Associate.Controllers" });
            
            context.MapRoute(
     "Associate-Products",
     "Associate-Products/{action}/{id}",
     new
     {
         area = "Associate",
         controller = "AssociateReport",
         action = "AssociateProductList",
         id = UrlParameter.Optional
     },
     new[] { "LifeOne.Areas.Associate.Controllers" });

                context.MapRoute(
     "Associate-ProductsDetails",
     "Associate-ProductsDetails/{action}/{id}",
     new
     {
         area = "Associate",
         controller = "AssociateReport",
         action = "AssociateProductDetails",
         id = UrlParameter.Optional
     },
     new[] { "LifeOne.Areas.Associate.Controllers" });
            
            context.MapRoute(
     "Associate-Order-Details",
     "Associate-Order-Details/{action}/{id}",
     new
     {
         area = "Associate",
         controller = "AssociateReport",
         action = "AssociateOrderDetails",
         id = UrlParameter.Optional
     },
     new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
                "Associate_ChangeProfilePicture",
                "Change-ProfilePicture/{action}/{id}",
                new
                {
                    area = "Associate",
                    controller = "AssociateViewProfile",
                    action = "AssociateChangeProfilePicture",
                    id = UrlParameter.Optional
                },
                new[] { "LifeOne.Areas.Associate.Controllers" });




            context.MapRoute(
                "Associate_ViewIdCard",
                "View-IdCard/{action}/{id}",
                new
                {
                    area = "Associate",
                    controller = "AssociateViewProfile",
                    action = "ViewIdCard",
                    id = UrlParameter.Optional
                },
                new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
               "Associate_Rewardlist",
               "Associate_Rewardlist/{action}/{id}",
               new
               {
                   area = "Associate",
                   controller = "AssociateReport",
                   action = "Rewardspage",
                   id = UrlParameter.Optional
               },
               new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
              "Associate_RewardsandRecogination",
              "Associate_RewardsandRecogination/{action}/{id}",
              new
              {
                  area = "Associate",
                  controller = "AssociateReport",
                  action = "RewardsandRecogination",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
              "Associate_PinRecognition",
              "Associate_PinRecognition/{action}/{id}",
              new
              {
                  area = "Associate",
                  controller = "AssociateReport",
                  action = "PinRecognition",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
              "Associate_MatchingBonus",
              "Associate_MatchingBonus/{action}/{id}",
              new
              {
                  area = "Associate",
                  controller = "AssociateReport",
                  action = "MatchingBonus",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
             "Associate_PerformanceBonus",
             "Associate_PerformanceBonus/{action}/{id}",
             new
             {
                 area = "Associate",
                 controller = "AssociateReport",
                 action = "PerformanceBonus",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Associate.Controllers" });



            context.MapRoute(
             "AssociateReport/PlaceOrder",
             "AssociateReport/PlaceOrder/{action}/{id}",
             new
             {
                 area = "Associate",
                 controller = "AssociateReport",
                 action = "PlaceOrder",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Associate.Controllers" });

            context.MapRoute(
        "AssociateCongratulations",
        "Associate_Congratulations/{action}/{id}",
        new
        {
            area = "Associate",
            controller = "AssociateRegistration",
            action = "AssociateCongratulations",
            id = UrlParameter.Optional
        },
        new[] { "LifeOne.Areas.Associate.Controllers" });
        }
    }
}