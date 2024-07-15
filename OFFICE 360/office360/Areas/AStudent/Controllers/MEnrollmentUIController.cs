using office360.Common.CommonHelper;
using office360.Common.DataBaseProcedures;
using office360.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static office360.Models.General.DocumentStatus;
using static office360.Models.General.HttpStatus;
using static office360.Models.General.DBListCondition;

namespace office360.Areas.AStudent.Controllers
{
    public class MEnrollmentUIController : Controller
    {
        SESEntities db = new SESEntities();
        int? StatusCode = 0;
        int? _Exe = 0;
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateNewEnrollment(_SqlParameters PostedData)
        {
            #region PASS VIEW
            _Exe = GetAllListFromDB.GetAllowedUsersRightsByParameter(PostedData.RightId);
            #endregion
            if (_Exe == (int?)HttpResponses.CODE_SUCCESS)
            {
                ViewBag.Title = PostedData.DisplayName.ToSafeString();
                ViewBag.DB_OperationType = PostedData.OperationType.ToSafeString();

                ViewBag.SessionDetail = Common.DataBaseProcedures.Common.GetDataFromDB.GET_ACTIVE_AppSession_DETAIL(PostedData);
                return View();
            }
            else
            {
                return RedirectToAction(_ActionsURL.LogIn, _Controller.Home, new { area = "" });
            }

        }
        public ActionResult GetDataIntoListByParameter(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = null;

            switch (PostedData.ActionCondition)
            {

            }
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }

        #region POPULATE LIST FROM DATABASE
        public ActionResult PopulateGenderList(_SqlParameters PostedData)
        {
            var data = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_Gender(PostedData);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PopulateMartialStatusList(_SqlParameters PostedData)
        {
            var data = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_MartialStatus(PostedData);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PopulateReligionList(_SqlParameters PostedData)
        {
            var data = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_Religion(PostedData);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PopulateNationalityList(_SqlParameters PostedData)
        {
            var data = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_Country(PostedData);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PopulateEducationLevelList(_SqlParameters PostedData)
        {
            // PostedData.ListCondition = DBListCondition..STUDYLEVEL_BY_BRANCH_JOIN_SETTINGS.ToSafeString();
            var data = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_StudyLevel(PostedData);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PopulateRelationshipsList(_SqlParameters PostedData)
        {
            var data = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_Relationship(PostedData);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PopulateOccupationList(_SqlParameters PostedData)
        {
            var data = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_Occupation(PostedData);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public JsonResult GetAllowedAppClasses(_SqlParameters PostedData)
        //{
        //    var data = Common.DataBaseProcedures.Common.GetDataFromDB.PopulateAppClassListByParamter(PostedData);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region INSERT DATA INTO DATABASE 
        public ActionResult Insert_AppStudent(_SqlParameters PostedData)
        {
            _Exe = Common.DataBaseProcedures.ACompany.InsertIntoDB.AppStudent_Insert(PostedData);
            var data = new { Message = HttpStatus.HTTPTransactionMessagByStatusCode(_Exe), StatusCode = StatusCode };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion  
    }
}