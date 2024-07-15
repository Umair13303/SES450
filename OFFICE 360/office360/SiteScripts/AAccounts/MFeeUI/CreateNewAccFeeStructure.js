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
    PopulateMTGeneralCompanyList();
    PopulateMTStructureFeeTypeListByParam();
    PopulateLKWHTaxPolicyList();
}

//-----------ALL CHANGE CASES
function ChangeCase() {
    $('#DropDownListCompany').change(function () {
        PopulateMTGeneralBranchListByParam();
    });
    $('#DropDownListCampus').change(function () {
        PopulateMTAppSessionListByParam();
    });
    $('#DropDownListSession').change(function () {
        PopulateMTAppClassListByParam();
    });
}

//-----------ALL DROPDOWN LIST
function PopulateMTGeneralCompanyList() {
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
function PopulateMTGeneralBranchListByParam() {
    var CompanyId = $("#DropDownListCompany :selected").val();
    var JsonArg = {
        CompanyId: CompanyId,
        ListCondition: PARAMETER.SPListCondition.BRANCH_BY_USER_ALLOWEDBRANCHIDS,
        ActionCondition: PARAMETER.SESCondition.GET_MT_GENERALBRANCH_BYPARAMETER,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListCampus").html(s);
        },
    });
}
function PopulateMTAppSessionListByParam() {
    var JsonArg = {
        CampusId: $('#DropDownListCampus :selected').val(),
        ActionCondition: PARAMETER.SESCondition.GET_MT_APPSESSION_BYPARAMETER,
        ListCondition: PARAMETER.SPListCondition.APPSESSION_BY_GENERALBRANCH,
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
function PopulateMTAppClassListByParam() {
    var JsonArg = {
        CampusId: $('#DropDownListCampus :selected').val(),
        ClassIds: $('#DropDownListSession option:selected').data('classids'),
        ActionCondition: PARAMETER.SESCondition.GET_MT_APPCLASS_BYPARAMETER,
        ListCondition: PARAMETER.SPListCondition.APPCLASS_BY_APPSESSION,

    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListClass").html(s);

        },
    });
}
function PopulateMTStructureFeeTypeListByParam() {
    var JsonArg = {
        ActionCondition: PARAMETER.SESCondition.GET_MT_STRUCTUREFEETYPE_BYPARAMETER,
        ListCondition: PARAMETER.SPListCondition.STRUCTUREFEETYPE_LIST,

    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListClass").html(s);

        },
    });
}
function PopulateLKWHTaxPolicyList() {
    var JsonArg = {
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_WHTAXPOLICY_BYPARAMTER,
        ListCondition: PARAMETER.SPListCondition.WHTAXPOLICY_LIST,

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
