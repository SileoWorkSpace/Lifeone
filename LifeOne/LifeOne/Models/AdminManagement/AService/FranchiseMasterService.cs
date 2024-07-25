using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LifeOne.Models.AdminManagement.AService
{
    public class FranchiseMasterService
    {
        ~FranchiseMasterService() { }
        FranchiseResponseMaster _objResponseModel = new FranchiseResponseMaster();
        public List<MAdminFranchise> FranchiseMasterGetService(MAdminFranchise _obj)
        {
            List<MAdminFranchise> _objResponseModel = new List<MAdminFranchise>();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALFranchiseMaster.GetFranchiseList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public FranchiseResponseMaster FranchiseMasterSaveService(MAdminFranchise _obj)
        {

            try
            {
                FranchiseResponseMaster _objResponseModel = new FranchiseResponseMaster();
                //decrypt app team reponse
                if (_obj.PKFranchiseRegistrationId != null)
                {
                    _objResponseModel = DALFranchiseMaster.UpdateFranchiseMaster(_obj);
                }
                else
                {
                    var password = "LifeOne123";
                    _obj.NormalPassword = password;
                    _obj.FranchiseType = "Franchise";
                    _obj.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
                    _objResponseModel = DALFranchiseMaster.Save(_obj);
                }


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }


        public MAdminFranchise FranchiseMasterEditService(MAdminFranchise _obj)
        {
            MAdminFranchise _objResponseModel = new MAdminFranchise();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALFranchiseMaster.GetFranchiseEdit(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public FranchiseResponseMaster FranchiseMasterDeleteService(string id)
        {
            FranchiseResponseMaster _objResponseModel = new FranchiseResponseMaster();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALFranchiseMaster.FranchiseDelete(id);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public FranchiseResponseMaster FranchiseMasterBlockUnblockService(string id)
        {
            FranchiseResponseMaster _objResponseModel = new FranchiseResponseMaster();
            try
            {

                //decrypt app team reponse
                long CreatedBY = SessionManager.Fk_MemId;
                _objResponseModel = DALFranchiseMaster.FranchiseBlockUnblock(id, CreatedBY);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public List<MAdminFranchise> ChildFranchiseGetService(MAdminFranchise _obj)
        {
            List<MAdminFranchise> _objResponseModel = new List<MAdminFranchise>();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALFranchiseMaster.GetFranchiseChildList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public MAdminFranchise FranchiseGetDetailsByLoginId(MAdminFranchise _obj)
        {
            MAdminFranchise _objResponseModel = new MAdminFranchise();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALFranchiseMaster.GetFranchiseDetailsByLoginId(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}