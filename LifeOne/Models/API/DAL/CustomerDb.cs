using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

using static LifeOne.Models.ShoppingRequest;

namespace LifeOne.Models.API.DAL
{
    public class CustomerDb : DbContext
    {
        public CustomerDb() : base("name=ConnectionString")
        {
            this.Database.CommandTimeout = 180;
        }
        public ResultSet RegistrationByReferral(Registration model)
        {
            try
            {

                var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@InvitedBy",Value=model.InvitedBy??string.Empty },
                new SqlParameter { ParameterName="@Leg",Value=model.Leg??string.Empty },
                new SqlParameter { ParameterName="@FirstName",Value=model.FirstName ??string.Empty},
                new SqlParameter { ParameterName="@LastName",Value=model.LastName ??string.Empty},
                new SqlParameter { ParameterName="@MobileNo",Value=model.MobileNo??string.Empty },
                new SqlParameter { ParameterName="@Password",Value=model.Password ??string.Empty},
                new SqlParameter { ParameterName="@Email",Value=model.Email ??string.Empty},
                new SqlParameter { ParameterName="@Pincode",Value=model.Pincode ??string.Empty},
                new SqlParameter { ParameterName="@State",Value=model.State??string.Empty },
                new SqlParameter { ParameterName="@City",Value=model.City ??string.Empty},
                new SqlParameter { ParameterName="@Tehsil",Value=model.Tehsil ??string.Empty},
                new SqlParameter { ParameterName="@Iskyc",Value=model.Iskyc },
                new SqlParameter { ParameterName="@AadharNo",Value=model.AadharNo ??string.Empty},
                new SqlParameter { ParameterName="@Normalpassword",Value=model.Normalpassword ??string.Empty},
                new SqlParameter { ParameterName="@Address",Value=model.Address ??string.Empty},
                new SqlParameter { ParameterName="@FatherName",Value=model.FatherName ??string.Empty},
                new SqlParameter { ParameterName="@DOB",Value=model.DOB ??string.Empty},
                new SqlParameter { ParameterName="@Gender",Value=model.Gender ??string.Empty},
                new SqlParameter { ParameterName="@YearOfBirth",Value=model.YearOfBirth ??string.Empty},
                new SqlParameter { ParameterName="@Type",Value=model.Type ??string.Empty},
                new SqlParameter { ParameterName="@PanNo",Value=model.PanNo ??string.Empty},
                new SqlParameter { ParameterName="@VoterId",Value=model.VoterId ??string.Empty},
                new SqlParameter { ParameterName="@PanImage",Value=model.PanImage ??string.Empty},
                new SqlParameter { ParameterName="@VoterIdImage",Value=model.VoterIdImage ??string.Empty},
              //--------------------Added for KYC--------------------------------
                new SqlParameter { ParameterName="@GSTNo",Value=model.GSTNo ??string.Empty},
                new SqlParameter { ParameterName="@GSTImage",Value=model.GSTImage ??string.Empty},
                new SqlParameter { ParameterName="@UdyogAadharNo",Value=model.UdyogAadharNo ??string.Empty},
                new SqlParameter { ParameterName="@UdyogAadharImage",Value=model.UdyogAadharImage ??string.Empty},
                new SqlParameter { ParameterName="@CINNo",Value=model.CINNo ??string.Empty},
                new SqlParameter { ParameterName="@CINImage",Value=model.CINImage ??string.Empty},
                new SqlParameter { ParameterName="@AccountType",Value=model.AccountType ??string.Empty},

            };

                var _proc = @"RegisterationByReferal @InvitedBy,@FirstName,@LastName,@MobileNo,@Password,@Email,@Pincode,@State,@City,@Tehsil,@Iskyc,@AadharNo,@Normalpassword,@Address,@FatherName,@DOB,@Gender,@YearOfBirth,@Type,@PanNo,@VoterId,@PanImage,@VoterIdImage,@GSTNo,@GSTImage,@UdyogAadharNo,@UdyogAadharImage,@CINNo,@CINImage,@AccountType";
                var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
                return res;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ResultSet WebRegistrationByReferral(Registration model)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@InvitedBy", model.InvitedBy ?? string.Empty);
                queryParameters.Add("@Leg", model.Leg ?? string.Empty);

                queryParameters.Add("@FirstName", model.FirstName ?? string.Empty);
                queryParameters.Add("@LastName", model.LastName ?? string.Empty);
                queryParameters.Add("@MobileNo", model.MobileNo ?? string.Empty);
                queryParameters.Add("@Password", model.Password ?? string.Empty);
                queryParameters.Add("@Email", model.Email ?? string.Empty);
                queryParameters.Add("@Pincode", model.Pincode ?? string.Empty);
                queryParameters.Add("@State", model.State ?? string.Empty);
                queryParameters.Add("@City", model.City ?? string.Empty);
                queryParameters.Add("@Tehsil", model.Tehsil ?? string.Empty);
                queryParameters.Add("@Iskyc", model.Iskyc);
                queryParameters.Add("@AadharNo", model.AadharNo ?? string.Empty);
                queryParameters.Add("@Normalpassword", model.Normalpassword ?? string.Empty);
                queryParameters.Add("@Address", model.Address ?? string.Empty);
                queryParameters.Add("@FatherName", model.FatherName ?? string.Empty);
                // queryParameters.Add("@DOB", model.DOB ?? string.Empty);
                queryParameters.Add("@DOB", string.IsNullOrEmpty(model.DOB) ? null : model.DOB);
                queryParameters.Add("@Gender", model.Gender ?? string.Empty);
                queryParameters.Add("@YearOfBirth", model.YearOfBirth ?? string.Empty);
                queryParameters.Add("@Type", model.Type ?? string.Empty);
                queryParameters.Add("@PanNo", model.PanNo ?? string.Empty);
                queryParameters.Add("@VoterId", model.VoterId ?? string.Empty);
                queryParameters.Add("@PanImage", model.PanImage ?? string.Empty);
                queryParameters.Add("@VoterIdImage", model.VoterIdImage ?? string.Empty);

                queryParameters.Add("@GSTNo", model.GSTNo ?? string.Empty);
                queryParameters.Add("@GSTImage", model.GSTImage ?? string.Empty);
                queryParameters.Add("@UdyogAadharNo", model.UdyogAadharNo ?? string.Empty);
                queryParameters.Add("@UdyogAadharImage", model.UdyogAadharImage ?? string.Empty);
                queryParameters.Add("@CINNo", model.CINNo ?? string.Empty);
                queryParameters.Add("@CINImage", model.CINImage ?? string.Empty);
                queryParameters.Add("@AccountType", model.AccountType ?? string.Empty);
                queryParameters.Add("@PlaceUnderId", model.PlaceUnderId ?? string.Empty);
                queryParameters.Add("@husbandOrFatherNamesuffix", model.husbandOrFatherNamesuffix ?? string.Empty);


                var _proc = DBHelperDapper.DAAddAndReturnModel<ResultSet>("RegisterationByReferal", queryParameters);
                return _proc;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ResultSet DALPinManagement(MPinManagement model)
        {
            var sqlparams = new SqlParameter[] {
             
              //--------------------Added for KYC--------------------------------
                new SqlParameter { ParameterName="@UserPin",Value=model.UserPin ??string.Empty},
                new SqlParameter { ParameterName="@Fk_MemId",Value=model.Fk_MemId},
                new SqlParameter { ParameterName="@Prod",Value=model.Prod},
            };
            var _proc = @"pinmanagement @UserPin,@Fk_MemId,@Prod";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }





        public ResultSet RegistrationByReferralKaryon(Registration model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@InvitedBy",Value=model.SponsorBy??string.Empty },
                new SqlParameter { ParameterName="@FirstName",Value=model.FirstName ??string.Empty},
                new SqlParameter { ParameterName="@LastName",Value=model.LastName ??string.Empty},
                new SqlParameter { ParameterName="@MobileNo",Value=model.MobileNo??string.Empty },
                new SqlParameter { ParameterName="@Password",Value=model.Password ??string.Empty},
                new SqlParameter { ParameterName="@Email",Value=model.Email ??string.Empty},
                new SqlParameter { ParameterName="@Pincode",Value=model.Pincode ??string.Empty},
                new SqlParameter { ParameterName="@State",Value=model.State??string.Empty },
                new SqlParameter { ParameterName="@City",Value=model.City ??string.Empty},
                new SqlParameter { ParameterName="@Tehsil",Value=model.Tehsil ??string.Empty},
                new SqlParameter { ParameterName="@Iskyc",Value=model.Iskyc },
                new SqlParameter { ParameterName="@AadharNo",Value=model.AadharNo ??string.Empty},
                new SqlParameter { ParameterName="@Normalpassword",Value=model.Normalpassword ??string.Empty},
                new SqlParameter { ParameterName="@Address",Value=model.Address ??string.Empty},
                new SqlParameter { ParameterName="@FatherName",Value=model.FatherName ??string.Empty},
                new SqlParameter { ParameterName="@DOB",Value=model.DOB ??string.Empty},
                new SqlParameter { ParameterName="@Gender",Value=model.Gender ??string.Empty},
                new SqlParameter { ParameterName="@YearOfBirth",Value=model.YearOfBirth ??string.Empty},
                new SqlParameter { ParameterName="@KaryonLoginId",Value=model.KaryonLoginId ??string.Empty}
            };
            var _proc = @"RegisterationByReferalKaryon @InvitedBy,@FirstName,@LastName,@MobileNo,@Password,@Email,@Pincode,@State,@City,@Tehsil,@Iskyc,@AadharNo,@Normalpassword,@Address,@FatherName,@DOB,@Gender,@YearOfBirth,@KaryonLoginId";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public ResultSet KYCAccountverify(Registration model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@FirstName",Value=model.FirstName ??string.Empty},
                new SqlParameter { ParameterName="@LastName",Value=model.LastName ??string.Empty},
                new SqlParameter { ParameterName="@MobileNo",Value=model.MobileNo??string.Empty },
                new SqlParameter { ParameterName="@Password",Value=model.Password ??string.Empty},
                new SqlParameter { ParameterName="@Email",Value=model.Email ??string.Empty},
                new SqlParameter { ParameterName="@Pincode",Value=model.Pincode ??string.Empty},
                new SqlParameter { ParameterName="@State",Value=model.State??string.Empty },
                new SqlParameter { ParameterName="@City",Value=model.City ??string.Empty},
                new SqlParameter { ParameterName="@Tehsil",Value=model.Tehsil ??string.Empty},
                new SqlParameter { ParameterName="@Iskyc",Value=model.Iskyc },
                new SqlParameter { ParameterName="@AadharNo",Value=model.AadharNo ??string.Empty},
                new SqlParameter { ParameterName="@Normalpassword",Value=model.Normalpassword ??string.Empty}  ,
                new SqlParameter { ParameterName="@Address",Value=model.Address ??string.Empty},
                new SqlParameter { ParameterName="@FatherName",Value=model.FatherName ??string.Empty},
                new SqlParameter { ParameterName="@DOB",Value=model.DOB ??string.Empty},
                new SqlParameter { ParameterName="@Gender",Value=model.Gender ??string.Empty},
                new SqlParameter { ParameterName="@YearOfBirth",Value=model.YearOfBirth ??string.Empty}
            };
            var _proc = @"KYCUPdateByReferal @FirstName,@LastName,@MobileNo,@Password,@Email,@Pincode,@State,@City,@Tehsil,@Iskyc,@AadharNo,@Normalpassword,@Address,@FatherName,@DOB,@Gender,@YearOfBirth";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public ResultSet KYCAccountverifyV2(Registration model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@Fk_memId",Value=model.Fk_memId},
                new SqlParameter { ParameterName="@FirstName",Value=model.FirstName ??string.Empty},
                new SqlParameter { ParameterName="@LastName",Value=model.LastName ??string.Empty},
                new SqlParameter { ParameterName="@MobileNo",Value=model.MobileNo??string.Empty },
                new SqlParameter { ParameterName="@Password",Value=model.Password ??string.Empty},
                new SqlParameter { ParameterName="@Email",Value=model.Email ??string.Empty},
                new SqlParameter { ParameterName="@Pincode",Value=model.Pincode ??string.Empty},
                new SqlParameter { ParameterName="@State",Value=model.State??string.Empty },
                new SqlParameter { ParameterName="@City",Value=model.City ??string.Empty},
                new SqlParameter { ParameterName="@Tehsil",Value=model.Tehsil ??string.Empty},
                new SqlParameter { ParameterName="@Iskyc",Value=model.Iskyc },
                new SqlParameter { ParameterName="@AadharNo",Value=model.AadharNo ??string.Empty},
                new SqlParameter { ParameterName="@Normalpassword",Value=model.Normalpassword ??string.Empty}  ,
                new SqlParameter { ParameterName="@Address",Value=model.Address ??string.Empty},
                new SqlParameter { ParameterName="@FatherName",Value=model.FatherName ??string.Empty},
                new SqlParameter { ParameterName="@DOB",Value=model.DOB ??string.Empty},
                new SqlParameter { ParameterName="@Gender",Value=model.Gender ??string.Empty},
                new SqlParameter { ParameterName="@YearOfBirth",Value=model.YearOfBirth ??string.Empty}
            };
            var _proc = @"KYCUPdateByReferal_V2 @Fk_memId,@FirstName,@LastName,@MobileNo,@Password,@Email,@Pincode,@State,@City,@Tehsil,@Iskyc,@AadharNo,@Normalpassword,@Address,@FatherName,@DOB,@Gender,@YearOfBirth";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public LoginDetails GetLogin(Login model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@LoginId",Value=model.LoginId ??string.Empty},
                new SqlParameter { ParameterName="@Password",Value=model.Password ??string.Empty},
                new SqlParameter { ParameterName="@AndroidId",Value=model.AndroidId ??string.Empty },
                new SqlParameter { ParameterName="@DeviceId",Value=model.DeviceId??string.Empty },
                new SqlParameter { ParameterName="@DeviceType",Value=model.DeviceType??string.Empty },
                new SqlParameter { ParameterName="@AppVersion",Value=model.AppVersion??string.Empty }
            };
            var _proc = @"Proc_CheckLogin @LoginId,@password,@androidId,@deviceId,@DeviceType,@AppVersion";
            var res = this.Database.SqlQuery<LoginDetails>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public ResultSet WalletRequest(WalletJsonRequest model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@paymentdate",Value=model.paymentdate ??string.Empty},
                new SqlParameter { ParameterName="@paymentmode",Value=model.paymentmode ??string.Empty},
                new SqlParameter { ParameterName="@transactionId",Value=model.transactionId ??string.Empty },
                new SqlParameter { ParameterName="@amount",Value=model.amount },
                new SqlParameter { ParameterName="@filepath",Value=model.filepath??string.Empty },
                new SqlParameter { ParameterName="@memberId",Value=model.memberId }
            };
            var _proc = @"Proc_WalletRequest @paymentdate,@paymentmode,@transactionId,@amount,@filepath,@memberId";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public ResultSet DebitWallet(WalletActionRequest model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@type",Value=model.type ??string.Empty},
                new SqlParameter { ParameterName="@transactionId",Value=model.transactionId ??string.Empty},
                new SqlParameter { ParameterName="@transactiondate",Value=model.transactiondate ??string.Empty },
                new SqlParameter { ParameterName="@CompAmount",Value=model.mainamount },
                new SqlParameter { ParameterName="@cashbackAmount",Value=model.walletamount},
                new SqlParameter { ParameterName="@memberId",Value=model.memberId },
                 new SqlParameter { ParameterName="@paymentgatewayamount",Value=model.paymentgatewayamount}
            };
            var _proc = @"Proc_WalletCreditDebit @type,@transactionId,@transactiondate,@CompAmount,@cashbackAmount,@memberId,@paymentgatewayamount";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public DetailsForOtp GenerateOTP(OTP model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@Fk_MemId",Value=model.Fk_memId },
                new SqlParameter { ParameterName="@AndroidId",Value=model.AndroidId ??string.Empty},
                new SqlParameter { ParameterName="@DeviceId",Value=model.DeviceId ??string.Empty},
                new SqlParameter { ParameterName="@ProcId",Value=model.ProcId },
                new SqlParameter { ParameterName="@OTPNo",Value=model.Otpno },
                new SqlParameter { ParameterName="@data",Value=model.Data??string.Empty },
                new SqlParameter { ParameterName="@Mobile",Value=model.Mobile??string.Empty },
                new SqlParameter { ParameterName="@purpose",Value=model.Purpose??string.Empty }
            };
            var _proc = @"GenerateOTPForMultipleAccount @Fk_MemId,@AndroidId,@DeviceId,@ProcId,@OTPNo,@data,@Mobile,@purpose";
            var res = this.Database.SqlQuery<DetailsForOtp>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        
        public ResponseReferalMobile GetReferalMobile(ReferalMobile model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@ReferalMobileNo",Value=model.RefMobile??string.Empty },
                new SqlParameter { ParameterName="@Email",Value=model.Email ??string.Empty }
            };
            var _proc = @"GetReferalMobileName @ReferalMobileNo,@Email";
            var res = this.Database.SqlQuery<ResponseReferalMobile>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public ResponseReferalMobile GetReferaldetails(ReferalDetails model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@InviteCode",Value=model.InviteCode ??string.Empty}
            };
            var _proc = @"GetRefferalDetails @InviteCode";
            var res = this.Database.SqlQuery<ResponseReferalMobile>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public MChkEmailExist CheckEmail(ReferalMobile obj)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter {ParameterName="@email",Value=obj.Email??string.Empty},
            };
            var _proc = @"Checkemail @email";
            var res = this.Database.SqlQuery<MChkEmailExist>(_proc, sqlparams).SingleOrDefault();
            return res;

        }
        /*Changed*/
        public ApiResponseForgetPassword AssociateForgotPassword(ForgotPassword model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@LoginId",Value=model.LoginId ??string.Empty},
                 new SqlParameter { ParameterName="@MobileNo",Value=model.Mobile??string.Empty }
            };
            var _proc = @"ForgetPasswordRecoveryNew_Api @LoginId,@MobileNo";
            var res = this.Database.SqlQuery<ApiResponseForgetPassword>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public APIResponseChangePassword Changepassword(ChangePassword model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@Fk_MemID",Value=Convert.ToInt64(model.Fk_MemId) },
                new SqlParameter { ParameterName="@Mobile",Value=model.Mobile ??string.Empty},
                new SqlParameter { ParameterName="@OldPassword",Value=model.OldPassword??string.Empty },
                new SqlParameter { ParameterName="@NormalPassword",Value=model.NormalPassword??string.Empty },
                new SqlParameter { ParameterName="@NewPassword",Value=model.NewPassword ??string.Empty},
                new SqlParameter { ParameterName="@UpdatedBy",Value=Convert.ToInt64(model.UpdatedBy)},
                new SqlParameter { ParameterName="@Formname",Value=model.Formname??string.Empty },
            };
            var _proc = @"webMemberChangePassword @Fk_MemID,@Mobile,@OldPassword,@NewPassword,@UpdatedBy,@NormalPassword,@Formname";
            var res = this.Database.SqlQuery<APIResponseChangePassword>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public UserProfile GetUserProfile(long memberId)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@Fk_memId",Value=memberId}
            };
            var _proc = @"Proc_GetProfileDetail @Fk_memId";
            var res = this.Database.SqlQuery<UserProfile>(_proc, sqlparams).FirstOrDefault();
            return res;
        }


        public UserProfile Proc_GetProfileDetailByLoginId(string LoginId)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@LoginId",Value=LoginId}
            };
            var _proc = @"Proc_GetProfileDetailByloginID @LoginId";
            var res = this.Database.SqlQuery<UserProfile>(_proc, sqlparams).FirstOrDefault();
            return res;
        }


        public List<Team> GetTeam(long memberId, int page, int pagesize, string searchtype, string searchvalue)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@memberId",Value=memberId},
                new SqlParameter { ParameterName="@Page",Value=page},
                new SqlParameter { ParameterName="@PageSize",Value=pagesize},
                new SqlParameter { ParameterName="@Searchtype",Value=searchtype},
                new SqlParameter { ParameterName="@Searchvalue",Value=searchvalue }
            };
            var _proc = @"Proc_GetTeam @memberId,@Page,@PageSize,@Searchtype,@Searchvalue";
            var res = this.Database.SqlQuery<Team>(_proc, sqlparams).ToList();
            return res;
        }

        public GetBusinessDetails GetBusinessDetails(long memberId)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@memberId",Value=memberId}
            };
            var _proc = @"GetBusinessDetails @memberId";
            var res = this.Database.SqlQuery<GetBusinessDetails>(_proc, sqlparams).SingleOrDefault();
            return res;
        }

        public CommonJsonReponse DirectTotalCount(long memberId, string searchtype, string searchvalue)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@FK_MemId",Value=memberId},
                  new SqlParameter { ParameterName="@Searchtype",Value=searchtype},
                new SqlParameter { ParameterName="@Searchvalue",Value=searchvalue }
            };
            var _proc = @"DirectTotalCount @FK_MemId,@Searchtype,@Searchvalue";
            var res = this.Database.SqlQuery<CommonJsonReponse>(_proc, sqlparams).SingleOrDefault();
            return res;
        }
        public List<DownlineTeamData> GetDownline(long memberId, int page, int pagesize, string searchtype, string searchvalue)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@FK_MemId",Value=memberId},
                new SqlParameter { ParameterName="@Page",Value=page},
                new SqlParameter { ParameterName="@PageSize",Value=pagesize},
                 new SqlParameter { ParameterName="@Searchtype",Value=searchtype},
                new SqlParameter { ParameterName="@Searchvalue",Value=searchvalue }
            };
            var _proc = @"GetDownline @FK_MemId,@Page,@PageSize,@Searchtype,@Searchvalue";
            var res = this.Database.SqlQuery<DownlineTeamData>(_proc, sqlparams).ToList();
            return res;
        }
        public DownLineJsonReponse getTotalCount(long memberId, string searchtype, string searchvalue)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@FK_MemId",Value=memberId},
                   new SqlParameter { ParameterName="@Searchtype",Value=searchtype},
                new SqlParameter { ParameterName="@Searchvalue",Value=searchvalue }
            };
            var _proc = @"GetDownlineTotalCount @FK_MemId,@Searchtype,@Searchvalue";
            var res = this.Database.SqlQuery<DownLineJsonReponse>(_proc, sqlparams).SingleOrDefault();
            return res;
        }
        public ResultSet UpdateProfile(UserProfile model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@Fk_memId",Value=model.fk_MemId},
                new SqlParameter { ParameterName="@firstName",Value=model.firstName??string.Empty},
                new SqlParameter { ParameterName="@lastName",Value=model.lastName??string.Empty},
                new SqlParameter { ParameterName="@city",Value=model.city??string.Empty},
                new SqlParameter { ParameterName="@tehsil",Value=model.tehsil??string.Empty},
                new SqlParameter { ParameterName="@pinCode",Value=model.pinCode??string.Empty},
                new SqlParameter { ParameterName="@address1",Value=model.address1??string.Empty},
                new SqlParameter { ParameterName="@address2",Value=model.address2??string.Empty},
                new SqlParameter { ParameterName="@address3",Value=model.address3??string.Empty},
                new SqlParameter { ParameterName="@dob",Value=model.dob??string.Empty},
                new SqlParameter { ParameterName="@fatherName",Value=model.fatherName??string.Empty},
                new SqlParameter { ParameterName="@nomineeName",Value=model.nomineeName??string.Empty},
                new SqlParameter { ParameterName="@mobile",Value=model.mobile??string.Empty},
                new SqlParameter { ParameterName="@email",Value=model.email??string.Empty},
                new SqlParameter { ParameterName="@gender",Value=model.gender??string.Empty},
                new SqlParameter { ParameterName="@marritalStatus",Value=model.marritalStatus??string.Empty},
                new SqlParameter { ParameterName="@relationWithApplicant",Value=model.relationWithApplicant??string.Empty},
                new SqlParameter { ParameterName="@state",Value=model.state??string.Empty},
                new SqlParameter { ParameterName="@Pancard",Value=model.pancard??string.Empty},
                new SqlParameter { ParameterName="@AadharNo",Value=model.aadhar??string.Empty},


                 new SqlParameter { ParameterName="@memberBankName",Value=model.memberBankName??string.Empty},
                new SqlParameter { ParameterName="@memberBranch",Value=model.memberBranch??string.Empty},
                new SqlParameter { ParameterName="@memberAccNo",Value=model.memberAccNo??string.Empty},


                 new SqlParameter { ParameterName="@bankHolderName",Value=model.bankHolderName??string.Empty},
                new SqlParameter { ParameterName="@ifscCode",Value=model.ifscCode??string.Empty},



            };
            var _proc = @"Proc_UpdatePersonalDetail @Fk_memId,@firstName,@lastName,@city,@tehsil,@pinCode,@address1,@address2,
             @address3,@dob,@fatherName,@nomineeName,@mobile,@email,@gender,@marritalStatus,@relationWithApplicant,@state,@Pancard,@AadharNo,@memberBankName,@memberBranch,@memberAccNo,@bankHolderName,@ifscCode";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public ResultSet UpdateBank(BankDetail model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@fk_memId",Value=model.fk_memId},
                new SqlParameter { ParameterName="@bankAccName",Value=model.bankAccName??string.Empty},
                new SqlParameter { ParameterName="@memberBankName",Value=model.memberBankName??string.Empty},
                new SqlParameter { ParameterName="@memberBranch",Value=model.memberBranch??string.Empty},
                new SqlParameter { ParameterName="@memberAccNo",Value=model.memberAccNo??string.Empty},
                new SqlParameter { ParameterName="@bankHolderName",Value=model.bankHolderName??string.Empty},
                new SqlParameter { ParameterName="@ifscCode",Value=model.ifscCode??string.Empty},
                new SqlParameter { ParameterName="@relationWithApplicant",Value=model.relationWithApplicant??string.Empty},
                new SqlParameter { ParameterName="@Pincode",Value=model.Pincode??string.Empty}
            };
            var _proc = @"UpdateBankDetail @fk_memId,@bankAccName,@memberBankName,@memberBranch,@memberAccNo,@bankHolderName,@ifscCode,@relationWithApplicant,@Pincode";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public ResultSet CheckJWTToken(string Token)
        {
            var sqlparams = new SqlParameter[] {

               new SqlParameter {ParameterName="@Token",Value=Token}
            };
            var _proc = @"CheckJWTToken @Token";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public ResultSet ClientIdWithandroid(string androidId)
        {
            var sqlparams = new SqlParameter[] {

               new SqlParameter {ParameterName="@androidId",Value=androidId}
            };
            var _proc = @"Proc_getClientIdByandroidId @androidId";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        #region getdashboardDetail

        public List<DashboardModel> GetDashboarddetaildata(int categoryId)
        {
            var sqlparam = new SqlParameter[] {
                new SqlParameter { ParameterName="@categoryId",Value=categoryId}
            };
            var _proc = @"ProcGetDashboardDetail @categoryId";
            var res = this.Database.SqlQuery<DashboardModel>(_proc, sqlparam).ToList();
            return res;
        }

        public List<Pincode> GetAreaDetailByPincode(string pincode)
        {
            var sqlparam = new SqlParameter[] {
                new SqlParameter { ParameterName="@Pincode",Value=pincode}
            };
            var _proc = @"Proc_GetAreaDetailByPincode @Pincode";
            var res = this.Database.SqlQuery<Pincode>(_proc, sqlparam).ToList();
            return res;
        }

        public Wallet GetWalletBalance(long MemberId)
        {
            var sqlparam = new SqlParameter[] {
                new SqlParameter { ParameterName="@MemberId",Value=MemberId}
            };
            var _proc = @"Proc_GetWalletBalance @MemberId";
            var res = this.Database.SqlQuery<Wallet>(_proc, sqlparam).FirstOrDefault();
            return res;
        }

        public FranchiseWallet GetWalletBalanceFranchise(long MemberId)
        {
            var sqlparam = new SqlParameter[] {
                new SqlParameter { ParameterName="@MemberId",Value=MemberId}
            };
            var _proc = @"Proc_GetFranchiseWalletbalance @MemberId";
            var res = this.Database.SqlQuery<FranchiseWallet>(_proc, sqlparam).FirstOrDefault();
            return res;
        }
        public List<WalletJsonRequest> GetWalletRequestLedger(WalletJsonCommonRequest model)
        {
            var sqlparam = new SqlParameter[] {
                new SqlParameter { ParameterName="@MemberId",Value=model.memberId},
                 new SqlParameter { ParameterName="@FromDate",Value=model.fromDate},
                  new SqlParameter { ParameterName="@ToDate",Value=model.toDate},
                  new SqlParameter { ParameterName="@Page",Value=model.page},
                  new SqlParameter { ParameterName="@Size",Value=model.size}
            };
            var _proc = @"Proc_GetWalletRequestLedger @MemberId,@FromDate,@ToDate,@Page,@Size";
            var res = this.Database.SqlQuery<WalletJsonRequest>(_proc, sqlparam).ToList();
            return res;
        }
        public List<WalletResponse> GetWalletLedger(WalletJsonCommonRequest model)
        {
            var sqlparam = new SqlParameter[] {
                new SqlParameter { ParameterName="@MemberId",Value=model.memberId},
                new SqlParameter { ParameterName="@WalletType",Value=model.walletType},
                 new SqlParameter { ParameterName="@FromDate",Value=model.fromDate},
                  new SqlParameter { ParameterName="@ToDate",Value=model.toDate},
                  new SqlParameter { ParameterName="@Page",Value=model.page},
                  new SqlParameter { ParameterName="@Size",Value=model.size}
            };
            var _proc = @"Proc_GetWalletLedger @MemberId,@WalletType,@FromDate,@ToDate,@Page,@Size";
            var res = this.Database.SqlQuery<WalletResponse>(_proc, sqlparam).ToList();
            return res;
        }
        public SupportRequestModel GetSupportMessageById(string MessageId)
        {
            var sqlparam = new SqlParameter[] {
                new SqlParameter { ParameterName="@MessageId",Value=MessageId}
            };
            var _proc = @"GetSupportMessageById @MessageId";
            var res = this.Database.SqlQuery<SupportRequestModel>(_proc, sqlparam).SingleOrDefault();
            return res;
        }
        public ResultSet AddImages(Imagelist model)
        {
            var sqlparam = new SqlParameter[] {
                new SqlParameter { ParameterName="@Fk_MemId",Value=model.fk_MemId??string.Empty},
                 new SqlParameter { ParameterName="@Action",Value=model.action??string.Empty},
                  new SqlParameter { ParameterName="@ImageUrl",Value=model.imageUrl??string.Empty},
                   new SqlParameter { ParameterName="@Type",Value=model.type??string.Empty},
                    new SqlParameter { ParameterName="@UniqueNo",Value=model.uniqueNo??string.Empty}
            };
            var _proc = @"UploadApiDocuments @Fk_MemId,@Action,@ImageUrl,@Type,@UniqueNo";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparam).SingleOrDefault();
            return res;
        }
        #endregion

        public ResultSet SignOutAPI(clsInputSignOut model)
        {
            var sqlparam = new SqlParameter[] {
                new SqlParameter { ParameterName="@FK_MemId",Value=model.FK_MemId},
                 new SqlParameter { ParameterName="@AndroidId",Value=model.AndroidId??string.Empty},
                  new SqlParameter { ParameterName="@DeviceId",Value=model.DeviceId??string.Empty},
                   new SqlParameter { ParameterName="@Token",Value=model.Token??string.Empty}
            };
            var _proc = @"API_CheckUserSignOut @FK_MemId,@AndroidId,DeviceId,@Token";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparam).SingleOrDefault();
            return res;
        }

        public List<Direct> GetDirect(long FK_MemId)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@Fk_memId",Value=FK_MemId}
            };
            var _proc = @"Proc_GetDirect @Fk_memId";
            var res = this.Database.SqlQuery<Direct>(_proc, sqlparams).ToList();
            return res;
        }

        public ResultSet CreateOrder(JsonOrderRequest model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@orderId",Value=model.orderId},
                new SqlParameter { ParameterName="@productName",Value=model.productName??string.Empty},
                new SqlParameter { ParameterName="@productPrice",Value=model.productPrice},
                new SqlParameter { ParameterName="@paymentMode",Value=model.paymentMode??string.Empty},
                new SqlParameter { ParameterName="@paymentDate",Value=model.paymentDate??string.Empty},
                new SqlParameter { ParameterName="@memberId",Value=model.memberId},
                new SqlParameter { ParameterName="@payout",Value=model.payout},
                 new SqlParameter { ParameterName="@mainamount",Value=model.mainamount},
                new SqlParameter { ParameterName="@walletamount",Value=model.walletamount},
                new SqlParameter { ParameterName="@paymentgatewayamount",Value=model.paymentgatewayamount},
                new SqlParameter { ParameterName="@transactionId",Value=model.transactionId??string.Empty}
            };
            var _proc = @"Proc_CreateOrder @orderId ,@productName,@productPrice ,@paymentMode ,@paymentDate,@memberId ,@payout,@mainamount,@walletamount,@paymentgatewayamount,@transactionId";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public ResultSet CreateOrderV3(CardItemRequest model)
        {
            try
            {
                var sqlparams = new SqlParameter[] {
                    new SqlParameter { ParameterName="@ProcId",Value=model.ProcId},
                    new SqlParameter { ParameterName="@Fk_MemID",Value=model.Fk_MemID},
                    new SqlParameter { ParameterName="@Token",Value=model.token??string.Empty},
                    new SqlParameter { ParameterName="@orderId",Value=model.orderId },
                    new SqlParameter { ParameterName="@OrderNo",Value=model.OrderNo??string.Empty },
                    new SqlParameter { ParameterName="@TransactionNo",Value=model.transactionNo??String.Empty},
                    new SqlParameter { ParameterName="@TotalAmount",Value=model.TotalAmount},
                    new SqlParameter { ParameterName="@cashbackAmount",Value=model.cashbackAmount},
                    new SqlParameter { ParameterName="@Amount",Value=model.WalletAmount}, //dream   y wallet
                    new SqlParameter { ParameterName="@gatewayAmount",Value=model.gatewayAmount},
                    new SqlParameter { ParameterName="@xmldata",Value=model.xmldata??string.Empty},
                    new SqlParameter { ParameterName="@ShippingAddress",Value=model.shippingAddress??string.Empty},
                    new SqlParameter { ParameterName="@BillingAddress",Value=model.billingAddress??string.Empty},
                    new SqlParameter { ParameterName="@donation",Value=model.donation},
                    new SqlParameter{ ParameterName="@CourierCharges",Value=model.CourierCharges}
                 };
                var _proc = @"Proc_CreateNewOrderV3 @ProcId,@Fk_MemID,@Token,@orderId,@OrderNo,@TransactionNo,@TotalAmount,@cashbackAmount,@Amount,@gatewayAmount,@xmldata,@ShippingAddress,@BillingAddress,@donation,@CourierCharges";
                var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
                return res;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public ResultSet CreateOrderV3APP(CardItemRequest model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@ProcId",Value=model.ProcId},
                new SqlParameter { ParameterName="@Fk_MemID",Value=model.Fk_MemID},
                new SqlParameter { ParameterName="@Token",Value=model.token??string.Empty},
                new SqlParameter { ParameterName="@orderId",Value=model.orderId },
                new SqlParameter { ParameterName="@OrderNo",Value=model.OrderNo??string.Empty },
                new SqlParameter { ParameterName="@TransactionNo",Value=model.transactionNo??String.Empty},
                new SqlParameter { ParameterName="@TotalAmount",Value=model.TotalAmount},
                new SqlParameter { ParameterName="@cashbackAmount",Value=model.cashbackAmount},
                new SqlParameter { ParameterName="@Amount",Value=model.WalletAmount}, //dreamy wallet
                new SqlParameter { ParameterName="@gatewayAmount",Value=model.gatewayAmount},
                new SqlParameter { ParameterName="@xmldata",Value=model.xmldata??string.Empty},
                new SqlParameter { ParameterName="@ShippingAddress",Value=model.shippingAddress??string.Empty},
                new SqlParameter { ParameterName="@BillingAddress",Value=model.billingAddress??string.Empty},
                new SqlParameter { ParameterName="@donation",Value=model.donation},
                new SqlParameter{ ParameterName="@CourierCharges",Value=model.CourierCharges}
            };
            var _proc = @"Proc_CreateNewOrderV3APP @ProcId,@Fk_MemID,@Token,@orderId,@OrderNo,@TransactionNo,@TotalAmount,@cashbackAmount,@Amount,@gatewayAmount,@xmldata,@ShippingAddress,@BillingAddress,@donation,@CourierCharges";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public ResultSet CreateOrderV3_Karyon(CardItemRequest model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@ProcId",Value=model.ProcId},
                new SqlParameter { ParameterName="@Fk_MemID",Value=model.Fk_MemID},
                new SqlParameter { ParameterName="@Token",Value=model.token??string.Empty},
                new SqlParameter { ParameterName="@orderId",Value=model.orderId },
                new SqlParameter { ParameterName="@OrderNo",Value=model.OrderNo??string.Empty },
                new SqlParameter { ParameterName="@TransactionNo",Value=model.transactionNo??String.Empty},
                new SqlParameter { ParameterName="@TotalAmount",Value=model.TotalAmount},
                new SqlParameter { ParameterName="@cashbackAmount",Value=model.cashbackAmount},
                new SqlParameter { ParameterName="@Amount",Value=model.WalletAmount}, //dreamy wallet
                new SqlParameter { ParameterName="@gatewayAmount",Value=model.gatewayAmount},
                new SqlParameter { ParameterName="@xmldata",Value=model.xmldata??string.Empty},
                new SqlParameter { ParameterName="@ShippingAddress",Value=model.shippingAddress??string.Empty},
                new SqlParameter { ParameterName="@BillingAddress",Value=model.billingAddress??string.Empty},
                new SqlParameter { ParameterName="@donation",Value=model.donation},
                new SqlParameter { ParameterName="@CourierCharges",Value=model.CourierCharges},
                new SqlParameter { ParameterName="@ShoppingFrom",Value=model.ShoppingFrom??string.Empty},
                new SqlParameter { ParameterName="@CropCode",Value=model.CropCode??string.Empty},
                new SqlParameter { ParameterName="@Area",Value=model.Area},
                new SqlParameter { ParameterName="@SowingDate",Value=model.SowingDate??string.Empty}
            };
            var _proc = @"Proc_CreateNewOrderV3_Karyon @ProcId,@Fk_MemID,@Token,@orderId,@OrderNo,@TransactionNo,@TotalAmount,@cashbackAmount,@Amount,@gatewayAmount,@xmldata,@ShippingAddress,@BillingAddress,@donation,@CourierCharges,@ShoppingFrom,@CropCode,@Area,@SowingDate";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public ResultSet PaypalToWallet(PaypalToWallet model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@Fk_MemId",Value=model.Fk_MemId},
                new SqlParameter { ParameterName="@Amount",Value=model.Amount},
                new SqlParameter { ParameterName="@InvoiceNo",Value=model.InvoiceNo??string.Empty}
            };
            var _proc = @"PaypalToWallet @Fk_MemId,@Amount,@InvoiceNo";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public ResultSet PaypalToWallet_V2(PaypalToWallet model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@Fk_MemId",Value=model.Fk_MemId},
                new SqlParameter { ParameterName="@Amount",Value=model.Amount},
                new SqlParameter { ParameterName="@TransId",Value=model.TransId??string.Empty},
                new SqlParameter { ParameterName="@OrderId",Value=model.OrderId??string.Empty},
                new SqlParameter { ParameterName="@Status",Value=model.Status??string.Empty}
            };
            var _proc = @"AddWalletByGateway @Fk_MemId,@Amount,@TransId,@OrderId,@Status";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public ResultSet GatewayFailedTrans(PaypalToWallet model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@Fk_MemId",Value=model.Fk_MemId},
                new SqlParameter { ParameterName="@Amount",Value=model.Amount},
                new SqlParameter { ParameterName="@InvoiceNo",Value=model.InvoiceNo??string.Empty},
                 new SqlParameter { ParameterName="@Type",Value=model.Type??string.Empty}
            };
            var _proc = @"PaymentGatewayFailedTrans @Fk_MemId,@Amount,@InvoiceNo,@Type";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public List<TodayReferal> TodayReferal()
        {
            var _proc = @"GetTopTenTodaysReferals";
            var res = this.Database.SqlQuery<TodayReferal>(_proc).ToList();
            return res;
        }
        public ResultSet MobileOperator(string MobileOperators)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@MobileOperators",Value=MobileOperators},
            };
            var _proc = @"InsertMobileOperator @MobileOperators";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public ResultSet GetBillPaymentOperatorsRequest(string MobileOperators)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@MobileOperators",Value=MobileOperators},
            };
            var _proc = @"InsertBillPaymentOperator @MobileOperators";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public ResultSet CheckOperatorStatus(string date, string type)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@date",Value=date},
                 new SqlParameter { ParameterName="@type",Value=type}
            };
            var _proc = @"CheckOperatorStatus @date,@type";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public List<LifeOne.Models.TravelModel.RechargeAPI.OperatorLists> GetPaymentOperator()
        {
            var sqlparams = new SqlParameter[] {

            };
            var _proc = @"GetPaymentOperator";
            var res = this.Database.SqlQuery<LifeOne.Models.TravelModel.RechargeAPI.OperatorLists>(_proc).ToList();
            return res;
        }
        public List<LifeOne.Models.TravelModel.RechargeAPI.OperatorLists> GetMobileOperator()
        {
            var sqlparams = new SqlParameter[] {

            };
            var _proc = @"GetMobileOperator";
            var res = this.Database.SqlQuery<LifeOne.Models.TravelModel.RechargeAPI.OperatorLists>(_proc).ToList();
            return res;
        }
        public List<LifeOne.Models.API.History.BookedTicket> GetBusHistory(LifeOne.Models.TravelModel.BusModel.GetBookedHistoryRequest obj)
        {
            var sqlparams = new SqlParameter[] {
                 new SqlParameter { ParameterName="@FromDate",Value=obj.BookedHistoryInput.FromDate},
                 new SqlParameter { ParameterName="@ToDate",Value=obj.BookedHistoryInput.ToDate},
                 new SqlParameter { ParameterName="@RecordsBy",Value=obj.BookedHistoryInput.RecordsBy},
                  new SqlParameter { ParameterName="@FK_memId",Value=obj.BookedHistoryInput.FK_memId}
            };
            var _proc = @"BusBookinghistory @FromDate,@ToDate,@RecordsBy,@FK_memId";
            var res = this.Database.SqlQuery<LifeOne.Models.API.History.BookedTicket>(_proc, sqlparams).ToList();
            return res;
        }
        public History.CancellationPolicyInput GetCancellationPolicyInput(string UserTrackId)
        {
            var sqlparams = new SqlParameter[] {
                 new SqlParameter { ParameterName="@UserTrackId",Value=UserTrackId},
            };
            var _proc = @"GetCancellationPolicyData @UserTrackId";
            var res = this.Database.SqlQuery<History.CancellationPolicyInput>(_proc, sqlparams).SingleOrDefault();
            return res;
        }
        public List<TravelModel.RechargeAPI.Recharges> GetRechargeHistory(TravelModel.RechargeAPI.GetRechargeHistory model)
        {
            var sqlparams = new SqlParameter[] {
                 new SqlParameter { ParameterName="@Fk_MemId",Value=Convert.ToInt64(model.RechargeHistoryInput.Fk_MemId)},
                 new SqlParameter { ParameterName="@Type",Value=model.RechargeHistoryInput.Type},
            };
            var _proc = @"GetRechargeHistory @Fk_MemId,@Type";
            var res = this.Database.SqlQuery<TravelModel.RechargeAPI.Recharges>(_proc, sqlparams).ToList();
            return res;
        }
        public ResultSet SavePaymentTransaction(paymentModel model)
        {
            var sqlparams = new SqlParameter[] {
                 new SqlParameter { ParameterName="@Itemname",Value=model.name??string.Empty},
                 new SqlParameter { ParameterName="@currency",Value=model.currency},
                 new SqlParameter { ParameterName="@price",Value=Convert.ToDecimal(model.price??string.Empty)},
                 new SqlParameter { ParameterName="@quantity",Value=Convert.ToInt32(model.quantity??string.Empty)},
                 new SqlParameter { ParameterName="@sku",Value=model.sku??string.Empty},
                 new SqlParameter { ParameterName="@tax",Value=Convert.ToDecimal(model.tax??string.Empty)},
                 new SqlParameter { ParameterName="@shipping",Value=model.shipping??string.Empty},
                 new SqlParameter { ParameterName="@subtotal",Value=Convert.ToDecimal(model.subtotal??string.Empty)},
                 new SqlParameter { ParameterName="@total",Value=Convert.ToDecimal(model.total??string.Empty)},
                 new SqlParameter { ParameterName="@invoice_number",Value=model.invoice_number??string.Empty},
                 new SqlParameter { ParameterName="@fk_memid",Value=Convert.ToInt64(model.fk_memid)},
                 new SqlParameter { ParameterName="@transtype",Value=model.transtype??string.Empty},
                 new SqlParameter { ParameterName="@fk_orderid",Value=Convert.ToInt64(model.fk_orderid??string.Empty)},
                 new SqlParameter { ParameterName="@paymentid",Value=model.paymentid??string.Empty},
                 new SqlParameter { ParameterName="@intent",Value=model.intent??string.Empty},
                 new SqlParameter { ParameterName="@payerid",Value=model.payerid??string.Empty },
                 new SqlParameter { ParameterName="@payeremail",Value=model.payeremail??string.Empty},
                 new SqlParameter { ParameterName="@payerfirstname",Value=model.payerfirstname??string.Empty},
                 new SqlParameter { ParameterName="@payerlastname",Value=model.payerlastname??string.Empty},
                 new SqlParameter { ParameterName="@merchantid",Value=model.merchantid??string.Empty},
                 new SqlParameter { ParameterName="@merchantemail",Value=model.merchantemail??string.Empty},
                 new SqlParameter { ParameterName="@transdesc",Value=model.transdesc??string.Empty},
                 new SqlParameter { ParameterName="@paymentstatus",Value=model.paymentstatus??string.Empty},
                 new SqlParameter { ParameterName="@transactionno",Value=model.transactionno??string.Empty}
            };
            var _proc = @"InsertPaypalTransaction @Itemname,@currency,@price,@quantity,@sku,@tax,@shipping,@subtotal,@total,@invoice_number,@fk_memid,@transtype,@fk_orderid,@paymentid,@intent,@payerid,@payeremail,@payerfirstname,@payerlastname
                        ,@merchantid,@merchantemail,@transdesc,@paymentstatus,@transactionno ";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public BankMasterModel GetDreamyBankDetail()
        {
            var _proc = @"Proc_GetDreamyBankdetail";
            var res = this.Database.SqlQuery<BankMasterModel>(_proc).FirstOrDefault();
            return res;
        }

        public ResultSet CreateOrderV2(CardItemRequest model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@ProcId",Value=model.ProcId},
                new SqlParameter { ParameterName="@Fk_MemID",Value=model.Fk_MemID},
                new SqlParameter { ParameterName="@Token",Value=model.token??string.Empty},
                new SqlParameter { ParameterName="@orderId",Value=model.orderId },
                new SqlParameter { ParameterName="@OrderNo",Value=model.OrderNo??string.Empty },
                new SqlParameter { ParameterName="@TransactionNo",Value=model.transactionNo??String.Empty},
                new SqlParameter { ParameterName="@TotalAmount",Value=model.TotalAmount},
                new SqlParameter { ParameterName="@cashbackAmount",Value=model.cashbackAmount},
                new SqlParameter { ParameterName="@CompAmount",Value=model.WalletAmount},
                new SqlParameter { ParameterName="@gatewayAmount",Value=model.gatewayAmount},
                new SqlParameter { ParameterName="@xmldata",Value=model.xmldata??string.Empty},
                new SqlParameter { ParameterName="@ShippingAddress",Value=model.shippingAddress??string.Empty},
                new SqlParameter { ParameterName="@BillingAddress",Value=model.billingAddress??string.Empty},
                new SqlParameter { ParameterName="@donation",Value=model.donation}
            };
            var _proc = @"Proc_CreateNewOrder @ProcId,@Fk_MemID,@Token,@orderId,@OrderNo,@TransactionNo,@TotalAmount,@cashbackAmount,@CompAmount,@gatewayAmount,@xmldata,@ShippingAddress,@BillingAddress,@donation";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }


        /*For MagentoAPI*/
        public ResultSet SaveOrderDetails(CardItemRequest model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@ProcId",Value=model.ProcId},
                new SqlParameter { ParameterName="@Fk_MemID",Value=model.Fk_MemID},
                new SqlParameter { ParameterName="@Token",Value=model.token??string.Empty},
                new SqlParameter { ParameterName="@orderId",Value=model.orderId },
                new SqlParameter { ParameterName="@OrderNo",Value=model.OrderNo??string.Empty },
                new SqlParameter { ParameterName="@TransactionNo",Value=model.transactionNo??String.Empty},
                new SqlParameter { ParameterName="@TotalAmount",Value=model.TotalAmount},
                new SqlParameter { ParameterName="@cashbackAmount",Value=model.cashbackAmount},
                new SqlParameter { ParameterName="@dreamyAmount",Value=model.WalletAmount},
                new SqlParameter { ParameterName="@gatewayAmount",Value=model.gatewayAmount},
                new SqlParameter { ParameterName="@xmldata",Value=model.xmldata??string.Empty},
                new SqlParameter { ParameterName="@ShippingAddress",Value=model.shippingAddress??string.Empty},
                new SqlParameter { ParameterName="@BillingAddress",Value=model.billingAddress??string.Empty},
                new SqlParameter { ParameterName="@donation",Value=model.donation}

            };
            var _proc = @"Proc_CreateNewOrder @ProcId,@Fk_MemID,@Token,@orderId,@OrderNo,@TransactionNo,@TotalAmount,@cashbackAmount,@dreamyAmount,@gatewayAmount,@xmldata,@ShippingAddress,@BillingAddress,@donation";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }



        public GetKYCStatus GetKYCStstus(long MemberId)
        {
            var sqlparam = new SqlParameter[] {
             new SqlParameter { ParameterName="@MemberId",Value=MemberId}
                  };
            var _proc = @"GetKYCStatus @MemberId";
            var res = this.Database.SqlQuery<GetKYCStatus>(_proc, sqlparam).FirstOrDefault();
            return res;
        }

        public List<SupportType> SupportType()
        {

            var _proc = @"GetSupportType";
            var res = this.Database.SqlQuery<SupportType>(_proc).ToList();
            return res;
        }

        public List<GetAllOrderId> GetAllpendingOrderId()
        {

            var _proc = @"GetAllPendingOrderId";
            var res = this.Database.SqlQuery<GetAllOrderId>(_proc).ToList();
            return res;
        }

        public ResultSet RazorPayPayment(string data)
        {
            var sqlparam = new SqlParameter[] {
             new SqlParameter { ParameterName="@Payment",Value=data??string.Empty}
             };
            var _proc = @"Proc_InsertRazorPayPayment @Payment";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparam).FirstOrDefault();
            return res;
        }


        public SupportResponse SupportRequest(SupportRequest model)
        {
            var sqlparam = new SqlParameter[] {
             new SqlParameter { ParameterName="@Fk_SupportId",Value=model.Fk_SupportId},
             new SqlParameter { ParameterName="@Fk_MemId",Value=model.Fk_MemId},
             new SqlParameter { ParameterName="@Subject",Value=model.Subject??string.Empty},
             new SqlParameter { ParameterName="@Message",Value=model.Message??string.Empty},
             new SqlParameter { ParameterName="@CreatedBy",Value=model.Fk_MemId}
           };
            var _proc = @"RequestSupport @Fk_SupportId,@Fk_MemId,@Subject,@Message,@CreatedBy";
            var res = this.Database.SqlQuery<SupportResponse>(_proc, sqlparam).FirstOrDefault();
            return res;
        }

        public SupportResponse ReplyMessageHistory(MSupportHistoryModel model)
        {
            var sqlparam = new SqlParameter[] {
             new SqlParameter { ParameterName="@Fk_SupportId",Value=model.SupportId},
             new SqlParameter { ParameterName="@Fk_MemId",Value=model.Fk_MemId},
             new SqlParameter { ParameterName="@Msg",Value=model.Msg??string.Empty},
             new SqlParameter { ParameterName="@Login",Value=model.LoginId??string.Empty},
             new SqlParameter { ParameterName="@Fk_MemLoginId",Value=model.Fk_MemLoginId??string.Empty},
             new SqlParameter { ParameterName="@MsgBy",Value=2}
           };
            var _proc = @"Proc_AddSupportMsg @Fk_SupportId,@Fk_MemId,@Msg,@Login,@Fk_MemLoginId,@MsgBy";
            var res = this.Database.SqlQuery<SupportResponse>(_proc, sqlparam).FirstOrDefault();
            return res;
        }


        public List<Support> GetAllSupportRequest(long MemberId)
        {
            var sqlparam = new SqlParameter[] {
 new SqlParameter { ParameterName="@Fk_memId",Value=MemberId}
 };
            var _proc = @"Proc_GetSupportById @Fk_memId";
            var res = this.Database.SqlQuery<Support>(_proc, sqlparam).ToList();
            return res;
        }
        public List<GetToken> GetAllToken(long memberId)
        {
            var sqlparam = new SqlParameter[] {
           new SqlParameter { ParameterName="@Fk_memId",Value=memberId}
              };
            var _proc = @"GetAllPendingOrderToken @Fk_memId";
            var res = this.Database.SqlQuery<GetToken>(_proc, sqlparam).ToList();
            return res;
        }

        public List<MSupportHistoryModel> GetAllMessageHistory(long Fk_SupportId)
        {
            var sqlparam = new SqlParameter[] {
                 new SqlParameter { ParameterName="@Fk_SupportId",Value=Fk_SupportId}
             };
            var _proc = @"Proc_RequestSupportList @Fk_SupportId";
            var res = this.Database.SqlQuery<MSupportHistoryModel>(_proc, sqlparam).ToList();
            return res;
        }

        public ResultSet UpdateOrderStatus(long memberId, decimal Amount, string Token, int ProcId, string OrderNo)
        {
            var sqlparam = new SqlParameter[] {
           new SqlParameter { ParameterName="@procId",Value=ProcId},
           new SqlParameter { ParameterName="@Fk_memId",Value=memberId},
           new SqlParameter { ParameterName="@Amount",Value=Convert.ToDecimal(Amount)},
           new SqlParameter { ParameterName="@token",Value=Token},
           new SqlParameter { ParameterName="@OrderNo",Value=OrderNo},
              };
            var _proc = @"UpdateOrderStatus @procId,@Fk_memId,@Amount,@token,@OrderNo";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparam).SingleOrDefault();
            return res;
        }

        public ResultSet BillDeskToWallet(BillDeskPayment model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@Fk_MemId",Value=model.Fk_MemId},
                new SqlParameter { ParameterName="@Amount",Value=model.Amount},
                new SqlParameter { ParameterName="@InvoiceNo",Value=model.PaymentId??string.Empty},
                new SqlParameter { ParameterName="@Status",Value=model.Status??string.Empty}
            };
            var _proc = @"BillDeskToWallet @Fk_MemId,@Amount,@InvoiceNo,@Status";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public List<GetVendorId> GetVendorId()
        {

            var _proc = @"GetVendorId";
            var res = this.Database.SqlQuery<GetVendorId>(_proc).ToList();
            return res;
        }
        public ResultSet SaveVendorDetails(VerdorDetails obj)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@VendorId",Value=obj.data[0].VendorId},
                new SqlParameter { ParameterName="@contact_number",Value=obj.data[0].contact_number??string.Empty},
                new SqlParameter { ParameterName="@seller_id",Value=obj.data[0].seller_id??string.Empty},
                new SqlParameter { ParameterName="@line1",Value=obj.data[0].line1??string.Empty},
                new SqlParameter { ParameterName="@line2",Value=obj.data[0].line2??string.Empty},
                new SqlParameter { ParameterName="@state",Value=obj.data[0].state??string.Empty},
                new SqlParameter { ParameterName="@city",Value=obj.data[0].city??string.Empty},
                new SqlParameter { ParameterName="@pincode",Value=obj.data[0].pincode??string.Empty},
                new SqlParameter { ParameterName="@country",Value=obj.data[0].country??string.Empty},
                new SqlParameter { ParameterName="@shop_logo",Value=obj.data[0].shop_logo??string.Empty},
                new SqlParameter { ParameterName="@shop_title",Value=obj.data[0].shop_title??string.Empty},
            };

            var _proc = @"SaveVendorDetails @VendorId,@contact_number,@seller_id,@line1,@line2,@state,@city,@pincode,@country,@shop_logo,@shop_title";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public List<Orders> GetAllOrders(long MemberId, string OrderNo, int procid)
        {
            var sqlparam = new SqlParameter[] {
                 new SqlParameter { ParameterName="@Fk_memId",Value=MemberId},
                  new SqlParameter { ParameterName="@OrderNo",Value=OrderNo??string.Empty},
                 new SqlParameter { ParameterName="@ProcId",Value=procid}
                 };
            var _proc = @"GetAllMembersOrders @Fk_memId,@OrderNo,@ProcId";
            var res = this.Database.SqlQuery<Orders>(_proc, sqlparam).ToList();
            return res;
        }


        public List<Orders> GetAllOrdersKaryon(long MemberId, string OrderNo, int procid)
        {
            var sqlparam = new SqlParameter[] {
                 new SqlParameter { ParameterName="@Fk_memId",Value=MemberId},
                  new SqlParameter { ParameterName="@OrderNo",Value=OrderNo??string.Empty},
                 new SqlParameter { ParameterName="@ProcId",Value=procid}
                 };
            var _proc = @"GetAllMembersOrdersKaryon @Fk_memId,@OrderNo,@ProcId";
            var res = this.Database.SqlQuery<Orders>(_proc, sqlparam).ToList();
            return res;
        }


        public List<OrdersDetails> GetOrderDetails(long MemberId, string OrderNo, int procid)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_memId", MemberId);
            queryParameters.Add("@OrderNo", OrderNo);
            queryParameters.Add("@ProcId", procid);
            List<OrdersDetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<OrdersDetails>("GetAllMembersOrders", queryParameters);
            return _iresult;
        }


        public List<OrdersDetails> GetOrderDetailsKaryon(long MemberId, string OrderNo, int procid)
        {
            var sqlparam = new SqlParameter[] {
                 new SqlParameter { ParameterName="@Fk_memId",Value=MemberId},
                  new SqlParameter { ParameterName="@OrderNo",Value=OrderNo??string.Empty},
                 new SqlParameter { ParameterName="@ProcId",Value=procid}
                 };
            var _proc = @"GetAllMembersOrdersKaryon @Fk_memId,@OrderNo,@ProcId";
            var res = this.Database.SqlQuery<OrdersDetails>(_proc, sqlparam).ToList();
            return res;
        }

        public AllPaymentDetailsResponse SaveUpdatePaymentDetails(AllPaymentDetailsRequest obj)
        {

            var sqlparams = new SqlParameter[] {
                      new SqlParameter {ParameterName="@ProcId",Value=obj.Action},
                      new SqlParameter {ParameterName="@Fk_Memid",Value=obj.Fk_Memid},
                      new SqlParameter {ParameterName="@Amount",Value=obj.Amount},
                      new SqlParameter {ParameterName="@Status",Value=obj.Status??string.Empty},
                      new SqlParameter {ParameterName="@PaymentId",Value=obj.PaymentId??string.Empty},
                      new SqlParameter {ParameterName="@Pk_PaymentId",Value=obj.Pk_PaymentId},
                      new SqlParameter {ParameterName="@OrderId",Value=obj.OrderId??string.Empty}
            };
            var _proc = @"UpdateSavePaymentDetails @ProcId,@Fk_Memid,@Amount,@Status,@PaymentId,@Pk_PaymentId,@OrderId";
            var res = this.Database.SqlQuery<AllPaymentDetailsResponse>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public ResultSet SaveWebHookString(string document, long MemberId, long Id)
        {
            var sqlparams = new SqlParameter[] {

                       new SqlParameter {ParameterName="@document",Value=document??string.Empty},
                       new SqlParameter {ParameterName="@FK_memId",Value=MemberId},
                        new SqlParameter {ParameterName="@Id",Value=Id}
            };
            var _proc = @"Proc_SaveWebHookResponse @document,@FK_memId,@Id";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public CheckTransaction CheckTransaction(CashFreeWebHook obj)
        {
            var sqlparams = new SqlParameter[] {
                       new SqlParameter {ParameterName="@order_id",Value=obj.data.order.order_id}
            };
            var _proc = @"Proc_CheckTransaction @order_id";
            var res = this.Database.SqlQuery<CheckTransaction>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public CheckTransaction UpdatePaymentStatus(CashFreeWebHook obj, string status)
        {
            var sqlparams = new SqlParameter[] {
                       new SqlParameter {ParameterName="@order_id",Value=obj.data.order.order_id},
                       new SqlParameter {ParameterName="@status",Value=status},
                       new SqlParameter {ParameterName="@Payment_id",Value=obj.data.payment.cf_payment_id},
            };
            var _proc = @"UpdatePaymentStatus @order_id,@status,@Payment_id";
            var res = this.Database.SqlQuery<CheckTransaction>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public AuditionResponse UpdatePaymentStatusAudition(string orderid, string paymentid, string status, string RazorpaySignature)
        {
            var sqlparams = new SqlParameter[] {

                 new SqlParameter {ParameterName="@OrderId",Value=orderid},
                 new SqlParameter {ParameterName="@Status",Value=status},
                 new SqlParameter {ParameterName="@PaymentId",Value=paymentid},
                  new SqlParameter {ParameterName="@RazorpaySignature",Value=RazorpaySignature??string.Empty},
            };
            var _proc = @"UpdatePaymentOfAudition @OrderId,@Status,@PaymentId,@RazorpaySignature";
            var res = this.Database.SqlQuery<AuditionResponse>(_proc, sqlparams).FirstOrDefault();
            return res;
        }


        //Added By Dainik
        public List<DownlineAPI> GetDownlineAPI(long memberId, int page, int pagesize)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@FK_MemId",Value=memberId},
                new SqlParameter { ParameterName="@Page",Value=page},
                new SqlParameter { ParameterName="@PageSize",Value=pagesize}
            };
            var _proc = @"GetDownlineApI @FK_MemId,@Page,@PageSize";
            var res = this.Database.SqlQuery<DownlineAPI>(_proc, sqlparams).ToList();
            return res;
        }

        public List<DirectAPI> GetDirectAPI(long FK_MemId)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter {
                    ParameterName="@Fk_memId",Value=FK_MemId}
            };
            var _proc = @"Proc_GetDirectAPI @Fk_memId";
            var res = this.Database.SqlQuery<DirectAPI>(_proc, sqlparams).ToList();
            return res;
        }

        public ResponseDirectAPIV2 GetDirectAPIV2(long FK_MemId, int Page, int PageSize)
        {
            try
            {

                ResponseDirectAPIV2 model = new ResponseDirectAPIV2();
                using (var connection = this.Database.Connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Proc_GetDirectAPIV2";
                    command.Parameters.Add(new SqlParameter("@Fk_memId", FK_MemId));
                    command.Parameters.Add(new SqlParameter("@Page", Page));
                    command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                    using (var reader = command.ExecuteReader())
                    {

                        model.MemberuserInfo =
                        ((IObjectContextAdapter)this)
                            .ObjectContext
                            .Translate<MemberuserInfor>(reader)
                            .ToList();

                        reader.NextResult();

                        model.lstDirect =
                            ((IObjectContextAdapter)this)
                                .ObjectContext
                                .Translate<MDirectV2>(reader)
                                .ToList();
                    }
                }
                return model;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public MemberKYC GetMemberKYCList(MemberKYC obj)
        {
            var sqlparams = new SqlParameter[]
            {
                new SqlParameter { ParameterName="@Fk_MemId",Value=obj.Fk_MemId}
            };
            var _proc = @"GetMemberKYCListAPI @Fk_MemId";
            var res = this.Database.SqlQuery<MemberKYC>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public MemberKYC GetMemberKYCListDownline(MemberKYC obj)
        {
            var sqlparams = new SqlParameter[]
            {
                new SqlParameter { ParameterName="@Fk_MemId",Value=obj.Fk_MemId},
                new SqlParameter { ParameterName="@LoginId",Value=obj.LoginId}
            };
            var _proc = @"GetMemberKYCListDownlineAPI @Fk_MemId,@LoginId";
            var res = this.Database.SqlQuery<MemberKYC>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public List<AccountStateMentDetails> GetAccountSatateMent(AccountStateMentDetails obj)
        {
            //var sqlparams = new SqlParameter[]
            //{
            //    new SqlParameter { ParameterName="@Fk_MemId",Value=obj.Fk_MemId},
            //    new SqlParameter { ParameterName="@PayoutNo",Value=obj.PayoutNo}
            //};
            //var _proc = @"AccountStatementApi @Fk_MemId,@PayoutNo";
            //var res = this.Database.SqlQuery<AccountStateMentDetails>(_proc, sqlparams).ToList();
            //return res;
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_MemId", obj.Fk_MemId);
            queryParameters.Add("@PayoutNo", obj.PayoutNo);
            List<AccountStateMentDetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<AccountStateMentDetails>("AccountStatementApi", queryParameters);
            return _iresult;
        }
        public List<AccountStateMentDetails> AssociateRepurchasePayoutStatement(AccountStateMentDetails obj)
        {
            //var sqlparams = new SqlParameter[]
            //{
            //    new SqlParameter { ParameterName="@Fk_MemId",Value=obj.Fk_MemId},
            //    new SqlParameter { ParameterName="@PayoutNo",Value=obj.PayoutNo}
            //};
            //var _proc = @"AccountStatementApi @Fk_MemId,@PayoutNo";
            //var res = this.Database.SqlQuery<AccountStateMentDetails>(_proc, sqlparams).ToList();
            //return res;
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_MemId", obj.Fk_MemId);
            queryParameters.Add("@PayoutNo", obj.PayoutNo);
            List<AccountStateMentDetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<AccountStateMentDetails>("RepurchaseIncomeDetails", queryParameters);
            return _iresult;
        }
        public List<Language> GetAllLanguage()
        {

            var _proc = @"Proc_GetLanguage";
            var res = this.Database.SqlQuery<Language>(_proc).ToList();
            return res;
        }
        public List<MemberLanguageResponse> UpdateMemberlanguage(long FK_MemId, string LanguageCode)
        {

            var _proc = @"Proc_UpdateMemberLanguage @FK_MemId=" + FK_MemId + ",@LanguageCode='" + LanguageCode + "'";
            var res = this.Database.SqlQuery<MemberLanguageResponse>(_proc).ToList();
            return res;
        }
        public List<CropDetail> GetAllCrop(string LanguageCode)
        {

            var _proc = @"Proc_GetCrop @LanguageCode ='" + LanguageCode + "'";
            var res = this.Database.SqlQuery<CropDetail>(_proc).ToList();
            return res;
        }

        public List<CropDetialListModel> GetCropDetail(CropDetialReq obj)
        {

            var _proc = @"Proc_getCropDetail @CropCode ='" + obj.CropCode + "',@Area=" + obj.Area + ",@LanguageCode='" + obj.LanguageCode + "',@Date='" + obj.Date + "'";
            var res = this.Database.SqlQuery<CropDetailbyLangunage>(_proc).ToList();
            List<CropDetialListModel> data = res.GroupBy(x => x.CropType).Select(x => new CropDetialListModel
            {
                CropType = x.FirstOrDefault().CropType,
                MethodOfApplication = x.FirstOrDefault().MethodOfApplication,
                CropDetialList = x.GroupBy(a => a.Days).Select(a => new CropDetialListModel1
                {
                    Days = a.FirstOrDefault().Days,
                    CropDetailbyLangunageList = a.ToList()
                }).ToList()
            }).ToList();
            return data;
        }

        public MemberLanguageResponse AddProductList(string xml)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@xmldata", xml);
            //MemberLanguageResponse _iresult = DBHelperDapper.DAAddAndReturnModel<MemberLanguageResponse>("Proc_AddProduct", queryParameters);
            MemberLanguageResponse _iresult = DBHelperDapper.DAAddAndReturnModel<MemberLanguageResponse>("Proc_AddProduct_Demo", queryParameters);
            return _iresult;
        }
        public List<ProductViewModel> GetProductList(ProductViewModel model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@ProductID", model.ProdID);
            queryParameters.Add("@Page", model.page);
            queryParameters.Add("@Size", SessionManager.Size);
            List<ProductViewModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<ProductViewModel>("Proc_GetProductList", queryParameters);
            return _iresult;
        }

        public List<ProductViewModel> GetOrderTempKaryonApp(ProductViewModel model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@ProductID", model.ProdID);
            queryParameters.Add("@Page", model.page);
            queryParameters.Add("@Size", SessionManager.Size);
            List<ProductViewModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<ProductViewModel>("Proc_GetProductListKaryOnAPI", queryParameters);
            return _iresult;
        }


        public List<ProductViewModel> getProductCompanyWise(ProductViewModel model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@ProdId", model.ProdID);
            queryParameters.Add("@CmpID", model.CmpID);
            List<ProductViewModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<ProductViewModel>("GetProductCompanyWise", queryParameters);
            return _iresult;
        }

        public List<MBindCompanyList> getProductCompanyList(MBindCompanyList model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@ProdId", model.ProdId);
            queryParameters.Add("@CmpID", model.CmpID);
            List<MBindCompanyList> _iresult = DBHelperDapper.DAAddAndReturnModelList<MBindCompanyList>("GetProductCompanyWise", queryParameters);
            return _iresult;
        }


        public AesPaymentResponse AesPayment(AesPayment Obj)
        {
            var sqlparams = new SqlParameter[] {

                 new SqlParameter {ParameterName="@Fk_MemId",Value=Obj.Fk_MemId},
                 new SqlParameter {ParameterName="@Id",Value=Obj.TransNo??string.Empty},
                 new SqlParameter {ParameterName="@BankName",Value=Obj.BankName??string.Empty},
                 new SqlParameter {ParameterName="@AadharNo",Value=Obj.AadharNo??string.Empty},
                 new SqlParameter {ParameterName="@DeviceId",Value=Obj.DeviceName??string.Empty},
                  new SqlParameter {ParameterName="@Date",Value=Obj.Date??string.Empty},
                  new SqlParameter {ParameterName="@Token",Value=Obj.Token??string.Empty},
                  new SqlParameter {ParameterName="@Amount",Value=Obj.Amount}
            };
            var _proc = @"AesPayment @Fk_MemId,@Id,@BankName,@AadharNo,@DeviceId,@Date,@Token,@Amount";
            var res = this.Database.SqlQuery<AesPaymentResponse>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public List<AesPayment> GetAesHistory(long MemberId)
        {

            var _proc = @"GetPaymentHistory @Fk_MemId ='" + MemberId + "'";
            var res = this.Database.SqlQuery<AesPayment>(_proc).ToList();
            return res;
        }
        public List<ProductPurchaseHistory> GetProductPurchaseHistory_Karyon(long MemberId, string LangId)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@FkMemId", MemberId);
            queryParameters.Add("@LanguageCode", LangId);
            List<ProductPurchaseHistory> _iresult = DBHelperDapper.DAAddAndReturnModelList<ProductPurchaseHistory>("dbo.Proc_GetProductPurchaseHistory_Karyon", queryParameters);
            return _iresult;
        }

        public List<ProductDetialListModel> GetProducHistorytDetail(ProductDetialReq obj)
        {

            var _proc = @"dbo.Proc_GetProductPurchaseHistoryDetail_Karyon @CropCode ='" + obj.CropCode + "',@FKMemid=" + obj.Fk_MemId + ",@PK_OrderId='" + obj.PK_OrderId + "',@LanguageCode='" + obj.LanguageCode + "'";
            var res = this.Database.SqlQuery<ProductDetailbyLangunage>(_proc).ToList();
            List<ProductDetialListModel> data = res.GroupBy(x => x.CropType).Select(x => new ProductDetialListModel
            {
                CropType = x.FirstOrDefault().CropType,
                MethodOfApplication = x.FirstOrDefault().MethodOfApplication,
                ProductDetialList = x.GroupBy(a => a.Days).Select(a => new ProductDetialListModel1
                {
                    Days = a.FirstOrDefault().Days,
                    Date = a.FirstOrDefault().Date,
                    ProductDetailbyLangunageList = a.ToList()
                }).ToList()
            }).ToList();
            return data;
        }

        public List<MsaveMobileDetailsList> GetSameMObileDataDetails(MMobileNo model)
        {
            var sqlparam = new SqlParameter[] {
                new SqlParameter { ParameterName="@mobileNo",Value=model.mobileNo}
            };
            var _proc = @"dbo.GetSameMobileUsers @mobileNo";
            var res = this.Database.SqlQuery<MsaveMobileDetailsList>(_proc, sqlparam).ToList();
            return res;
        }


        public List<MPincodeDetails> GetPincodeDetais(MPincode model)
        {
            var sqlparam = new SqlParameter[] {
                new SqlParameter { ParameterName="@PinCode",Value=model.PinCode}
            };
            var _proc = @"dbo.Proc_GetPinCodeDetail @PinCode";
            var res = this.Database.SqlQuery<MPincodeDetails>(_proc, sqlparam).ToList();
            return res;
        }
        public ResultSet DAlAssociateFranchiseRequest(LifeOne.Models.HomeManagement.HEntity.MemberDetailsForFranchiseModel model)
        {
            var sqlparams = new SqlParameter[] {

                    new SqlParameter { ParameterName="@YearOfBirth",Value=model.Cr_Address ??string.Empty}
            };
            var _proc = @"RegisterationByReferal @InvitedBy,@FirstName,@LastName,@MobileNo,@Password,@Email,@Pincode,@State,@City,@Tehsil,@Iskyc,@AadharNo,@Normalpassword,@Address,@FatherName,@DOB,@Gender,@YearOfBirth";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public MGetMembersDetailsByIdForFranchiseRequest GetMembersDetailsByIdForFranchiseRequest(MBIDRequest obj)
        {
            var sqlparams = new SqlParameter[]
            {
                new SqlParameter { ParameterName="@BID",Value=obj.BID}
            };
            var _proc = @"GetMembersDetailsByIdForFranchiseRequest @BID";
            var res = this.Database.SqlQuery<MGetMembersDetailsByIdForFranchiseRequest>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public MGetMembersDetailsByIdForFranchiseRequest GetMembersDetailsByIdForFranchiseRequest_v2(MBIDRequest obj)
        {
            var sqlparams = new SqlParameter[]
            {
                new SqlParameter { ParameterName="@BID",Value=obj.BID}
            };
            var _proc = @"GetMembersDetailsByIdForFranchiseRequest_V2 @BID";
            var res = this.Database.SqlQuery<MGetMembersDetailsByIdForFranchiseRequest>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public MemberLanguageResponse AddCategoryList(string xml)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@xmldata", xml);
            MemberLanguageResponse _iresult = DBHelperDapper.DAAddAndReturnModel<MemberLanguageResponse>("Proc_AddCropCategory", queryParameters);
            return _iresult;
        }
        public MemberLanguageResponse AddCropProducts(string xml)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@xmldata", xml);
            MemberLanguageResponse _iresult = DBHelperDapper.DAAddAndReturnModel<MemberLanguageResponse>("Proc_AddCropProduct", queryParameters);
            return _iresult;
        }
        public ResultSet AddFomilynTour(FamilyNTourViewModel model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@FK_MemId", model.FK_MemId);
            queryParameters.Add("@Location", model.Location);
            queryParameters.Add("@From_Dt", model.From_Dt);
            queryParameters.Add("@To_Dt", model.To_Dt);
            queryParameters.Add("@No_Of_Person", model.TotalPerson);
            queryParameters.Add("@Hotel_Category", model.Hotel_Category);
            queryParameters.Add("@Domestic_International", model.TravelType);
            queryParameters.Add("@Return_Air_tour", model.IsReturn);
            queryParameters.Add("@Adults", model.Adults);
            queryParameters.Add("@Child_below_12Years", model.Child_below_12Years);
            queryParameters.Add("@Child_above_12Years", model.Child_above_12Years);
            queryParameters.Add("@CreatedBy", model.FK_MemId);
            queryParameters.Add("@TotalDays", model.TotalDays);
            queryParameters.Add("@TotalNights", model.TotalNights);
            queryParameters.Add("@Your_Budget", model.Your_Budget);
            ResultSet res = DBHelperDapper.DAAddAndReturnModel<ResultSet>("Proc_AddFomilynTour", queryParameters);
            return res;
        }
        public VerifyPanOrVoterIdresponse VeriFyPanOrVoterId(VerifyPanOrVoterId model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@Fk_MemId",Value=model.Fk_MemId },
                new SqlParameter { ParameterName="@Type",Value=model.Type ??string.Empty},
                new SqlParameter { ParameterName="@VoterId",Value=model.VoterId ??string.Empty},
                new SqlParameter { ParameterName="@VoterImage",Value=model.VoterImage??string.Empty },
                new SqlParameter { ParameterName="@PanNo",Value=model.PanNo??string.Empty },
                new SqlParameter { ParameterName="@PanImage",Value=model.PanImage ??string.Empty},

                new SqlParameter { ParameterName="@GSTNo",Value=model.GSTNo ??string.Empty},
                new SqlParameter { ParameterName="@GSTImage",Value=model.GSTImage ??string.Empty},
                new SqlParameter { ParameterName="@UdyogAadharNo",Value=model.UdyogAadharNo ??string.Empty},
                new SqlParameter { ParameterName="@UdyogAadharImage",Value=model.UdyogAadharImage ??string.Empty},
                new SqlParameter { ParameterName="@CINNo",Value=model.CINNo ??string.Empty},
                new SqlParameter { ParameterName="@CINImage",Value=model.CINImage ??string.Empty},
                new SqlParameter { ParameterName="@AccountType",Value=model.AccountType ??string.Empty},

            };
            var _proc = @"UPDATEPanOrVoterIdDetail @Fk_MemId,@Type,@VoterId,@VoterImage,@PanNo,@PanImage,@GSTNo,@GSTImage,@UdyogAadharNo,@UdyogAadharImage,@CINNo,@CINImage,@AccountType";
            var res = this.Database.SqlQuery<VerifyPanOrVoterIdresponse>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public ResultSet FranchisePaymentRequest(WalletJsonRequest model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@paymentdate",Value=model.paymentdate ??string.Empty},
                new SqlParameter { ParameterName="@paymentmode",Value=model.paymentmode ??string.Empty},
                new SqlParameter { ParameterName="@transactionId",Value=model.transactionId ??string.Empty },
                new SqlParameter { ParameterName="@amount",Value=model.amount },
                new SqlParameter { ParameterName="@filepath",Value=model.filepath??string.Empty },
                new SqlParameter { ParameterName="@memberId",Value=model.memberId },
                new SqlParameter { ParameterName="@FranchiseId",Value=model.FranchiseId }
            };
            var _proc = @"Proc_FranchisePaymentRequest @paymentdate,@paymentmode,@transactionId,@amount,@filepath,@memberId,@FranchiseId";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }


        public ResultSet FranchisePaymentRequest_V1(WalletJsonRequest model)
        {
            var sqlparams = new SqlParameter[] {

                new SqlParameter { ParameterName="@PK_FranchiseId",Value=model.FranchiseId },
                new SqlParameter { ParameterName="@paymentdate",Value=model.paymentdate ??string.Empty},
                new SqlParameter { ParameterName="@paymentmode",Value=model.paymentmode ??string.Empty},
                new SqlParameter { ParameterName="@transactionId",Value=model.transactionId ??string.Empty },
                new SqlParameter { ParameterName="@amount",Value=model.amount },
                new SqlParameter { ParameterName="@filepath",Value=model.filepath??string.Empty },
                new SqlParameter { ParameterName="@memberId",Value=model.memberId }
            };
            var _proc = @"Proc_FranchisePaymentRequest_V1 @PK_FranchiseId,@paymentdate,@paymentmode,@transactionId,@amount,@filepath,@memberId";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public KaryionOrderResponse KaryonOrderSmryItemsAPI(KaryonOrderSmryItemsAPIViewModel model)
        {
            var sqlparam = new SqlParameter[] {
                 new SqlParameter { ParameterName="@InvoiceNo",Value=model.InvoiceNo},
                  new SqlParameter { ParameterName="@ProductCode",Value=model.ProductCode},
                 new SqlParameter { ParameterName="@MRP",Value=model.MRP},
                 new SqlParameter { ParameterName="@DP",Value=model.DP},
                 new SqlParameter { ParameterName="@ProductQty",Value=model.ProductQty},
                 new SqlParameter { ParameterName="@TaxableAmount",Value=model.TaxableAmount},
                 new SqlParameter { ParameterName="@GSTAmount",Value=model.GSTAmount},
                 new SqlParameter { ParameterName="@TotalAmount",Value=model.TotalAmount},
                 new SqlParameter { ParameterName="@TotalProductPV",Value=model.TotalProductPV}
                 };
            var _proc = @"Proc_KaryonOrderSmryItemsAPI @InvoiceNo,@ProductCode,@MRP,@DP,@ProductQty,@TaxableAmount,@GSTAmount,@TotalAmount,@TotalProductPV";
            var res = this.Database.SqlQuery<KaryionOrderResponse>(_proc, sqlparam).FirstOrDefault();
            return res;
        }

        public KaryionOrderResponse KaryonOrderBillingSummary(MOrderBillingAPI model)
        {
            var sqlparam = new SqlParameter[] {
                 new SqlParameter { ParameterName="@KaryOnLoginId",Value=model.KaryOnLoginId},
                 new SqlParameter { ParameterName="@InvoiceNo",Value=model.InvoiceNo},
                 new SqlParameter { ParameterName="@InvoiceDate",Value=model.InvoiceDate},
                 new SqlParameter { ParameterName="@DiscountAmount",Value=model.DiscountAmount},
                 new SqlParameter { ParameterName="@TaxableAmount",Value=model.TaxableAmount},
                 new SqlParameter { ParameterName="@GSTAmount",Value=model.GSTAmount},
                 new SqlParameter { ParameterName="@TotalAmount",Value=model.TotalAmount},
                 new SqlParameter { ParameterName="@TotalPv",Value=model.TotalPv},
                 new SqlParameter { ParameterName="@AirOrbit",Value=model.AirOrbit},
                 new SqlParameter { ParameterName="@WaterOrbit",Value=model.WaterOrbit},
                 new SqlParameter { ParameterName="@SpaceOrbit",Value=model.SpaceOrbit},
                 };
            var _proc = @"Proc_KaryonOrderBillingSummary @KaryOnLoginId,@InvoiceNo,@InvoiceDate,@DiscountAmount,@TaxableAmount,@GSTAmount,@TotalAmount,@TotalPv,@AirOrbit,@WaterOrbit,@SpaceOrbit";
            var res = this.Database.SqlQuery<KaryionOrderResponse>(_proc, sqlparam).FirstOrDefault();
            return res;
        }


        public KaryionOrderResponse AddKaryonOrderSummaryDAL(MSummaryAndOrderItemsInformation model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@KaryOnLoginId", model.KaryOnLoginId);
            queryParameters.Add("@InvoiceNo", model.InvoiceNo);
            queryParameters.Add("@InvoiceDate", model.InvoiceDate);
            queryParameters.Add("@DiscountAmount", model.DiscountAmount);
            queryParameters.Add("@TaxableAmount", model.TaxableAmount);
            queryParameters.Add("@GSTAmount", model.GSTAmount);
            queryParameters.Add("@TotalAmount", model.TotalAmount);
            queryParameters.Add("@TotalPv", model.TotalPv);
            queryParameters.Add("@AirOrbit", model.AirOrbit);
            queryParameters.Add("@WaterOrbit", model.WaterOrbit);
            queryParameters.Add("@SpaceOrbit", model.SpaceOrbit);
            queryParameters.Add("@xmldata", model.xmldata);
            KaryionOrderResponse _iresult = DBHelperDapper.DAAddAndReturnModel<KaryionOrderResponse>("Proc_AddKaryonOrderSummaryWithItems", queryParameters);
            return _iresult;
        }

        public KaryionOrderResponse DALUPdateKYC(ApproveDeclineKYCKaryon _model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginID", _model.LoginId);
            queryParameters.Add("@Type", _model.Type);
            queryParameters.Add("@Status", _model.Status);
            queryParameters.Add("@Remark", _model.Remarks);
            KaryionOrderResponse _iresult = DBHelperDapper.DAAddAndReturnModel<KaryionOrderResponse>("ApproveDecinePanOrVoterIdAPIKaryon", queryParameters);
            return _iresult;
        }



        public KaryionOrderResponse UpdateUserProfile(UpdateUserProfileViewModel model)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@LoginId",Value=model.LoginId},
                new SqlParameter { ParameterName="@mobile",Value=model.mobile},
                new SqlParameter { ParameterName="@firstName",Value=model.firstName},
                new SqlParameter { ParameterName="@lastName",Value=model.lastName},
                new SqlParameter { ParameterName="@Pincode",Value=model.PinCode},
                new SqlParameter { ParameterName="@PanCard",Value=model.pancard},
                new SqlParameter { ParameterName="@AadharNo",Value=model.aadhar},
                new SqlParameter { ParameterName="@memberBankName",Value=model.memberBankName},
                new SqlParameter { ParameterName="@memberBranch",Value=model.memberBranch},
                new SqlParameter { ParameterName="@memberAccNo",Value=model.memberAccNo},
                new SqlParameter { ParameterName="@bankHolderName",Value=model.bankHolderName},
                new SqlParameter { ParameterName="@ifscCode",Value=model.ifscCode}
            };
            var _proc = @"Proc_UpdatePersonalDetailByAPI @LoginId,@mobile,@firstName,@lastName,@Pincode,@PanCard,@AadharNo,@memberBankName,@memberBranch,@memberAccNo,@bankHolderName,@ifscCode";
            var res = this.Database.SqlQuery<KaryionOrderResponse>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public MGetMembersDetailsByIdForFranchiseRequest GetMembersDetailsByIdForFranchiseRequest_v3(MBIDRequest_V3 obj)
        {
            var sqlparams = new SqlParameter[]
            {
                new SqlParameter { ParameterName="@BID",Value=obj.BID},
                  new SqlParameter { ParameterName="@keyId",Value=obj.KeyId}
            };
            var _proc = @"GetMembersDetailsByIdForFranchiseRequest_V3 @BID,@keyId";
            var res = this.Database.SqlQuery<MGetMembersDetailsByIdForFranchiseRequest>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public List<StateMasterListModel> StateMasterList()
        {
            return this.Database.SqlQuery<StateMasterListModel>("Proc_StateMasterList").ToList();
        }

        public List<DistrictMasterListModel> DistrictMasterList(DistrictMasterListModel model)
        {
            var sqlparams = new SqlParameter[]
            {
                new SqlParameter { ParameterName="@FK_StateId",Value=model.StateId}
            };
            var _proc = @"Proc_DistrictMasterList @FK_StateId";
            return this.Database.SqlQuery<DistrictMasterListModel>(_proc, sqlparams).ToList();
        }
        public FranchiseBalanceDetailModel FranchiseBalanceDetail(FranchiseBalanceDetailRequestModel model)
        {
            var sqlparams = new SqlParameter[]
            {
                new SqlParameter { ParameterName="@FranchiseId",Value=model.FranchiseId},
                new SqlParameter { ParameterName="@FkMemId",Value=model.Fk_MemId}
            };
            var _proc = @"Proc_FranchiseBalanceDetail @FranchiseId,@FkMemId";
            var data = this.Database.SqlQuery<FranchiseBalanceDetailModel>(_proc, sqlparams).FirstOrDefault();
            if (!String.IsNullOrEmpty(data.PinCode))
                data.Status = true;
            return data;
        }

        public List<FranchiseCrDrDetails> FranchiseCrDrDetails(FranchiseCrDetailsReq model)
        {
            var sqlparams = new SqlParameter[]
            {
                new SqlParameter { ParameterName="@FkMemId",Value=model.Fk_MemId},
                new SqlParameter { ParameterName="@FKReqId",Value=model.FK_ReqId}
            };
            var _proc = @"Proc_FranchiseProductCrDrDetail @FkMemId,@FKReqId ";
            var data = this.Database.SqlQuery<FranchiseCrDrDetails>(_proc, sqlparams).ToList();
            return data;
        }

        public ResultSet FranchiseCancelOrder(MCancelOrder model)
        {
            var sqlparams = new SqlParameter[] {

                new SqlParameter { ParameterName="@OrderNo",Value=model.OrderNo}
            };
            var _proc = @"FranchiseCancelOrder @OrderNo";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public FranchiseAllowRequestDetailModel FranchiseAllowRequestByID(FranchiseAllowRequestModel model)
        {
            var sqlparams = new SqlParameter[]
            {
                new SqlParameter { ParameterName="@FK_MemId",Value=model.Fk_MemId}
            };
            var _proc = @"Proc_AllowFranchiseRegistrationByID @FK_MemId ";
            var data = this.Database.SqlQuery<FranchiseAllowRequestDetailModel>(_proc, sqlparams).FirstOrDefault();
            if (data != null)
            {
                data.Flag = 1;
                data.Msg = "Record Exist";
            }
            else
            {
                data = new FranchiseAllowRequestDetailModel();
                data.Flag = 0;
                data.Msg = "Record Does Not Exist";
            }
            return data;
        }
        public ResultSet CompanionPaymentRequest(CompanionPaymentReq model)
        {
            var sqlparams = new SqlParameter[] {

                new SqlParameter { ParameterName="@Fk_FranchiseId",Value=model.FranchiseId},
                  new SqlParameter { ParameterName="@Fk_MemId",Value=model.Fk_MemId},
                    new SqlParameter { ParameterName="@NoOfShare",Value=model.NoOfShare},
                      new SqlParameter { ParameterName="@Amount",Value=model.Amount}
            };
            var _proc = @"Proc_CompanionPaymentRequest @Fk_FranchiseId,@Fk_MemId,@NoOfShare,@Amount";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public ResultSet VirtualCompanionPaymentRequest(WalletJsonRequest model)
        {
            var sqlparams = new SqlParameter[] {

                new SqlParameter { ParameterName="@PK_FranchiseId",Value=model.FranchiseId },
                new SqlParameter { ParameterName="@paymentdate",Value=model.paymentdate ??string.Empty},
                new SqlParameter { ParameterName="@paymentmode",Value=model.paymentmode ??string.Empty},
                new SqlParameter { ParameterName="@transactionId",Value=model.transactionId ??string.Empty },
                new SqlParameter { ParameterName="@amount",Value=model.amount },
                new SqlParameter { ParameterName="@filepath",Value=model.filepath??string.Empty },
                new SqlParameter { ParameterName="@memberId",Value=model.memberId },
                new SqlParameter { ParameterName="@Pk_CompanionId",Value=model.Pk_CompanionId }
            };
            var _proc = @"Proc_VirtualPaymentRequest @PK_FranchiseId,@paymentdate,@paymentmode,@transactionId,@amount,@filepath,@memberId,@Pk_CompanionId";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public List<VirtualCompanionRequestForPaymentHistory> CompanionPaymentHistory(CompanionPayMentHistoryReq model)
        {
            var sqlparams = new SqlParameter[]
            {
                new SqlParameter { ParameterName="@Pk_CompanionId",Value=model.Pk_CompanionId},
            };
            var _proc = @"dbo.Proc_GetVirtualCompanionPaymentHistory @Pk_CompanionId";
            var data = this.Database.SqlQuery<VirtualCompanionRequestForPaymentHistory>(_proc, sqlparams).ToList();
            return data;
        }


        public PayAddaResponse DALUpdateRechargePendingStatus(string Status, string ClientTransactionId, string TransactionId, string OperatorTransactionId)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Status", Status);
                queryParameters.Add("@ClientTransactionId", ClientTransactionId);
                queryParameters.Add("@TransactionId", TransactionId);
                queryParameters.Add("@OperatorTransactionId", OperatorTransactionId);
                PayAddaResponse _iresult = DBHelperDapper.DAAddAndReturnModel<PayAddaResponse>("updatepayaddarechargeStatus", queryParameters);
                return _iresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<MCompanyDetails> GetCompanyListFromDB()
        {
            var _proc = @"GetProcDetailsForINRTransaction";
            var res = this.Database.SqlQuery<MCompanyDetails>(_proc).ToList();
            return res;
        }


        public CustomerDetailsResponse GetCustomerDetailsByMobile(string Mobile)
        {
            var sqlparams = new SqlParameter[] {
                new SqlParameter { ParameterName="@Mobile",Value=Mobile??string.Empty}
            };
            var _proc = @"CustomerDetailsByMobile @Mobile";
            var res = this.Database.SqlQuery<CustomerDetailsResponse>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        public List<ProductDetailByFranchise> GetProductFranchiseByFranchise(ProductReq model)
        {
            var sqlparams = new SqlParameter[]
            {
                new SqlParameter { ParameterName="@ProcId",Value=1},
                new SqlParameter { ParameterName="@Value",Value=0},
                new SqlParameter { ParameterName="@FranchiseId",Value=model.FranchiseId},

            };
            var _proc = @"dbo.Proc_BindFranchiseProductDDl @ProcId,@Value,@FranchiseId";
            var data = this.Database.SqlQuery<ProductDetailByFranchise>(_proc, sqlparams).ToList();
            return data;
        }
        public List<MAssociateMonthlyBusiness> APIAssociateBusinessReport(MAssociateMonthlyBusiness model)
        {
            var sqlparams = new DynamicParameters();
            sqlparams.Add("@LoginId", model.LoginId);
            sqlparams.Add("@FromDate", string.IsNullOrEmpty(model.FromDate) ? null : model.FromDate);
            sqlparams.Add("@ToDate", string.IsNullOrEmpty(model.ToDate) ? null : model.ToDate);


            //var _proc = @"AssociateMonthlyBusinessReport @LoginId,@FromDate,@ToDate";
            var data = DBHelperDapper.DAAddAndReturnModelList<MAssociateMonthlyBusiness>("AssociateMonthlyBusinessReport", sqlparams);
            return data;
        }
        public MGetMembersDetailsByIdForFranchiseRequest GetMembersDetailsByIdForFranchiseRequest_v3MasterFranchise(MBIDRequest_V3 obj)
        {
            var sqlparams = new SqlParameter[]
            {
                new SqlParameter { ParameterName="@BID",Value=obj.BID},
                  new SqlParameter { ParameterName="@keyId",Value=obj.KeyId}
            };
            var _proc = @"GetMembersDetailsByIdForFranchiseRequestMasterFranchise_V3 @BID,@keyId";
            var res = this.Database.SqlQuery<MGetMembersDetailsByIdForFranchiseRequest>(_proc, sqlparams).FirstOrDefault();
            return res;
        }
        #region Shppoing
        public List<CategoryRequest> GetCategory()
        {

            var sqlparams = new DynamicParameters();
            var data = DBHelperDapper.DAAddAndReturnModelList<CategoryRequest>("GetCategory", sqlparams);
            return data;
        }
        public List<ProductRequest> GetProductDetails(ProductRequest productRequest)
        {
            var sqlparams = new DynamicParameters();
            sqlparams.Add("@Pk_ProductId", productRequest.Pk_ProductId);
            sqlparams.Add("@Fk_CategoryId", productRequest.Fk_CategoryId);
            var data = DBHelperDapper.DAAddAndReturnModelList<ProductRequest>("GetProductDetails", sqlparams);
            return data;
        }
        public List<AddressRequest> GetAddressDetails(AddressRequest productRequest)
        {
            var sqlparams = new DynamicParameters();
            sqlparams.Add("@Pk_AddressId", productRequest.Pk_AddressId);
            sqlparams.Add("@Fk_MemId", productRequest.Fk_MemId);
            var data = DBHelperDapper.DAAddAndReturnModelList<AddressRequest>("GetAddressDetails", sqlparams);
            return data;
        }
        public ResultSet AddAddress(AddressRequest productRequest)
        {

            var sqlparams = new SqlParameter[] {

                new SqlParameter { ParameterName="@Fk_MemId",Value=productRequest.Fk_MemId},
                new SqlParameter { ParameterName="@Name",Value=productRequest.Name},
                new SqlParameter { ParameterName="@MobileNo",Value=productRequest.MobileNo ??string.Empty},
                new SqlParameter { ParameterName="@Landmark",Value=productRequest.Landmark ??string.Empty },
                new SqlParameter { ParameterName="@Address",Value=productRequest.Address},
                new SqlParameter { ParameterName="@State",Value=productRequest.State },
                new SqlParameter { ParameterName="@City",Value=productRequest.City },
                new SqlParameter { ParameterName="@PinCode",Value=productRequest.PinCode },
                new SqlParameter { ParameterName="@AddressType",Value=productRequest.AddressType },
                new SqlParameter { ParameterName="@IsDefault",Value=productRequest.IsDefault }
            };
            var _proc = @"InsertAddress @Fk_MemId,@Name,@MobileNo,@Landmark,@Address,@State,@City,@PinCode,@AddressType,@IsDefault";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;

        }
        public ResultSet UpdateAddress(AddressRequest productRequest)
        {

            var sqlparams = new SqlParameter[] {

                new SqlParameter { ParameterName="@Fk_MemId",Value=productRequest.Fk_MemId},
                new SqlParameter { ParameterName="@Name",Value=productRequest.Name},
                new SqlParameter { ParameterName="@MobileNo",Value=productRequest.MobileNo ??string.Empty},
                new SqlParameter { ParameterName="@Landmark",Value=productRequest.Landmark ??string.Empty },
                new SqlParameter { ParameterName="@Address",Value=productRequest.Address},
                new SqlParameter { ParameterName="@State",Value=productRequest.State },
                new SqlParameter { ParameterName="@City",Value=productRequest.City },
                new SqlParameter { ParameterName="@PinCode",Value=productRequest.PinCode },
                new SqlParameter { ParameterName="@AddressType",Value=productRequest.AddressType },
                new SqlParameter { ParameterName="@IsDefault",Value=productRequest.IsDefault },
                new SqlParameter { ParameterName="@Pk_AddressId",Value=productRequest.Pk_AddressId }
            };
            var _proc = @"UpdateAddress @Fk_MemId,@Name,@MobileNo,@Landmark,@Address,@State,@City,@PinCode,@AddressType,@IsDefault,@Pk_AddressId";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;

        }
        public ResultSet DeleteAddress(AddressRequest productRequest)
        {

            var sqlparams = new SqlParameter[] {

                new SqlParameter { ParameterName="@Fk_MemId",Value=productRequest.Fk_MemId},              
                new SqlParameter { ParameterName="@Pk_AddressId",Value=productRequest.Pk_AddressId }
            };
            var _proc = @"DeleteAddress @Fk_MemId,@Pk_AddressId";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;

        }
        public ResultSet PlaceOrder(PlaceShoppingOrderRequest placeShoppingOrderRequest)
        {
            DataTable dt = new DataTable();

            var sqlparams = new SqlParameter[] {

                new SqlParameter { ParameterName="@FK_MemId",Value=placeShoppingOrderRequest.Fk_MemId},
                new SqlParameter { ParameterName="@FK_AddressId",Value=placeShoppingOrderRequest.Fk_AddressId },
                new SqlParameter { ParameterName="@dtProductDetails",Value=placeShoppingOrderRequest.dtProductDetails??dt },
                new SqlParameter { ParameterName="@dtPaymentDetails",Value=placeShoppingOrderRequest.dtPaymentDetails??dt },
                new SqlParameter { ParameterName="@OpCode",Value=placeShoppingOrderRequest.OpCode },
                new SqlParameter { ParameterName="@OrderId",Value=placeShoppingOrderRequest.OrderIdRazorpay??string.Empty },
                new SqlParameter { ParameterName="@FK_OrderId",Value=placeShoppingOrderRequest.FK_OrderId }
            };
            sqlparams[2].SqlDbType = SqlDbType.Structured;
            sqlparams[2].ParameterName = "@dtProductDetails";
            sqlparams[2].TypeName = "dbo.dtProductDetails";
            sqlparams[2].Value = placeShoppingOrderRequest.dtProductDetails;

            sqlparams[3].SqlDbType = SqlDbType.Structured;
            sqlparams[3].ParameterName = "@dtPaymentDetails";
            sqlparams[3].TypeName = "dbo.dtPaymentDetails";
            sqlparams[3].Value = placeShoppingOrderRequest.dtPaymentDetails;

            var _proc = @"PlaceShoppingOrder @FK_MemId,@FK_AddressId,@dtProductDetails,@dtPaymentDetails,@OpCode,@OrderId,@FK_OrderId";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;

        }
        public List<GetOrdersRequest> GetOrderList(GetOrdersRequest getOrdersRequest)
        {
            var sqlparams = new DynamicParameters();
            sqlparams.Add("@Fk_MemId", getOrdersRequest.Fk_MemId);          
            sqlparams.Add("@OpCode", getOrdersRequest.OpCode);          
            var data = DBHelperDapper.DAAddAndReturnModelList<GetOrdersRequest>("PlaceShoppingOrder", sqlparams);
            return data;
        }
        public List<ShoppingOrderDetailsRequest> ShoppingOrderDetails(ShoppingOrderDetailsRequest getOrdersRequest)
        {
            var sqlparams = new DynamicParameters();
            sqlparams.Add("@FK_OrderId", getOrdersRequest.FK_OrderId);
            sqlparams.Add("@OpCode", getOrdersRequest.OpCode);
            sqlparams.Add("@token", getOrdersRequest.token);
            var data = DBHelperDapper.DAAddAndReturnModelList<ShoppingOrderDetailsRequest>("PlaceShoppingOrder", sqlparams);
            
            return data;
        }

     


        #endregion Shopping

        public List<PayoutDetailsRequest> PayoutIncomeReportType(PayoutDetailsRequest getOrdersRequest)
        {
            var sqlparams = new DynamicParameters();
            sqlparams.Add("@FK_MemID", getOrdersRequest.Fk_MemId);
            sqlparams.Add("@IncomeType", getOrdersRequest.Fk_PlanId);
            var data = DBHelperDapper.DAAddAndReturnModelList<PayoutDetailsRequest>("PayoutIncomeReportType", sqlparams);
            return data;
        }
       
        public List<BusinessSummaryRequest> GetBusinessSummary(BusinessSummaryRequest businessSummaryRequest)
        {
            var sqlparams = new DynamicParameters();
            sqlparams.Add("@Fk_MemId", businessSummaryRequest.Fk_MemId);        
            sqlparams.Add("@FromDate", businessSummaryRequest.FromDate);        
            sqlparams.Add("@ToDate", businessSummaryRequest.ToDate);        
            var data = DBHelperDapper.DAAddAndReturnModelList<BusinessSummaryRequest>("GetBusinessSummaryForAssociate", sqlparams);
            return data;
        }

      

    }
}