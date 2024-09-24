using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALAdminProductMaster
    {

        public static ResponseMaster Save(MAdminProductMaster obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProductName", obj.ProductName);
                queryParameters.Add("@ProductDescription", obj.ProductDescription );
                queryParameters.Add("@MRP", obj.MRP );
                queryParameters.Add("@SalesPrice", obj.SalesPrice );
                queryParameters.Add("@PV", obj.PV );
                queryParameters.Add("@BV", obj.BV );
                queryParameters.Add("@ProductImage", obj.ProductImage );
                queryParameters.Add("@CGST", obj.CGST );
                queryParameters.Add("@IGST", obj.IGST );
                queryParameters.Add("@SGST", obj.SGST );
                queryParameters.Add("@Fk_ProductTypeId", obj.Fk_ProductTypeId );
                queryParameters.Add("@Fk_StatusId", obj.Fk_StatusId );
                queryParameters.Add("@Fk_UnitId", obj.Fk_UnitId );
                queryParameters.Add("@IsBox", obj.IsBox);
                queryParameters.Add("@BoxQty", obj.BoxQty);
                queryParameters.Add("@BoxPv", obj.BoxPV);
                queryParameters.Add("@Fk_OrbitType", obj.FK_OrbitType);
                queryParameters.Add("@HSNCode", obj.HSNCode);
                queryParameters.Add("@DirectionOfUse", obj.DirectionOfUse);
                queryParameters.Add("@Doses", obj.Doses);
                queryParameters.Add("@Avalibility", obj.Avalibility);
                queryParameters.Add("@Packing", obj.Packing);
                queryParameters.Add("@Fk_SupplierId", obj.Fk_SupplierId);

                ResponseMaster _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseMaster>("ProductMasterInsert", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ResponseMaster UpdateProductMaster(MAdminProductMaster obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_ProductId", obj.Pk_ProductId);
                queryParameters.Add("@ProductName", obj.ProductName);
                queryParameters.Add("@ProductDescription", obj.ProductDescription);
                queryParameters.Add("@MRP", obj.MRP);
                queryParameters.Add("@SalesPrice", obj.SalesPrice);
                queryParameters.Add("@PV", string.IsNullOrEmpty(obj.PV)?"0": obj.PV);
                queryParameters.Add("@BV", string.IsNullOrEmpty(obj.BV)?"0": obj.BV);
                queryParameters.Add("@ProductImage", obj.ProductImage);
                queryParameters.Add("@CGST", obj.CGST);
                queryParameters.Add("@IGST", obj.IGST);
                queryParameters.Add("@SGST", obj.SGST);
                queryParameters.Add("@Fk_ProductTypeId", obj.Fk_ProductTypeId);
                queryParameters.Add("@Fk_StatusId", obj.Fk_StatusId);
                queryParameters.Add("@Fk_UnitId", obj.Fk_UnitId);
                queryParameters.Add("@IsBox", obj.IsBox);
                queryParameters.Add("@BoxQty", obj.BoxQty);
                queryParameters.Add("@BoxPv", obj.BoxPV);
                queryParameters.Add("@Fk_OrbitType", obj.FK_OrbitType);
                queryParameters.Add("@Updatedby", SessionManager.Fk_MemId);
                queryParameters.Add("@HSNCode", obj.HSNCode);
                queryParameters.Add("@DirectionOfUse", obj.DirectionOfUse);
                queryParameters.Add("@Doses", obj.Doses);
                queryParameters.Add("@Avalibility", obj.Avalibility);
                queryParameters.Add("@Packing", obj.Packing);
                queryParameters.Add("@Fk_SupplierId", obj.Fk_SupplierId);
                ResponseMaster _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseMaster>("ProductMasterUpdate", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MAdminProductMaster> GetProductList(MAdminProductMaster obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                List<MAdminProductMaster> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminProductMaster>("ProductMasterList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static  MAdminProductMaster  GetProductEdit(MAdminProductMaster obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                 queryParameters.Add("@Pk_ProductId", obj.Pk_ProductId);
                //queryParameters.Add("@BillerId", _objrequestdetails.BillerId);
                //queryParameters.Add("@IPAdress", _objrequestdetails.IP);
                //queryParameters.Add("@AndroidId", _objrequestdetails.AndroidId);
                //queryParameters.Add("@DeviceId", _objrequestdetails.DeviceId);
                //queryParameters.Add("@CompanyId", _objrequestdetails.CompanyId);
                //queryParameters.Add("@Fk_MemId", _objrequestdetails.Fk_MemID);
                //queryParameters.Add("@CreatedBy", _objrequestdetails.Fk_MemID);
                //queryParameters.Add("@ProcId", 1); // For adding on rows
                //queryParameters.Add("@BillAmount", _objDetails.billerResponse.billAmount);
                //queryParameters.Add("@BillDate", _objDetails.billerResponse.billDate);
                //queryParameters.Add("@BillNumber", _objDetails.billerResponse.billNumber);
                //queryParameters.Add("@BillPeriod", _objDetails.billerResponse.billPeriod);
                //queryParameters.Add("@CustomerName", _objDetails.billerResponse.customerName);
                //queryParameters.Add("@DueDate", _objDetails.billerResponse.dueDate);
                 MAdminProductMaster  _iresult = DBHelperDapper.DAAddAndReturnModel<MAdminProductMaster>("ProductMasterList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseMaster  ProductDelete(string id)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_ProductId", id);

                ResponseMaster _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseMaster>("ProductMasterDelete", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}