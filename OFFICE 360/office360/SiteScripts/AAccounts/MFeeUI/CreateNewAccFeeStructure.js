var OperationType = "";

var table = "";
var FeeSettingIsSecurity;
var FeeSettingIsRefundable;
var FeeSettingIsDiscount;
var WH_Percentage;
var WH_SlabAmount;
var WH_FixedCharges;
var WH_IsOnExceedingAmount;

$(document).ready(function () {
    InitDataTable();
    PopulateDropDownLists();
    ChangeCase();
});
//-----------ALL DATA TABLE
function InitDataTable() {
    table = $('#MainTableFeeStructure').DataTable({
        "responsive": true,        "ordering": false,
        "processing": true,        "pagination": false,
        "paging": false,
        "columns": [
            { "title": "#", "orderable": false, },
            { "title": "Fee" },
            { "title": "Amount"                 },
            { "title": "Action"                 },
            { "title": "FeeTypeId",             },
            { "title": "RevenueAccountId",      },
            { "title": "AssetAccountId",        },
            { "title": "LiabilityAccountId",    },
            { "title": "CostOfSaleAccountId",   },
        ],
        "columnDefs": [
            { visible: false, targets: [4,5,6,7,8] },
        ],
    });
    table.on('draw', function () {
        CalcBoxDataTable();
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });

    }).draw();

    AppendTableFooterTotals("MainTableFeeStructure", "1", "TDTotalFeeAmount", "Recievable Fee");
    AppendTableFooterTotals("MainTableFeeStructure", "1", "TDWHTAmount", "Applicable Tax");
    AppendTableFooterTotals("MainTableFeeStructure", "1", "TDGrossFeeAmount","Total Fee Incl. Tax");
}
//-----------DATA TABLE BASED OPERATION
$('#ButtonPlus').click(function (event) {
    event.preventDefault();
    var IsValid = ValidateInputFieldsFeeStructure();
    if (IsValid) {
        try {

            var AssetAccountId = null; var RevenueAccountId = null; var CostOfSaleAccountId = null; var LiabilityAccountId = null;
            var row_data = [];
            var FeeType = $('#DropDownListFeeType :selected').text();
            var FeeTypeId = $('#DropDownListFeeType :selected').val();
            var Amount = $('#TextBoxAmount').val();
            if (FeeSettingIsSecurity == true && FeeSettingIsDiscount == true) {
                AssetAccountId = $('#DropDownListAssetAccount :selected').val();
                LiabilityAccountId = $('#DropDownListLiabilityAccount :selected').val();
                CostOfSaleAccount = $('#DropDownListCostOfSaleAccount :selected').val();
            }
            else if (FeeSettingIsSecurity == false && FeeSettingIsDiscount == true) {
                RevenueAccountId = $('#DropDownListRevenueAccount :selected').val();
                AssetAccountId = $('#DropDownListAssetAccount :selected').val();
                CostOfSaleAccountId = $('#DropDownListCostOfSaleAccount :selected').val();
            }
            else if (FeeSettingIsSecurity == true && FeeSettingIsDiscount == false) {
                AssetAccountId = $('#DropDownListAssetAccount :selected').val();
                LiabilityAccountId = $('#DropDownListLiabilityAccount :selected').val();
            }
            else if (FeeSettingIsSecurity == false && FeeSettingIsDiscount == false) {
                RevenueAccountId = $('#DropDownListRevenueAccount :selected').val();
                AssetAccountId = $('#DropDownListAssetAccount :selected').val();
            }
            row_data[0] = "";
            row_data[1] = FeeType;
            row_data[2] = Amount;
            row_data[3] = GetDeletebtn();
            row_data[4] = FeeTypeId;
            row_data[5] = RevenueAccountId;
            row_data[6] = AssetAccountId;
            row_data[7] = LiabilityAccountId;
            row_data[8] = CostOfSaleAccountId;
            var IsAlreadyExist = false;
            table.column(1).data().each(function (value, index) {
                if (value == (row_data[1])) {
                    IsAlreadyExist = true;
                }
            });
            if (IsAlreadyExist) {
                GetMessageBox("Duplicate Record Exist", 505)
            }
            else {
                table.row.add(row_data).draw();
                ClearOtherFeeSetting();
            }
            IsAlreadyExist = false;
        }
        catch {
            GetMessageBox(err, 500);
        }
    }
});

function ValidateInputFieldsFeeStructure() {
    if ($('#DropDownListWHTaxPolicy').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListFeeType').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxAmount').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    return true;
}
$('#MainTableFeeStructure tbody').on('click', '.delete', function (e) {
    var selectedRow = $(this).closest('tr');
    var RIdx = table.row(selectedRow).index();
    table.row(selectedRow).remove().draw();
});
function CalcBoxDataTable() {
    var TotalFeeAmount = table.column(2).data().reduce(function (a, b) {
        return parseFloat(a) + parseFloat(b);
    }, 0);

    var TaxAmount = 0;
    //  if (IsOnExceedingAmount == true) {
    if (TotalFeeAmount > WH_SlabAmount) {
        var ExceedingAmount = parseFloat(TotalFeeAmount - WH_SlabAmount);
        TaxAmount = parseFloat(WH_FixedCharges + ((ExceedingAmount / 100) * WH_Percentage));
    }
    if (TotalFeeAmount > WH_SlabAmount) {
        TaxAmount = parseFloat(WH_FixedCharges + ((TotalFeeAmount / 100) * WH_Percentage));
    }

    var NetPayable = TaxAmount + TotalFeeAmount;

    $('#TDTotalFeeAmount').text(TotalFeeAmount.toFixed(2));
    $('#TDWHTAmount').text(TaxAmount.toFixed(2));
    $('#TDGrossFeeAmount').text(NetPayable.toFixed(2));
}
function ClearOtherFeeSetting() {
    $('#DropDownListRevenueAccount').val('-1').change();
    $('#DropDownListAssetAccount').val('-1').change();
    $('#DropDownListLiabilityAccount').val('-1').change();
    $('#DropDownListCostOfSaleAccount').val('-1').change();
}


function PopulateDropDownLists() {
    PopulateMT_GeneralCompany_List();
    PopulateMT_StructureFeeType_ListByParam();
    PopulateLK_WHTaxPolicy_List();
    PopulateMT_Account_REV_List();
    PopulateMT_Account_AST_List();
    PopulateMT_Account_LIAB_List();
    PopulateMT_Account_COS_List();
}

//-----------ALL CHANGE CASES
function ChangeCase() {
    $('#DropDownListCompany').change(function (event) {
        event.preventDefault();
        PopulateMT_GeneralBranch_ListByParam();
    });
    $('#DropDownListCampus').change(function (event) {
        event.preventDefault();
        PopulateMT_AppSession_ListByParam();
    });
    $('#DropDownListSession').change(function (event) {
        event.preventDefault();
        PopulateMT_AppClass_ListByParam();
    });
    $('#DropDownListSession').change(function (event) {
        event.preventDefault();
        PopulateMT_AppClass_ListByParam();
    });
    $('#DropDownListWHTaxPolicy').change(function (event) {
        event.preventDefault();
        WH_Percentage               = $('#DropDownListWHTaxPolicy :selected').attr('data-Percentage');
        WH_SlabAmount               = $('#DropDownListWHTaxPolicy :selected').attr('data-SlabAmount');
        WH_FixedCharges             = $('#DropDownListWHTaxPolicy :selected').attr('data-FixedCharges');
        WH_IsOnExceedingAmount      = $('#DropDownListWHTaxPolicy :selected').attr('data-IsOnExceedingAmount');
        CalcBoxDataTable();
    });
    $('#DropDownListFeeType').change(function (event) {
        event.preventDefault();
        GET_STRUCTUREFEETYPE_DETAILBYID();
    });

}

//-----------ALL DROPDOWN LIST
function PopulateMT_GeneralCompany_List() {
    var JsonArg = {
        ActionCondition     : PARAMETER.SESCondition.GET_MT_GENERALCOMPANY_BYPARAMETER,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].CompanyName + '' + '</option>';
            }
            $("#DropDownListCompany").html(s);
            $("#DropDownListCompany").val(CompanyId).trigger('change');
            if (RoleId == Roles.RoleID_ADMIN || RoleId == Roles.RoleID_DEVELOPER) {
                $("#DropDownListCompany").prop('disabled', Status.Close);
            }
            else {
                $("#DropDownListCompany").prop('disabled', Status.Open);
            }
        },
    });
}
function PopulateMT_GeneralBranch_ListByParam() {
    var JsonArg = {
        ActionCondition     : PARAMETER.SESCondition.GET_MT_GENERALBRANCH_BYPARAMETER,
        DB_IF_PARAM         : PARAMETER.DB_IF_Condition.BRANCH_BY_USER_ALLOWEDBRANCHIDS,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListCampus").html(s);
        },
    });
}
function PopulateMT_AppSession_ListByParam() {
    var CampusId = $('#DropDownListCampus :selected').val();

    var JsonArg = {
        ActionCondition     : PARAMETER.SESCondition.GET_MT_APPSESSION_BYPARAMETER,
        DB_IF_PARAM         : PARAMETER.DB_IF_Condition.APPSESSION_BY_GENERALBRANCH,
        CampusId            : CampusId,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option ' +
                        'data-ClassIds="' + data[i].ClassIds + '" ' +
                        'value="' + data[i].Id + '">' + data[i].Description +
                    '</option>';
            }
            $("#DropDownListSession").html(s);

        },
    });
}
function PopulateMT_AppClass_ListByParam() {
    var CampusId = $('#DropDownListCampus :selected').val();
    var ClassIds = $('#DropDownListSession :selected').attr('data-ClassIds');
    var JsonArg = {
        ActionCondition     : PARAMETER.SESCondition.GET_MT_APPCLASS_BYPARAMETER,
        DB_IF_PARAM         : PARAMETER.DB_IF_Condition.APPCLASS_BY_APPSESSION,
        CampusId            : CampusId,
        ClassIds            : ClassIds,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description +  '' + '</option>';
            }
            $("#DropDownListClass").html(s);

        },
    });
}
function PopulateLK_WHTaxPolicy_List() {
    var JsonArg = {
        ActionCondition     : PARAMETER.LookUpCondition.GET_LK1_WHTAXPOLICY_BYPARAMTER,
        DB_IF_PARAM         : PARAMETER.DB_IF_Condition.WHTAXPOLICY_LIST,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option ' +
                        'data-Percentage="' + data[i].Percentage + '" ' +                        'data-IsOnExceedingAmount="' + data[i].IsOnExceedingAmount + '" ' +
                        'data-SlabAmount="' + data[i].SlabAmount + '" ' +                        'data-FixedCharges="' + data[i].FixedCharges + '" ' +
                        'value="' + data[i].Id + '">' + data[i].Description +
                    '</option>';
            }
            $("#DropDownListWHTaxPolicy").html(s);
        }
    });
}
function PopulateMT_StructureFeeType_ListByParam() {
    var JsonArg = {
        ActionCondition     : PARAMETER.SESCondition.GET_MT_STRUCTUREFEETYPE_BYPARAMETER,
        DB_IF_PARAM         : PARAMETER.DB_IF_Condition.STRUCTUREFEETYPE_BY_ACADEMICFEE,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option ' +
                    'data-FeeTypeGuId="' + data[i].GuID + '" ' +
                    'value="' + data[i].Id + '">' + data[i].Description +
                    '</option>';
            }
            $("#DropDownListFeeType").html(s);

        },
    });
}
function PopulateMT_Account_REV_List() {
    var JsonArg = {
        ActionCondition     : PARAMETER.SESCondition.GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER,
        DB_IF_PARAM         : PARAMETER.DB_IF_Condition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
        CoaCatagoryIds      : FILTER.COA_ACCOUNTTYPE.SALES_REVENUES,
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
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListRevenueAccount").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateMT_Account_AST_List() {
    var JsonArg = {
        ActionCondition     : PARAMETER.SESCondition.GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER,
        DB_IF_PARAM         : PARAMETER.DB_IF_Condition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
        CoaCatagoryIds      : FILTER.COA_ACCOUNTTYPE.ACCOUNTS_RECEIVABLE,

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
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListAssetAccount").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateMT_Account_LIAB_List() {
    var JsonArg = {
        ActionCondition     : PARAMETER.SESCondition.GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER,
        DB_IF_PARAM         : PARAMETER.DB_IF_Condition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
        CoaCatagoryIds      : FILTER.COA_ACCOUNTTYPE.CURRENT_LIABILITIES + "," + FILTER.COA_ACCOUNTTYPE.OTHER_CURRENT_LIABILITIES,

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
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListLiabilityAccount").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateMT_Account_COS_List() {
    var JsonArg = {
        ActionCondition     : PARAMETER.SESCondition.GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER,
        DB_IF_PARAM         : PARAMETER.DB_IF_Condition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
        CoaCatagoryIds      : FILTER.COA_ACCOUNTTYPE.COST_OF_SALES,

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
                s += '<option  value=' + data[i].Id + '>' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListCostOfSaleAccount").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}

function ValidateInputFields() {
    if ($('#DropDownListCompany').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListCampus').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListSession').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListClass').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListWHTaxPolicy').RequiredDropdown() == false) {
        return false;
    }
    if (!table.data().count()) {
        GetMessageBox("No Fee Structure Has Been Entered", 505);
        return false;
    }
    return true;
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
            alert(err);
            GetMessageBox(err, 500);
        }
    }
});

function UpSertDataIntoDB() {
    var CampusId        = $('#DropDownListCampus :selected').val();
    var SessionId       = $('#DropDownListSession :selected').val();
    var ClassId         = $('#DropDownListClass :selected').val();
    var WHTaxPolicyId   = $('#DropDownListWHTaxPolicy :selected').val();
    var TotalFeeAmount  = $('#TDTotalFeeAmount').text();
    var WHTAmount       = $('#TDWHTAmount').text();
    var GrossFeeAmount  = $('#TDGrossFeeAmount').text();
    var FeeStructureGuID = $('#HiddenFieldFeeStructureGuID').val();

    var JsonArg = {
        GuID            : FeeStructureGuID,
        CampusId        : CampusId,
        SessionId       : SessionId,
        ClassId         : ClassId,
        WHTaxPolicyId   : WHTaxPolicyId,
        TotalFeeAmount  : TotalFeeAmount,
        WHTAmount       : WHTAmount,
        GrossFeeAmount  : GrossFeeAmount,
    }

    var FeeStructureDetailArray = [];
    table.rows().every(function (rowIdx, tableLoop, rowLoop) {
        var FeeAmount           = table.cell({ row: rowIdx, column: 2 }).data();
        var FeeTypeId           = table.cell({ row: rowIdx, column: 4 }).data();
        var RevenueAccountId    = table.cell({ row: rowIdx, column: 5 }).data();
        var AssetAccountId      = table.cell({ row: rowIdx, column: 6 }).data();
        var LiabilityAccountId  = table.cell({ row: rowIdx, column: 7 }).data();
        var CostOfSaleAccountId = table.cell({ row: rowIdx, column: 8 }).data();

        var JsonArg_FeeStructureDetail = {
            FeeAmount           : FeeAmount,
            FeeTypeId           : FeeTypeId,
            RevenueAccountId    : RevenueAccountId,
            AssetAccountId      : AssetAccountId,
            LiabilityAccountId  : LiabilityAccountId,
            CostOfSaleAccountId : CostOfSaleAccountId,
        }

        FeeStructureDetailArray.push(JsonArg_FeeStructureDetail);
    });

    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/Update_Insert_StructureFeeType",
        dataType: 'json',
        data: { 'PostedData': (JsonArg), 'PostedDataDetail': (FeeStructureDetailArray), },
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
function ClearInputFields() {
    $('.form-control').val('');
    $('.select2').val('-1').change();
    $('form').removeClass('Is-Valid');
}

//-----------LOAD ENTERY RECORD
function GET_STRUCTUREFEETYPE_DETAILBYID() {
    var FeeTypeId = $('#DropDownListFeeType :selected').attr('data-FeeTypeGuId');
        var JsonArg = {
            GuID            : FeeTypeId,
            ActionCondition : PARAMETER.SESCondition.GET_MT_STRUCTUREFEETYPE_DETAILBYID,
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
                if (data.length == undefined) {
                }
                else {
                    ClearOtherFeeSetting();
                     FeeSettingIsSecurity   = data[0].IsSecurity;
                     FeeSettingIsRefundable = data[0].IsRefundable;
                     FeeSettingIsDiscount   = data[0].IsDiscount;

                    if (FeeSettingIsSecurity == true && FeeSettingIsDiscount == true) {
                        $('#DivDropDownListRevenueAccount').hide();
                        $('#DivDropDownListAssetAccount').show();
                        $('#DivDropDownListLiabilityAccount').show();
                        $('#DivDropDownListCostOfSaleAccount').show();
                    }
                    else if (FeeSettingIsSecurity == false && FeeSettingIsDiscount == true) {
                        $('#DivDropDownListRevenueAccount').show();
                        $('#DivDropDownListAssetAccount').show();
                        $('#DivDropDownListLiabilityAccount').hide();
                        $('#DivDropDownListCostOfSaleAccount').show();
                    }
                    else if (FeeSettingIsSecurity == true && FeeSettingIsDiscount == false) {
                        $('#DivDropDownListRevenueAccount').hide();
                        $('#DivDropDownListAssetAccount').show();
                        $('#DivDropDownListLiabilityAccount').show();
                        $('#DivDropDownListCostOfSaleAccount').hide();
                    }
                    else if (FeeSettingIsSecurity == false && FeeSettingIsDiscount == false) {
                        $('#DivDropDownListRevenueAccount').show();
                        $('#DivDropDownListAssetAccount').show();
                        $('#DivDropDownListLiabilityAccount').hide();
                        $('#DivDropDownListCostOfSaleAccount').hide();
                    }
                }
            },
            complete: function () {
                stopLoading();
            },
        });

}

