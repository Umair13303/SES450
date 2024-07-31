using office360.Common.CommonHelper;
using office360.Common.DataBaseProcedures;
using office360.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static office360.Models.General.HttpStatus;
using static office360.Models.General.DBListCondition;

namespace office360.Areas.ACompany.Controllers
{
    public class MBranchUIController : Controller
    {
        SESEntities db = new SESEntities();
        int? StatusCode = 0;
        int? _Exe = 0;

        #region RENDER VIEWS
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateNewGeneralBranch(_SqlParameters PostedData)
        {
            #region PASS VIEW
            _Exe = GetAllListFromDB.GetAllowedUsersRightsByParameter(PostedData.RightId);
            #endregion
            if (_Exe == (int?)HttpResponses.CODE_SUCCESS)
            {
                ViewBag.Title = PostedData.DisplayName.ToSafeString();
                ViewBag.DB_OperationType = PostedData.OperationType.ToSafeString();
                return View();
            }
            else
            {
                return RedirectToAction(_ActionsURL.LogIn, _Controller.Home, new { area = "" });
            }
        }

        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult GeneralBranchList(_SqlParameters PostedData)
        {
            #region PASS VIEW
            _Exe = GetAllListFromDB.GetAllowedUsersRightsByParameter(PostedData.RightId);
            #endregion
            if (_Exe == (int?)HttpResponses.CODE_SUCCESS)
            {
                ViewBag.Title = PostedData.DisplayName.ToSafeString();
                ViewBag.DB_OperationType = PostedData.OperationType.ToSafeString();
                return View();
            }
            else
            {
                return RedirectToAction(_ActionsURL.LogIn, _Controller.Home, new { area = "" });
            }
        }

        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateNewGeneralBranchEmailSetting(_SqlParameters PostedData)
        {
            #region PASS VIEW
            _Exe = GetAllListFromDB.GetAllowedUsersRightsByParameter(PostedData.RightId);
            #endregion
            if (_Exe == (int?)HttpResponses.CODE_SUCCESS)
            {
                ViewBag.Title = PostedData.DisplayName.ToSafeString();
                ViewBag.DB_OperationType = PostedData.OperationType.ToSafeString();
                return View();
            }
            else
            {
                return RedirectToAction(_ActionsURL.LogIn, _Controller.Home, new { area = "" });
            }
        }
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult GeneralBranchEmailSettingList(_SqlParameters PostedData)
        {
            #region PASS VIEW
            _Exe = GetAllListFromDB.GetAllowedUsersRightsByParameter(PostedData.RightId);
            #endregion
            if (_Exe == (int?)HttpResponses.CODE_SUCCESS)
            {
                ViewBag.Title = PostedData.DisplayName.ToSafeString();
                ViewBag.DB_OperationType = PostedData.OperationType.ToSafeString();
                return View();
            }
            else
            {
                return RedirectToAction(_ActionsURL.LogIn, _Controller.Home, new { area = "" });
            }
        }
        #endregion

        #region DROP DOWN LIST HELPER
        public ActionResult GET_DATA_BY_PARAMETER(_SqlParameters PostedData)
        {

            List<_SqlParameters> DATA = null;

            switch (PostedData.ActionCondition)
            {
                case nameof(SESActionCondition.GET_MT_GENERALCOMPANY_BYPARAMETER):
                    DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALCOMPANY_BYPARAM(PostedData).ToList();
                    break;

                case nameof(LookUpActionCondition.GET_LK1_CAMPUSTYPE):
                    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_CampusType(PostedData).ToList();
                    break;

                case nameof(LookUpActionCondition.GET_LK1_ORGANIZATIONTYPE):
                    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_OrganizationType(PostedData).ToList();
                    break;

                case nameof(LookUpActionCondition.GET_LK1_COUNTRY):
                    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_Country(PostedData).ToList();
                    break;

                case nameof(LookUpActionCondition.GET_LK1_CITY_BYPARAMETER):
                    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_City(PostedData).ToList();
                    break;

                case nameof(LookUpActionCondition.GET_LK1_POLICYPERIOD):
                    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_PolicyPeriod(PostedData).ToList();
                    break;

                case nameof(LookUpActionCondition.GET_LK1_CHALLANMETHOD_BYPARAMTER):
                    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_ChallanMethod(PostedData).ToList();
                    break;

                case nameof(LookUpActionCondition.GET_LK1_ROLLCALLSYSTEM):
                    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_RollCallSystem(PostedData).ToList();
                    break;

                case nameof(LookUpActionCondition.GET_LK1_BILLINGMETHOD):
                    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_BillingMethod(PostedData).ToList();
                    break;

                case nameof(LookUpActionCondition.GET_LK1_STUDYGROUP_BYPARAMTER):
                    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_StudyGroup(PostedData).ToList();
                    break;

                case nameof(LookUpActionCondition.GET_LK1_STUDYLEVEL_BYPARAMTER):
                    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_StudyLevel(PostedData).ToList();
                    break;

                case nameof(SESActionCondition.GET_MT_GENERALBRANCH_BYPARAMETER):
                    DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALBRANCH_BYPARAM(PostedData).ToList();
                    break;

                case nameof(SESActionCondition.GET_MT_GENERALBRANCH_DETAILBYID):
                    DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALBRANCH_INFO_BY_GUID(PostedData).ToList();
                    break;
            }
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region POPULATE DATATABLE LIST FOR DBO GENERAL BRANCH
        public ActionResult PopulateGeneralBranchList_FORDT(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALBRANCH_LIST_SEARCHPARAM(PostedData).ToList();
            return Json(new { success = true, data = DATA }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region INSERT DATA INTO DATABASE FOR DBO GENERAL BRANCH
        [HttpPost]
        public ActionResult Insert_GeneralBranches(_SqlParameters PostedData)
        {
            _Exe = Common.DataBaseProcedures.ACompany.InsertIntoDB.Update_Insert_GeneralBranch(PostedData);
            var data = new { Message = HttpStatus.HTTPTransactionMessagByStatusCode(_Exe), StatusCode = StatusCode };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}