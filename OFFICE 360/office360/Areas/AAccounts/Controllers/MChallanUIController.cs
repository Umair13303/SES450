using office360.Common.CommonHelper;
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

namespace office360.Areas.AAccounts.Controllers
{
    public class MChallanUIController : Controller
    {
        SESEntities db = new SESEntities();
        int? StatusCode = 0;
        int? _Exe = 0;

        #region RENDER VIEW 
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateNewAccFeeChallan(_SqlParameters PostedData)
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

        public ActionResult GetDataIntoListByParameter(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = null;

            switch (PostedData.ActionCondition)
            {
                //case (int)ActionCondition.GET_DDL_FOR_APPSESSION_BYPARAMETER:
                //    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.POPULATE_AAppSessionListByParameter(PostedData).ToList();
                //    break;
                //case (int)ActionCondition.GET_DDL_FOR_APPCLASS_BYPARAMETER:
                //    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.POPULATE_APPCLASS_BYPARAM(PostedData).ToList();
                //    break;
                //case (int)LookUpActionCondition.GET_LK1_REGISTRATIONTYPE:
                //    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_RegistrationType(PostedData).ToList();
                //    break;
                //case (int)ActionCondition.GET_DDL_FOR_APPCLASSREGISTRATION_BYPARAMETER:
                //    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.PopulateAppClassRegistrationListByParameter(PostedData).ToList();
                //    break;
                //case (int)ActionCondition.GET_DDL_FOR_ACCFEESTRUCTURE_BYPARAMETER:
                //    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.PopulateAccFeeStructureByParameter(PostedData).ToList();
                //    break;

            }
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        #region INSERT DATA INTO DATABASE
        [HttpPost]
        public ActionResult Insert_AccFeeChallan(_SqlParameters PostedData, List<_SqlParameters> PostedDataDetail)
        {
            _Exe = Common.DataBaseProcedures.AAccounts.InsertIntoDB.AccFeeChallan_Insert(PostedData, PostedDataDetail);
            var data = new { Message = HttpStatus.HTTPTransactionMessagByStatusCode(_Exe), StatusCode = StatusCode };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}