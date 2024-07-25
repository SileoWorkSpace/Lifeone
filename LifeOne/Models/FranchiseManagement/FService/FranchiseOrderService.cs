using LifeOne.Models.Common;
using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.Manager;
using LifeOne.Models.QUtility.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FService
{
    public class FranchiseOrderService
    {
        #region Franchise to Admin
        public static MSimpleString AddProductService(MFranchiseorderRequest _data)
        {
            try
            {
                _data.Fk_Memid = SessionManager.FranchiseFk_MemId;
                MSimpleString obj = DALFranchise.AddProduct(_data);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }

        }
        /*Add to temp*/
        public static MSimpleString AddAssociateProductService(MFranchiseorderRequest _data)
        {
            try
            {

                _data.Fk_Memid = SessionManager.FranchiseFk_MemId;
                MSimpleString obj = DALFranchise.AddProduct(_data);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static MSimpleString SaveProductService(MFranchiseorderRequest _data)
        {
            try
            {
                _data.FranchiseId = SessionManager.FranchiseFk_MemId;
                MSimpleString obj = DALFranchise.SaveProduct(_data);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static MSimpleString SaveAssociateProductService(MFranchiseorderRequest _data)
        {
            try
            {
                _data.FranchiseId = SessionManager.FranchiseFk_MemId;
                /*Find out the Franchises here to whom stock will be debited */
                ManageFranchiseStockService _objStockManage = new ManageFranchiseStockService();
                _data.XmlData = _objStockManage.ManageFransStockByPincode(_data.ReqId);
                MSimpleString obj = DALFranchise.DALSaveAssociateProduct(_data);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static MFranchiseItemStock MyStockDetails(MFranchiseItemStock _data)
        {
            try
            {
                int totalRecords = 0;
                _data.FranchiseId = SessionManager.FranchiseFk_MemId;
                _data.Size = SessionManager.Size;
                _data.LstStock = DALFranchise.MyStockDetails(_data);
                if (_data.LstStock.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_data.LstStock[0].TotalRecords);
                    var pager = new Pager(totalRecords, _data.Page, SessionManager.Size);
                    _data.Pager = pager;
                }
                else
                {
                    _data.LstStock = new List<MFranchiseItemStock>();
                }
                return _data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<MOrder> ViewRequestItemService(long OrderId)
        {
            List<MOrder> _objResponseDetailModel = null;
            try
            {
                _objResponseDetailModel = DALFranchise.DALGetOrderRequestItemDetail(OrderId);
                return _objResponseDetailModel;
            }
            catch (Exception ex)
            {
                return _objResponseDetailModel;
            }

        }


        public static List<MOrder> ViewRequestItemServiceFranchiseWise(int FranchiseId, long OrderId)
        {
            List<MOrder> _objResponseDetailModel = null;
            try
            {
                _objResponseDetailModel = DALFranchise.DALGetOrderRequestItemDetailFranchiseId(FranchiseId, OrderId);
                return _objResponseDetailModel;
            }
            catch (Exception ex)
            {
                return _objResponseDetailModel;
            }

        }
        public static MOrder RequestdOrderStatusService(int? Page, MOrder _data)
        {
            if (Page == null)
            {
                Page = 1;
            }
            _data.Page = Page;
            _data.Size = SessionManager.Size;
            _data.Fk_MemId = SessionManager.FranchiseFk_MemId;
            _data.LstOrder = DALFranchise.GetAllRequestdOrderList(_data);
            int totalRecords = 0;
            if (_data.LstOrder.Count > 0)
            {
                totalRecords = Convert.ToInt32(_data.LstOrder[0].TotalRecords);
                var pager = new Pager(totalRecords, _data.Page, _data.Size);
                _data.Pager = pager;
            }
            else
            {
                _data.LstOrder = new List<MOrder>();
            }
            return _data;
        }



        #endregion

        #region Franchise to Customer
        public static MSimpleString AddFCustomerProductService(MFranchiseorderRequest data)
        {
            try
            {
                data.FranchiseId = SessionManager.FranchiseFk_MemId;

                if (data.IsBox)
                {
                    data.TotalPv = Convert.ToDecimal((data.BoxPV * data.Quantity));
                    data.ReqBoxQty = data.Quantity;
                    data.Quantity = data.Quantity * data.BoxQty;
                }
                else
                {
                    data.TotalPv = (data.PointValue * data.Quantity);
                }
                MSimpleString obj = DALFranchise.AddFCustomerProduct(data);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static MSimpleString AddFCustomerProductServiceAPI(MFranchiseorderRequest data)
        {
            try
            {
                if (data.IsBox)
                {
                    data.TotalPv = Convert.ToDecimal((data.BoxPV * data.Quantity));
                    data.ReqBoxQty = data.Quantity;
                    data.Quantity = data.Quantity * data.BoxQty;
                }
                else
                {
                    data.TotalPv = (data.PointValue * data.Quantity);
                }
                MSimpleString obj = DALFranchise.AddFCustomerProduct(data);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<MOrder> ViewSupplyItemService(long OrderId)
        {
            List<MOrder> _objResponseDetailModel = null;
            try
            {
                _objResponseDetailModel = DALFranchise.DALGetOrderSupplyItemDetail(OrderId);
                return _objResponseDetailModel;
            }
            catch (Exception ex)
            {
                return _objResponseDetailModel;
            }

        }

        public static MOrder SupplyOrderStatusService(int? Page, MOrder _data)
        {
            if (Page == null)
            {
                Page = 1;
            }
            _data.Page = Page;
            _data.Size = SessionManager.Size;
            _data.Fk_MemId = SessionManager.FranchiseFk_MemId;
            _data.LstOrder = DALFranchise.GetAllSuppliedOrderList(_data);
            int totalRecords = 0;
            if (_data.LstOrder.Count > 0)
            {
                totalRecords = Convert.ToInt32(_data.LstOrder[0].TotalRecords);
                var pager = new Pager(totalRecords, _data.Page, _data.Size);
                _data.Pager = pager;
            }
            else
            {
                _data.LstOrder = new List<MOrder>();
            }
            return _data;
        }
        public static MOrder SupplyOrderStatusServiceForAdmin(int? Page, MOrder _data)
        {
            if (_data.Page == null || _data.Page==0)
            {
                _data.Page = 1;
            }
             
            _data.Size = SessionManager.Size;

            _data.LstOrder = DALFranchise.GetAllSuppliedOrderList(_data);
            int totalRecords = 0;
            if (_data.LstOrder.Count > 0)
            {
                totalRecords = Convert.ToInt32(_data.LstOrder[0].TotalRecords);
                var pager = new Pager(totalRecords, _data.Page, _data.Size);
                _data.Pager = pager;
            }
            else
            {
                _data.LstOrder = new List<MOrder>();
            }
            return _data;
        }
        public static MSimpleString SaveFCustomerProductService(MFranchiseorderRequest data)
        {
            try
            {
                data.FranchiseId = SessionManager.FranchiseFk_MemId;
                MSimpleString obj = DALFranchise.SaveFCustomerProduct(data);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public static MSimpleString SaveFCustomerProductServiceAPI(MFranchiseorderRequest data)
        {
            try
            {
                MSimpleString obj = DALFranchise.SaveFCustomerProduct(data);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }



        #endregion

        public static MSimpleString ReceiveFranchiseOrder(long FK_ReqId)
        {
            MSimpleString _objResponseDetailModel = null;
            try
            {
                _objResponseDetailModel = DALFranchise.DALReceiveFranchiseOrder(FK_ReqId);
                return _objResponseDetailModel;
            }
            catch (Exception ex)
            {
                return _objResponseDetailModel;
            }

        }
        public static MOrderModel FranchisePersonalDetails()
        {
            try
            {
                MOrderModel response = DALFranchise.FranchisePersonalDetails(SessionManager.LoginId);
                return response;
            }
            catch (Exception e)
            {
                throw;
            }

        }


        public static MOrder Get_Current_SuppliedOrderList(MOrder _data)
        {


            _data.Fk_MemId = SessionManager.FranchiseFk_MemId;
            _data.LstOrder = DALFranchise.Get_Current_SuppliedOrderList(_data);
            int totalRecords = 0;
            if (_data.LstOrder.Count > 0)
            {
                totalRecords = Convert.ToInt32(_data.LstOrder[0].TotalRecords);
                var pager = new Pager(totalRecords, 1, 50);
                _data.Pager = pager;
            }
            else
            {
                _data.LstOrder = new List<MOrder>();
            }
            return _data;
        }


        public static MOrder ExportToExcelBillingOrdered(int? Page, MOrder _data)
        {
            if (Page == null)
            {
                Page = 1;
            }
            _data.Page = Page;
            _data.Size = 1000000000;
            _data.Fk_MemId = SessionManager.FranchiseFk_MemId;
            _data.LstOrder = DALFranchise.GetAllSuppliedOrderList(_data);
            int totalRecords = 0;
            if (_data.LstOrder.Count > 0)
            {
                totalRecords = Convert.ToInt32(_data.LstOrder[0].TotalRecords);
                var pager = new Pager(totalRecords, _data.Page, _data.Size);
                _data.Pager = pager;
            }
            else
            {
                _data.LstOrder = new List<MOrder>();
            }
            return _data;
        }


        public static MOrder DeleteCustomerOrder(MOrder _model)
        {
            try
            {
                MOrder obj = DALFranchise.DeleteCustomerOrder(_model);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static MOrder CancelPackageSale(MOrder _model)
        {
            try
            {
                MOrder obj = DALFranchise.CancelPackageSale(_model);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static MSimpleString AdminTempSaveOrder(MFranchiseorderRequest data)
        {
            try
            {
                data.Fk_Memid = SessionManager.Fk_MemId;
                data.OpCode =1;
                data.TotalPv = (data.PointValue * data.Quantity);
                MSimpleString obj = DALFranchise.TempManageOrderbyAdmin(data);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static MSimpleString SaveCustomerProductService(MFranchiseorderRequest data)
        {
            try
            {
                data.Fk_Memid = SessionManager.Fk_MemId;
                MSimpleString obj = DALFranchise.SaveCustomerProduct(data);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<MOrder> ViewOpenOrderDetails(long OrderId)
        {
            List<MOrder> _objResponseDetailModel = null;
            try
            {
                _objResponseDetailModel = DALFranchise.GetOpenOrderDetail(OrderId);
                return _objResponseDetailModel;
            }
            catch (Exception ex)
            {
                return _objResponseDetailModel;
            }

        }
    }
}