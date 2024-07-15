using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static office360.Models.General.DBListCondition;

namespace office360.Models.General
{
    public class ASPManagRoles
    {
        public static readonly Dictionary<int?, string> RoleGuids = new Dictionary<int?, string>
        {
               { 1, "ROLE_ADMIN" },
               { 2, "ROLE_DEVELOPER" },
               { 3, "ROLE_MANAGER" },
               { 4, "ROLE_DEO" },
        };


        public static readonly int URLTYPEID_FORM = 1;
        public static readonly int URLTYPEID_REPORT_RDLC = 2;
    }
}