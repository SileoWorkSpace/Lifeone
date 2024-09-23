using System.Web.Mvc;

namespace LifeOne.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
            "Admin_default",
            "Admin/{controller}/{action}/{id}",
            new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Agriculture-Master_default",
            "Agriculture-Master/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AgricultureMaster",
                action = "AddMaster",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Dashboard_default",
            "Admin-Dashboard/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Admin",
                action = "Dashboard",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
       "Admin-Employee-Dashboard",
       "Employee-Dashboard/{action}/{id}",
       new
       {
           area = "Admin",
           controller = "Admin",
           action = "EmpDashboard",
           id = UrlParameter.Optional
       },
       new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Update-Payout-Income-Default",
            "Update-Payout-Income/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "UpdatePayoutIncome",
                action = "UpdateNow",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Manage-App-Dashboard",
            "Manage-App-Dashboard/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Master",
                action = "UtilityMaster",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });





            context.MapRoute(
            "Admin-IncomeType_default",
            "IncomeType/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "IncomeType",
                action = "IncomeType",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-PlanIncome_default",
            "PlanIncome/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "planIncome",
                action = "PlanIncome",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Bank_default",
            "Bank/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "BankMaster",
                action = "Bank",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Utility_default",
            "Utility/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Master",
                action = "Utility",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-ProductType_default",
            "ProductType/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "ProducType",
                action = "ProductType",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-ProductMasterList_default",
            "ProductMasterList/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "ProductMaster",
                action = "ProductMasterList",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Unit_default",
            "Unit/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "UnitMaster",
                action = "Unit",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-SupportType_default",
            "SupportType/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "SupportTypeMaster",
                action = "SupportType",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-News_default",
            "News/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "NewsMaster",
                action = "News",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Commission_default",
            "Commission/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "CommissionMaster",
                action = "Commission",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-PerformanceLevelMaster_default",
            "PerformanceLevelMaster/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "PerformanceLevelMaster",
                action = "PerformanceLevelMaster",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-UpdatePerformanceLevel_default",
            "UpdatePerformanceLevel/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "ManagePerformance",
                action = "UpdatePerformanceLevel",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Members_default",
            "DirectSellers/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Admin",
                action = "Members",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-BlockUnblockDetails_default",
            "BlockUnblockDetails/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "BlockUnblockDetails",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-ActiveUnActiveMemberList_default",
            "ActiveUnActiveMemberList/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "ActiveUnActiveMemberList",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-RequestDetails_default",
            "RequestDetails/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Transactions",
                action = "RequestDetails",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-OrderedDetails_default",
            "OrderedDetails/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "ManageAssociateOrdered",
                action = "OrderedDetails",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
       "Admin-OrderList",
       "Admin-OrderList/{action}/{id}",
       new
       {
           area = "Admin",
           controller = "AdminReport",
           action = "AdminOrderList",
           id = UrlParameter.Optional
       },
       new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-CustomerDetail_default",
            "CustomerDetail/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "ManageAssociateOrdered",
                action = "CustomerDetail",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-OrderForCustomer_default",
            "OrderForCustomer/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "ManageAssociateOrdered",
                action = "OrderForCustomer",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Direct_default",
            "Direct/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Team",
                action = "Direct",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Upline",
            "Upline/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Team",
                action = "Upline",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });





            context.MapRoute(
            "Admin-Downline_default",
            "Downline/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Team",
                action = "Downline",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-ProductMaster_default",
            "ProductMaster/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "ProductMaster",
                action = "ProductMaster",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-EditUserDetails_default",
            "Admin-EditUserDetails/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Admin",
                action = "EditUserDetails",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-ViewProfile_default",
            "Admin-ViewProfile/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Admin",
                action = "ViewProfile",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
     "Admin-FranchiseViewProfile_default",
     "Admin-FranchiseViewProfile/{action}/{id}",
     new
     {
         area = "Admin",
         controller = "Admin",
         action = "FranchiseViewProfile",
         id = UrlParameter.Optional
     },
     new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-AdminChangePassword_default",
            "Admin-ChangePassword/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminProfile",
                action = "AdminChangePassword",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
       "Admin-FranchiseChangePassword_default",
       "Admin-FranchiseChangePassword/{action}/{id}",
       new
       {
           area = "Admin",
           controller = "AdminProfile",
           action = "FranchiseChangePassword",
           id = UrlParameter.Optional
       },
       new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-AddOrderForCustomer_default",
            "AddOrderForCustomer/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "ManageAssociateOrdered",
                action = "AddOrderForCustomer",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });




            context.MapRoute(
            "Admin-AllTransactionLog_default",
            "AllTransactionLog/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "Admin",
                action = "AllTransactionLog",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });




            context.MapRoute(
            "Admin-RechargeBillPaymentReport_default",
            "RechargeBillPaymentReport/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "UtilityBillPaymentReport",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-RazorPaymentSearch_default",
            "RazorPaymentSearch/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "RazorPaymentSearch",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-OtherShopping_default",
            "OtherShopping/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "OtherShopping",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Allwalletdetails_default",
            "Allwalletdetails/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "Allwalletdetails",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-WalletRequest_default",
            "WalletRequest/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Wallet",
                action = "WalletRequest",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "wallet-ApproveRequest",
            "ApproveRequest/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Wallet",
                action = "ApproveRequest",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-FranchisePaymentRequest_default",
            "FranchisePaymentRequest/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Wallet",
                action = "FranchisePaymentRequest",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "wallet-ApproveFranchisePaymentRequest",
            "ApproveFranchisePaymentRequest/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Wallet",
                action = "ApproveFranchisePaymentRequest",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-WalletDetails_default",
            "WalletDetails/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "ManagePayout",
                action = "WalletDetails",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-KYCSection_default",
            "KYCSection/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "KYCSection",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-CurrentBusinessReportDetail",
            "Franchise-Business-Summary/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "BusinessReport",
                action = "CurrentBusinessReportDetail",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-PayoutReport",
            "PayoutReport/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "PayoutReport",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
           "Admin-DownlineReport",
           "Admin-DownlineReport/{action}/{id}",
           new
           {
               area = "Admin",
               controller = "AdminReport",
               action = "UserDownlineReport",
               id = UrlParameter.Optional
           },
           new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
     "Admin-RepurchasePayout",
     "Admin-RepurchasePayout/{action}/{id}",
     new
     {
         area = "Admin",
         controller = "UpdatePayoutIncome",
         action = "RepurchasePayoutReport",
         id = UrlParameter.Optional
     },
     new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-DownlinePayoutReport",
            "Downline-Payout-Report/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "DownlinePayoutReport",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-IncomeDetails",
            "Income-Details/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "PlanIncome",
                action = "IncomeDetails",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });





            context.MapRoute(
            "Admin-TDSReport",
            "TDS-Report/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "TDSReport",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-PayoutLeadger",
            "PayoutLeadger/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "ManagePayout",
                action = "PayoutLeadger",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-PayoutReportForBank",
            "Payout-Report-For-Bank/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "PayoutReportForBank",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-UpdatePayoutPayment",
            "Update-Payout-Payment/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "UpdatePayoutPayment",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-PayoutCalculationStatus",
            "Payout-Calculation-Status/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "PayoutClosing",
                action = "PayoutCalculationStatus",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-StockReport",
            "Stock-Report/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "StockManagement",
                action = "StockReport",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-ManageStock",
            "Manage-Stock/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "ManageStock",
                action = "ManageStock",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-ViewNewRequest",
            "View-New-Request/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "FranchiseOrderRequest",
                action = "ViewNewRequest",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-FranchiseMasterList",
            "Franchise-Master-List/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "FranchiseMaster",
                action = "FranchiseMasterList",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-FranchiseSell",
            "FranchiseSell/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "FranchiseSell",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-FranchisePurchaseHistory",
            "FranchisePurchaseHistory/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "FranchisePurchaseHistory",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-AchieverClubBonusMaster",
            "AchieverClubBonusMaster/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AchieverClubBonusMaster",
                action = "AchieverClubBonusMaster",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-Support_Request",
            "Admin-SupportRequest/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "SupportRequest",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-SupportRequestByLoginId",
            "Admin-SupportRequestByLoginId/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "SupportRequestByLoginId",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-ChangeMemberParent",
            "Admin-Change-Member-Sponsor/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "MemberParent",
                action = "ChangeMemberParent",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-ChangeProfileList",
            "Admin-Change-Profile-List/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "MemberParent",
                action = "ChangeProfileList",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-ChangeProfile",
            "ChangeProfile/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "MemberParent",
                action = "ChangeProfile",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-AddMiddleMember",
            "Admin-Add-Middle-Member/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "MemberParent",
                action = "AddMiddleMember",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin- Product-List",
            "Product-List/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminProducts",
                action = "AdminProductList",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin- Agriculture-Master-Report",
            "Agriculture-Master-Report/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AgricultureMaster",
                action = "AgricultureMasterReport",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Edit-Agriculture-Master",
            "Edit-Agriculture-Master/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AgricultureMaster",
                action = "EditMaster",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Advance-Payment",
            "Advance-Payment/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdvancePayment",
                action = "AdvancePaymentDetails",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Associate-BusinessSummary",
            "Associate-BusinessSummary/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AssociateBusinessSummary",
                action = "BusinessSummary",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-Franchise-Master",
            "Admin-Franchise-Master/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "FranchiseMaster",
                action = "FranchiseMaster",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-CustomerTaxInvoice",
            "Admin-CustomerTaxInvoice/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "ManageAssociateOrdered",
                action = "CustomerTaxInvoice",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-OrderReport",
            "OrderReport/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "ManageAssociateOrdered",
                action = "OrderReport",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-PayoutClosing",
            "Admin-PayoutClosing/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "PayoutClosing",
                action = "PayoutClosing",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-MemberTaxInvoice",
            "Admin-MemberTaxInvoice/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "ManageAssociateOrdered",
                action = "MemberTaxInvoice",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-FranchiseApproval",
            "Franchise-Approval/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "FranchiseMaster",
                action = "FranchiseApproval",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-VerifyKyc",
            "VerifyKyc/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "VerifyPanOrVoterIdDetails",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-KaryonProductMaster",
            "Admin-KaryonProductMaster/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Master",
                action = "Karyon_ProductMaster",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-CropCategory",
            "Admin-CropCategory/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Master",
                action = "CropCategory",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-ApproveKYC",
            "Admin-ApproveKYC/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "ApprovePanOrVoterIdDetails",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-DeclineKYC",
            "Admin-DeclineKYC/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "DeclinePanOrVoterIdDetails",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-ApprovedFranchise",
            "Admin-ApprovedFranchise/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "FranchiseMaster",
                action = "ApprovedFranchise",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-DeclinedFranchise",
            "Admin-DeclinedFranchise/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "FranchiseMaster",
                action = "DeclinedFranchise",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-NotifyMembers",
            "Admin-NotifyMembers/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Admin",
                action = "NotifyMembers",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-RefundAmount",
            "Admin-RefundAmount/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "RefundAmount",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-MemberWalletDetail",
            "Admin-MemberWalletDetail/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Admin",
                action = "MemberWalletDetail",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-MemberCashBack",
            "Admin-MemberCashBack/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Admin",
                action = "MemberCashBack",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-MemberCommision",
            "Admin-MemberCommision/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Admin",
                action = "MemberCommision",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-RecentMembers",
            "Admin-Members/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Admin",
                action = "RecentMembers",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-RecentTransactionLog",
            "Admin-TransactionLog/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Admin",
                action = "RecentTransactionLog",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "AdminForm-Master",
            "Form-Master/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "FormMaster",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });





            context.MapRoute(
            "Admin-Form-Master",
            "Form-Master-List/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "FormMasterList",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });




            //            context.MapRoute(
            //       "AdminForm-Master",
            //    "Form-Master/{action}/{id}",

            //new
            //{
            //    area = "Admin",
            //    controller = "UserPermission",
            //    action = "FormMaster",
            //    id = UrlParameter.Optional
            //},
            //new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Form-Type",
            "Form-Type/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "FormType",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-Form-Type-List",
            "Form-Type-List/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "FormTypeList",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Role-Master",
            "Role-Master/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "RoleMaster",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });




            context.MapRoute(
            "Admin-Role-Master-List",
            "Role-Master-List/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "RoleMasterList",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-Edit-Role-Master",
            "Edit-Role-Master/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "RoleMasterFill",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Delete-Role-Master",
            "Delete-Role-Master/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "RoleMasterDelete",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Role-Permission",
            "Role-Permission/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "RolePermission",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Role-Permission-List",
            "Role-Permission-List/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "RolePermissionList",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Set-Permission",
            "Set-Permission/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "SetPermission",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-Edit-Role-Permission",
            "Edit-Role-Permission/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "RolePermissionFill",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });




            context.MapRoute(
            "Admin-Set-Permission-Delete",
            "Delete-Role-Permission/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "RoleMasterDelete",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-Set-Permission-List",
            "Set-Permission-List/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "SetPermissionList",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-Set-User-Permission",
            "Set-User-Permission/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "SetUserPermission",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
            "Admin-TestimonialApproval",
            "Admin-TestimonialApproval/{action}/{id}",

            new
            {
                area = "Admin",
                controller = "Admin",
                action = "TestiMonials",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Edit-User-Permission",
            "Edit-User-Permission/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "UserPermission",
                action = "SetPermissionFill",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
                        "Admin-Delete-User-Permission",
                        "Delete-User-Permission/{action}/{id}",
                        new
                        {
                            area = "Admin",
                            controller = "UserPermission",
                            action = "SetPermissionDelete",
                            id = UrlParameter.Optional
                        },
                        new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-Employee-Registration",
            "Employee-Registration/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Admin",
                action = "EmployeeRegistration",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-TourRequest",
            "Admin-TourRequest/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "MemberTour",
                action = "TourRequestList",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
             "Admin-ApproveMemberTour",
             "Admin-ApproveMemberTour/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "MemberTour",
                 action = "ApproveMemberTour",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
             "Admin-TestimonialPermission",
             "Admin-TestimonialPermission/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "Admin",
                 action = "TestimonialPermission",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
             "Make-Member-Leader",
             "Make-Member-Leader/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "MakeMemberLeader",
                 action = "ViewAndMakePayment",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
             "Admin-CropSubCategory",
             "Admin-CropSubCategory/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "Master",
                 action = "CropSubCategory",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
             "Admin-tetsimonial-now",
             "Tetsimonial-Member/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "MakeMemberLeader",
                 action = "ViewandAddTestimonialUsers",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
             "Admin-AllowFranchiseRequest",
             "Admin-AllowFranchiseRequest/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "Admin",
                 action = "AllowFranchiseRequest",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-ApproveOrderRequest",
            "Franchise-Approve-OrderRequest/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "FranchiseOrderRequest",
                action = "FranchiseApprovedOrder",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
             "Admin-CancelOrderRequest",
             "Franchise-Cancel-OrderRequest/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "FranchiseOrderRequest",
                 action = "FranchiseCancelOrderDetails",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
            "Admin-VirtualCompanionRequest_default",
            "VirtualCompanionRequest/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Wallet",
                action = "VirtualCompanionPaymentRequest",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
           "Admin-VirtualCompanionApproveDecline_default",
           "VirtualCompanionApproveDeclineDetails/{action}/{id}",
           new
           {
               area = "Admin",
               controller = "Wallet",
               action = "VirtualCompanionApproveDeclineDetails",
               id = UrlParameter.Optional
           },
           new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
             "Associate-Tree-New",
             "Associate-Tree/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "AssoicateTree",
                 action = "ViewTree",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
             "Admin-Employees",
             "Admin-Employees/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "Admin",
                 action = "Employees",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
             "Admin-Topup",
             "Admin-Topup/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "Master",
                 action = "TopupByAdmin",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
          "Admin-DirectIncome",
          "Admin-DirectIncome/{action}/{id}",
          new
          {
              area = "Admin",
              controller = "AdminReport",
              action = "DirectIncome",
              id = UrlParameter.Optional
          },
          new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
        "Admin-MatchingtIncome",
        "Admin-MatchingIncome/{action}/{id}",
        new
        {
            area = "Admin",
            controller = "AdminReport",
            action = "MatchingtIncome",
            id = UrlParameter.Optional
        },
        new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
       "Admin-LeaderShipIncome",
       "Admin-LeaderShipIncome/{action}/{id}",
       new
       {
           area = "Admin",
           controller = "AdminReport",
           action = "LeaderShipIncome",
           id = UrlParameter.Optional
       },
       new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
      "Admin-PackageUpgrade",
      "Admin-PackageUpgrade/{action}/{id}",
      new
      {
          area = "Admin",
          controller = "AdminReport",
          action = "UpgradePackage",
          id = UrlParameter.Optional
      },
      new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
     "Admin-PendingPayoutReport",
     "Admin-PendingPayoutReport/{action}/{id}",
     new
     {
         area = "Admin",
         controller = "AdminReport",
         action = "UnPaidPayoutreport",
         id = UrlParameter.Optional
     },
     new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
                "Admin-ChangePopup",
                "Admin-ChangePopup/{action}/{id}",
                new
                {
                    area = "Admin",
                    controller = "Master",
                    action = "ChangeWebSitePopup",
                    id = UrlParameter.Optional
                },
                new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
              "Admin-UploadImage",
              "Admin-UploadImage/{action}/{id}",
              new
              {
                  area = "Admin",
                  controller = "Master",
                  action = "UploadImage",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
             "Admin-UploadVideo",
             "Admin-UploadVideo/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "Master",
                 action = "UploadVideo",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
                "Admin-RepurchaseIncomeType",
                "Admin-RepurchaseIncomeType/{action}/{id}",
                new
                {
                    area = "Admin",
                    controller = "UpdatePayoutIncome",
                    action = "RepurchaseIncomeType",
                    id = UrlParameter.Optional
                },
                new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
          "Admin-BonanzaMaster",
          "Admin-BonanzaMaster/{action}/{id}",
          new
          {
              area = "Admin",
              controller = "Master",
              action = "BonanzaMasterList",
              id = UrlParameter.Optional
          },
          new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
                "Admin-NewBonanza",
                "Admin-NewBonanza/{action}/{id}",
                new
                {
                    area = "Admin",
                    controller = "Master",
                    action = "AddNewBonanza",
                    id = UrlParameter.Optional
                },
                new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
               "Admin-BonanzaDetail",
               "Admin-BonanzaDetail/{action}/{id}",
               new
               {
                   area = "Admin",
                   controller = "Master",
                   action = "BonanzaDetail",
                   id = UrlParameter.Optional
               },
               new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
              "Admin-AddBonanzaDetail",
              "Admin-AddBonanzaDetail/{action}/{id}",
              new
              {
                  area = "Admin",
                  controller = "Master",
                  action = "AddBonanzaDetail",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
             "Admin-RewardMaster",
             "Admin-RewardMaster/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "Master",
                 action = "RewardMaster",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Admin-AddNewReward",
            "Admin-AddNewReward/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Master",
                action = "AddNewReward",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
           "Admin-RewardDetail",
           "Admin-RewardDetail/{action}/{id}",
           new
           {
               area = "Admin",
               controller = "Master",
               action = "RewardDetail",
               id = UrlParameter.Optional
           },
           new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
       "Admin-AddRewardDetail",
       "Admin-AddRewardDetail/{action}/{id}",
       new
       {
           area = "Admin",
           controller = "Master",
           action = "AddRewardDetail",
           id = UrlParameter.Optional
       },
       new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
       "Admin-RewardAchieverList",
       "Admin-RewardAchieverList/{action}/{id}",
       new
       {
           area = "Admin",
           controller = "AdminReport",
           action = "AdminRewardAchieverList",
           id = UrlParameter.Optional
       },
       new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
      "Admin-BonanzaAchieverList",
      "Admin-BonanzaAchieverList/{action}/{id}",
      new
      {
          area = "Admin",
          controller = "AdminReport",
          action = "AdminBonanzaAchieverList",
          id = UrlParameter.Optional
      },
      new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
             "Admin-AssociateMonthlyReport",
             "Admin-AssociateMonthlyReport/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "ManagePayout",
                 action = "AssociateMonthlyBusinessReport",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
           "GetFranchise-E-WalletReq_default",
           "GetFranchise-E-WalletReq/{action}/{id}",
           new
           {
               area = "Admin",
               controller = "Admin",
               action = "GetFranchiseEwalletRequest",
               id = UrlParameter.Optional
           },
           new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
           "ActiveMemberFranchise_default",
           "ActiveMemberFranchise/{action}/{id}",
           new
           {
               area = "Admin",
               controller = "Admin",
               action = "GetActiveMembersFranchise",
               id = UrlParameter.Optional
           },
           new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
             "CustomerOrder_default",
             "CustomerOrder/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "Admin",
                 action = "ViewFranchiseSupplyOrderStatus",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
     "Admin-PayoutreportIncome",
     "Admin-PayoutreportIncome/{action}/{id}",
     new
     {
         area = "Admin",
         controller = "AdminReport",
         action = "PayoutReport",
         id = UrlParameter.Optional
     },
     new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "FranchiseBusiness_default",
            "FranchiseBusiness/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "FranchiseBusiness",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
         "Admin-RepurchasePayoutreportIncome",
         "Admin-RepurchasePayoutreportIncome/{action}/{id}",
         new
         {
             area = "Admin",
             controller = "AdminReport",
             action = "Repurchase_PayoutReport",
             id = UrlParameter.Optional
         },
         new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
"Admin-RepurchaseUnPaidPayoutreport",
"Admin-RepurchaseUnPaidPayoutreport/{action}/{id}",
new
{
    area = "Admin",
    controller = "AdminReport",
    action = "Repurchase_UnPaidPayoutreport",
    id = UrlParameter.Optional
},
new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
           "Admin-RepurchasePayoutReportForBank",
           "Repurchase_Payout-Report-For-Bank/{action}/{id}",
           new
           {
               area = "Admin",
               controller = "AdminReport",
               action = "Repurchase_PayoutReportForBank",
               id = UrlParameter.Optional
           },
           new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
          "Admin-RepurchaseUpdatePayoutPayment",
          "Repurchase-Update-Payout-Payment/{action}/{id}",
          new
          {
              area = "Admin",
              controller = "AdminReport",
              action = "Repurchase_UpdatePayoutPayment",
              id = UrlParameter.Optional
          },
          new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
          "GSTReport",
          "GSTReport/{action}/{id}",
          new
          {
              area = "Admin",
              controller = "AdminReport",
              action = "GSTReport",
              id = UrlParameter.Optional
          },
          new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
        "DirectBusiness",
        "DirectBusiness/{action}/{id}",
        new
        {
            area = "Admin",
            controller = "AdminReport",
            action = "GetDirectBusiness",
            id = UrlParameter.Optional
        },
        new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
                      "Admin-PANTDSReport",
                      "PANTDS-Report/{action}/{id}",
                      new
                      {
                          area = "Admin",
                          controller = "AdminReport",
                          action = "PanCard_TDSReport",
                          id = UrlParameter.Optional
                      },
                      new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
     "FranchiseSales",
     "FranchiseSales/{action}/{id}",
     new
     {
         area = "Admin",
         controller = "AdminReport",
         action = "GetFranchiseSales",
         id = UrlParameter.Optional
     },
     new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
     "Franchise-Wallet-Ledger",
     "Franchise-Wallet-Ledger/{action}/{id}",
     new
     {
         area = "Admin",
         controller = "AdminReport",
         action = "GetFranchiseWalletLedgerForAdmin",
         id = UrlParameter.Optional
     },
     new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
                   "Direct_Seller_Direct_Business",
                   "Direct_Seller_Direct_Business/{action}/{id}",
                   new
                   {
                       area = "Admin",
                       controller = "ManagePayout",
                       action = "AssociateBusinessReport",
                       id = UrlParameter.Optional
                   },
                   new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
                     "PackageSaleReport",
                     "PackageSaleReport/{action}/{id}",
                     new
                     {
                         area = "Admin",
                         controller = "AdminReport",
                         action = "GetPackageSaleReport",
                         id = UrlParameter.Optional
                     },
                     new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
                  "Admin-AssociateRepurchaseBusiness",
                  "Admin-AssociateRepurchaseBusiness/{action}/{id}",
                  new
                  {
                      area = "Admin",
                      controller = "ManagePayout",
                      action = "AssociateRepurchaseBusiness",
                      id = UrlParameter.Optional
                  },
                  new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
                    "BusinessStatistics",
                    "BusinessStatistics/{action}/{id}",
                    new
                    {
                        area = "Admin",
                        controller = "AdminReport",
                        action = "GetBusinessStatistics",
                        id = UrlParameter.Optional
                    },
                    new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
                  "FranchiseStock",
                  "FranchiseStock/{action}/{id}",
                  new
                  {
                      area = "Admin",
                      controller = "AdminReport",
                      action = "GetFranchiseStockForAdmin",
                      id = UrlParameter.Optional
                  },
                  new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
                  "Monthly-Bonanza-Reward",
                  "Monthly-Bonanza-Reward/{action}/{id}",
                  new
                  {
                      area = "Admin",
                      controller = "AdminReport",
                      action = "GetBonanzaRewardForAdmin",
                      id = UrlParameter.Optional
                  },
                  new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
                  "PackageGstReport",
                  "PackageGstReport/{action}/{id}",
                  new
                  {
                      area = "Admin",
                      controller = "AdminReport",
                      action = "GetPackageGstReport",
                      id = UrlParameter.Optional
                  },
                  new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
                  "ProductStockReport",
                  "ProductStockReport/{action}/{id}",
                  new
                  {
                      area = "Admin",
                      controller = "AdminReport",
                      action = "GetProductStockReport",
                      id = UrlParameter.Optional
                  },
                  new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
      "ProductGSTReport",
      "ProductGSTReport/{action}/{id}",
      new
      {
          area = "Admin",
          controller = "AdminReport",
          action = "GetProductGstReport",
          id = UrlParameter.Optional
      },
      new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
    "Generate-Purchase-Order",
    "Generate-Purchase-Order/{action}/{id}",
     new
     {
         area = "Admin",
         controller = "Admin",
         action = "GeneratePurchaseOrder",
         id = UrlParameter.Optional
     },
     new[] { "LifeOne.Areas.Admin.Controllers" });


            //        context.MapRoute(
            //"Admin-ShoppingLogin",
            //"Admin-ShoppingLogin/{action}/{id}",
            // new
            // {
            //     area = "Home",
            //     controller = "Home",
            //     action = "ShoppingLogin",
            //     id = UrlParameter.Optional
            // },
            // new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "Shopping-Order-Details",
            "ShoppingOrderDetails/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "AdminReport",
                action = "GetShoppingOrderDetails",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
             "PremiumClubMember",
             "PremiumClubMember/{action}/{id}",
             new
             {
                 area = "Admin",
                 controller = "AdminReport",
                 action = "PremiumClubMember",
                 id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
              "PremiumClubQualifier",
              "PremiumClubQualifier/{action}/{id}",
              new
              {
                  area = "Admin",
                  controller = "AdminReport",
                  action = "PremiumClubQualifier",
                  id = UrlParameter.Optional
              },
              new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
        "ConsolidatedGstReport",
        "ConsolidatedGstReport/{action}/{id}",
        new
        {
            area = "Admin",
            controller = "AdminReport",
            action = "ConsolidatedGstReport",
            id = UrlParameter.Optional
        },
        new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
      "StatewiseGstReport",
      "StatewiseGstReport/{action}/{id}",
      new
      {
          area = "Admin",
          controller = "AdminReport",
          action = "StatewiseGstReport",
          id = UrlParameter.Optional
      },
      new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
    "InvoiceGSTReport",
    "InvoiceGSTReport/{action}/{id}",
    new
    {
        area = "Admin",
        controller = "AdminReport",
        action = "GetInvoiceGSTReport",
        id = UrlParameter.Optional
    },
    new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
                   "CancelInvoicReport",
                    "CancelInvoicRequest/{action}/{id}",
                   new
                   {
                       area = "Admin",
                       controller = "AdminReport",
                       action = "CancelInvoicRequest",
                       id = UrlParameter.Optional
                   },
                   new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
                  "LifeTimeReward",
                   "LifeTimeReward/{action}/{id}",
                  new
                  {
                      area = "Admin",
                      controller = "AdminReport",
                      action = "LifeTimeReward",
                      id = UrlParameter.Optional
                  },
                  new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
                  "PackageMasterList",
                   "PackageMasterList/{action}/{id}",
                  new
                  {
                      area = "Admin",
                      controller = "Master",
                      action = "PackageMasterList",
                      id = UrlParameter.Optional
                  },
                  new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
                "PackageMaster",
                 "PackageMaster/{action}/{id}",
                new
                {
                    area = "Admin",
                    controller = "Master",
                    action = "PackageMaster",
                    id = UrlParameter.Optional
                },
                new[] { "LifeOne.Areas.Admin.Controllers" });







            context.MapRoute(
          "SupplierMasterList",
          "SupplierMasterList/{action}/{id}",
          new
          {
              area = "Admin",
              controller = "Master",
              action = "SupplierMasterList",
              id = UrlParameter.Optional
          },
          new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
           "Admin-SupplierMasterList_default",
         "SupplierMasterList/{action}/{id}",
         new
         {
             area = "Admin",
             controller = "Master",
             action = "SupplierMasterList",
             id = UrlParameter.Optional
         },
         new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
                  "Franchise-Payout-Published-Report",
                  "Franchise-Payout-Published-Report/{action}/{id}",
                  new
                  {
                      area = "Admin",
                      controller = "AdminReport",
                      action = "FranchisePayoutPublishedReport",
                      id = UrlParameter.Optional
                  },
                  new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
          "SupplierMaster",
          "SupplierMaster/{action}/{id}",
          new
          {
              area = "Admin",
              controller = "Master",
              action = "SupplierMaster",
              id = UrlParameter.Optional
          },
          new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
                   "Pay-Payout-Report",
                   "Pay-Payout-Report/{action}/{id}",
                  new
                  {
                      area = "Admin",
                      controller = "AdminReport",
                      action = "GetPayPayoutReport",
                      id = UrlParameter.Optional
                  },
                  new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
                   "Members-Recognition",
                   "Members-Recognition/{action}/{id}",
                  new
                  {
                      area = "Admin",
                      controller = "AdminReport",
                      action = "GetMembersRecognition",
                      id = UrlParameter.Optional
                  },
                  new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
                  "AllGetShoppingOrderDetails",
                  "AllGetShoppingOrderDetails/{action}/{id}",
                 new
                 {
                     area = "Admin",
                     controller = "AdminReport",
                     action = "AllGetShoppingOrderDetails",
                     id = UrlParameter.Optional
                 },
                 new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
                   "Franchise-Payout-Report",
                   "Franchise-Payout-Report/{action}/{id}",
                  new
                  {
                      area = "Admin",
                      controller = "AdminReport",
                      action = "GetFranchisePayoutReport",
                      id = UrlParameter.Optional
                  },
                  new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
                  "OfferDateMaster",
                  "OfferDateMaster/{action}/{id}",
                  new
                  {
                      area = "Admin",
                      controller = "Master",
                      action = "OfferDateMaster",
                      id = UrlParameter.Optional
                  },
                  new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
            "GetOfferDateMaster",
            "GetOfferDateMaster/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "Master",
                action = "GetOfferDateMaster",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });



            context.MapRoute(
         "Admin-ChangeProfilePicture",
         "Admin-ChangeProfilePicture/{action}/{id}",
         new
         {
             area = "Admin",
             controller = "AdminProfile",
             action = "AdminChangeProfilePicture",
             id = UrlParameter.Optional
         },
         new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
          "Admin-TallyGSTReport",
          "Admin-TallyGSTReport/{action}/{id}",
        new
        {
            area = "Admin",
            controller = "AdminReport",
            action = "TallyGST",
            id = UrlParameter.Optional
        },
        new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
          "Admin-ActivationTallyGSTReport",
          "Admin-ActivationTallyGSTReport/{action}/{id}",
        new
        {
            area = "Admin",
            controller = "AdminReport",
            action = "ActivationTallyGST",
            id = UrlParameter.Optional
        },
        new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
          "VerifyFranchiseeKYC",
          "VerifyFranchiseeKYC/{action}/{id}",
        new
        {
            area = "Admin",
            controller = "AdminReport",
            action = "VerifyFranchiseeKYC",
            id = UrlParameter.Optional
        },
        new[] { "LifeOne.Areas.Admin.Controllers" });


            context.MapRoute(
        "PackageWayActivation",
        "PackageWayActivation/{action}/{id}",
      new
      {
          area = "Admin",
          controller = "AdminReport",
          action = "PackageWayActivation",
          id = UrlParameter.Optional
      },
      new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
                 "PurchaseOrderDetails",
                 "PurchaseOrderDetails/{action}/{id}",
                 new
                 {
                     area = "Admin",
                     controller = "AdminReport",
                     action = "PurchaseOrdersList",
                     id = UrlParameter.Optional
                 },
                 new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
                 "FranchiseeStockReturn",
                 "FranchiseeStockReturn/{action}/{id}",
                 new
                 {
                     area = "Admin",
                     controller = "AdminReport",
                     action = "FranchiseeStockReturn",
                     id = UrlParameter.Optional
                 },
                 new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
               "PurchasrOrderList",
               "PurchasrOrderList/{action}/{id}",
               new
               {
                   area = "Admin",
                   controller = "AdminReport",
                   action = "PurchaseOrderList",
                   id = UrlParameter.Optional
               },
               new[] { "LifeOne.Areas.Admin.Controllers" });
            //ask
            context.MapRoute(
               "Admin_Ragistration",
               "Admin_Ragistration/{action}/{id}",
               new
               {
                   area = "Admin",
                   controller = "Admin",
                   action = "AdminRagistration",
                   id = UrlParameter.Optional
               },
               new[] { "LifeOne.Areas.Admin.Controllers" });

              context.MapRoute(
               "Admin-UploadTermsandCondition_default",
              "UploadTermsandCondition/{action}/{id}",
             new
             {
                area = "Admin",
                controller = "UploadTermsandCondition",
                action = "UploadTermsandCondition",
                id = UrlParameter.Optional
             },
             new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
              "Admin-UploadTermsandConditionList_default",
             "UploadTermsandConditionList/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "UploadTermsandCondition",
                action = "UploadTermsandConditionList",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
           "Admin-PrintPayoutReportForBank",
           "Print Payout-Report-For-Bank/{action}/{id}",
           new
           {
               area = "Admin",
               controller = "AdminReport",
               action = "PrintPayoutReportForBank",
               id = UrlParameter.Optional
           },
           new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
              "Admin-UploadPrivacyPolicy_default",
             "UploadPrivacyPolicy/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "UploadPrivacyPolicy",
                action = "UploadPrivacyPolicy",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
              "Admin-UploadPrivacyPolicyList_default",
             "UploadPrivacyPolicyList/{action}/{id}",
            new
            {
                area = "Admin",
                controller = "UploadPrivacyPolicy",
                action = "UploadPrivacyPolicyList",
                id = UrlParameter.Optional
            },
            new[] { "LifeOne.Areas.Admin.Controllers" });

            context.MapRoute(
             "Admin-UploadFAQ_default",
            "UploadFAQ/{action}/{id}",
           new
           {
               area = "Admin",
               controller = "FAQMaster",
               action = "UploadFAQ",
               id = UrlParameter.Optional
           },
           new[] { "LifeOne.Areas.Admin.Controllers" });
            context.MapRoute(
           "EWallet_RequestList",
           "EWallet_RequestList/{action}/{id}",
           new
           {
               area = "Admin",
               controller = "Admin",
               action = "EWalletRequestList",
               id = UrlParameter.Optional
           },
           new[] { "LifeOne.Areas.Admin.Controllers" });
            context.MapRoute(
          "Approved_EWallet",
          "Approved_EWallet/{action}/{id}",
          new
          {
              area = "Admin",
              controller = "Admin",
              action = "Approved",
              id = UrlParameter.Optional
          },
          new[] { "LifeOne.Areas.Admin.Controllers" });
            context.MapRoute(
          "Declined_EWallet",
          "Declined_EWallet/{action}/{id}",
          new
          {
              area = "Admin",
              controller = "Admin",
              action = "Declined",
              id = UrlParameter.Optional
          },
          new[] { "LifeOne.Areas.Admin.Controllers" });
        }


    }
    
}
