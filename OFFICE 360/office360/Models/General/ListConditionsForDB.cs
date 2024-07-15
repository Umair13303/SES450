using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.General
{
    public class DBListCondition
    {
        public enum LookUpActionCondition
        {
            GET_LK1_COUNTRY,
            GET_LK1_CAMPUSTYPE,
            GET_LK1_ORGANIZATIONTYPE,
            GET_LK1_CITY_BYPARAMETER,
            GET_LK1_ROLLCALLSYSTEM,
            GET_LK1_BILLINGMETHOD,
            GET_LK1_POLICYPERIOD,
            GET_LK1_REGISTRATIONTYPE,
            GET_LK1_ENROLLMENTTYPES,
            GET_LK1_FEECATAGORY,
            GET_LK1_CHARGINGMETHOD,
            GET_LK1_CHALLANMETHOD_BYPARAMTER,
            GET_LK1_SEARCHPARAMETER_BYPARAMTER,
            GET_LK1_STUDYGROUP_BYPARAMTER,
            GET_LK1_STUDYLEVEL_BYPARAMTER,
            GET_LK1_WHTAXPOLICY_BYPARAMTER,

          
        }

        public enum SESActionCondition
        {
            GET_MT_GENERALCOMPANY_BYPARAMETER,
            GET_MT_APPSESSION_BYPARAMETER,
            GET_MT_APPCLASS_BYPARAMETER,
            GET_MT_ACCFEESTRUCTURE_BYPARAMETER,
            GET_MT_GENERALBRANCH_BYPARAMETER,
            GET_MT_APPCLASSREGISTRATION_BYPARAMETER,
            GET_MT_STRUCTUREFEETYPE_BYPARAMETER,
            GET_MT_STRUCTUREFEETYPE_DETAILBYID,
            GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER,

        }

        public enum SPListCondition
        {
            BRANCH_BY_USER_ALLOWEDBRANCHIDS,
            BRANCH_BY_USER_ALLOWEDBRANCHIDS_CHECK_ACTIVE_APPSESSION,
            APPSESSION_BY_GENERALBRANCH,
            APPCLASS_BY_GENERALBRANCH,
            APPCLASS_BY_APPSESSION,
            STUDYLEVEL_BY_BRANCHSETTING,
            WHTAXPOLICY_BY_ACCFEESTRUCTURE,
            STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,


            CHALLANMETHOD_LIST,
            WHTAXPOLICY_LIST,
            STUDYLEVEL_LIST,
            STUDYGROUP_LIST,


            GET_ALL_COUNTRIES,
            GET_COUNTRY_CODE,
            GET_COMPANY_ACTIVE_EMAIL_SETTINGS,
            GET_COMPANY_ACTIVE_MODULES,
            GET_USER_CONFIRMATION,
            GET_ALL_ALLOWED_RIGHTS_TO_LOGIN_USER_FOR_SIDE_MENUE,
            GET_ALLOWED_RIGHTS_TO_LOGIN_USER_BY_RIGHTID,
            GET_ALLOWED_RIGHTS_TO_LOGIN_USER_BY_URL,
            GET_ACTIVE_ENROLLMENT_SETTINGS,
            GET_APPCLASS_LIST_TO_RUN_ENROLLMENT
        }
        public enum FN_Condition
        {
            GET_EFFECTIVE_DATE_BY_BRANCH_POLICY_PERIOD_ID,
            GET_EXPIRY_DATE_BY_BRANCH_POLICY_PERIOD_ID
        }
        public enum DB_OperationType
        {
            INSERT_DATA_INTO_DB,
            UPDATE_DATA_INTO_DB,
        }
    }
}