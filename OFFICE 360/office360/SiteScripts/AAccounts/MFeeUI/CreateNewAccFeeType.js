
var OperationType = "";


$(document).ready(function () {
   var DB_OperationType = $('#HiddenFieldDB_OperationType').val();
    switch (DB_OperationType) {
        case PARAMETER.DB_OperationType.INSERT:

            $('#DivButtonSubmitDown').show();
            $('#DivButtonUpdateDown').hide();
            break;
        case PARAMETER.DB_OperationType.UPDATE:
            PopulateMTFeeTypeList();
            $('#DivButtonSubmitDown').hide();
            $('#DivButtonUpdateDown').show();
            break;
    }
    PopulateDropDownLists();
    ChangeCase();
});


function PopulateDropDownLists() {
    PopulateLKFeeCatagoryList();
    PopulateLKChargingMethodList();
    PopulateMTAccount_REV_List();
    PopulateMTAccount_AST_List();
    PopulateMTAccount_LIAB_List();
    PopulateMTAccount_COS_List();
}

//-----------ALL CHANGE CASE
function ChangeCase() {
    $('#DropDownListFeeType').change(function (event) {
        event.preventDefault();
        GET_STRUCTUREFEETYPE_DETAILBYID();
    });

    $('#DropDownListFeeCatagory').change(function (event) {
        event.preventDefault();
        $('.CheckBox').prop("checked", false).change();

        ClearOtherFeeSetting();
        var FeeCatagoryId = $('#DropDownListFeeCatagory :selected').val();
        switch (FeeCatagoryId) {
            case ('-1'):
            case FILTER.FEE_CATAGORY.ACADEMIC_FEE.toString():

                $('#DropDownListChargingMethod').val('-1').change().prop('disabled', false);

                $('#DivCOAOtherFeeType').hide();
                break;

            case FILTER.FEE_CATAGORY.OTHER_FEE.toString():
                $('#DropDownListChargingMethod').val('-1').change().prop('disabled', true);
                $('#DivCOAOtherFeeType').show();
                break;
        }
    });
    $('#DropDownListChargingMethod').change(function (event) {
        event.preventDefault();
        var ChargingMethodId = $('#DropDownListChargingMethod').val();
        $('#CheckBoxIsOnAdmission').prop("checked", false).change();
        switch (ChargingMethodId) {
            case FILTER.FEE_CHARGING_METHOD.ONE_TIME.toString():
                $('#DivCheckBoxIsOnAdmission').show();
                break;

            case FILTER.FEE_CHARGING_METHOD.RECURRING.toString():
            case "-1":
                $('#DivCheckBoxIsOnAdmission').hide();
                break;
        }
    });
    $('#CheckBoxIsSecurity, #CheckBoxIsDiscount').change(function (event) {
        event.preventDefault();

        ClearOtherFeeSetting();
        var FeeCatagoryId = $('#DropDownListFeeCatagory :selected').val();
        switch (FeeCatagoryId) {
            case FILTER.FEE_CATAGORY.OTHER_FEE.toString():
                var IsSecurity = $('#CheckBoxIsSecurity').prop('checked');
                var IsDiscount = $('#CheckBoxIsDiscount').prop('checked');

                if (IsSecurity == true && IsDiscount == true) {
                    $('#DivDropDownListRevenueAccount').hide();
                    $('#DivDropDownListAssetAccount').show();
                    $('#DivDropDownListLiabilityAccount').show();
                    $('#DivDropDownListCostOfSaleAccount').show();
                }
                else if (IsSecurity == false && IsDiscount == true) {
                    $('#DivDropDownListRevenueAccount').show();
                    $('#DivDropDownListAssetAccount').show();
                    $('#DivDropDownListLiabilityAccount').hide();
                    $('#DivDropDownListCostOfSaleAccount').show();
                }
                else if (IsSecurity == true && IsDiscount == false) {
                    $('#DivDropDownListRevenueAccount').hide();
                    $('#DivDropDownListAssetAccount').show();
                    $('#DivDropDownListLiabilityAccount').show();
                    $('#DivDropDownListCostOfSaleAccount').hide();
                }
                else if (IsSecurity == false && IsDiscount == false) {
                    $('#DivDropDownListRevenueAccount').show();
                    $('#DivDropDownListAssetAccount').show();
                    $('#DivDropDownListLiabilityAccount').hide();
                    $('#DivDropDownListCostOfSaleAccount').hide();
                }


                break;

        }

    });
}


//-----------ALL DROPDOWN LIST
function PopulateLKFeeCatagoryList() {
    var JsonArg = {
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_FEECATAGORY,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListFeeCatagory").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLKChargingMethodList() {
    var JsonArg = {
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_CHARGINGMETHOD,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListChargingMethod").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateMTAccount_REV_List() {
    var JsonArg = {
        ActionCondition: PARAMETER.SESCondition.GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER,
        ListCondition: PARAMETER.SPListCondition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
        CoaCatagoryIds: FILTER.COA_ACCOUNTTYPE.SALES_REVENUES,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListRevenueAccount").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateMTAccount_AST_List() {
    var JsonArg = {
        ActionCondition: PARAMETER.SESCondition.GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER,
        ListCondition: PARAMETER.SPListCondition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
        CoaCatagoryIds: FILTER.COA_ACCOUNTTYPE.ACCOUNTS_RECEIVABLE,

    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListAssetAccount").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateMTAccount_LIAB_List() {
    var JsonArg = {
        ActionCondition: PARAMETER.SESCondition.GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER,
        ListCondition: PARAMETER.SPListCondition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
        CoaCatagoryIds: FILTER.COA_ACCOUNTTYPE.CURRENT_LIABILITIES + "," + FILTER.COA_ACCOUNTTYPE.OTHER_CURRENT_LIABILITIES,

    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListLiabilityAccount").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateMTAccount_COS_List() {
    var JsonArg = {
        ActionCondition: PARAMETER.SESCondition.GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER,
        ListCondition: PARAMETER.SPListCondition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
        CoaCatagoryIds: FILTER.COA_ACCOUNTTYPE.COST_OF_SALES,

    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value=' + data[i].Id + '>' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListCostOfSaleAccount").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}




//-----------DB OPERATION CALL

$('#ButtonSubmitDown').click(function (event) {
    event.preventDefault();
    var IsValid = ValidateInputFields();
    if (IsValid) {
        try {
            OperationType = PARAMETER.DB_OperationType.INSERT;
            UpSertDataIntoDB();
        }
        catch {
            GetMessageBox(err, 500);
        }
    }
});
$('#ButtonUpdateDown').click(function (event) {
    event.preventDefault();
    var IsValid = ValidateInputFields();
    if (IsValid) {
        try {
            OperationType = PARAMETER.DB_OperationType.UPDATE;
            UpSertDataIntoDB();
        }
        catch {
            GetMessageBox(err, 500);
        }
    }
});
function ValidateInputFields() {
    if ($('#DropDownListFeeCatagory').RequiredDropdown() == false) {
        return false;
    }
    var FeeCatagoryId = $('#DropDownListFeeCatagory :selected').val();
    switch (FeeCatagoryId) {
        case FILTER.FEE_CATAGORY.ACADEMIC_FEE.toString():
            if ($('#DropDownListChargingMethod').RequiredDropdown() == false) {
                return false;
            }
            if ($('#TextBoxDescription').RequiredTextBoxInputGroup() == false) {
                return false;
            }
            return true;

            break;

        case FILTER.FEE_CATAGORY.OTHER_FEE.toString():
            var IsSecurity = $('#CheckBoxIsSecurity').prop('checked');
            var IsDiscount = $('#CheckBoxIsDiscount').prop('checked');

            if ($('#TextBoxDescription').RequiredTextBoxInputGroup() == false) {
                return false;
            }
            if (IsSecurity == true && IsDiscount == true) {

                if ($('#DropDownListAssetAccount').RequiredDropdown() == false) {
                    return false;
                }
                if ($('#DropDownListLiabilityAccount').RequiredDropdown() == false) {
                    return false;
                }
                if ($('#DropDownListCostOfSaleAccount').RequiredDropdown() == false) {
                    return false;
                }
            }
            else if (IsSecurity == false && IsDiscount == true) {

                if ($('#DropDownListRevenueAccount').RequiredDropdown() == false) {
                    return false;
                }
                if ($('#DropDownListAssetAccount').RequiredDropdown() == false) {
                    return false;
                }
                if ($('#DropDownListCostOfSaleAccount').RequiredDropdown() == false) {
                    return false;
                }

            }
            else if (IsSecurity == true && IsDiscount == false) {

                if ($('#DropDownListAssetAccount').RequiredDropdown() == false) {
                    return false;
                }

                if ($('#DropDownListLiabilityAccount').RequiredDropdown() == false) {
                    return false;
                }
            }
            else if (IsSecurity == false && IsDiscount == false) {
                if ($('#DropDownListRevenueAccount').RequiredDropdown() == false) {
                    return false;
                }
                if ($('#DropDownListAssetAccount').RequiredDropdown() == false) {
                    return false;
                }
            }
            return true;
            break;
    }
    return false;
}
function UpSertDataIntoDB() {
    try {
        var FeeCatagoryId = null; var ChargingMethodId = null; var Description = null;
        var IsOnAdmission = null; var IsDiscount = null; var IsRefundable = null; var IsSecurity = null;
        var AssetAccountId = null; var RevenueAccountId = null; var CostOfSaleAccountId = null; var LiabilityAccountId = null;


        FeeCatagoryId = $('#DropDownListFeeCatagory :selected').val();
        switch (FeeCatagoryId) {
            case FILTER.FEE_CATAGORY.ACADEMIC_FEE.toString():

                FeeCatagoryId = $('#DropDownListFeeCatagory :selected').val();
                ChargingMethodId = $('#DropDownListChargingMethod :selected').val();
                Description = $('#TextBoxDescription').val();
                IsOnAdmission = $('#CheckBoxIsOnAdmission').prop('checked');
                IsDiscount = $('#CheckBoxIsDiscount').prop('checked');
                IsRefundable = $('#CheckBoxIsRefundable').prop('checked');
                IsSecurity = $('#CheckBoxIsSecurity').prop('checked');

                break;

            case FILTER.FEE_CATAGORY.OTHER_FEE.toString():

                FeeCatagoryId = $('#DropDownListFeeCatagory :selected').val();
                Description = $('#TextBoxDescription').val();
                IsDiscount = $('#CheckBoxIsDiscount').prop('checked');
                IsRefundable = $('#CheckBoxIsRefundable').prop('checked');
                IsSecurity = $('#CheckBoxIsSecurity').prop('checked');
                if (IsSecurity == true && IsDiscount == true) {
                    AssetAccountId = $('#DropDownListAssetAccount :selected').val();
                    LiabilityAccountId = $('#DropDownListLiabilityAccount :selected').val();
                    CostOfSaleAccountId = $('#DropDownListCostOfSaleAccount :selected').val();
                }
                else if (IsSecurity == false && IsDiscount == true) {
                    RevenueAccountId = $('#DropDownListRevenueAccount :selected').val();
                    AssetAccountId = $('#DropDownListAssetAccount :selected').val();
                    CostOfSaleAccountId = $('#DropDownListCostOfSaleAccount :selected').val();
                }
                else if (IsSecurity == true && IsDiscount == false) {
                    AssetAccountId = $('#DropDownListAssetAccount :selected').val();
                    LiabilityAccountId = $('#DropDownListLiabilityAccount :selected').val();
                }
                else if (IsSecurity == false && IsDiscount == false) {
                    RevenueAccountId = $('#DropDownListRevenueAccount :selected').val();
                    AssetAccountId = $('#DropDownListAssetAccount :selected').val();
                }
                break;

        }
        var FeeTypeGuID = $('#HiddenFieldFeeTypeGuID').val();


        var JsonArg = {
            GuID: FeeTypeGuID,
            OperationType: OperationType,
            FeeCatagoryId: FeeCatagoryId,
            ChargingMethodId: ChargingMethodId,
            Description: Description,
            IsOnAdmission: IsOnAdmission,
            IsSecurity: IsSecurity,
            IsRefundable: IsRefundable,
            IsDiscount: IsDiscount,
            RevenueAccountId: RevenueAccountId,
            AssetAccountId: AssetAccountId,
            LiabilityAccountId: LiabilityAccountId,
            CostOfSaleAccountId: CostOfSaleAccountId,
        }
        $.ajax({
            type: "POST",
            url: BasePath + "/AAccounts/MFeeUI/Update_Insert_StructureFeeType",
            dataType: 'json',
            data: { 'PostedData': (JsonArg) },
            beforeSend: function () {
                startLoading();
            },
            success: function (data) {
                GetMessageBox(data.Message, data.Code);
                ClearInputFields();
            },
            complete: function () {
                stopLoading();
                ClearInputFields();

            },
            error: function (jqXHR, error, errorThrown) {
                GetMessageBox("The Transaction Can Not Be Performed Due To Serve Activity", 500);

            },
        });
    }
    catch (error) {
        console.error("Error: ", error);
        GetMessageBox("An unexpected error occurred", 500);
    }

}
function ClearInputFields() {
    $('.form-control').val('');
    $('.select2').val('-1').change();
    $('form').removeClass('Is-Valid');
    $('.CheckBox').prop("checked", false);
}
function ClearOtherFeeSetting() {
    $('#DropDownListRevenueAccount').val('-1').change();
    $('#DropDownListAssetAccount').val('-1').change();
    $('#DropDownListLiabilityAccount').val('-1').change();
    $('#DropDownListCostOfSaleAccount').val('-1').change();
}

//-----------LOAD ENTERY RECORD 

function PopulateMTFeeTypeList() {
    var JsonArg = {
        ActionCondition: PARAMETER.SESCondition.GET_MT_STRUCTUREFEETYPE_BYPARAMETER,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                var Description = data[i].FeeName;
                s += '<center><option  value="' + data[i].GuID + '">' + Description + '' + '</option>';
            }
            $("#DropDownListFeeType").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}

function GET_STRUCTUREFEETYPE_DETAILBYID() {
    var FeeTypeId = $('#DropDownListFeeType :selected').val();

    if (FeeTypeId != null && FeeTypeId != undefined && FeeTypeId != "" && FeeTypeId != "-1") {
        var JsonArg = {
            GuID: FeeTypeId,
            ActionCondition: PARAMETER.SESCondition.GET_MT_STRUCTUREFEETYPE_DETAILBYID,
        }
        $.ajax({
            type: "POST",
            url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
            dataType: 'json',
            data: { 'PostedData': (JsonArg) },
            beforeSend: function () {
                startLoading();
            },
            success: function (data) {
                $('#DropDownListFeeCatagory').val(data[0].FeeCatagoryId).change();
                $('#TextBoxDescription').val(data[0].FeeName);

                switch (data[0].FeeCatagoryId) {
                    case FILTER.FEE_CATAGORY.ACADEMIC_FEE:
                        $('#DropDownListChargingMethod').val(data[0].ChargingMethodId).change();
                        break;
                    case FILTER.FEE_CATAGORY.OTHER_FEE:
                        $('#DropDownListChargingMethod').val('-1').change();
                        break;
                }
                $('#CheckBoxIsOnAdmission').prop('checked', (data[0].IsOnAdmission)).change();
                $('#CheckBoxIsSecurity').prop('checked', (data[0].IsSecurity)).change();
                $('#CheckBoxIsRefundable').prop('checked', (data[0].IsRefundable)).change();
                $('#CheckBoxIsRefundable').prop('checked', (data[0].IsRefundable)).change();
                $('#CheckBoxIsDiscount').prop('checked', (data[0].IsDiscount)).change();
                $('#DropDownListRevenueAccount').val(data[0].RevenueAccountId).change();
                $('#DropDownListAssetAccount').val(data[0].AssetAccountId).change();
                $('#DropDownListLiabilityAccount').val(data[0].LiabilityAccountId).change();
                $('#DropDownListCostOfSaleAccount').val(data[0].CostOfSaleAccountId).change();
                $('#HiddenFieldFeeTypeGuID').val(FeeTypeId);
            },
            complete: function () {
                stopLoading();
            },
        });
    }

}