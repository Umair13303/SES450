using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.DBF
{
    #region DB-FIRST APPROACH CLASS
    //public class AccFeeChallan
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public string Code { get; set; }
    //    public string InvoiceNo { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<int> SessionId { get; set; }
    //    public Nullable<int> ClassId { get; set; }
    //    public Nullable<int> RegistrationTypeId { get; set; }
    //    public Nullable<int> ClassRegistrationId { get; set; }
    //    public Nullable<System.DateTime> TransactionDate { get; set; }
    //    public Nullable<System.DateTime> DueDate { get; set; }
    //    public Nullable<System.DateTime> ExpiryDate { get; set; }
    //    public Nullable<int> FeeStructureId { get; set; }
    //    public Nullable<decimal> GrossRecievable { get; set; }
    //    public Nullable<decimal> LateFee { get; set; }
    //    public Nullable<decimal> Discount { get; set; }
    //    public Nullable<decimal> NetRecievable { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<int> DocumentStatus { get; set; }
    //    public Nullable<int> DocType { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //}
    //public class AccFeeChallanDetail
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public Nullable<int> FeeChallanId { get; set; }
    //    public Nullable<int> FeeTypeId { get; set; }
    //    public Nullable<decimal> ActualAmount { get; set; }
    //    public Nullable<decimal> ChargedAmount { get; set; }
    //    public Nullable<decimal> Difference { get; set; }
    //}
    //public class AccFeeStructure
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public string Code { get; set; }
    //    public Nullable<int> CampusId { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<int> SessionId { get; set; }
    //    public Nullable<int> ClassId { get; set; }
    //    public Nullable<int> WHTaxPolicyId { get; set; }
    //    public Nullable<decimal> TotalFeeExclusive { get; set; }
    //    public Nullable<decimal> WHTAmount { get; set; }
    //    public Nullable<decimal> TotalFee { get; set; }
    //    public Nullable<System.DateTime> EffectiveFrom { get; set; }
    //    public Nullable<System.DateTime> ExpiredOn { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<int> DocType { get; set; }
    //    public Nullable<int> DocumentStatus { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //}
    //public class AccFeeStructureDetail
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public Nullable<int> FeeStructureId { get; set; }
    //    public Nullable<int> FeeTypeId { get; set; }
    //    public Nullable<int> RevenueAccountId { get; set; }
    //    public Nullable<int> AssetAccountId { get; set; }
    //    public Nullable<int> LiabilityAccountId { get; set; }
    //    public Nullable<int> CostOfSaleAccountId { get; set; }
    //    public Nullable<decimal> Amount { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class AccJournal
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public string Code { get; set; }
    //    public Nullable<System.DateTime> TransactionDate { get; set; }
    //    public string FolioNo { get; set; }
    //    public string Reference { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<int> DebitAccountId { get; set; }
    //    public Nullable<decimal> DebitAmount { get; set; }
    //    public Nullable<int> CreditAccountId { get; set; }
    //    public Nullable<decimal> CreditAmount { get; set; }
    //    public Nullable<decimal> BalanceAmount { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> DocType { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //}
    //public class AccLedgerFee
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public Nullable<System.DateTime> Date { get; set; }
    //    public string ReferenceCode { get; set; }
    //    public string VoucherCode { get; set; }
    //    public Nullable<int> AccountId { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<decimal> Debit { get; set; }
    //    public Nullable<decimal> Credit { get; set; }
    //    public Nullable<decimal> Balance { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> DocumentType { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //}
    //public class AccStudentOB
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public Nullable<int> FeeTypeId { get; set; }
    //    public Nullable<int> SessionId { get; set; }
    //    public Nullable<int> ClassId { get; set; }
    //    public Nullable<int> ClassRegistrationId { get; set; }
    //}
    //public class AppClass
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public string Code { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<int> StudyLevelId { get; set; }
    //    public Nullable<int> StudyGroupId { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> EffectiveFrom { get; set; }
    //    public Nullable<System.DateTime> ExpiredOn { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<int> DocType { get; set; }
    //    public Nullable<int> DocumentStatus { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //}
    //public class AppClass_GetListByParam_Result
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //}
    //public class AppClassRegistration
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public Nullable<int> SessionId { get; set; }
    //    public Nullable<int> StudentId { get; set; }
    //    public Nullable<int> ClassId { get; set; }
    //    public string RollCallId { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<int> DocumentStatus { get; set; }
    //    public Nullable<int> DocType { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //}
    //public class AppSession
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public string Code { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<int> CampusId { get; set; }
    //    public Nullable<System.DateTime> SessionStartOn { get; set; }
    //    public Nullable<System.DateTime> SessionEndOn { get; set; }
    //    public Nullable<int> EnrollmentTypeId { get; set; }
    //    public string ClassIds { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> EffectiveFrom { get; set; }
    //    public Nullable<System.DateTime> ExpiredOn { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<int> DocType { get; set; }
    //    public Nullable<int> DocumentStatus { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //}
    //public class AppStudent
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public Nullable<int> SessionId { get; set; }
    //    public string Code { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string CnicNo_FormBNo { get; set; }
    //    public Nullable<int> Gender { get; set; }
    //    public Nullable<System.DateTime> DOB { get; set; }
    //    public Nullable<int> MartialStatusId { get; set; }
    //    public Nullable<int> ReligionId { get; set; }
    //    public Nullable<int> NationalityId { get; set; }
    //    public string ResedenitalAddress { get; set; }
    //    public string MobileNumber { get; set; }
    //    public string EmailAddress { get; set; }
    //    public string ParentName { get; set; }
    //    public string ParentCnic { get; set; }
    //    public Nullable<int> ParentStudyLevelId { get; set; }
    //    public Nullable<int> OccupationId { get; set; }
    //    public Nullable<int> RelationshipId { get; set; }
    //    public Nullable<decimal> MonthlyIncome { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<int> DocumentStatus { get; set; }
    //    public Nullable<int> DocType { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //}
    //public class BillingMethod
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class CampusType
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class ChallanMethod
    //{
    //    public int Id { get; set; }
    //    public string Code { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<int> ChallanNo { get; set; }
    //    public Nullable<int> MonthsNo { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class ChargingMethod
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //}
    //public class City
    //{
    //    public int Id { get; set; }
    //    public Nullable<int> CountryId { get; set; }
    //    public string Code { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<int> StateId { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class CoaCatagory
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public string ShortCode { get; set; }
    //    public Nullable<int> CoaTypeId { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class CoaType
    //{
    //    public int Id { get; set; }
    //    public string ShortCode { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class Country
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public string Code { get; set; }
    //    public string CallingCode { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class DocType
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public string Purpose { get; set; }
    //}
    //public class DocumentStatus
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<int> DocumentId { get; set; }
    //}
    //public class EducationBoard
    //{
    //    public int Id { get; set; }
    //    public string Code { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class EnrollmentType
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class Gender
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //}
    //public class GeneralBranch
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public string Code { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<int> CampusTypeId { get; set; }
    //    public Nullable<int> OrganizationTypeId { get; set; }
    //    public Nullable<int> CountryId { get; set; }
    //    public Nullable<int> CityId { get; set; }
    //    public string Address { get; set; }
    //    public string ContactNo { get; set; }
    //    public string EmailAddress { get; set; }
    //    public string NTNNo { get; set; }
    //    public string Remarks { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> EffectiveFrom { get; set; }
    //    public Nullable<System.DateTime> ExpiredOn { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<int> DocType { get; set; }
    //    public Nullable<int> DocumentStatus { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //}
    //public class GeneralBranch_GetListByParam_Result
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public string Description { get; set; }
    //    public string ExtraClass { get; set; }
    //}
    //public class GeneralBranch_GetListBySearch_Result
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public string Code { get; set; }
    //    public string Description { get; set; }
    //    public string CampusType { get; set; }
    //    public string OrganizationType { get; set; }
    //    public string Country { get; set; }
    //    public string City { get; set; }
    //    public string Address { get; set; }
    //    public string ContactNo { get; set; }
    //    public string EmailAddress { get; set; }
    //    public string NTNNo { get; set; }
    //}
    //public class GeneralBranchSetting
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public Nullable<int> CampusId { get; set; }
    //    public Nullable<int> RollCallSystemId { get; set; }
    //    public Nullable<int> BillingMethodId { get; set; }
    //    public string StudyLevelIds { get; set; }
    //    public string StudyGroupIds { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> EffectiveFrom { get; set; }
    //    public Nullable<System.DateTime> ExpiredOn { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<int> PolicyPeriodId { get; set; }
    //    public Nullable<int> ChallanMethodId { get; set; }
    //}
    //public class GeneralCompany
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public string CompanyName { get; set; }
    //    public Nullable<int> CityId { get; set; }
    //    public Nullable<int> CountryId { get; set; }
    //    public string AddressLine { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public string EmailAddress { get; set; }
    //    public string CompanyWebsite { get; set; }
    //    public string UploadLogo { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<System.DateTime> CreateOn { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //}
    //public class GeneralCompany_GetDetailByParam_Result
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public string CompanyName { get; set; }
    //    public Nullable<int> CityId { get; set; }
    //    public Nullable<int> CountryId { get; set; }
    //    public string AddressLine { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public string EmailAddress { get; set; }
    //    public string CompanyWebsite { get; set; }
    //    public string UploadLogo { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public string CityName { get; set; }
    //    public string CountryName { get; set; }
    //    public Nullable<System.DateTime> CreateOn { get; set; }
    //}
    //public class GeneralEmailSetting
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public string StmpClient { get; set; }
    //    public Nullable<int> OutGoingStmpPort { get; set; }
    //    public string EmailAddress { get; set; }
    //    public string Password { get; set; }
    //    public Nullable<bool> IsSslEnabled { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //}
    //public class GeneralEmailSetting_GetDetailByParam_Result
    //{
    //    public int Id { get; set; }
    //    public string StmpClient { get; set; }
    //    public Nullable<int> OutGoingStmpPort { get; set; }
    //    public string EmailAddress { get; set; }
    //    public string Password { get; set; }
    //    public Nullable<bool> IsSslEnabled { get; set; }
    //}
    //public class GeneralRightSetting
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public Nullable<int> RightId { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<int> URLTypeId { get; set; }
    //    public Nullable<int> CreateBy { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> EffectiveFrom { get; set; }
    //    public Nullable<System.DateTime> ExpiredOn { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //}
    //public class GeneralRightSetting_GetListByParam_Result
    //{
    //    public int Id { get; set; }
    //    public string FormName { get; set; }
    //    public string DisplayName { get; set; }
    //    public Nullable<bool> IsDisplayAllowed { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public string Menu { get; set; }
    //    public string Description { get; set; }
    //    public string DB_OperationType { get; set; }
    //    public Nullable<int> URLTypeId { get; set; }
    //    public string SubMenu { get; set; }
    //}
    //public class GeneralUser
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string UserName { get; set; }
    //    public string Password { get; set; }
    //    public string EmailAddress { get; set; }
    //    public string MobileNumber { get; set; }
    //    public int EmployeeId { get; set; }
    //    public Nullable<int> RoleId { get; set; }
    //    public string AllowedCampusIds { get; set; }
    //    public Nullable<bool> IsLogIn { get; set; }
    //    public Nullable<bool> IsDeveloper { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //}
    //public class GeneralUser_GetDetailByParam_Result
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string UserName { get; set; }
    //    public string Password { get; set; }
    //    public string EmailAddress { get; set; }
    //    public string MobileNumber { get; set; }
    //    public Nullable<int> RoleId { get; set; }
    //    public Nullable<bool> IsLogIn { get; set; }
    //    public string AllowedCampusIds { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //    public Nullable<bool> IsDeveloper { get; set; }
    //    public int EmployeeId { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public string RoleName { get; set; }
    //}
    //public class GeneralUserRight
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public Nullable<int> UserId { get; set; }
    //    public Nullable<int> RightId { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<System.DateTime> EffectiveFrom { get; set; }
    //    public Nullable<System.DateTime> ExpiryDate { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //}
    //public class LK_ChallanMethod_GetListByParam_Result
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<int> MonthsNo { get; set; }
    //    public Nullable<int> ChallanNo { get; set; }
    //}
    //public class LK_StudyGroup_GetListByParam_Result
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //}
    //public class LK_StudyLevel_GetListByParam_Result
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //}
    //public class LK_WHTaxPolicy_GetListByParam_Result
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<decimal> SlabAmount { get; set; }
    //    public Nullable<decimal> FixedCharges { get; set; }
    //    public Nullable<decimal> Percentage { get; set; }
    //}
    //public class MartialStatus
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //}
    //public class Occupation
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //}
    //public class OrganizationType
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class PolicyPeriod
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<int> MonthNo { get; set; }
    //}
    //public class RegistrationType
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //}
    //public class Relationship
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //}
    //public class Right
    //{
    //    public int Id { get; set; }
    //    public string FormName { get; set; }
    //    public string DisplayName { get; set; }
    //    public Nullable<bool> IsDisplayAllowed { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public string Menu { get; set; }
    //    public string RoleIds { get; set; }
    //    public string SubMenu { get; set; }
    //    public string Purpose { get; set; }
    //}
    //public class Role
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class RollCallSystem
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //}
    //public class StructureCOAAccount
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public string Code { get; set; }
    //    public string Description { get; set; }
    //    public string ShortDescription { get; set; }
    //    public Nullable<int> CoaCatagoryId { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> EffectiveFrom { get; set; }
    //    public Nullable<System.DateTime> ExpiredOn { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<int> DocType { get; set; }
    //    public Nullable<int> DocumentStatus { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //}
    //public class StructureCOAAccount_GetListByParam_Result
    //{
    //    public int Id { get; set; }
    //    public string Account { get; set; }
    //    public string Description { get; set; }
    //}
    //public class StructureFeeType
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public Nullable<int> FeeCatagoryId { get; set; }
    //    public Nullable<int> ChargingMethodId { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<bool> IsOnAdmission { get; set; }
    //    public bool IsSecurity { get; set; }
    //    public Nullable<bool> IsRefundable { get; set; }
    //    public Nullable<bool> IsDiscount { get; set; }
    //    public Nullable<int> RevenueAccountId { get; set; }
    //    public Nullable<int> AssetAccountId { get; set; }
    //    public Nullable<int> LiabilityAccountId { get; set; }
    //    public Nullable<int> CostOfSaleAccountId { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public Nullable<int> CreatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //    public Nullable<int> UpdatedBy { get; set; }
    //    public Nullable<int> BranchId { get; set; }
    //    public Nullable<int> CompanyId { get; set; }
    //}
    //public class StructureFeeType_GetListByParam_Result
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public string Description { get; set; }
    //}
    //public class StructureFeeType_GetListBySearch_Result
    //{
    //    public int Id { get; set; }
    //    public Nullable<System.Guid> GuID { get; set; }
    //    public string FeeName { get; set; }
    //    public string FeeCatagory { get; set; }
    //    public string ChargingMethod { get; set; }
    //    public Nullable<int> FeeCatagoryId { get; set; }
    //    public Nullable<int> ChargingMethodId { get; set; }
    //    public Nullable<bool> IsOnAdmission { get; set; }
    //    public Nullable<bool> IsDiscount { get; set; }
    //    public Nullable<bool> IsRefundable { get; set; }
    //    public bool IsSecurity { get; set; }
    //}
    //public class StudyGroup
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class StudyLevel
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class URLType
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //}
    //public class WHTaxPolicy
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<decimal> Percentage { get; set; }
    //    public string Section { get; set; }
    //    public Nullable<decimal> SlabAmount { get; set; }
    //    public Nullable<decimal> FixedCharges { get; set; }
    //}
    //public class WorkingShift
    //{
    //    public int Id { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<bool> Status { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //}
    #endregion 
}