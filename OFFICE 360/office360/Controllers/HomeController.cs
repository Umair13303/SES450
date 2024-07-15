using ClosedXML.Excel;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices;
using office360.Common.CommonHelper;
using office360.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using office360.Models.ViewComponents;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static office360.Models.General.DocumentStatus;
using static office360.Models.General.HttpStatus;
using static office360.Models.General.RightsGUID;

namespace REP.Controllers
{
    public class HomeController : Controller
    {

        SESEntities db = new SESEntities();

       
        public ActionResult LogInPage()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction(_ActionsURL.LogIn, _Controller.Home);
        }
        #region DASHBOARD RIGHTS & SIDE MENUE
        [HttpPost]
        public ActionResult GET_USER_CONFIRMATION(GeneralUser Users)
        {
            int? RoleId = null;
            if (Users != null)
            {
                int? StatusCode = 0;
                var CheckData = LogInAuthentication.CheckUserAuthorization(Users,out  RoleId, out StatusCode);
               
                switch (StatusCode)
                {
                    case (int?)HttpResponses.CODE_SUCCESS:
                        return RedirectToAction(_ActionsURL.GetDashBoard, _Controller.Home);
                    case (int?)HttpResponses.CODE_INTERNAL_SERVER_ERROR:
                        return RedirectToAction(_ActionsURL.InternalServerError, _Controller.Home);
                    case (int?)HttpResponses.CODE_DATA_DOES_NOT_EXIST:
                        return RedirectToAction(_ActionsURL.LogIn, _Controller.Home);
                    default:
                        return RedirectToAction(_ActionsURL.LogIn, _Controller.Home);
                }
            }
            else
            {
                return RedirectToAction(_ActionsURL.LogIn, _Controller.Home);
            }
        }
        [UsersSessionCheck]
        [CompanySessionCheck]
        public  ActionResult GetDashboard()
        {
            string roleKey;
            if (ASPManagRoles.RoleGuids.TryGetValue(Session_Manager.RoleId, out roleKey))
            {
                switch (roleKey)
                {
                    case nameof(UserRoles.ROLE_ADMIN):
                        return View(_ActionsURL.GetDashBoard_ADMIN);
                    case nameof(UserRoles.ROLE_DEVELOPER):
                        return View(_ActionsURL.GetDashBoard_DEVELOPER);
                    case nameof(UserRoles.ROLE_MANAGER):
                        return View(_ActionsURL.GetDashBoard_DEVELOPER);
                    case nameof(UserRoles.ROLE_DEO):
                        return View(_ActionsURL.GetDashBoard_DEVELOPER);
                    default:
                        return View(_ActionsURL.LogIn);
                }
            }
            else
            {
            }
            return View();
        }
        [UsersSessionCheck]
        [CompanySessionCheck]
        public ActionResult PopulateAllowedRights(_SqlParameters Parameter)
        {

            var RightPaths = this.ControllerContext.RouteData.Values["area"]?.ToString() +"/"+ this.ControllerContext.RouteData.Values["controller"].ToString()+"/"+ this.ControllerContext.RouteData.Values["action"].ToString(); ;

            var data = GetAllListFromDB.GetRightsByParameter();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AUTOMATE COLUMS EXCEL SHEET
        public ActionResult DownloadExcel(GeneralCompany GeneralBranch)
        {
            // Assuming GeneralBranch is a collection of objects
            List<GeneralCompany> data = YourGetDataMethod(); // Replace YourObjectType with the actual type

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("GeneralBranch");

            // Get property names dynamically
            var headers = typeof(GeneralCompany).GetProperties()
                .Select(property => property.Name)
                .ToArray();

            for (int i = 1; i <= headers.Length; i++)
            {
                worksheet.Cell(1, i).Value = headers[i - 1];
            }

            var row = 2;
            foreach (var item in data)
            {
                for (int i = 1; i <= headers.Length; i++)
                {
                    var propertyValue = typeof(GeneralBranch)
                        .GetProperty(headers[i - 1])
                        .GetValue(item);

                    worksheet.Cell(row, i).Value = propertyValue?.ToString();
                }
                row++;
            }

            // Save the workbook to a memory stream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                byte[] excelBytes = memoryStream.ToArray();

                // Send Excel as file response
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "COY_Branches.xlsx");
            }
        }
        private List<GeneralCompany> YourGetDataMethod()
        {
            List<GeneralCompany> data;
            data = db.GeneralCompany.ToList();
            return data;
        }
        #endregion
    }
}


