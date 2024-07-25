using LifeOne.Models.FranchiseManagement.FDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FService
{
    public class ManageFranchiseStockService
    {
        DataTable dtFinalStockManage = null;
        public ManageFranchiseStockService()
        {
            dtFinalStockManage = new DataTable();
            dtFinalStockManage.Columns.AddRange(new DataColumn[6] {new DataColumn("InvoiceNo", typeof(string)),new DataColumn("PrdId", typeof(int)),
                            new DataColumn("ReqId", typeof(int)),
                            new DataColumn("FranchiseID", typeof(int)),
                            new DataColumn("DebitedQty",typeof(string)),
                new DataColumn("DebitedBy",typeof(string))});

        }


        /*Manage By PinCode*/

        public string ManageFransStockByPincode(long _RequestId)
        {

            string _stringXML = "";
            try
            {
                string _Searchby = "PinCode";
                DataSet _ds = DALManageStock.ReturnDatasetForUpateingFranStock(_RequestId, "PinCode");
                DataTable _dtrequestedQty = _ds.Tables[0];  //Customer requested qty

                if (_ds.Tables[1].Rows.Count > 0)
                {

                    DataTable _dtFranCurrentQty = _ds.Tables[1]; // //franchise current qrty
                    bool flag = false;
                    DataRow _drForFinalDatatable = null;

                    foreach (DataRow _itemRequstedQty in _dtrequestedQty.Rows)
                    {
                        string _prdidReq = _itemRequstedQty["PrdId"].ToString();
                        int _ReqQty = Convert.ToInt32(_itemRequstedQty["Quantity"]);
                        int _ReqId = Convert.ToInt32(_itemRequstedQty["ReqId"]);
                        string InvoiceNO = DateTime.Now.ToString("ddMMyyhh");

                        // Find out Franchise for stock debited
                        foreach (DataRow FranCurrentQty in _dtFranCurrentQty.Rows)
                        {
                            string _prdidFReq = FranCurrentQty["Fk_ProductId"].ToString();
                            string _PKFranchiseId = FranCurrentQty["PKFranchiseId"].ToString();
                            int _CurrentFQty = Convert.ToInt32(FranCurrentQty["CurrentQty"]);

                            if (_prdidReq == _prdidFReq && _CurrentFQty >= _ReqQty)
                            {
                                _drForFinalDatatable = dtFinalStockManage.NewRow();
                                _drForFinalDatatable["InvoiceNo"] = ReturnInvoiceNO(_ReqId, InvoiceNO, _PKFranchiseId);
                                _drForFinalDatatable["PrdId"] = _prdidReq;
                                _drForFinalDatatable["ReqId"] = _ReqId;
                                _drForFinalDatatable["FranchiseID"] = _PKFranchiseId;
                                _drForFinalDatatable["DebitedQty"] = _ReqQty;
                                _drForFinalDatatable["DebitedBy"] = _Searchby;
                                dtFinalStockManage.Rows.Add(_drForFinalDatatable);
                                _itemRequstedQty.Delete();
                                flag = true;
                                break;
                            }
                        }

                        if (flag) continue;

                    }
                }
                _dtrequestedQty.AcceptChanges();
                /*if the stock left will Find the things in Tehsil*/
                if (_dtrequestedQty.Rows.Count > 0)
                {
                    ManageFransStockByTehsil(_dtrequestedQty, _RequestId);
                }
                //return xml to save data on the database
                dtFinalStockManage.TableName = "XMLTable";
                StringWriter sw = new StringWriter();
                dtFinalStockManage.WriteXml(sw, XmlWriteMode.IgnoreSchema);
                _stringXML = sw.ToString();
                return _stringXML;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /*Manage By Tehsil*/
        public void ManageFransStockByTehsil(DataTable _dtCurrentStatusOfRequestedQty, long _RequestId)
        {
            try
            {
                string _Searchby = "Tehsil";
                DataSet _ds = DALManageStock.ReturnDatasetForUpateingFranStock(_RequestId, _Searchby);
                DataTable _dtFranCurrentQty = _ds.Tables[1]; // //fran current qrty for requsted product
                bool flag = false;

                DataRow _drForFinalDatatable = null;
                foreach (DataRow _itemRequstedQty in _dtCurrentStatusOfRequestedQty.Rows)
                {

                    string _prdidReq = _itemRequstedQty["PrdId"].ToString();
                    int _ReqQty = Convert.ToInt32(_itemRequstedQty["Quantity"]);
                    int _ReqId = Convert.ToInt32(_itemRequstedQty["ReqId"]);
                    string InvoiceNO = DateTime.Now.ToString("ddMMyyhh");
                    // Find out Franchise for stock debited
                    foreach (DataRow FranCurrentQty in _dtFranCurrentQty.Rows)
                    {
                        string _prdidFReq = FranCurrentQty["Fk_ProductId"].ToString();
                        string _PKFranchiseId = FranCurrentQty["PKFranchiseId"].ToString();
                        int _CurrentFQty = Convert.ToInt32(FranCurrentQty["CurrentQty"]);
                        if (_prdidReq == _prdidFReq && _CurrentFQty >= _ReqQty)
                        {
                            _drForFinalDatatable = dtFinalStockManage.NewRow();
                            _drForFinalDatatable["InvoiceNo"] = ReturnInvoiceNO(_ReqId, InvoiceNO, _PKFranchiseId);
                            _drForFinalDatatable["PrdId"] = _prdidReq;
                            _drForFinalDatatable["ReqId"] = _ReqId;
                            _drForFinalDatatable["FranchiseID"] = _PKFranchiseId;
                            _drForFinalDatatable["DebitedQty"] = _ReqQty;
                            _drForFinalDatatable["DebitedBy"] = _Searchby;
                            dtFinalStockManage.Rows.Add(_drForFinalDatatable);
                            _itemRequstedQty.Delete();
                            flag = true;
                            break;
                        }
                    }
                    if (flag) continue;
                }
                _dtCurrentStatusOfRequestedQty.AcceptChanges();
                /*if the stock left will Find the things with district*/
                if (_dtCurrentStatusOfRequestedQty.Rows.Count > 0)
                {
                    // district level chiking
                    ManageFransStockBydistrict(_dtCurrentStatusOfRequestedQty, _RequestId);
                    // ManageFransStockByHO(_dtCurrentStatusOfRequestedQty, _RequestId);



                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /*Manage By district*/
        public void ManageFransStockBydistrict(DataTable _dtCurrentStatusOfRequestedQty, long _RequestId)
        {
            try
            {
                string _Searchby = "District";
                DataSet _ds = DALManageStock.ReturnDatasetForUpateingFranStock(_RequestId, _Searchby);
                DataTable _dtFranCurrentQty = _ds.Tables[1]; // //fran current qrty for requsted product

                bool flag = false;
                DataRow _drForFinalDatatable = null;
                foreach (DataRow _itemRequstedQty in _dtCurrentStatusOfRequestedQty.Rows)
                {

                    string _prdidReq = _itemRequstedQty["PrdId"].ToString();
                    int _ReqQty = Convert.ToInt32(_itemRequstedQty["Quantity"]);
                    int _ReqId = Convert.ToInt32(_itemRequstedQty["ReqId"]);
                    string InvoiceNO = DateTime.Now.ToString("ddMMyyhh");
                    // Find out Franchise for stock debited
                    foreach (DataRow FranCurrentQty in _dtFranCurrentQty.Rows)
                    {
                        string _prdidFReq = FranCurrentQty["Fk_ProductId"].ToString();
                        string _PKFranchiseId = FranCurrentQty["PKFranchiseId"].ToString();
                        int _CurrentFQty = Convert.ToInt32(FranCurrentQty["CurrentQty"]);
                        if (_prdidReq == _prdidFReq && _CurrentFQty >= _ReqQty)
                        {
                            _drForFinalDatatable = dtFinalStockManage.NewRow();
                            _drForFinalDatatable["InvoiceNo"] = ReturnInvoiceNO(_ReqId, InvoiceNO, _PKFranchiseId);
                            _drForFinalDatatable["PrdId"] = _prdidReq;
                            _drForFinalDatatable["ReqId"] = _ReqId;
                            _drForFinalDatatable["FranchiseID"] = _PKFranchiseId;
                            _drForFinalDatatable["DebitedQty"] = _ReqQty;
                            _drForFinalDatatable["DebitedBy"] = _Searchby;
                            dtFinalStockManage.Rows.Add(_drForFinalDatatable);
                            _itemRequstedQty.Delete();
                            flag = true;
                            break;
                        }
                    }
                    if (flag) continue;
                }
                _dtCurrentStatusOfRequestedQty.AcceptChanges();
                /*if the stock left will Find the things with district*/
                if (_dtCurrentStatusOfRequestedQty.Rows.Count > 0)
                {
                    // zone level chiking
                    ManageFransStockByZone(_dtCurrentStatusOfRequestedQty, _RequestId);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /*Manage By Zone*/
        public void ManageFransStockByZone(DataTable _dtCurrentStatusOfRequestedQty, long _RequestId)
        {
            try
            {
                string _Searchby = "Zone";
                DataSet _ds = DALManageStock.ReturnDatasetForUpateingFranStock(_RequestId, _Searchby);
                DataTable _dtFranCurrentQty = _ds.Tables[1]; // //fran current qrty for requsted product
                bool flag = false;
                DataRow _drForFinalDatatable = null;
                foreach (DataRow _itemRequstedQty in _dtCurrentStatusOfRequestedQty.Rows)
                {

                    string _prdidReq = _itemRequstedQty["PrdId"].ToString();
                    int _ReqQty = Convert.ToInt32(_itemRequstedQty["Quantity"]);
                    int _ReqId = Convert.ToInt32(_itemRequstedQty["ReqId"]);
                    string InvoiceNO = DateTime.Now.ToString("ddMMyyhh");
                    // Find out Franchise for stock debited
                    foreach (DataRow FranCurrentQty in _dtFranCurrentQty.Rows)
                    {
                        string _prdidFReq = FranCurrentQty["Fk_ProductId"].ToString();
                        string _PKFranchiseId = FranCurrentQty["PKFranchiseId"].ToString();
                        int _CurrentFQty = Convert.ToInt32(FranCurrentQty["CurrentQty"]);
                        if (_prdidReq == _prdidFReq && _CurrentFQty >= _ReqQty)
                        {
                            _drForFinalDatatable = dtFinalStockManage.NewRow();
                            _drForFinalDatatable["InvoiceNo"] = ReturnInvoiceNO(_ReqId, InvoiceNO, _PKFranchiseId);
                            _drForFinalDatatable["PrdId"] = _prdidReq;
                            _drForFinalDatatable["ReqId"] = _ReqId;
                            _drForFinalDatatable["FranchiseID"] = _PKFranchiseId;
                            _drForFinalDatatable["DebitedQty"] = _ReqQty;
                            _drForFinalDatatable["DebitedBy"] = _Searchby;
                            dtFinalStockManage.Rows.Add(_drForFinalDatatable);
                            _itemRequstedQty.Delete();
                            flag = true;
                            break;
                        }
                        if (flag) continue;
                    }
                }
                _dtCurrentStatusOfRequestedQty.AcceptChanges();
                /*if the stock left will Find the things with district*/
                if (_dtCurrentStatusOfRequestedQty.Rows.Count > 0)
                {
                    // HO level chiking
                    ManageFransStockByHO(_dtCurrentStatusOfRequestedQty, _RequestId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*Manage By HO*/
        //in the case if we not found any franch. qty in the any of the serached...
        //will assign it to the HO
        public void ManageFransStockByHO(DataTable _dtCurrentStatusOfRequestedQty, long _RequestId)
        {
            try
            {
                string _Searchby = "HO";
                DataRow _drForFinalDatatable = null;
                foreach (DataRow _itemRequstedQty in _dtCurrentStatusOfRequestedQty.Rows)
                {

                    string _prdidReq = _itemRequstedQty["PrdId"].ToString();
                    int _ReqQty = Convert.ToInt32(_itemRequstedQty["Quantity"]);
                    int _ReqId = Convert.ToInt32(_itemRequstedQty["ReqId"]);
                    string InvoiceNO = DateTime.Now.ToString("ddMMyyhh");
                    // Find out Franchise for stock debited
                    _drForFinalDatatable = dtFinalStockManage.NewRow();
                    _drForFinalDatatable["InvoiceNo"] = ReturnInvoiceNO(_ReqId, InvoiceNO, "1000");
                    _drForFinalDatatable["PrdId"] = _prdidReq;
                    _drForFinalDatatable["ReqId"] = _ReqId;
                    _drForFinalDatatable["FranchiseID"] = "1000"; //1000 it's a HO ID..
                    _drForFinalDatatable["DebitedQty"] = _ReqQty;
                    _drForFinalDatatable["DebitedBy"] = _Searchby;
                    dtFinalStockManage.Rows.Add(_drForFinalDatatable);
                    _itemRequstedQty.Delete();
                    // break;
                }
                _dtCurrentStatusOfRequestedQty.AcceptChanges();
                /*if the stock left will Find the things with district*/
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string ReturnInvoiceNO(int _ReqId, string InvoiceNO, string _PKFranchiseId)
        {
            string _returnInvoiceNO = InvoiceNO + _PKFranchiseId + _ReqId.ToString();
            return _returnInvoiceNO;
        }

    }

}