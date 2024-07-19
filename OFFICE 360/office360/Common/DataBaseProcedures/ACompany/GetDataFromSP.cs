using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office360.Common.DataBaseProcedures.ACompany
{
    public class GetDataFromSP
    {
        public static List<_SqlParameters> GET_MT_GENERALCOMPANY_BYPARAM(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                var DATA = db.GeneralCompany
                    .Where(x => x.Id == Session_Manager.CompanyId && x.Status == true)
                    .Select(x => new _SqlParameters
                    {
                        Id = x.Id,
                        CompanyName = x.CompanyName
                    })
                    .ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_MT_GENERALBRANCH_BYPARAM(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = new List<_SqlParameters>();

            using (SESEntities db = new SESEntities())
            {
                DATA = db.GeneralBranch_GetListByParam(
                                                       PostedData.DB_IF_PARAM,
                                                       Session_Manager.CompanyId,
                                                       Session_Manager.BranchId,
                                                       Session_Manager.AllowedCampusIds,
                                                       (int?)Models.General.DocumentStatus.DocStatus.Working_BRANCHES,
                                                       (int?)Models.General.DocumentStatus.DocStatus.Open_ADMISSION
                                                       )
                        .Select(x => new _SqlParameters
                        {
                            Id = x.Id,
                            Description = x.Description,
                            ExtraClass = x.ExtraClass
                        }).ToList();

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_MT_GENERALBRANCH_INFO_BY_GUID(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = new List<_SqlParameters>();

            using (SESEntities db = new SESEntities())
            {
                DATA = ((List<_SqlParameters>)
                       (from GB in db.GeneralBranch
                        where GB.Id ==PostedData.Id
                                               select new _SqlParameters
                                               {
                                                   Id = GB.Id,
                                                   GuID = GB.GuID,
                                                   Code = GB.Code,
                                                   Description = GB.Description,
                                                   CampusTypeId = GB.CampusTypeId,
                                                   OrganizationTypeId = GB.OrganizationTypeId,
                                                   CountryId = GB.CountryId,
                                                   CityId = GB.CityId,
                                                   Address = GB.Address,
                                                   ContactNo = GB.ContactNo,
                                                   EmailAddress = GB.EmailAddress,
                                                   NTNNo = GB.NTNNo,
                                                   Remarks = GB.Remarks,
                                                   RollCallSystemId = db.GeneralBranchSetting.Where(GBS => GBS.CampusId == GB.Id).Select(GBS => GBS.RollCallSystemId).FirstOrDefault(),
                                                   BillingMethodId = db.GeneralBranchSetting.Where(GBS => GBS.CampusId == GB.Id).Select(GBS => GBS.BillingMethodId).FirstOrDefault(),
                                                   StudyLevelIds = db.GeneralBranchSetting.Where(GBS => GBS.CampusId == GB.Id).Select(GBS => GBS.StudyLevelIds).FirstOrDefault(),
                                                   StudyGroupIds = db.GeneralBranchSetting.Where(GBS => GBS.CampusId == GB.Id).Select(GBS => GBS.StudyGroupIds).FirstOrDefault(),
                                                   PolicyPeriodId = db.GeneralBranchSetting.Where(GBS => GBS.CampusId == GB.Id).Select(GBS => GBS.PolicyPeriodId).FirstOrDefault(),
                                                   ChallanMethodId = db.GeneralBranchSetting.Where(GBS => GBS.CampusId == GB.Id).Select(GBS => GBS.ChallanMethodId).FirstOrDefault()

                                               }).ToList());

                return DATA;
            }
        }
        public static List<_SqlParameters> GET_MT_APPCLASS_BYPARAM(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                List<_SqlParameters> DATA = new List<_SqlParameters>();


                DATA = db.AppClass_GetListByParam(
                                                    PostedData.DB_IF_PARAM,
                                                    Session_Manager.CompanyId,
                                                    PostedData.CampusId,
                                                    PostedData.SessionId,
                                                    PostedData.ClassIds.ToSafeString()
                                                    )
                    .Select(x => new _SqlParameters
                    {
                        Id = x.Id,
                        Description = x.Description,
                    }).ToList();



                return DATA;
            }
        }
        public static List<_SqlParameters> GET_MT_APPSESSION_BYPARAM(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                List<_SqlParameters> DATA = new List<_SqlParameters>();
                switch ((PostedData.DB_IF_PARAM))
                {
                    case nameof(DBListCondition.DB_IF_Condition.APPSESSION_BY_GENERALBRANCH):
                        DATA = db.AppSession
                            .Where(x => x.CompanyId == Session_Manager.CompanyId && x.CampusId == PostedData.CampusId
                                     && x.Status == true
                                     && x.DocumentStatus != (int?)Models.General.DocumentStatus.DocStatus.Cancelled_ADMISSION
                                  )
                            .Select(x => new _SqlParameters
                            {
                                Id = x.Id,
                                Description = x.Description + " [ " + x.SessionStartOn + " - " + x.SessionEndOn + " ]",
                                ClassIds = x.ClassIds
                            }).ToList();
                        break;
                }
                return DATA;
            }
        }
        public static List<GeneralBranch_GetListBySearch_Result> GET_MT_GENERALBRANCH_LIST_SEARCHPARAM(_SqlParameters PostedData)
        {
            List<GeneralBranch_GetListBySearch_Result> List = new List<GeneralBranch_GetListBySearch_Result>();
            using (var db = new SESEntities())
            {
                List = db.GeneralBranch_GetListBySearch(
                                                        Session_Manager.CompanyId,
                                                        Session_Manager.BranchId,
                                                        PostedData.SearchById,
                                                        PostedData.InputText
                                                        ).ToList<GeneralBranch_GetListBySearch_Result>();
            }
            return List;

        }

    }
}