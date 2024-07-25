using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Common;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALShippingInformation
    {
       
        public static int DALAddShippingInformation(MAddShippingInformation _objparam)
        {
            try
            {
                var queryParameters = new DynamicParameters();                                                      
                queryParameters.Add("@Fk_Rid", _objparam.Fk_Rid);
                queryParameters.Add("@FK_FId", _objparam.Fk_FId);
                queryParameters.Add("@DocNo", _objparam.DocNo);
                queryParameters.Add("@DocDate", _objparam.DocDate);
                queryParameters.Add("@NoOfCarton", _objparam.NoOfCarton);
                queryParameters.Add("@ModeOfTrip", _objparam.ModeOfTrip);
                queryParameters.Add("@VehicalNo", _objparam.VehicalNo);
                queryParameters.Add("@DeliveryAddress", _objparam.DeliveryAddress);
                queryParameters.Add("@State", _objparam.State);
                queryParameters.Add("@City", _objparam.City);
                queryParameters.Add("@PinCode", _objparam.PinCode);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                queryParameters.Add("@CourierName", _objparam.CourierName);
                queryParameters.Add("@RefNo", _objparam.RefNo);
                queryParameters.Add("@FreightCharge", _objparam.FreightCharge);
                queryParameters.Add("@Discount", _objparam.Discount);
                queryParameters.Add("@PersonName", _objparam.PersonName);
                queryParameters.Add("@MobileNo", _objparam.MobileNo);
                queryParameters.Add("@ElectronicRefNo", _objparam.ElectronicRefNo);
                int _iresult = DBHelperDapper.DAAdd("Proc_FranchiseDeliveryDetails", queryParameters);
                return _iresult;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int DALAddActiveMemberShippingInformation(MAddShippingInformation _objparam)
        {
            try
            {
                var queryParameters = new DynamicParameters();                                                      
                queryParameters.Add("@Fk_TransId", _objparam.Pk_TransId);
                
                queryParameters.Add("@DocNo", _objparam.DocNo);
                queryParameters.Add("@DocDate", _objparam.DocDate);
                queryParameters.Add("@NoOfCarton", _objparam.NoOfCarton);
                queryParameters.Add("@ModeOfTrip", _objparam.ModeOfTrip);
                queryParameters.Add("@VehicalNo", _objparam.VehicalNo);
                queryParameters.Add("@DeliveryAddress", _objparam.DeliveryAddress);
                queryParameters.Add("@State", _objparam.State);
                queryParameters.Add("@City", _objparam.City);
                queryParameters.Add("@PinCode", _objparam.PinCode);
                queryParameters.Add("@CreatedBy", _objparam.CreatedBy);
                queryParameters.Add("@CourierName", _objparam.CourierName);
                queryParameters.Add("@RefNo", _objparam.RefNo);
                queryParameters.Add("@FreightCharge", _objparam.FreightCharge);
                queryParameters.Add("@Discount", _objparam.Discount);
                queryParameters.Add("@PersonName", _objparam.PersonName);
                queryParameters.Add("@MobileNo", _objparam.MobileNo);
                queryParameters.Add("@ElectronicRefNo", _objparam.ElectronicRefNo);
                int _iresult = DBHelperDapper.DAAdd("ActiveMembersFranchiseDeliveryDetails", queryParameters);
                return _iresult;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GETShippingInformation(MAddShippingInformation _objparam)
        {
            try
            {
                SqlParameter[] queryParameters = {
                            new SqlParameter("@Fk_Rid", _objparam.Fk_Rid),
                new SqlParameter("@FK_FId", _objparam.Fk_FId),
                new SqlParameter("@DocNo", _objparam.DocNo),
                new SqlParameter("@DocDate", _objparam.DocDate),
                new SqlParameter("@NoOfCarton", _objparam.NoOfCarton),
                new SqlParameter("@ModeOfTrip", _objparam.ModeOfTrip),
                new SqlParameter("@VehicalNo", _objparam.VehicalNo),
                new SqlParameter("@DeliveryAddress", _objparam.DeliveryAddress),
                new SqlParameter("@State", _objparam.State),
                new SqlParameter("@City", _objparam.City),
                new SqlParameter("@PinCode", _objparam.PinCode),
                new SqlParameter("@CreatedBy", _objparam.CreatedBy),
                new SqlParameter("@CourierName", _objparam.CourierName),
                new SqlParameter("@RefNo", _objparam.RefNo),
                new SqlParameter("@FreightCharge", _objparam.FreightCharge),
                new SqlParameter("@Discount", _objparam.Discount),
                new SqlParameter("@PersonName", _objparam.PersonName),
                new SqlParameter("@MobileNo", _objparam.MobileNo),
                new SqlParameter("@ElectronicRefNo", _objparam.ElectronicRefNo) };

                DataSet ds = DBHelper.ExecuteQuery("GetFranchiseDeliveryDetails", queryParameters);

                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int DALAddShippingInformationForCustomer(MAddShippingInformation _objparam)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PK_Uniid", _objparam.PK_Uniid);
                queryParameters.Add("@Fk_FbSID", _objparam.Fk_FbSID);
                queryParameters.Add("@InvoiceTransferNo ", _objparam.InvoiceTransferNo);
                queryParameters.Add("@FK_FId", _objparam.Fk_FId);
                queryParameters.Add("@DocNo", _objparam.DocNo);
                queryParameters.Add("@DocDate", _objparam.DocDate);
                queryParameters.Add("@NoOfCarton", _objparam.NoOfCarton);
                queryParameters.Add("@ModeOfTrip", _objparam.ModeOfTrip);
                queryParameters.Add("@VehicalNo", _objparam.VehicalNo);
                queryParameters.Add("@DeliveryAddress", _objparam.DeliveryAddress);
                queryParameters.Add("@State", _objparam.State);
                queryParameters.Add("@City", _objparam.City);
                queryParameters.Add("@PinCode", _objparam.PinCode);
                queryParameters.Add("@CreatedBy", _objparam.CreatedBy);
                queryParameters.Add("@CourierName", _objparam.CourierName);
                queryParameters.Add("@RefNo", _objparam.RefNo);
                queryParameters.Add("@FreightCharge", _objparam.FreightCharge);
                queryParameters.Add("@Discount", _objparam.Discount);
                queryParameters.Add("@PersonName", _objparam.PersonName);
                queryParameters.Add("@MobileNo", _objparam.MobileNo);
                queryParameters.Add("@ElectronicRefNo", _objparam.ElectronicRefNo);
                int _iresult = DBHelperDapper.DAAdd("Proc_AssignFranchiseDeliveryDetails", queryParameters);
                return _iresult;

            }
            catch (Exception)
            {
                throw;
            }
        }



        public static int DALApproveOrderStock(MBindAssignOrdertoFranchise _objparam)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@XmlData", _objparam.xml);
                int _iresult = 0;
                if (_objparam.CustomType == "1")
                    _iresult = DBHelperDapper.DAAdd("Proc_InsertAssociateOrderFinal", queryParameters);
                else
                    _iresult = DBHelperDapper.DAAdd("Proc_DeclineCustomerOrder", queryParameters);
                return _iresult;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<MAddShippingInformation> DALGetSgippingInformationOnEdit(string _reqID)
        {
            try
            {
                string _qry = "Proc_GetFDelivery @ReqId=" + int.Parse(_reqID) + "";
                List<MAddShippingInformation> _iresult = DBHelperDapper.DAGetDetailsInList<MAddShippingInformation>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MAddShippingInformation> DALGetShippingInformationEdit(string Fk_TransId)
        {
            try
            {
                string _qry = "GetActivatedFDelivery @Trans_Id=" + int.Parse(Fk_TransId) + "";
                List<MAddShippingInformation> _iresult = DBHelperDapper.DAGetDetailsInList<MAddShippingInformation>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MAddShippingInformation> DALAssignGetSgippingInformationOnEdit(string Fk_FbSID, long FId)
        {
            try
            {
                string _qry = "Proc_GetAssignFDelivery @Fk_FbSID=" + Fk_FbSID + ",@FK_FId=" + FId + "";
                List<MAddShippingInformation> _iresult = DBHelperDapper.DAGetDetailsInList<MAddShippingInformation>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MShowSHippingInfoToAssociate> DalShowShippingInfoToAsssciate(string _reqID)
        {
            try
            {
                string _qry = "GetFranchiseAssignOrder @Bid=" + _reqID + ",@FranchiseId=0,@TableName='ShowFranchiseWiseShipinginfo'";
                List<MShowSHippingInfoToAssociate> _iresult = DBHelperDapper.DAGetDetailsInList<MShowSHippingInfoToAssociate>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<MBindAssignOrdertoFranchise> DALGetAssignFranchOrdrInfoamtion(string _reqID)
        {
            try
            {
                string _qry = "Proc_GetAssignMemberOrder @Bid=" + _reqID + "";
                List<MBindAssignOrdertoFranchise> _iresult = DBHelperDapper.DAGetDetailsInList<MBindAssignOrdertoFranchise>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

         public static UpdateInvoicedateReponse DALUpdateInvoiceDate(int RequestId,string InvoiceDate)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@RequestId", RequestId);
                queryParameters.Add("@InvoiceDate", InvoiceDate);
                queryParameters.Add("@CreateBy", SessionManager.Fk_MemId);
                UpdateInvoicedateReponse _iresult = DBHelperDapper.DAAddAndReturnModel<UpdateInvoicedateReponse>("dbo.UpdaetInvoiceDate ", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
       

    }
}