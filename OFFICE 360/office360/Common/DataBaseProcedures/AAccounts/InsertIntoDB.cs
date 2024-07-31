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
using DocType = office360.Models.General.DocumentStatus.DocType;

namespace office360.Common.DataBaseProcedures.AAccounts
{
    public class InsertIntoDB
    {


        public static int? StructureFeeType_UPDATE_INSERT(_SqlParameters PostedData)
        {
            using (var db = new SESEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {

                        #region OUTPUT VARAIBLE
                        var ResponseParameter       = new ObjectParameter("Response", typeof(int));
                        #endregion
                        #region EXECUTE STORE PROCEDURE
                        var data = db.StructureFeeType_UpSert(
                                                             PostedData.OperationType,
                                                             PostedData.GuID,
                                                             PostedData.FeeCatagoryId,
                                                             PostedData.ChargingMethodId,
                                                             PostedData.Description,
                                                             PostedData.IsOnAdmission,
                                                             PostedData.IsSecurity,
                                                             PostedData.IsRefundable,
                                                             PostedData.IsDiscount,
                                                             PostedData.RevenueAccountId,
                                                             PostedData.AssetAccountId,
                                                             PostedData.LiabilityAccountId,
                                                             PostedData.CostOfSaleAccountId,
                                                             DateTime.Now,
                                                             Session_Manager.UserId,
                                                             DateTime.Now,
                                                             Session_Manager.UserId,
                                                             Session_Manager.BranchId,
                                                             Session_Manager.CompanyId,
                                                             ResponseParameter
                                                              );
                        #endregion
                        #region RESPONSE VALUES IN VARIABLE
                        int? Response               = (int)ResponseParameter.Value;
                        #endregion
                        #region TRANSACTION HANDLING DETAIL
                        switch (Response)
                        {
                            case (int?)HttpResponses.CODE_SUCCESS:
                            case (int?)HttpResponses.CODE_DATA_UPDATED:
                                dbTran.Commit();
                                break;
                            case (int?)HttpResponses.CODE_BAD_REQUEST:
                                dbTran.Rollback();
                                break;
                            default:
                                dbTran.Rollback();
                                break;
                        }
                       
                        return HttpStatus.HttpResponseByReturnValue(Response);
                        #endregion

                    }
                    catch (Exception Ex)
                    {
                        dbTran.Rollback();
                        return HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                    }
                }
            }
        }
        public static int? AccFeeStructure_UPDATE_INSERT(_SqlParameters PostedData, List<_SqlParameters> PostedDataDetail)
        {
            using (var db = new SESEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        int? Response = HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();



                        #region COUNT CHECK FEE STRUCTURE CLASS WISE
                        var FeeStructureCount= db.AccFeeStructure.Where(x =>
                                                                        x.DocumentStatus== (int?)DocStatus.Active_FEE_STRUCTURE 
                                                                        && x.Status ==true 
                                                                        && x.SessionId == PostedData.SessionId 
                                                                        && x.ClassId == PostedData.ClassId 
                                                                        && x.BranchId== PostedData.CampusId 
                                                                        && x.CompanyId==Session_Manager.CompanyId).Select(x => new _SqlParameters { Id = x.Id }).ToList();
                        #endregion


                        #region DESCRIPTIONAL VARIABLE
                        var Code = (DateTime.Now.ToString("ddMMyy") + "-000" + (db.AccFeeStructure.ToList().Count() + 1)).ToSafeString();
                        var SessionName = db.AppSession.Where(x => x.Id == PostedData.SessionId).Select(x => new _SqlParameters { Description = x.Description, EffectiveFrom = x.EffectiveFrom, ExpiredOn = x.ExpiredOn }).FirstOrDefault();
                        var ClassName = db.AppClass.Where(x => x.Id == PostedData.ClassId).Select(x => new _SqlParameters { Description = x.Description }).FirstOrDefault();
                        #endregion

                        switch (PostedData.OperationType)
                        {
                            #region INSERT DATA INTO DB:: ACCFEESTRUCTURE AND ACCFEESTRUCTUREDETAIL
                            case nameof(DB_OperationType.INSERT_DATA_INTO_DB):
                                // DUPLICATE RECORD DOES NOT EXIST :: PROCEED INSERT DATA
                                if (FeeStructureCount.Count == 0)
                                {

                                    var FacultyParentGuID = Uttility.fn_GetHashGuid();

                                    #region EXECUTE STORE PROCEDURE PARENT ::ACCFEESTRUCTURE
                                    var NewFeeStructureParent = new AccFeeStructure
                                    {
                                        GuID = FacultyParentGuID,
                                        Code = Code,
                                        CampusId = PostedData.CampusId,
                                        Description = ("Fee Structure Generated For :" + ClassName.Description + " Applicable For Academic Session of: " + SessionName.Description).ToSafeString(),
                                        SessionId = PostedData.SessionId,
                                        ClassId = PostedData.ClassId,
                                        WHTaxPolicyId = PostedData.WHTaxPolicyId,
                                        TotalFeeAmount = PostedData.TotalFeeAmount,
                                        WHTAmount = PostedData.WHTAmount,
                                        GrossFeeAmount = PostedData.GrossFeeAmount,
                                        EffectiveFrom = SessionName.EffectiveFrom,
                                        ExpiredOn = SessionName.ExpiredOn,
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = Session_Manager.UserId,
                                        DocType = (int?)DocType.FEE_STRUCTURE,
                                        DocumentStatus = (int?)DocStatus.Active_FEE_STRUCTURE,
                                        Status = true,
                                        BranchId = Session_Manager.BranchId,
                                        CompanyId = Session_Manager.CompanyId,
                                    };
                                    db.AccFeeStructure.Add(NewFeeStructureParent);
                                    db.SaveChanges();

                                    var InsertedFeeStructureId = db.AccFeeStructure.Where(x => x.GuID == FacultyParentGuID).Select(x => new _SqlParameters { Id = x.Id }).ToList();

                                    #endregion


                                    #region EXECUTE STORE PROCEDURE DETAIL ::ACCFEESTRUCTUREDETAIL
                                    if (InsertedFeeStructureId.Count > 0)
                                    {
                                        var NewFeeStructureDetail = PostedDataDetail.Select(FeeStructureDetailList => new AccFeeStructureDetail
                                        {
                                            GuID = Uttility.fn_GetHashGuid(),
                                            FeeStructureId = InsertedFeeStructureId.FirstOrDefault().Id,
                                            FeeTypeId = FeeStructureDetailList.FeeTypeId,
                                            RevenueAccountId = FeeStructureDetailList.RevenueAccountId,
                                            AssetAccountId = FeeStructureDetailList.AssetAccountId,
                                            LiabilityAccountId = FeeStructureDetailList.LiabilityAccountId,
                                            CostOfSaleAccountId = FeeStructureDetailList.CostOfSaleAccountId,
                                            Amount = FeeStructureDetailList.FeeAmount,
                                            Status = true,
                                        }).ToList();
                                        db.AccFeeStructureDetail.AddRange(NewFeeStructureDetail);
                                        Response = (int?)HttpResponses.CODE_SUCCESS;
                                    }
                                    else
                                    {
                                        Response = HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                                    }
                                    #endregion


                                }
                                // DUPLICATE RECORD EXIST :: PROCEED DO NOT INSERT DATA
                                else
                                {
                                    Response = (int?)HttpResponses.CODE_DATA_ALREADY_EXIST;
                                }

                                break;

                            #endregion

                            #region DELETE DATA INTO DB:: ACCFEESTRUCTURE AND ACCFEESTRUCTUREDETAIL
                            case nameof(DB_OperationType.DELETE_DATA_INTO_DB):
                                try
                                {
                                    Response = (int?)HttpResponses.CODE_DATA_UPDATED;
                                }
                                catch
                                {
                                    Response = (int?)HttpResponses.CODE_DATA_ALREADY_EXIST;
                                }
                                break;
                            #endregion

                        }


                        #region TRANSACTION HANDLING DETAIL
                        switch (Response)
                        {
                            case (int?)HttpResponses.CODE_SUCCESS:
                            case (int?)HttpResponses.CODE_DATA_UPDATED:
                                db.SaveChanges();
                                dbTran.Commit();
                                break;
                            
                            case (int?)HttpResponses.CODE_BAD_REQUEST:
                                dbTran.Rollback();
                                break;
                            default:
                                dbTran.Rollback();
                                break;
                        }
                       
                        return HttpStatus.HttpResponseByReturnValue(Response);
                        #endregion

                    }
                    catch (Exception Ex)
                    {
                        dbTran.Rollback();
                        return HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                    }
                }
            }
        }
        public static int? AccFeeChallan_Insert(_SqlParameters PostedData, List<_SqlParameters> PostedDataDetail)
        {
            using (var db = new SESEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        #region CHECK IF 1ST FEE INSTALLMENT ALREADY EXIST
                        var IsAlreadyExist = office360.Common.DataBaseProcedures.Common.GetDataFromDB.CountCheck_AccFeeChallan(PostedData);
                        #endregion

                        #region IF DATA DOES NOT EXIST PROCEED
                        if (IsAlreadyExist.Count == 0)
                        {
                            #region GET MONTHS ON RANGE BASIS FOR ISSUING FEE CHALLAN
                            var TransactionMonthNo = 4;
                                //office360.Common.DataBaseProcedures.Common.GetDataFromDB.PopulateAccFeeStructureByParameter(PostedData);
                            PostedData.TransactionMonth = TransactionMonthNo;
                            #endregion
                            #region DECLARATION OF VARIABLES FOR FEE CHALLAN PARENT
                       //     Guid? FeeChallanId = Uttility.GetHashGuid();
                            string Description = "Fee Challan For Month of " + Uttility.fn_GetMonthRange(PostedData);
                            int? SessionId = PostedData.SessionId;
                            int? ClassId = PostedData.ClassId;
                            int? RegistrationTypeId = PostedData.RegistrationTypeId;
                            int? ClassRegistrationId = PostedData.ClassRegistrationId;
                            DateTime? TransactionDate = PostedData.TransactionDate;
                            DateTime? DueDate = PostedData.DueDate;
                            DateTime? ExpiryDate = PostedData.DueDate;
                            int? FeeStructureId = PostedData.FeeStructureId;
                            decimal? GrossRecievable = PostedData.GrossRecievable;
                            decimal? LateFee = PostedData.LateFee;
                            decimal? Discount = PostedData.Discount;
                            decimal? NetRecievable = PostedData.NetRecievable;
                            #endregion
                            #region OUTPUT PARAMETER
                            ObjectParameter Response = new ObjectParameter("Response", typeof(int));
                            #endregion
                            #region INSERT INTO DATA BASE AccFeeChallan
                            var Data = db.AccFeeChallan_Insert(
                                null,
                                                               // FeeChallanId,
                                                                Description,
                                                                SessionId,
                                                                ClassId,
                                                                RegistrationTypeId,
                                                                ClassRegistrationId,
                                                                TransactionDate,//CHALLAN ISSUE DATE
                                                                DueDate,
                                                                ExpiryDate,
                                                                FeeStructureId,
                                                                GrossRecievable,
                                                                LateFee,
                                                                Discount,
                                                                NetRecievable,
                                                                Session_Manager.UserId,
                                                                (int?)Models.General.DocumentStatus.DocStatus.UnPaid_FEE_CHALLAN,
                                                                (int?)Models.General.DocumentStatus.DocType.FEE_CHALLAN,
                                                                Session_Manager.BranchId,
                                                                Session_Manager.CompanyId,
                                                                Response
                                                              );
                            #endregion
                            #region INSERT INTO DATA BASE AccFeeChallanDetail
                            foreach (var Item in PostedDataDetail)
                            {
                                decimal? DifferenceCharge = Item.ActualFeeAmount - Item.ChargedFeeAmount;
                                db.AccFeeChallanDetail_Insert(
                                                                  null, // FeeChallanId,
                                                                  Item.FeeTypeId,
                                                                  Item.ActualFeeAmount,
                                                                  Item.ChargedFeeAmount,
                                                                  DifferenceCharge
                                                                );
                            }
                            #endregion
                            #region INSERT INTO DATA BASE FEE LEDGER
                            #endregion
                            #region INSERT INTO DATA BASE GENERAL JOURNAL
                            foreach (var Item in PostedDataDetail)
                            {
                                if (Item.AssetAccountId != null)
                                {

                                }
                                else if (Item.LiabilityAccountId != null)
                                {

                                }
                            }
                            #endregion

                            dbTran.Commit();
                            return HttpStatus.HttpResponseByReturnValue((int?)Response.Value);
                        }
                        #endregion
                        else
                        {
                            return HttpStatus.HttpResponses.CODE_DATA_ALREADY_EXIST.ToInt();
                        }

                    }
                    catch (Exception Ex)
                    {
                        dbTran.Rollback();
                        return HttpStatus.HttpResponses.CODE_INTERNAL_SERVER_ERROR.ToInt();
                    }
                }
            }
        }
    }
}