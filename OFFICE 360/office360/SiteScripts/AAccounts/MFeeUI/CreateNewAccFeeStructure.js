var table = "";
$(document).ready(function () {
    InitDataTable();
    PopulateDropDownLists();
    ChangeCase();
});
//-----------ALL DATA TABLE
function InitDataTable() {
    table = $('#MainTableFeeStructure').DataTable({
        "responsive": true,
        "ordering": false,
        "processing": true,
        "columns": [
            { "title": "#", "orderable": false, },
            { "title": "Fee" },
            { "title": "Asset A/c" },
            { "title": "Liability A/c" },
            { "title": "Revenue   A/c" },
            { "title": "IsTaxable" },
            { "title": "Amount" },
            { "title": "Action" },
            { "title": "FeeTypeId",         visible:false },
            { "title": "AssetAccountId",    visible: false },
            { "title": "LiabilityAccountId",visible: false },
            { "title": "RevenueAccountId",  visible: false },
        ]
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });

    }).draw();
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
    $('#DropDownListCompany').change(function () {
        PopulateMT_GeneralBranch_ListByParam();
    });
    $('#DropDownListCampus').change(function () {
        PopulateMT_AppSession_ListByParam();
    });
    $('#DropDownListSession').change(function () {
        PopulateMT_AppClass_ListByParam();
    });
    $('#DropDownListFeeType').change(function (event) {
        event.preventDefault();
        GET_STRUCTUREFEETYPE_DETAILBYID();
    });
}

//-----------ALL DROPDOWN LIST
function PopulateMT_GeneralCompany_List() {
    var JsonArg = {
        ActionCondition: PARAMETER.SESCondition.GET_MT_GENERALCOMPANY_BYPARAMETER,
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
        ActionCondition: PARAMETER.SESCondition.GET_MT_GENERALBRANCH_BYPARAMETER,
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.BRANCH_BY_USER_ALLOWEDBRANCHIDS,
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
        ActionCondition: PARAMETER.SESCondition.GET_MT_APPSESSION_BYPARAMETER,
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.APPSESSION_BY_GENERALBRANCH,
        CampusId: CampusId,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option data-ClassIds="' + data[i].ClassIds + '" value="' + data[i].Id + '">' + data[i].Description + '</option>';            }
            $("#DropDownListSession").html(s);

        },
    });
}
function PopulateMT_AppClass_ListByParam() {
    var CampusId = $('#DropDownListCampus :selected').val();
    var ClassIds = $('#DropDownListSession :selected').attr('data-ClassIds');
    var JsonArg = {
        ActionCondition: PARAMETER.SESCondition.GET_MT_APPCLASS_BYPARAMETER,
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.APPCLASS_BY_APPSESSION,
        CampusId: CampusId,
        ClassIds: ClassIds,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListClass").html(s);

        },
    });
}
function PopulateLK_WHTaxPolicy_List() {
    var JsonArg = {
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_WHTAXPOLICY_BYPARAMTER,
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.WHTAXPOLICY_LIST,

    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option data-Percentage="' + data[i].Percentage + '" value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListWHTaxPolicy").html(s);

        },
    });
}
function PopulateMT_StructureFeeType_ListByParam() {
    var JsonArg = {
        ActionCondition: PARAMETER.SESCondition.GET_MT_STRUCTUREFEETYPE_BYPARAMETER,
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.STRUCTUREFEETYPE_BY_ACADEMICFEE,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                
                s += '<option data-FeeTypeGuId="'+data[i].GuID+'" value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListFeeType").html(s);

        },
    });
}
function PopulateMT_Account_REV_List() {
    var JsonArg = {
        ActionCondition: PARAMETER.SESCondition.GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER,
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
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
        ActionCondition: PARAMETER.SESCondition.GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER,
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
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
        ActionCondition: PARAMETER.SESCondition.GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER,
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
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
        ActionCondition: PARAMETER.SESCondition.GET_MT_STRUCTURECOAACCOUNT_BYPARAMETER,
        DB_IF_PARAM: PARAMETER.DB_IF_Condition.STRUCTURECOAACCOUNT_BY_GENERALCOMPANY,
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
                s += '<option  value=' + data[i].Id + '>' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListCostOfSaleAccount").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}

//-----------LOAD ENTERY RECORD
function GET_STRUCTUREFEETYPE_DETAILBYID() {
    var FeeTypeId = $('#DropDownListFeeType :selected').attr('data-FeeTypeGuId');

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
                ClearOtherFeeSetting();
                var IsSecurity = data[0].IsSecurity;
                var IsRefundable = data[0].IsRefundable;
                var IsDiscount = data[0].IsDiscount;

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

            },
            complete: function () {
                stopLoading();
            },
        });

}

function ClearOtherFeeSetting() {
    $('#DropDownListRevenueAccount').val('-1').change();
    $('#DropDownListAssetAccount').val('-1').change();
    $('#DropDownListLiabilityAccount').val('-1').change();
    $('#DropDownListCostOfSaleAccount').val('-1').change();
}