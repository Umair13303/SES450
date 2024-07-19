using office360.Common.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static office360.Models.General.DocumentStatus;
using static office360.Models.General.DBListCondition;

namespace office360.Common.DataBaseProcedures.Common
{
    public class GetDataFromDB
    {

        #region FUNCTION FOR :: GET DATA FROM LOOKUP/BY PARAMETER USING LINQ-QUERY
        public static List<_SqlParameters> GET_LK1_EnrollmentType(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.EnrollmentType
                     .Where(x => x.Status == true)
                     .Select(x => new _SqlParameters
                     {
                         Id = x.Id,
                         Description = x.Description,
                     }).ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_SearchParameter(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.SearchParameter
                     .Where(x => x.Status == true && (x.DocTypeId == PostedData.DocType || x.DocTypeId==0) )
                     .Select(x => new _SqlParameters
                     {
                         Id = x.Id,
                         Description = x.Description,
                     }).ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_Gender(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.Gender
                         .Select(x => new _SqlParameters
                         {
                             Id = x.Id,
                             Description = x.Description,
                         })
                         .ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_MartialStatus(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.MartialStatus
                         .Select(x => new _SqlParameters
                         {
                             Id = x.Id,
                             Description = x.Description,
                         })
                         .ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_Religion(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.Religion
                         .Select(x => new _SqlParameters
                         {
                             Id = x.Id,
                             Description = x.Description,
                         })
                         .ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_Country(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.Country
                         .Select(x => new _SqlParameters
                         { 
                             Id = x.Id,
                             Description = "( "+x.CallingCode+" )"+x.Description,
                         })
                         .ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_Relationship(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.Relationship
                         .Select(x => new _SqlParameters
                         {
                             Id = x.Id,
                             Description = x.Description,
                         })
                         .ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_Occupation(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.Occupation
                         .Select(x => new _SqlParameters
                         {
                             Id = x.Id,
                             Description = x.Description,
                         })
                         .ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_PolicyPeriod(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.PolicyPeriod
                         .Select(x => new _SqlParameters
                         {
                             Id = x.Id,
                             Description = x.Description,
                             MonthsNo = x.MonthNo,
                         })
                         .ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_CampusType(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.CampusType
                         .Select(x => new _SqlParameters
                         {
                             Id = x.Id,
                             Description = x.Description,
                         })
                         .ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_OrganizationType(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.OrganizationType
                        .Where(x => x.Status == true)
                         .Select(x => new _SqlParameters
                         {
                             Id = x.Id,
                             Description = x.Description,
                         })
                         .ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_City(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.City
                         .Where(x => x.CountryId == PostedData.CountryId && x.Status == true)
                         .Select(x => new _SqlParameters { Id = x.Id, Description = x.Description, }).ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_RollCallSystem(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.RollCallSystem
                         .Select(x => new _SqlParameters { Id = x.Id, Description = x.Description, }).ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_BillingMethod(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.BillingMethod
                         .Select(x => new _SqlParameters { Id = x.Id, Description = x.Description, }).ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_RegistrationType(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.RegistrationType
                      .Select(x => new _SqlParameters { Id = x.Id, Description = x.Description })
                      .ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_FeeCatagory(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.FeeCatagory
                      .Where(X=> X.Status==true)
                      .Select(x => new _SqlParameters { Id = x.Id, Description = x.Description })
                      .ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_LK1_ChargingMethod(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.ChargingMethod
                      .Select(x => new _SqlParameters { Id = x.Id, Description = x.Description })
                      .ToList();

                return DATA;
            }
        }
        #endregion

        #region FUNCTION FOR :: GET DATA FROM LOOKUP/BY PARAMETER USING STORE PROCEDURE
        public static List<_SqlParameters>  GET_LK1_StudyLevel(_SqlParameters PostedData)
        {
            List<_SqlParameters> data = new List<_SqlParameters>();
            using (var db = new SESEntities())
            {
                data = db.LK_StudyLevel_GetListByParam(
                                                        PostedData.DB_IF_PARAM,
                                                        Session_Manager.CompanyId,
                                                        Session_Manager.BranchId,
                                                        Session_Manager.StudyLevelIds    
                                                      )
                         .Select(x => new _SqlParameters
                         {
                             Id = x.Id,
                             Description = x.Description,
                         }).ToList();
            }
            return data;
        }
        public static List<_SqlParameters>  GET_LK1_StudyGroup(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = new List<_SqlParameters>();
            using (var db = new SESEntities())
            {
                DATA = db.LK_StudyGroup_GetListByParam(
                                                        PostedData.DB_IF_PARAM,
                                                        Session_Manager.CompanyId,
                                                        Session_Manager.BranchId,
                                                        Session_Manager.StudyGroupIds
                                                      )
                         .Select(x => new _SqlParameters
                         {
                             Id = x.Id,
                             Description = x.Description,
                         }).ToList();
            }
            return DATA;
        }
        public static List<_SqlParameters>  GET_LK1_ChallanMethod(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = new List<_SqlParameters>();
            using (var db = new SESEntities())
            {
                DATA = db.LK_ChallanMethod_GetListByParam(
                                                        PostedData.DB_IF_PARAM
                                                        )
                         .Select(x => new _SqlParameters
                         {
                             Id = x.Id,  
                             Description = x.Description,
                             MonthsNo = x.MonthsNo,
                             NumberOfChallan = x.ChallanNo,
                         }).ToList();
            }
            return DATA;
        }
        public static List<_SqlParameters>  GET_LK1_WHTaxPolicy(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = new List<_SqlParameters>();
            using (var db = new SESEntities())
            {
                DATA = db.LK_WHTaxPolicy_GetListByParam(
                                                        PostedData.DB_IF_PARAM,
                                                        Session_Manager.CompanyId,
                                                        Session_Manager.BranchId,
                                                        PostedData.FeeStructureId,
                                                        (int?)DocStatus.Active_FEE_STRUCTURE
                                                        )
                         .Select(x => new _SqlParameters
                         {
                             Id = x.Id,  
                             Description = x.Description,
                             Percentage = x.Percentage,
                             SlabAmount = x.SlabAmount,
                             FixedCharges = x.FixedCharges,
                         }).ToList();
            }
            return DATA;
        }
        #endregion

        #region FUNCTION FOR :: COUNT CHECK::IF DATA ALREAD EXIST FROM MAIN DB/BY PARAMETER
        public static List<_SqlParameters> CountCheck_AccFeeChallan(_SqlParameters PostedData)
        {
            using (var db = new SESEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    var DATA = db.AccFeeChallan
                         .Where(AFC =>
                          AFC.SessionId == PostedData.SessionId && AFC.ClassId == PostedData.ClassId
                       && AFC.RegistrationTypeId == PostedData.RegistrationTypeId && AFC.ClassRegistrationId == PostedData.ClassRegistrationId
                       && AFC.FeeStructureId == PostedData.FeeStructureId && AFC.BranchId == Session_Manager.BranchId
                       && AFC.CompanyId == Session_Manager.CompanyId
                               )
                         .Select(x => new _SqlParameters { Id = x.Id, Description = x.Description }).ToList();
                    return DATA;
                }
            }
        }
        public static List<_SqlParameters> CountCheck_AppSession(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                #region CHECK :: IF SESSION WITH FileStatus.Open_ADMISSION EXIST FOR A BRANCH
                var today = DateTime.Now;
                var DATA = db.AppSession
                      .Where(x =>
                      x.Status == true && x.DocumentStatus == (int?)Models.General.DocumentStatus.DocStatus.Open_ADMISSION
                      && x.CampusId == PostedData.CampusId && x.CompanyId == Session_Manager.CompanyId)
                      .Select(x => new _SqlParameters { Id = x.Id, Description = x.Description }).ToList();
                #endregion
                return DATA;
            }
        }
        #endregion

        #region FUNCTION FOR :: C# TEMP DATA TYPES: VIEWBAG && VIEW DATA FROM MAIN DB/BY PARAMETER
        public static AppSession GET_ACTIVE_AppSession_DETAIL(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                AppSession AdmissionTitle = db.AppSession
                    .FirstOrDefault(x =>
                     DateTime.Now >= x.SessionStartOn && DateTime.Now <= x.SessionEndOn
                  && x.DocumentStatus == (int?)DocStatus.Open_ADMISSION
                  && x.Status == true
                  && x.CompanyId == Session_Manager.CompanyId
                  && x.BranchId == Session_Manager.BranchId
                                    );
                return AdmissionTitle;
            }
        }
        #endregion


        
    }
}