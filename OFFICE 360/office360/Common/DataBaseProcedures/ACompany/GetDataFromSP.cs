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
        public static List<GeneralBranch_SearchByParam_Result> GetGeneralBranchList_BySearchParameter(_SqlParameters PostedData)
        {
            List<GeneralBranch_SearchByParam_Result> List = new List<GeneralBranch_SearchByParam_Result>();
            using (var db = new SESEntities())
            {
                List = db.GeneralBranch_SearchByParam(PostedData.SearchById, PostedData.InputText).ToList<GeneralBranch_SearchByParam_Result>();
            }
            return List;

        }
        public static List<_SqlParameters> GET_MT_GENERALBRANCH_BYPARAM(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = new List<_SqlParameters>();

            using (SESEntities db = new SESEntities())
            {
                DATA = db.GeneralBranch_GetDetailByParam(
                                                       PostedData.ListCondition,
                                                       Session_Manager.AllowedCampusIds,
                                                       Session_Manager.UserId,
                                                       Session_Manager.CompanyId,
                                                       Session_Manager.BranchId,
                                                       (int?)Models.General.DocumentStatus.FileStatus.Working_BRANCHES,
                                                       (int?)Models.General.DocumentStatus.FileStatus.Open_ADMISSION,
                                                       DateTime.Now
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

        public static List<_SqlParameters> GET_MT_APPCLASS_BYPARAM(_SqlParameters PostedData)
        {
            using (SESEntities db = new SESEntities())
            {
                List<_SqlParameters> DATA = new List<_SqlParameters>();


                DATA = db.AppClass_GetDetailByParam(
                                                    PostedData.ListCondition,
                                                    PostedData.CampusId,
                                                    Session_Manager.CompanyId,
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
                switch ((PostedData.ListCondition))
                {
                    case nameof(DBListCondition.SPListCondition.APPSESSION_BY_GENERALBRANCH):
                        DATA = db.AppSession
                            .Where(x => x.CompanyId == Session_Manager.CompanyId && x.CampusId == PostedData.CampusId
                                     && x.Status == true
                                     && x.DocumentStatus != (int?)Models.General.DocumentStatus.FileStatus.Cancelled_ADMISSION
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
    }
}