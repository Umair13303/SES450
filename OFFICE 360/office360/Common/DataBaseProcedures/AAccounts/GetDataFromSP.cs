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
     
        
        #region HELPER FOR :: DROP DOWN LIST
        public static List<_SqlParameters> GET_MT_STRUCTUREFEETYPE_BYPARAM(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = new List<_SqlParameters>();
            using (var db = new SESEntities())
            {
                DATA = db.StructureFeeType_GetListByParam(
                                                            PostedData.DB_IF_PARAM,
                                                            Session_Manager.CompanyId,
                                                            Session_Manager.BranchId
                                                          )
                    .Select(x => new _SqlParameters
                    {
                        Id = x.Id,
                        GuID = x.GuID,
                        Description = x.Description,

                    }).ToList();
            }
                return DATA;

        }
        public static List<_SqlParameters> GET_MT_STRUCTURECOAACCOUNT_BYPARAM(_SqlParameters PostedData)
        {
            List<_SqlParameters> DATA = new List<_SqlParameters>();
            using (var db = new SESEntities())
            {
                DATA = db.StructureCOAAccount_GetListByParam(
                                                 Session_Manager.CompanyId,
                                                 Session_Manager.BranchId,
                                                 PostedData.DB_IF_PARAM,
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
        #endregion

        
        #region HELPER FOR :: DATA TABLE LIST
        public static List<StructureFeeType_GetListBySearch_Result> GET_MT_STRUCTUREFEETYPE_LISTSEARCHPARAM(_SqlParameters PostedData)
        {
            List<StructureFeeType_GetListBySearch_Result> List = new List<StructureFeeType_GetListBySearch_Result>();
            using (var db = new SESEntities())
            {
                List = db.StructureFeeType_GetListBySearch(
                                                                    Session_Manager.CompanyId,
                                                                    Session_Manager.BranchId,
                                                                    PostedData.SearchById,
                                                                    PostedData.InputText
                                                                    ).ToList<StructureFeeType_GetListBySearch_Result>();
            }
            return List;

        }
        public static List<StructureFeeType_GetListBySearch_Result> GET_MT_ACCFEESTRUCTURE_LISTSEARCHPARAM(_SqlParameters PostedData)
        {
            List<StructureFeeType_GetListBySearch_Result> List = new List<StructureFeeType_GetListBySearch_Result>();
            using (var db = new SESEntities())
            {
                List = db.StructureFeeType_GetListBySearch(
                                                                    Session_Manager.CompanyId,
                                                                    Session_Manager.BranchId,
                                                                    PostedData.SearchById,
                                                                    PostedData.InputText
                                                                    ).ToList<StructureFeeType_GetListBySearch_Result>();
            }
            return List;

        }
        #endregion


        #region HELPER FOR :: GET DETAIL FOR ID
        public static List<_SqlParameters> GET_MT_STRUCTUREFEETYPE_INFO_BY_GUID(_SqlParameters PostedData)
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
        #endregion

    }
}