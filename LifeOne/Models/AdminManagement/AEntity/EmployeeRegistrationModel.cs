using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.API;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class EmployeeRegistrationModel
    {
        public long PK_MembersInfoUniqueid { get; set; }
        public long FK_MemId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public DateTime DOB { get; set; }
        public string DOBStr { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string MarritalStatus { get; set; }
        public string NomineeName { get; set; }
        public string RelationWithNominee { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string VillageName { get; set; }
        public string LandMark { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Tehsil { get; set; }
        public string PinCode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string AlternateMobile { get; set; }
        public string Email { get; set; }
        public int CreatedBy { get; set; }
        public int StateId { get; }
        public int CityId { get; }
        public string AccountPurpose { get; set; }
        public string RegistrationType { get; set; }
        public string OrganisationType { get; set; }
        public string CustAadharNo { get; set; }
        public string Aadhar { get; set; }
        public string CustAadharImg { get; set; }
        public string CustPanNo { get; set; }
        public string CustGstNo { get; set; }
        public DateTime CommencementDate { get; set; }
        public string SpouseName { get; set; }
        public string SpouseAadharNo { get; set; }
        public string SpouseAadharImg { get; set; }
        public string SpousePanNo { get; set; }
        public string District { get; set; }
        public string Zone { get; set; }
        public string YearOfBirth { get; set; }
        public EmployeeRegResponseModel EmplReg()
        {
            try
            {
                string Password = new Random().Next(999999999).ToString();
                EmployeeRegResponseModel _response = new EmployeeRegResponseModel();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_MemId", FK_MemId);
                queryParameters.Add("@FirstName", FirstName);
                queryParameters.Add("@LastName", LastName);
                queryParameters.Add("@Mobile", Mobile);
                queryParameters.Add("@Email", Email);
                queryParameters.Add("@DOB", DOB);
                queryParameters.Add("@Gender", Gender);
                queryParameters.Add("@Aadhar", Aadhar);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                queryParameters.Add("@Password", Password);
                _response = DBHelperDapper.DAAddAndReturnModel<EmployeeRegResponseModel>("ProEmployeeRegistration", queryParameters);
                if (_response.Status)
                {
                    string Message = string.Empty;
                    Message = "Dear " + FirstName + "" + LastName + "\nYou have successfully registered with LifeOne.\nLoginId : " + _response.LoginId + "\nPassword : " + Password+ "\nBest Regards\nFamilyN";
                    SMS.SendSMS(Mobile, Message);  
                }
                return _response;
            }
            catch (Exception e)
            {
                return new EmployeeRegResponseModel { Flag = 0, Status = false, Message = e.Message, response = "failed" };
            }
        }
    }
    public class EmployeeRegResponseModel
    {
        public int Flag { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public string response { get; set; }
        public string LoginId { get; set; }
    }
}