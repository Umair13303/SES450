using office360.Common.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static office360.Models.General.DocumentStatus;
using static office360.Models.General.HttpStatus;
using static office360.Models.General.DBListCondition;

namespace office360.Common.DataBaseProcedures.ACompany
{
    public class InsertIntoDB
    {
        public static int? GeneralBranch_Insert(_SqlParameters PostedData)
        {
            using (var db = new SESEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {

                        #region OUTPUT VARAIBLE
                        var CodeParameter = new ObjectParameter("Code", typeof(string));
                        var ResponseParameter = new ObjectParameter("Response", typeof(int));
                        var CampusIdParameter = new ObjectParameter("CampusId", typeof(int));
                        #endregion
                        #region EXECUTE STORE PROCEDURE
                        var data = db.GeneralBranch_Insert(
                                                             PostedData.Description.ToSafeString(),
                                                             PostedData.CampusTypeId,
                                                             PostedData.OrganizationTypeId,
                                                             PostedData.CountryId,
                                                             PostedData.CityId,
                                                             PostedData.Address.ToSafeString(),
                                                             PostedData.ContactNo.ToSafeString(),
                                                             PostedData.EmailAddress.ToSafeString(),
                                                             PostedData.NTNNo.ToSafeString(),
                                                             PostedData.Remarks.ToSafeString(),
                                                             DateTime.Now,
                                                             Session_Manager.BranchId,
                                                             Session_Manager.CompanyId,
                                                             Session_Manager.UserId,
                                                             Uttility.fn_CalculatePolicyPeriod(PostedData, DBListCondition.FN_Condition.GET_EFFECTIVE_DATE_BY_BRANCH_POLICY_PERIOD_ID.ToSafeString()),
                                                             Uttility.fn_CalculatePolicyPeriod(PostedData, DBListCondition.FN_Condition.GET_EXPIRY_DATE_BY_BRANCH_POLICY_PERIOD_ID.ToSafeString()),
                                                             CodeParameter,
                                                             ResponseParameter,
                                                             CampusIdParameter
                            );
                        #endregion
                        #region RESPONSE VALUES IN VARIABLE
                        string code = CodeParameter.Value.ToString();
                        int? Response = (int)ResponseParameter.Value;
                        int? CampusId = (int)CampusIdParameter.Value;
                        #endregion
                        #region TRANSACTION HANDLING DETAIL
                        if (Response != (int?)HttpResponses.CODE_SUCCESS)
                        {
                            dbTran.Rollback();
                        }
                        else if (Response == (int?)HttpResponses.CODE_SUCCESS)
                        {
                            var newSetting = new GeneralBranchSetting
                            {
                                GuID = Uttility.fn_GetHashGuid(),
                                CampusId = CampusId,
                                RollCallSystemId = PostedData.RollCallSystemId,
                                BillingMethodId = PostedData.BillingMethodId,
                                ChallanMethodId = PostedData.ChallanMethodId,
                                StudyLevelIds = PostedData.StudyLevelIds.ToSafeString(),
                                StudyGroupIds = PostedData.StudyGroupIds.ToSafeString(),
                                BranchId = Session_Manager.BranchId,
                                CompanyId = PostedData.CompanyId,
                                CreatedBy = Session_Manager.UserId,
                                CreatedOn = DateTime.Now,
                                PolicyPeriodId=PostedData.PolicyPeriodId,
                                EffectiveFrom = Uttility.fn_CalculatePolicyPeriod(PostedData, DBListCondition.FN_Condition.GET_EFFECTIVE_DATE_BY_BRANCH_POLICY_PERIOD_ID.ToSafeString()),
                                ExpiredOn = Uttility.fn_CalculatePolicyPeriod(PostedData, DBListCondition.FN_Condition.GET_EXPIRY_DATE_BY_BRANCH_POLICY_PERIOD_ID.ToSafeString()),
                                Status = true
                            };

                            db.GeneralBranchSetting.Add(newSetting);
                            db.SaveChanges();
                            dbTran.Commit();
                        }
                        #endregion
                        return HttpStatus.HttpResponseByReturnValue(Response);

                    }
                    catch (Exception Ex)
                    {
                        dbTran.Rollback();
                        return HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                    }
                }
            }
        }
        public static int? AppClass_Insert(_SqlParameters PostedData)
        {
            using (var db = new SESEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        #region OUTPUT VARIABLES
                        var CodeParameter = new ObjectParameter("Code_", typeof(string));
                        var ResponseParameter = new ObjectParameter("Response", typeof(string));
                        #endregion
                        #region STORE PROCEDURE EXECUTION AppClass_Insert
                        var data = db.AppClass_Insert(
                                                    PostedData.Description,
                                                    Session_Manager.BranchId,
                                                    Session_Manager.CompanyId,
                                                    Session_Manager.UserId,
                                                    PostedData.StudyLevelId,
                                                    PostedData.StudyGroupId,
                                                    ResponseParameter, 
                                                    CodeParameter
                                                    );
                        #endregion
                        #region RESPONSE VALUES IN VARIABLE
                        string code = CodeParameter.Value.ToString();
                        int? Response = (int)ResponseParameter.Value;
                        #endregion
                        #region TRANSACTION HANDLING DETAIL
                        if (Response != (int?)HttpResponses.CODE_SUCCESS)
                        {
                            dbTran.Rollback();
                        }
                        else if (Response == (int?)HttpResponses.CODE_SUCCESS)
                        {
                            dbTran.Commit();
                        }
                        #endregion
                        return HttpStatus.HttpResponseByReturnValue(Response);
                        
                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        return HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                    }
                }
            }
        }
        public static int? AppSession_Insert(_SqlParameters PostedData)
        {
            using (var db = new SESEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        int? Response = (int?)HttpResponses.CODE_DATA_ALREADY_EXIST;
                        var IsAlreadyActiveSession = office360.Common.DataBaseProcedures.Common.GetDataFromDB.CountCheck_AppSession(PostedData).ToList();
                        if (IsAlreadyActiveSession.Count == 0)
                        {

                            #region VARIABLES
                            DateTime? EffectiveFrom = PostedData.SessionStartDate;
                            DateTime ? ExpiredOn = PostedData.SessionEndDate;
                            #endregion
                            #region OUTPUT VARIABLES
                            var CodeParameter = new ObjectParameter("Code_", typeof(string));
                            var ResponseParameter = new ObjectParameter("Response", typeof(string));
                            #endregion
                            #region EXECUTION OF STORE PROCEDURE AppSession_Insert
                            var data = db.AppSession_Insert(
                                                PostedData.Description,
                                                PostedData.CampusId,
                                                PostedData.SessionStartDate,
                                                PostedData.SessionEndDate,
                                                (int?)FileStatus.Open_ADMISSION,
                                                Session_Manager.CompanyId,
                                                Session_Manager.BranchId,
                                                Session_Manager.UserId,
                                                PostedData.ClassIds,
                                                PostedData.EnrollmentTypeId,
                                                EffectiveFrom,
                                                ExpiredOn,
                                                ResponseParameter,
                                                CodeParameter
                                );
                            #endregion
                            #region RESPONSE VALUES IN VARIABLE
                            Response = (int)ResponseParameter.Value;
                            string code = CodeParameter.Value.ToString();
                            #endregion
                            #region TRANSACTION HANDLING DETAIL
                            if (Response != (int?)HttpResponses.CODE_SUCCESS)
                            {
                                dbTran.Rollback();
                            }
                            else if (Response == (int?)HttpResponses.CODE_SUCCESS)
                            {
                                dbTran.Commit();
                            }
                            #endregion
                        }

                        return HttpStatus.HttpResponseByReturnValue(Response);
                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        return HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                    }
                }
            }
        }
        public static int? AppStudent_Insert(_SqlParameters PostedData)
        {
            int? Response = (int?)HttpResponses.CODE_BAD_REQUEST;
            using (var db = new SESEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        #region OUTPUT VARAIBLE PARENT
                        var ResponseParameter = new ObjectParameter("Response", typeof(int));
                        var CodeParameter = new ObjectParameter("Code_", typeof(string));
                        var StudentIdParameter = new ObjectParameter("StudentId", typeof(int));
                        #endregion
                        #region EXECUTE STORE PROCEDURE AppStudent_Insert
                        db.AppStudent_Insert(
                                            PostedData.SessionId,
                                            PostedData.FirstName,
                                            PostedData.LastName,
                                            PostedData.CnicNo_FormBNo,
                                            PostedData.GenderId,
                                            PostedData.DateofBirth,
                                            PostedData.MartialStatusId,
                                            PostedData.ReligionId,
                                            PostedData.NationalityId,
                                            PostedData.ResedenitalAddress,
                                            PostedData.MobileNumber,
                                            PostedData.EmailAddress,
                                            PostedData.ParentName,
                                            PostedData.ParentNICNo,
                                            PostedData.ParentStudyLevelId,
                                            PostedData.OccupationId,
                                            PostedData.RelationshipId,
                                            PostedData.MonthlyIncome,
                                            (int?)FileStatus.Active_STUDENT,
                                            Session_Manager.UserId,
                                            Session_Manager.BranchId,
                                            Session_Manager.CompanyId,
                                            (int?)Models.General.DocumentStatus.DocType.STUDENT,
                                            CodeParameter,
                                            ResponseParameter,
                                            StudentIdParameter
                                            );
                        #endregion
                        #region RESPONSE VALUES IN VARIABLE
                        string code = (string)CodeParameter.Value;
                        int? response = (int)ResponseParameter.Value;
                        int? StudentId = (int)StudentIdParameter.Value;
                        #endregion
                        #region TRANSACTION HANDLING PARENT
                        if (response != (int?)HttpResponses.CODE_SUCCESS)
                        {
                            Response = (int?)HttpResponses.CODE_INTERNAL_SERVER_ERROR;
                            dbTran.Rollback();
                        }
                        else if (response == (int?)HttpResponses.CODE_SUCCESS)
                        {
                            #region OUTPUT VARAIBLE DETAIL
                            var _ResponseParameter = new ObjectParameter("Response", typeof(int));
                            #endregion
                            #region EXECUTE STORE PROCEDURE AppClassRegistration_Insert
                            db.AppClassRegistration_Insert(
                                PostedData.SessionId,
                                StudentId,
                                PostedData.ClassId,
                                (int?)Models.General.DocumentStatus.FileStatus.NewEnrollment_CLASS_REGISTRATION,
                                (int?)Models.General.DocumentStatus.DocType.CLASS_REGISTRATION,
                                Session_Manager.UserId,
                                Session_Manager.BranchId,
                                Session_Manager.CompanyId,
                                _ResponseParameter
                                );
                            #endregion
                            #region RESPONSE VALUES IN VARIABLE
                            int? _response = (int)ResponseParameter.Value;
                            #endregion
                            #region TRANSACTION HANDLING DETAIL
                            if (_response != (int?)HttpResponses.CODE_SUCCESS)
                            {
                                Response = (int?)HttpResponses.CODE_INTERNAL_SERVER_ERROR;

                                dbTran.Rollback();
                            }
                            else if (_response == (int?)HttpResponses.CODE_SUCCESS)
                            {
                                Response= (int?)HttpResponses.CODE_SUCCESS;
                                dbTran.Commit();
                            }
                            #endregion

                        }
                       
                        #endregion
                        return HttpStatus.HttpResponseByReturnValue(Response);
                    }
                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        return HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                    }
                }
            }
        }
    }
}