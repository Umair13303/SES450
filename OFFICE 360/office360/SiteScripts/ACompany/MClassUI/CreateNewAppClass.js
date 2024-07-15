$(document).ready(function () {
    PopulateDropDownLists();
});
function PopulateDropDownLists() {
    PopulateLKStudyGroupList();
    PopulateLKStudyLevelList();
}

//-----------ALL DROPDOWN LIST
function PopulateLKStudyGroupList() {
    var JsonArg = {
        ListCondition: PARAMETER.SPListCondition.STUDYGROUP_BY_BRANCHSETTING,
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_STUDYGROUP_BYPARAMTER,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MClassUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListStudyGroup").html(s);
        },
    });
}
function PopulateLKStudyLevelList() {
    var JsonArg = {
        ListCondition: PARAMETER.SPListCondition.STUDYLEVEL_BY_BRANCHSETTING,
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_STUDYLEVEL_BYPARAMTER,

    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MClassUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListStudyLevel").html(s);
        },
    });
}


//-----------DB OPERATION CALL
$('#ButtonSubmitDown').click(function (event) {
    event.preventDefault();
    var IsValid = ValidateInputFields();
    if (IsValid) {
        try {
            InsertData();
        }
        catch {
            GetMessageBox(err, 500);
        }
    }
});
function ValidateInputFields() {
    if ($('#TextBoxDescription').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#DropDownListStudyLevel').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListStudyGroup').RequiredDropdown() == false) {
        return false;
    }
    return true;
}
function InsertData() {
    var Description = $('#TextBoxDescription').val();
    var StudyGroupId = $('#DropDownListStudyGroup :selected').val();
    var StudyLevelId = $('#DropDownListStudyLevel :selected').val();
    var JsonArg = {
        Description: Description,
        StudyLevelId: StudyLevelId,
        StudyGroupId: StudyGroupId,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MClassUI/Insert_AppClass",
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
