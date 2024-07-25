using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LifeOne.Models.FranchiseManagement.FService
{
    public class ChildFranchiseService
    {
        ~ChildFranchiseService() { }
        ChildFranchiseResponseMaster _objResponseModel = new ChildFranchiseResponseMaster();
        public List<MChildFranchise> ChildChildFranchiseGetService(MChildFranchise _obj)
        {
            List<MChildFranchise> _objResponseModel = new List<MChildFranchise>();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALChildFranchise.GetChildFranchiseList(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ChildFranchiseResponseMaster ChildFranchiseSaveService(MChildFranchise _obj)
        {

            try
            {
                ChildFranchiseResponseMaster _objResponseModel = new ChildFranchiseResponseMaster();
                //decrypt app team reponse
                if (_obj.PKFranchiseRegistrationId != null)
                {
                    _objResponseModel = DALChildFranchise.UpdateChildFranchiseMaster(_obj);
                }
                else
                {
                    var password = "FamilyN123";
                    _obj.NormalPassword = password;
                    _obj.FranchiseType = "Franchise";
                    _obj.Fk_ParentID = Convert.ToString(SessionManager.Fk_MemId);
                    _obj.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
                    _objResponseModel = DALChildFranchise.Save(_obj);
                }


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }


        public MChildFranchise ChildFranchiseEditService(MChildFranchise _obj)
        {
            MChildFranchise _objResponseModel = new MChildFranchise();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALChildFranchise.GetChildFranchiseEdit(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ChildFranchiseResponseMaster ChildFranchiseDeleteService(string id)
        {
            ChildFranchiseResponseMaster _objResponseModel = new ChildFranchiseResponseMaster();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALChildFranchise.ChildFranchiseDelete(id);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public ChildFranchiseResponseMaster ChildFranchiseBlockunblockService(string id)
        {
            ChildFranchiseResponseMaster _objResponseModel = new ChildFranchiseResponseMaster();
            try
            {
                  long CreatedBy = SessionManager.Fk_MemId;
                //decrypt app team reponse
                _objResponseModel = DALChildFranchise.ChildFranchiseBlockUnblock(id, CreatedBy);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }


        public static MCustomerWalletBalnce GetCusetomerMemberWalletBalanceService(int Fk_MemId)
        {
            MCustomerWalletBalnce _objResponseModel = new MCustomerWalletBalnce();
            try
            {
                long CreatedBy = SessionManager.Fk_MemId;
                //decrypt app team reponse
                _objResponseModel = DALChildFranchise.GetCustomerWalletBalance(Fk_MemId);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }


        public List<MChildFranchise> ChildFranchiseGetService(MChildFranchise _obj)
        {
            List<MChildFranchise> _objResponseModel = new List<MChildFranchise>();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALChildFranchise.GetChildChildFranchiseList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}