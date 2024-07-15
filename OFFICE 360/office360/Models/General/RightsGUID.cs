using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.General
{
    public class RightsGUID
    {
        public static class RightId
        {
            #region STAGE 1 AREA BRANCH
            public static readonly Guid CreateNewGeneralBranch = new Guid("7AF81BD4-732C-4892-BA5A-1C7C1F09C1BC");
            public static readonly Guid CreateNewAppClass = new Guid("21A231AC-3E9F-4E9F-AACD-D7055F05C23E");
            public static readonly Guid CreateNewAppAdmissions = new Guid("4EA1C0B4-41C9-4A13-B1DC-616A1BCFADF8");
            public static readonly Guid CreateNewEnrollment = new Guid("88D26B1C-8431-41D6-905C-DF3F5F3BAFEF");

            //public static readonly Guid ListGeneralBranch = new Guid("3E8B3611-376D-4E1C-88A9-A92C8330B77A");
            //public static readonly Guid EditGeneralBranch = new Guid("3E8B3611-376D-4E1C-88A9-A92C8330B77A");
            //public static readonly Guid ViewGeneralBranch(Resport) = new Guid("3E8B3611-376D-4E1C-88A9-A92C8330B77A");


            //public static readonly Guid ListAppClass = new Guid("3E8B3611-376D-4E1C-88A9-A92C8330B77A");
            //public static readonly Guid EditAppClass = new Guid("3E8B3611-376D-4E1C-88A9-A92C8330B77A");
            //public static readonly Guid ListAppAdmissions = new Guid("3E8B3611-376D-4E1C-88A9-A92C8330B77A");
            //public static readonly Guid EditAppAdmissions = new Guid("3E8B3611-376D-4E1C-88A9-A92C8330B77A");


            //public static readonly Guid ListEnrollment = new Guid("31AEE84F-16F6-4D6B-BE49-B1B5001F0AF0");
            //public static readonly Guid EditEnrollment = new Guid("31AEE84F-16F6-4D6B-BE49-B1B5001F0AF0");
            //public static readonly Guid ViewEnrollment = new Guid("31AEE84F-16F6-4D6B-BE49-B1B5001F0AF0");
            #endregion

        }

        public class MenuViewModel
        {
            public string Menu { get; set; }
            public List<ModuleViewModel> SubMenu { get; set; } = new List<ModuleViewModel>();
        }
        public class ModuleViewModel
        {
            public string SubMenu { get; set; }
            public List<int> Id { get; set; } = new List<int>();
            public List<string> DisplayName { get; set; } = new List<string>();
            public List<string> Description { get; set; } = new List<string>();
            public List<string> DB_OperationType { get; set; } = new List<string>();
        }
    }
}