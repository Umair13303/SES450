var OperationType = "";

var StudyLevelIds = "";
var StudyGroupIds = "";
$(document).ready(function () {
    var DB_OperationType = $('#HiddenFieldDB_OperationType').val();
    switch (DB_OperationType) {
        case PARAMETER.DB_OperationType.INSERT:

            $('#DivButtonSubmitDown').show();
            $('#DivButtonUpdateDown').hide();
            break;
        case PARAMETER.DB_OperationType.UPDATE:
            $('#DivButtonSubmitDown').hide();
            $('#DivButtonUpdateDown').show();
            break;
    }
    PopulateDropDownLists();
    ChangeCase();
});
function PopulateDropDownLists() {
    PopulateMTGeneralCompanyList();
    PopulateLKCampusTypeList();
    PopulateLKOrganizationTypeList();
    PopulateLKCountryList();
    PopulateLKPolicyPeriodList();
    PopulateLKChallanMethodList();
    PopulateLKRollCallSystemList();
    PopulateLKBillingMethodList();
    PopulateLKStudyLevelList();
    PopulateLKStudyGroupList();
}

//-----------ALL CHANGE CASES
function ChangeCase() {
    $('#DropDownListCountry').change(function () {
        PopulateLKCityListByParam();
    });
    $("#DropDownListStudyLevels").attr('data-width', '100%').select2({
        placeholder: 'Select an Option',
        multiple: true,
    }).change(function (event) {
        if (event.target == this) {
            StudyLevelIds = $(this).val();
        }
    });
    $("#DropDownListStudyGroups").attr('data-width', '100%').select2({
        placeholder: 'Select an Option',
        multiple: true,
    }).change(function (event) {
        if (event.target == this) {
            StudyGroupIds = $(this).val();
        }
    });
}

//-----------ALL DROPDOWN LIST
function PopulateMTGeneralCompanyList() {
    var JsonArg = {
        ActionCondition: PARAMETER.SESCondition.GET_MT_GENERALCOMPANY_BYPARAMETER,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MBranchUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].CompanyName + '' + '</option>';
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
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLKCampusTypeList() {
    var JsonArg = {
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_CAMPUSTYPE,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MBranchUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListCampusType").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLKOrganizationTypeList() {
    var JsonArg = {
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_ORGANIZATIONTYPE,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MBranchUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListOrganizationType").html(s);

        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLKCountryList() {
    var JsonArg = {
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_COUNTRY,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MBranchUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListCountry").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLKCityListByParam() {
    var CountryId = $("#DropDownListCountry :selected").val();
    var JsonArg = {
        CountryId: CountryId,
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_CITY_BYPARAMETER,
    }
    $.ajax({

        type: "POST",
        url: BasePath + "/ACompany/MBranchUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListCity").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });

}
function PopulateLKPolicyPeriodList() {
    var JsonArg = {
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_POLICYPERIOD,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MBranchUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListPolicyPeriod").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLKChallanMethodList() {
    var JsonArg = {
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_CHALLANMETHOD_BYPARAMTER,
        ListCondition: PARAMETER.SPListCondition.CHALLANMETHOD_LIST,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MBranchUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListChallanMethod").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLKRollCallSystemList() {
    var JsonArg = {
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_ROLLCALLSYSTEM,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MBranchUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListRollCallSystem").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLKBillingMethodList() {
    var JsonArg = {
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_BILLINGMETHOD,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MBranchUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListBillingMethod").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLKStudyLevelList() {
    var JsonArg = {
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_STUDYLEVEL_BYPARAMTER,
        ListCondition: PARAMETER.SPListCondition.STUDYLEVEL_LIST,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MBranchUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListStudyLevels").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}
function PopulateLKStudyGroupList() {
    var JsonArg = {
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_STUDYGROUP_BYPARAMTER,
        ListCondition: PARAMETER.SPListCondition.STUDYGROUP_LIST,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MBranchUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListStudyGroups").html(s);
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
    if ($('#DropDownListCompany').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxDescription').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#DropDownListCampusType').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListOrganizationType').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListCountry').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListCity').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxAddress').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxContactNo').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxEmailAddress').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#DropDownListPolicyPeriod').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListChallanMethod').RequiredDropdown() == false) {
        return false;
    }

    if ($('#DropDownListRollCallSystem').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListBillingMethod').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListStudyLevels').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListStudyGroups').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxNTNNo').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxRemarks').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    return true;
}
function UpSertDataIntoDB() {
    var CompanyId = $('#DropDownListCompany :selected').val();
    var Description = $('#TextBoxDescription').val();
    var CampusTypeId = $('#DropDownListCampusType :selected').val();
    var OrganizationTypeId = $('#DropDownListOrganizationType :selected').val();
    var CountryId = $('#DropDownListCountry :selected').val();
    var CityId = $('#DropDownListCity :selected').val();
    var Address = $('#TextBoxAddress').val();
    var ContactNo = $('#TextBoxContactNo').val();
    var EmailAddress = $('#TextBoxEmailAddress').val();
    var PolicyPeriodId = $('#DropDownListPolicyPeriod :selected').val();
    var ChallanMethodId = $('#DropDownListChallanMethod :selected').val();
    var RollCallSystemId = $('#DropDownListRollCallSystem :selected').val();
    var BillingMethodId = $('#DropDownListBillingMethod :selected').val();
    var NTNNo = $('#TextBoxNTNNo').val();
    var Remarks = $('#TextBoxRemarks').val();

    var CampusGuID = $('#HiddenFieldCampusGuID').val();

    var JsonArg = {
        GuID: CampusGuID,
        OperationType: OperationType,
        CompanyId: CompanyId,
        Description: Description,
        CampusTypeId: CampusTypeId,
        OrganizationTypeId: OrganizationTypeId,
        CountryId: CountryId,
        CityId: CityId,
        Address: Address,
        ContactNo: ContactNo,
        EmailAddress: EmailAddress,
        ChallanMethodId: ChallanMethodId,
        PolicyPeriodId: PolicyPeriodId,
        RollCallSystemId: RollCallSystemId,
        BillingMethodId: BillingMethodId,
        StudyLevelIds: StudyLevelIds.toString(),
        StudyGroupIds: StudyGroupIds.toString(),

        NTNNo: NTNNo,
        Remarks: Remarks,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MBranchUI/Insert_GeneralBranches",
        dataType: 'json',
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            debugger
            GetMessageBox(data.Message, data.Code);
            debugger
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
function GET_GENERALBRANCH_DETAILBYID() {
    var CampusGuID = $('#HiddenFieldCampusGuID').val();
    var JsonArg = {
        GuID: CampusGuID,
        ActionCondition: PARAMETER.SESCondition.GET_MT_GENERALBRANCH_DETAILBYID,
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

        },
        complete: function () {
            

        },
    });
};