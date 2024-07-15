using office360.Common.CommonHelper;
using office360.Extensions;
using office360.Models.EDMX;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static office360.Models.General.DocumentStatus;
using static office360.Models.General.HttpStatus;
using static office360.Models.General.DBListCondition;
using System.Globalization;
using System.Data.Entity.Core.Objects;

namespace office360.Common.DataBaseProcedures.AAccounts
{
    public class CheckDuplicateRecord
    {
        SESEntities db = new SESEntities();
    }
}