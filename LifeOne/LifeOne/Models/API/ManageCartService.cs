using LifeOne.Models.API.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class ManageCartService
    {
        DataTable dtFinalCart = null;
        public ManageCartService()
        {
            dtFinalCart = new DataTable();
            dtFinalCart.Columns.AddRange(new DataColumn[8] {new DataColumn("PrdId", typeof(int)),
                        new DataColumn("PrdQty", typeof(int)),
                        new DataColumn("ProductSKU", typeof(string)),
                        new DataColumn("SizeKeyID", typeof(int)),
                        new DataColumn("option_id", typeof(int)),
                        new DataColumn("option_value", typeof(int)),
                        new DataColumn("RequestedWeight", typeof(decimal)),
                        new DataColumn("DynamicGeneratedWeight", typeof(decimal))});

        }

        //Get 1st chk equal product ml or same type product just like Boss
        //thn chk less thn Products details --
        //1 A 100 ml
        //2 B 200 ml
        //if it done all that good Otherwise get the 1st prodcut which will make the ml quantity 0
        //-------------------------

        public DataTable CreateCartProduct(string _area, string _selectedPrdCod, string _laungvage, string _Date)
        {
            bool _continueLoopOrNOt = false;
            try
            {
                DataSet _ds = DALDynamicCartService.ReturnProductMasterAndCartInfromation(_area, _selectedPrdCod, _laungvage, _Date);
                DataTable _dtRequiredProduct = _ds.Tables[0];  //Customer requested qty
                DataTable _dtProductdtls = _ds.Tables[1];  //Customer requested qty
                if (_ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow FranCurrentQty in _dtRequiredProduct.Rows)
                    {
                        string _prdidFReq = FranCurrentQty["ProductId"].ToString();
                        decimal _pkProductQtyNeeded = Convert.ToDecimal(FranCurrentQty["ProductQty"]);

                        DataTable _dtGetSelectedProduct = _dtProductdtls.Select("ProductId = " + _prdidFReq + "").CopyToDataTable();//Get Selected Product.
                                                                                                                                    //Chk if the requested Qty is same in the existing product

                        DataTable _dtEqualSelectedQty = null;
                        if (_dtGetSelectedProduct.Select("ProductQty = " + _pkProductQtyNeeded + "").Length > 0)
                        {
                            _dtEqualSelectedQty = _dtGetSelectedProduct.Select("ProductQty = " + _pkProductQtyNeeded + "").CopyToDataTable();//Get Equal
                            if (_dtEqualSelectedQty.Rows.Count > 0)
                            {
                                DataRow dtFinalCartRows = dtFinalCart.NewRow();
                                dtFinalCartRows["PrdId"] = _prdidFReq;
                                dtFinalCartRows["PrdQty"] = 1; //in case produtc is exsist thn qty will 
                                dtFinalCartRows["ProductSKU"] = _dtEqualSelectedQty.Rows[0].ItemArray[4];//  ProductSKU
                                dtFinalCartRows["SizeKeyID"] = Convert.ToInt32(_dtEqualSelectedQty.Rows[0].ItemArray[1]); //KeyId

                                dtFinalCartRows["option_id"] = Convert.ToInt32(_dtEqualSelectedQty.Rows[0].ItemArray[6]); //option_id
                                dtFinalCartRows["option_value"] = Convert.ToInt32(_dtEqualSelectedQty.Rows[0].ItemArray[5]); //option_value

                                dtFinalCartRows["RequestedWeight"] = _pkProductQtyNeeded;
                                dtFinalCartRows["DynamicGeneratedWeight"] = _dtEqualSelectedQty.Rows[0].ItemArray[2]; //ProductQty -how many ML needed
                                dtFinalCart.Rows.Add(dtFinalCartRows);
                                _continueLoopOrNOt = true;

                            }
                        }


                        if (!_continueLoopOrNOt)
                        {
                            decimal _pkProductQtyNeededMin = 0.00m;
                            //Chk how many products are there which are minimum from requested one
                            if (_dtGetSelectedProduct.Select("ProductQty < " + _pkProductQtyNeeded + "").Length > 0)
                            {
                                DataTable _dtMinSelectedQty = _dtGetSelectedProduct.Select("ProductQty < " + _pkProductQtyNeeded + "").CopyToDataTable();//Get Min Qty Product
                                if (_dtMinSelectedQty.Rows.Count > 0)
                                {
                                    string _prdIDs = "", _prdQty = "", _pKeysizeID = "", _pSKUID = "", _optionid = "", _optionvaluev = "";
                                    foreach (DataRow _chkitem in _dtMinSelectedQty.Rows)
                                    {

                                        _pkProductQtyNeededMin = _pkProductQtyNeededMin + Convert.ToDecimal(_chkitem["ProductQty"]);

                                        //chk if the multiple product qty sum is qual to 
                                        //requested QTy thn will get the all product ID here

                                        _prdIDs = _prdIDs + "," + _chkitem["ProductId"].ToString();
                                        _prdQty = _prdQty + "," + _chkitem["ProductQty"].ToString();
                                        _pKeysizeID = _pKeysizeID + "," + _chkitem["KeyId"].ToString();
                                        _pSKUID = _pSKUID + "," + _chkitem["ProductSKU"].ToString();

                                        _optionid = _optionid + "," + _chkitem["option_id"].ToString();
                                        _optionvaluev = _optionvaluev + "," + _chkitem["option_value"].ToString();



                                        if (_pkProductQtyNeededMin == _pkProductQtyNeeded)
                                        {
                                            string[] _strProductArray = _prdIDs.Split(',');
                                            string[] _strProductQty = _prdQty.Split(',');
                                            string[] _str_pKeysizeID = _pKeysizeID.Split(',');
                                            string[] _str_pSKUID = _pSKUID.Split(',');


                                            string[] _str_optionid = _optionid.Split(',');
                                            string[] _str_optionvaluev = _optionvaluev.Split(',');



                                            for (var i = 0; i < _strProductArray.Length; i++)
                                            {
                                                var prdId = _strProductArray[i];
                                                var _Qty = _strProductQty[i];
                                                var _KeySizeID = _str_pKeysizeID[i];
                                                var _str_pSKUIDVar = _str_pSKUID[i];


                                                var optionid_ = _str_optionid[i];
                                                var _str_optionvaluev_ = _str_optionvaluev[i];

                                                if (prdId != "")
                                                {
                                                    // work with item here
                                                    DataRow dtFinalCartRows = dtFinalCart.NewRow();
                                                    dtFinalCartRows["PrdId"] = prdId;
                                                    dtFinalCartRows["PrdQty"] = 1; //in case produtc is exsist thn qty will 
                                                    dtFinalCartRows["ProductSKU"] = _str_pSKUIDVar;
                                                    dtFinalCartRows["SizeKeyID"] = _KeySizeID;

                                                    dtFinalCartRows["option_id"] = optionid_; //option_id
                                                    dtFinalCartRows["option_value"] = _str_optionvaluev_; //option_value


                                                    dtFinalCartRows["RequestedWeight"] = _pkProductQtyNeeded;
                                                    dtFinalCartRows["DynamicGeneratedWeight"] = _Qty;
                                                    dtFinalCart.Rows.Add(dtFinalCartRows);
                                                }

                                            }
                                            _continueLoopOrNOt = true;
                                        }
                                    }
                                }
                            }

                        }


                        if (!_continueLoopOrNOt)
                        {
                            //Chk how many products are there which are maximum from requested one
                            if (_dtGetSelectedProduct.Select("ProductQty > " + _pkProductQtyNeeded + "").Length > 0)
                            {

                                bool _readOnlyOntime = false;
                                DataTable _dtMaxSelectedQty = _dtGetSelectedProduct.Select("ProductQty > " + _pkProductQtyNeeded + "").CopyToDataTable();//Get Max Qty Product                     
                                foreach (DataRow item in _dtMaxSelectedQty.Rows)
                                {
                                    string _CureentprdidFReq = item["ProductId"].ToString();
                                    string _ProductSKU = item["ProductSKU"].ToString();
                                    decimal _CurrentQtryNeeded = Convert.ToDecimal(item["ProductQty"]);
                                    int _CurrKey = Convert.ToInt32(item["KeyId"]);
                                    int optionid_ = Convert.ToInt32(item["option_id"]);
                                    int _str_optionvaluev_ = Convert.ToInt32(item["option_value"]);

                                    if (!_readOnlyOntime && _CurrentQtryNeeded > _pkProductQtyNeeded)
                                    {
                                        DataRow dtFinalCartRows = dtFinalCart.NewRow();
                                        dtFinalCartRows["PrdId"] = _CureentprdidFReq;
                                        dtFinalCartRows["PrdQty"] = 1; //in case produtc is exsist thn qty will 
                                        dtFinalCartRows["ProductSKU"] = _ProductSKU;
                                        dtFinalCartRows["SizeKeyID"] = _CurrKey;
                                        dtFinalCartRows["option_id"] = optionid_; //option_id
                                        dtFinalCartRows["option_value"] = _str_optionvaluev_; //option_value

                                        dtFinalCartRows["RequestedWeight"] = _pkProductQtyNeeded;
                                        dtFinalCartRows["DynamicGeneratedWeight"] = _CurrentQtryNeeded;
                                        dtFinalCart.Rows.Add(dtFinalCartRows);
                                        _readOnlyOntime = true;
                                        _continueLoopOrNOt = true;
                                    }

                                }
                                _readOnlyOntime = false;
                            }
                        }



                        // Now system will chk if it's is there qty addition full fill the request
                        if (!_continueLoopOrNOt)
                        {

                            if (_dtProductdtls.Select("ProductId = " + _prdidFReq + "").Length > 0)
                            {
                                bool _ralonlyQty = false;
                                DataTable _dtGetSelectedProductQty = _dtProductdtls.Select("ProductId = " + _prdidFReq + "").CopyToDataTable();//Get Se
                                foreach (DataRow itemQty in _dtGetSelectedProductQty.Rows)
                                {
                                    var getProductQtyMl = Convert.ToDecimal(itemQty["ProductQty"]);
                                    decimal GetNewvalye = _pkProductQtyNeeded / getProductQtyMl;
                                    int x = Convert.ToInt32(Math.Round(GetNewvalye)); 
                                    //chk any qty left here      

                                    if (x == (int)x && !_ralonlyQty && x > 0)
                                    {
                                        DataRow dtFinalCartRows = dtFinalCart.NewRow();
                                        dtFinalCartRows["PrdId"] = _prdidFReq;
                                        dtFinalCartRows["PrdQty"] = Convert.ToInt32(GetNewvalye); //in case produtc is exsist thn qty will 
                                        dtFinalCartRows["ProductSKU"] = itemQty["ProductSKU"];//  ProductSKU
                                        dtFinalCartRows["SizeKeyID"] = itemQty["KeyID"]; //KeyId
                                        dtFinalCartRows["option_id"] = Convert.ToInt32(itemQty["option_id"]); //option_id
                                        dtFinalCartRows["option_value"] = Convert.ToInt32(itemQty["option_value"]); //option_value
                                        dtFinalCartRows["RequestedWeight"] = _pkProductQtyNeeded;
                                        dtFinalCartRows["DynamicGeneratedWeight"] = itemQty["ProductQty"]; //ProductQty -how many ML needed
                                        dtFinalCart.Rows.Add(dtFinalCartRows);
                                        _ralonlyQty = true;
                                        _continueLoopOrNOt = true;

                                        // chk if value is still left thn we calculate value from here 
                                        // if the value is successfully devided no need to calculate again
                                        decimal newValue = _pkProductQtyNeeded - getProductQtyMl * x;
                                        if (newValue > 0)
                                        {
                                            
                                            DataView dv = new DataView(_dtGetSelectedProductQty);
                                            dv.Sort = "ProductQty";
                                            _dtGetSelectedProductQty = dv.ToTable();

                                            bool chkvalueodneornot = false;
                                            foreach (DataRow item in _dtGetSelectedProductQty.Rows)
                                            {
                                                decimal ExsitingproductQty = Convert.ToDecimal(item["ProductQty"]);
                                                if (ExsitingproductQty >= newValue && !chkvalueodneornot)
                                                {
                                                    DataRow dtFinalCartRowsNew = dtFinalCart.NewRow();
                                                    dtFinalCartRowsNew["PrdId"] = _prdidFReq;
                                                    dtFinalCartRowsNew["PrdQty"] = 1; //in case produtc is exsist thn qty will 
                                                    dtFinalCartRowsNew["ProductSKU"] = item["ProductSKU"];//  ProductSKU
                                                    dtFinalCartRowsNew["SizeKeyID"] = item["KeyID"]; //KeyId
                                                    dtFinalCartRowsNew["option_id"] = Convert.ToInt32(item["option_id"]); //option_id
                                                    dtFinalCartRowsNew["option_value"] = Convert.ToInt32(item["option_value"]); //option_value
                                                    dtFinalCartRowsNew["RequestedWeight"] = newValue;
                                                    dtFinalCartRowsNew["DynamicGeneratedWeight"] = item["ProductQty"]; //ProductQty -how many ML needed
                                                    dtFinalCart.Rows.Add(dtFinalCartRowsNew);
                                                    chkvalueodneornot = true;
                                                }
                                            }
                                            chkvalueodneornot = false;
                                        }
                                    }
                                  
                                }
                                _ralonlyQty = false;
                            }
                        }

                      



                        _continueLoopOrNOt = false;
                    }

                }

                //dtFinalCart.TableName = "XMLTable";
                //StringWriter sw = new StringWriter();
                //dtFinalCart.WriteXml(sw, XmlWriteMode.IgnoreSchema);
                //_stringXML = sw.ToString();
                return dtFinalCart;

            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}