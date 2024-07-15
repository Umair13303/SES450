var ClassIds;
$(document).ready(function () {
    PopulateDropDownLists();
    ChangeCase();
});
function PopulateDropDownLists() {
    PopulateLKEnrollmentTypeList();

    PopulateMTGeneralCompanyList();
}

//-----------ALL CHANGE CASES
function ChangeCase() {
    $('#DropDownListCompany').change(function () {
        PopulateMTGeneralBranchListByParam();
    });
    $('#DropDownListCampus').change(function () {
        PopulateMTAppClassListByParam();
    });
    $("#DropDownListClass").attr('data-width', '100%').select2({
        placeholder: 'Select an Option',
        multiple: true,
    }).change(function (event) {
        if (event.target == this) {
            ClassIds = $(this).val();
        }
    });
}

//-----------ALL DROPDOWN LIST
function PopulateLKEnrollmentTypeList() {
    var JsonArg = {
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_ENROLLMENTTYPES,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MAcademicSessionUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListEnrollmentType").html(s);
        },
    });
}

function PopulateMTGeneralCompanyList() {
    var JsonArg = {
        ActionCondition: PARAMETER.SESCondition.GET_MT_GENERALCOMPANY_BYPARAMETER,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MAcademicSessionUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="(-1)">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option  value="' + data[i].Id + '">' + data[i].CompanyName + '' + '</option>';
            }
            $("#DropDownListCompany").html(s);
            $("#DropDownListCompany").val(CompanyId).trigger('change');
            if (RoleId == Roles.RoleID_ADMIN || RoleId == Roles.RoleID_DEVELOPER)
            {
                $("#DropDownListCompany").prop('disabled', Status.Close);
            }
            else
            {
                $("#DropDownListCompany").prop('disabled', Status.Open);
            }
        },
    });
}

function PopulateMTGeneralBranchListByParam() {
    var CompanyId = $("#DropDownListCompany :selected").val();
    var JsonArg = {
        CompanyId: CompanyId,
        ListCondition: PARAMETER.SPListCondition.BRANCH_BY_USER_ALLOWEDBRANCHIDS_CHECK_ACTIVE_APPSESSION,
        ActionCondition: PARAMETER.SESCondition.GET_MT_GENERALBRANCH_BYPARAMETER,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MAcademicSessionUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Id + '" ' + data[i].ExtraClass + '>' + data[i].Description + '</option>';
            }
            $("#DropDownListCampus").html(s);
        },
    });
}

function PopulateMTAppClassListByParam() {
    var JsonArg = {
        CampusId: $('#DropDownListCampus :selected').val(),
        ActionCondition: PARAMETER.SESCondition.GET_MT_APPCLASS_BYPARAMETER,
        ListCondition: PARAMETER.SPListCondition.APPCLASS_BY_GENERALBRANCH,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MAcademicSessionUI/GET_DATA_BY_PARAMETER",
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

//-----------DB OPERATION CALL

$('#ButtonSubmitDown').click(function (event) {
    event.preventDefault();
    var IsValid = ValidateInputFields();
    debugger
    if (IsValid) {
        try {
            InsertData();
        }
        catch (err) {
            GetMessageBox(err, 500);

        }
    }
});

function ValidateInputFields() {
    debugger
    if ($('#DropDownListCompany').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListCampus').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListEnrollmentType').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxSessionDescription').RequiredTextBoxInputGroup() == false) {
        return false;
    }  
    if ($('#TextBoxSessionPeriod').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#DropDownListClass').RequiredDropdown() == false) {
        return false;
    }
    return true;
}

function InsertData() {

    var SessionPeriod = $('#TextBoxSessionPeriod').val();
    var Dates = SessionPeriod.split("to");
    var SessionStartDate = Dates[0].trim();
    var SessionEndDate = Dates[1].trim();

    var CampusId = $('#DropDownListCampus :selected').val();
    var Description = $('#TextBoxSessionDescription').val();
    var EnrollmentTypeId = $('#DropDownListEnrollmentType :selected').val();
    var JsonArg = {
        CampusId: CampusId,
        Description: Description,
        EnrollmentTypeId: EnrollmentTypeId,
        SessionStartDate: SessionStartDate,
        SessionEndDate: SessionEndDate,
        ClassIds: ClassIds.toString(),
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MAcademicSessionUI/Insert_AppSession",
        dataType: 'json',
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            debugger
            startLoading();
        },
        success: function (data) {
            GetMessageBox(data.Message, data.Code);
            ClearInputFields();
        },
        complete: function () {
            stopLoading();
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
