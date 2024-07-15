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


namespace office360.Common.DataBaseProcedures.AAccounts
{
    public class GetDataFromSP
    {
        SESEntities db = new SESEntities();
        public static List<_SqlParameters> GET_StructureFeeType_ListByParam(_SqlParameters PostedData)
        {
            List<_SqlParameters> List = new List<_SqlParameters>();
            using (var db = new SESEntities())
            {
                List = 
                       ((List<_SqlParameters>)
                       (from SFT in db.StructureFeeType
                            where
                                SFT.CompanyId == Session_Manager.CompanyId && SFT.BranchId == Session_Manager.BranchId
                                select new _SqlParameters
                                {
                                         Id           = SFT.Id, GuID           = SFT.GuID,            FeeName    = SFT.Description,
                                         FeeCatagory    = db.FeeCatagory.Where(FC => FC.Id == SFT.FeeCatagoryId).Select(FC => FC.Description).FirstOrDefault(),
                                         ChargingMethod = db.ChargingMethod.Where(CM => CM.Id == SFT.ChargingMethodId).Select(CM => CM.Description).FirstOrDefault(),
                                         FeeCatagoryId  = SFT.FeeCatagoryId,   ChargingMethodId  = SFT.ChargingMethodId,
                                         IsOnAdmission  = SFT.IsOnAdmission,   IsDiscount = SFT.IsDiscount,
                                         IsRefundable   = SFT.IsRefundable,    IsSecurity = SFT.IsSecurity,
                                }).ToList());
            }
            return List;

        }
        public static List<_SqlParameters> GET_StructureFeeType_DetailsByGUID(_SqlParameters PostedData)
        {
            List<_SqlParameters> List = new List<_SqlParameters>();
            using (var db = new SESEntities())
            {
                List = 
                       ((List<_SqlParameters>)
                       (from SFT in db.StructureFeeType
                            where
                                SFT.CompanyId == Session_Manager.CompanyId && SFT.BranchId == Session_Manager.BranchId && SFT.GuID==PostedData.GuID
                                select new _SqlParameters
                                {
                                         Id           = SFT.Id, GuID           = SFT.GuID,            FeeName    = SFT.Description,
                                         FeeCatagory    = db.FeeCatagory.Where(FC => FC.Id == SFT.FeeCatagoryId).Select(FC => FC.Description).FirstOrDefault(),
                                         ChargingMethod = db.ChargingMethod.Where(CM => CM.Id == SFT.ChargingMethodId).Select(CM => CM.Description).FirstOrDefault(),
                                         FeeCatagoryId  = SFT.FeeCatagoryId,   ChargingMethodId  = SFT.ChargingMethodId,
                                         IsOnAdmission  = SFT.IsOnAdmission,   IsDiscount = SFT.IsDiscount,
                                         IsRefundable   = SFT.IsRefundable,    IsSecurity = SFT.IsSecurity,
                                         AssetAccountId   = SFT.AssetAccountId,    RevenueAccountId = SFT.RevenueAccountId,
                                         LiabilityAccountId = SFT.LiabilityAccountId,    CostOfSaleAccountId = SFT.CostOfSaleAccountId,
                                }).ToList());
            }
            return List;

        }

        public static List<_SqlParameters> GET_MT_STRUCTURECOAACCOUNT_BYPARAM(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = new List<_SqlParameters>();
            using (var db = new SESEntities())
            {
                DATA = db.StructureCOAAccount_GetDetailByParam(
                                                 Session_Manager.CompanyId,
                                                 Session_Manager.BranchId,
                                                 PostedData.ListCondition,
                                                 PostedData.CoaCatagoryIds
                                                 )
                 .Select(x => new _SqlParameters
                 {
                     Id = x.Id,
                     Description = x.Description,
                 }).ToList();
                return DATA;
            }


        }

    }
}