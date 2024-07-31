using Newtonsoft.Json;
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
using static office360.Models.General.DBListCondition;
using static office360.Models.General.HttpStatus;

namespace office360.Areas.AAccounts.Controllers
{
    public class MFeeUIController : Controller
    {
        SESEntities db = new SESEntities();
        int? StatusCode = 0;
        int? _Exe = 0;


        #region RENDER VIEWS
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult CreateNewAccFeeType(_SqlParameters PostedData)
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
        public ActionResult CreateNewAccFeeStructure(_SqlParameters PostedData)
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
        public ActionResult AccFeeTypeList(_SqlParameters PostedData)
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
        public ActionResult AccFeeStructureList(_SqlParameters PostedData)
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
                case nameof(LookUpActionCondition.GET_LK1_FEECATAGORY):
                    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_FeeCatagory(PostedData).ToList();
                    break;

                case nameof(LookUpActionCondition.GET_LK1_CHARGINGMETHOD):
                    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_ChargingMethod(PostedData).ToList();
                    break;

                case nameof(LookUpActionCondition.GET_LK1_WHTAXPOLICY_BYPARAMTER):
                    DATA = Common.DataBaseProcedures.Common.GetDataFromDB.GET_LK1_WHTaxPolicy(PostedData).ToList();
                    break;

                case nameof(SESActionCondition.GET_MT_GENERALCOMPANY_BYPARAMETER):
                    DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALCOMPANY_BYPARAM(PostedData).ToList();
                    break;

                case nameof(SESActionCondition.GET_MT_GENERALBRANCH_BYPARAMETER):
                    DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_GENERALBRANCH_BYPARAM(PostedData).ToList();
                    break;

                case nameof(SESActionCondition.GET_MT_APPSESSION_BYPARAMETER):
                    DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_APPSESSION_BYPARAM(PostedData).ToList();
                    break;

                case nameof(SESActionCondition.GET_MT_APPCLASS_BYPARAMETER):
                    DATA = Common.DataBaseProcedures.ACompany.GetDataFromSP.GET_MT_APPCLASS_BYPARAM(PostedData).ToList();
                    break;

                case nameof(SESActionCondition.GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER):
                    DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_STRUCTURECOAACCOUNT_BYPARAM(PostedData).ToList();
                    break;

                case nameof(SESActionCondition.GET_MT_STRUCTUREFEETYPE_BYPARAMETER):
                    DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_STRUCTUREFEETYPE_BYPARAM(PostedData).ToList();
                    break;

                case nameof(SESActionCondition.GET_MT_STRUCTUREFEETYPE_DETAILBYID):
                    DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_STRUCTUREFEETYPE_INFO_BY_GUID(PostedData).ToList();
                    break;


            }
            return Json(DATA, JsonRequestBehavior.AllowGet);
        }
        #endregion




        #region POPULATE DATATABLE LIST
        public ActionResult PopulateStructureFeeType_ListByParam_FORDT(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_STRUCTUREFEETYPE_LISTSEARCHPARAM(PostedData).ToList();
            return Json(new { success = true, data = DATA }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AccFeeStructure_ListByParam_FORDT(_SqlParameters PostedData)
        {
            var DATA = Common.DataBaseProcedures.AAccounts.GetDataFromSP.GET_MT_ACCFEESTRUCTURE_LISTSEARCHPARAM(PostedData).ToList();
            return Json(new { success = true, data = DATA }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region FUNCTION TO PASS DATA TO HELPER
        [HttpPost]
        public ActionResult Update_Insert_StructureFeeType(_SqlParameters PostedData)
        {
            _Exe = Common.DataBaseProcedures.AAccounts.InsertIntoDB.StructureFeeType_UPDATE_INSERT(PostedData);
            var data = new { Message = HttpStatus.HTTPTransactionMessagByStatusCode(_Exe), StatusCode = StatusCode };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Update_Insert_AccFeeStructure(_SqlParameters PostedData,List<_SqlParameters> PostedDataDetail)
        {
            _Exe = Common.DataBaseProcedures.AAccounts.InsertIntoDB.AccFeeStructure_UPDATE_INSERT(PostedData, PostedDataDetail);
            var data = new { Message = HttpStatus.HTTPTransactionMessagByStatusCode(_Exe), StatusCode = StatusCode };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        #endregion
    }
}