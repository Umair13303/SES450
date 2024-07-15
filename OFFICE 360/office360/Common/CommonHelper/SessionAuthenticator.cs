using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using office360.CommonHelper;
using static office360.Models.General.HttpStatus;

namespace office360.Common.CommonHelper
{
    public class SessionAuthenticator
    {
        public static bool Company_GetStatusUsingSession(int? companyId)
        { 
            using (SESEntities context = new SESEntities())
            {
                string sql = "  SELECT C.[Status] FROM [dbo].[GeneralCompany] C WHERE C.[Id] =@CompanyId";
                SqlParameter parameter = new SqlParameter("CompanyId", companyId);
                parameter.Value = (object)companyId ?? DBNull.Value;

                bool result = context.Database.SqlQuery<bool>(sql, parameter).FirstOrDefault();

                return result;
            }
        }
        public static bool ACC_Users_GetStatusUsingSession(int? UserId)
        {
            using (SESEntities context = new SESEntities())
            {
                string sql = "SELECT U.[IsLogIn] FROM [dbo].[GeneralUser] U WHERE U.[Id] =@UserId";
                SqlParameter parameter = new SqlParameter("UserId", UserId);


                parameter.Value = (object)UserId ?? DBNull.Value;
                bool result = context.Database.SqlQuery<bool>(sql, parameter).FirstOrDefault();

                return result;
            }
        }

    }
    public class UsersSessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var UserStatus = SessionAuthenticator.ACC_Users_GetStatusUsingSession(Session_Manager.UserId);
            if ((Session_Manager.CompanyId == null && Session_Manager.Status == true))
            {

                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
               {
                        { "area",""  },
                        { "controller",_Controller.Home  },
                        { "action", _ActionsURL.LogIn }
                });
            }
            if (UserStatus == false)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                    {
                        { "area",""  },
                        { "controller",_Controller.Home  },
                        { "action", _ActionsURL.LogIn }
                    });
            }
        }
    }
    public class CompanySessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var CompanyStatus = SessionAuthenticator.Company_GetStatusUsingSession(Session_Manager.CompanyId);
            if ((Session_Manager.CompanyId == null && Session_Manager.Status == true))
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                    {
                        { "area",""  },
                        { "controller",_Controller.Home  },
                        { "action", _ActionsURL.LogIn }
                    });
            }
            if (CompanyStatus == false)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                    {
                        { "area",""  },
                        { "controller",_Controller.Home  },
                        { "action", _ActionsURL.LogIn }
                    });
            }
        }
    }
    public class IsRightAuthorized : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var AreaName = filterContext.RouteData.DataTokens["area"]?.ToString();
            var ControllerName = filterContext.RouteData.Values["controller"]?.ToString();
            var ActionName = filterContext.RouteData.Values["action"]?.ToString();

            // Combine them into a single URL string if needed
            var URL = $"/{AreaName}/{ControllerName}/{ActionName}";



            var IsUserAuthorized = GetAllListFromDB.GetAllowedUsersRightsByURL(URL);
            if (IsUserAuthorized == (int?)HttpResponses.CODE_UNAUTHORIZED)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                    {
                        { "area",""  },
                        { "controller",_Controller.Home  },
                        { "action", _ActionsURL.LogIn }
                    });
            }
            else if(IsUserAuthorized == (int?)HttpResponses.CODE_SUCCESS)
            {
                

            }
        }
    }

}