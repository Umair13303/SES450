using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using office360.Models.General;
using office360.Models.EDMX;
using office360.Extensions;
using static office360.Models.General.DBListCondition;
using static office360.Models.General.HttpStatus;

namespace office360.CommonHelper
{
    public class GetAllListFromDB
    {

        #region COMMONS LIST FUNCTIONS


        public static List<GeneralRightSetting_GetDetailByParam_Result> GetRightsByParameter()
        {
            using (var db = new SESEntities())
            {
                var data = db.GeneralRightSetting_GetDetailByParam(
                                                              Session_Manager.CompanyId
                                                            , ASPManagRoles.URLTYPEID_FORM
                                                            , true
                                                            , SPListCondition.GET_ALL_ALLOWED_RIGHTS_TO_LOGIN_USER_FOR_SIDE_MENUE.ToSafeString()
                                                            , null
                                                            , Session_Manager.UserId
                                                            , null
                                                            , null
                                                            ).ToList();


                return data;
            }
        }
        public static int? GetAllowedUsersRightsByParameter(int? RightId)
        {
            using (var db = new SESEntities())
            {
                var data = db.GeneralRightSetting_GetDetailByParam(
                                                              Session_Manager.CompanyId
                                                            , ASPManagRoles.URLTYPEID_FORM
                                                            , true
                                                            , SPListCondition.GET_ALLOWED_RIGHTS_TO_LOGIN_USER_BY_RIGHTID.ToSafeString()
                                                            , null
                                                            , Session_Manager.UserId
                                                            , RightId
                                                            , null
                                                            ).ToList();
                if (data.Count > 0)
                {
                    return (int?)HttpResponses.CODE_SUCCESS;


                }
                else
                {
                    return (int?)HttpResponses.CODE_UNAUTHORIZED;
                }
            }
        }
        public static int? GetAllowedUsersRightsByURL(string RightPATH)
        {
            using (var db = new SESEntities())
            {
                var data = db.GeneralRightSetting_GetDetailByParam(
                                                              Session_Manager.CompanyId
                                                            , ASPManagRoles.URLTYPEID_FORM
                                                            , true
                                                            , SPListCondition.GET_ALLOWED_RIGHTS_TO_LOGIN_USER_BY_URL.ToSafeString()
                                                            , null
                                                            , Session_Manager.UserId
                                                            , null
                                                            , RightPATH
                                                            ).ToList();
                if (data.Count > 0)
                {
                    return (int?)HttpResponses.CODE_SUCCESS;


                }
                else
                {
                    return (int?)HttpResponses.CODE_UNAUTHORIZED;
                }
            }
        }
        #endregion
    }
}