using System.Web.Mvc;

namespace LifeOne.Areas.Franchise
{
    public class FranchiseAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Franchise";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Franchise_default",
                "Franchise/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );


            context.MapRoute(
             "Franchise-Dashboard_default",
             "Franchise-Dashboard/{action}/{id}",
             new
             {
                 area = "Franchise",
                 controller = "Franchise",
                 action = "FranchiseDashboard",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Franchise.Controllers" });




            context.MapRoute(
             "Franchise-OrderRequest_default",
             "Franchise-Order-Request/{action}/{id}",
             new
             {
                 area = "Franchise",
                 controller = "ManageFranchise",
                 action = "OrderRequest",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Franchise.Controllers" });


            context.MapRoute(
             "Franchise-ViewRequestdOrderStatus_default",
             "Franchise-View-Requested-OrderStatus/{action}/{id}",
             new
             {
                 area = "Franchise",
                 controller = "FranchiseOrderStatus",
                 action = "ViewRequestdOrderStatus",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Franchise.Controllers" });

            context.MapRoute(
             "Franchise-MyStockDetails_default",
             "Franchise-MyStockDetails/{action}/{id}",
             new
             {
                 area = "Franchise",
                 controller = "FranchiseOrderStatus",
                 action = "MyStockDetails",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Franchise.Controllers" });


            context.MapRoute(
             "Franchise-CustomerDetail_default",
             "Franchise-CustomerDetail/{action}/{id}",
             new
             {
                 area = "Franchise",
                 controller = "FranchiseCustomerOrder",
                 action = "CustomerDetail",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Franchise.Controllers" });




            context.MapRoute(
             "Franchise-ViewFranchiseSupplyOrderStatus_default",
             "ViewFranchise-Supply-OrderStatus/{action}/{id}",
             new
             {
                 area = "Franchise",
                 controller = "FranchiseCustomerOrder",
                 action = "ViewFranchiseSupplyOrderStatus",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Franchise.Controllers" });
            
            context.MapRoute(
             "Franchise-BusinessSummary_default",
             "Franchise-BusinessSummary/{action}/{id}",
             new
             {
                 area = "Franchise",
                 controller = "FranchiseBusinessReport",
                 action = "BusinessSummary",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Franchise.Controllers" });


            context.MapRoute(
             "Franchise-FranchiseSell_default",
             "Franchise-Sell/{action}/{id}",
             new
             {
                 area = "Franchise",
                 controller = "Franchise",
                 action = "FranchiseSell",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Franchise.Controllers" });


            context.MapRoute(
             "Franchise-FranchisePurchaseHistory_default",
             "Franchise-Purchase-History/{action}/{id}",
             new
             {
                 area = "Franchise",
                 controller = "Franchise",
                 action = "FranchisePurchaseHistory",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Franchise.Controllers" });


            context.MapRoute(
             "Franchise-ViewShippingDetails_default",
             "View-Shipping-Details/{action}/{id}",
             new
             {
                 area = "Franchise",
                 controller = "FranchiseShippingOrder",
                 action = "ViewShippingDetails",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Franchise.Controllers" });

            context.MapRoute(
            "Franchise-FranchiseMaster_default",
            "Franchise-Master/{action}/{id}",
            new
            {
                area = "Franchise",
                controller = "FranchiseProfile",
                action = "FranchiseMaster",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Franchise.Controllers" });

            context.MapRoute(
            "Franchise-FranchiseChangePassword_default",
            "Franchise-ChangePassword/{action}/{id}",
            new
            {
                area = "Franchise",
                controller = "FranchiseProfile",
                action = "FranchiseChangePassword",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Franchise.Controllers" });


            context.MapRoute(
             "FranchiseEWalletRequest_default",
             "Franchise-E-WalletRequest/{action}/{id}",
             new
             {
                 area = "Franchise",
                 controller = "Franchise",
                 action = "FranchiseEWalletRequest",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Franchise.Controllers" });

            context.MapRoute(
            "GetEWalletRequest_default",
            "GetFranchise-E-WalletRequest/{action}/{id}",
            new
            {
                area = "Franchise",
                controller = "ManageFranchise",
                action = "GetFranchiseEwalletRequest",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Franchise.Controllers" });

            context.MapRoute(
           "TopupByFranchise_default",
           "TopupByFranchise/{action}/{id}",
           new
           {
               area = "Franchise",
               controller = "Franchise",
               action = "TopupByFranchise",
               id = UrlParameter.Optional
           },
           new[] { "LifeOne.Areas.Franchise.Controllers" });

          context.MapRoute(
          "E-WalletLedger_default",
          "franchisewalletledger/{action}/{id}",
          new
          {
              area = "Franchise",
              controller = "ManageFranchise",
              action = "EWalletLedger",
              id = UrlParameter.Optional
          },
          new[] { "LifeOne.Areas.Franchise.Controllers" });

            context.MapRoute(
            "ActivatedMembers_default",
            "ActivatedMembers/{action}/{id}",
            new
            {
                area = "Franchise",
                controller = "ManageFranchise",
                action = "GetActiveMembersFranchise",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Franchise.Controllers" });
            
            context.MapRoute(
             "Franchise-PayoutReport",
             "Franchise-PayoutReport/{action}/{id}",
            new
            {
                area = "Franchise",
                controller = "FranchiseBusinessReport",
                action = "GetFranchisePayoutReport",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Franchise.Controllers" });

            context.MapRoute(
              "Franchisee-KYC",
              "Franchisee-KYC/{action}/{id}",
              new
              {
                  area = "Franchise",
                  controller = "FranchiseProfile",
                  action = "FranchiseeKYC",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Franchise.Controllers" });
        }
    }
}