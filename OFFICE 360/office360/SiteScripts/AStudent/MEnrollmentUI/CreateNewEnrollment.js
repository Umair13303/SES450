var OperationType;
$(document).ready(function () {
    PopulateDropDownLists();
    InitDatePick();
});
function InitDatePick() {
    $('.date_masking').datepicker({
        format: 'dd/mm/yyyy',
        todayHighlight: true,
        autoclose: true,
        orientation: "bottom"
    }).datepicker("setDate", 'now');
}


// POPULATE DROPDOWN FUNCTION
function PopulateDropDownLists() {
    PopulateGenderList();
    PopulateMartialStatusList();
    PopulateReligionList();
    PopulateNationalityList();
    PopulateEducationLevelList();
    PopulateOccupationList();
    PopulateRelationshipsList();
    GetAllowedAppClasses();
}

// ALL DROPDOWNS
function PopulateGenderList() {
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/PopulateGenderList",
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description  + '</option>';
            }
            $("#DropDownListGender").html(s);
        },
    });
}
function PopulateMartialStatusList() {
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/PopulateMartialStatusList",
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description  + '</option>';
            }
            $("#DropDownListMartialStatus").html(s);
        },
    });
}
function PopulateReligionList() {
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/PopulateReligionList",
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description  + '</option>';
            }
            $("#DropDownListReligion").html(s);
        },
    });
}
function PopulateNationalityList() {
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/PopulateNationalityList",
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description  + '</option>';
            }
            $("#DropDownListNationality").html(s);
        },
    });
}
function PopulateEducationLevelList() {
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/PopulateEducationLevelList",
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description  + '</option>';
            }
            $("#DropDownListParentStudyLevel").html(s);
        },
    });
}
function PopulateOccupationList() {
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/PopulateOccupationList",
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description  + '</option>';
            }
            $("#DropDownListOccupation").html(s);
        },
    });
}
function PopulateRelationshipsList() {
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/PopulateRelationshipsList",
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description  + '</option>';
            }
            $("#DropDownListRelationship").html(s);
        },
    });
}
function GetAllowedAppClasses() {
    var JsonArg = {
        SessionId: $('#HiddenFieldSessionId').val(),
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/GetAllowedAppClasses",
        data: { PostedData :(JsonArg)},
        success: function (data) {
            debugger
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + ' - ' + data[i].Code +'[ '+data[i].StudyLevel+' ( '+data[i].StudyGroup+ ' ) ]'  + '</option>';
            }
            $("#DropDownListClass").html(s);
        },
    });
}

// VALIDATE INPUT FIELDS
function ValidateInputFields() {
    debugger
    if ($('#TextBoxFirstName').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxLastName').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxDateofBirth').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxCnicNo_FormBNo').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#DropDownListGender').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListMartialStatus').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListReligion').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListNationality').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxResedenitalAddress').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxMobileNumber').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxEmailAddress').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxParentName').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxParentNICNo').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#DropDownListParentStudyLevel').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListOccupation').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListRelationship').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxMonthlyIncome').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#DropDownListClass').RequiredDropdown() == false) {
        return false;
    }
    return true;
}

// CLEAR ALL FIELDS
function ClearInputFields() {

    $('.form-control').val('');
    $('.select2').val('-1').change();
    $('form').removeClass('Is-Valid');
}
// CLICK FUNCTION
$('#ButtonSubmitDown').click(function (event) {
    event.preventDefault();
    var IsValid = ValidateInputFields();
    if (IsValid) {
        try {
            var OperationType = 1;
            InsertData(OperationType);
        }
        catch (err) {
            GetMessageBox(err, 500);

        }
    }
});
// INSERT FUNCTION
function InsertData(OperationType) {
    var FirstName = $('#TextBoxFirstName').val();
    var LastName = $('#TextBoxLastName').val();
    var DateofBirth = $('#TextBoxDateofBirth').val();
    var CnicNo_FormBNo = $('#TextBoxCnicNo_FormBNo').val();
    var GenderId = $('#DropDownListGender :selected').val();
    var MartialStatusId = $('#DropDownListMartialStatus :selected').val();
    var ReligionId = $('#DropDownListReligion :selected').val();
    var NationalityId = $('#DropDownListNationality :selected').val();
    var ResedenitalAddress = $('#TextBoxResedenitalAddress').val();
    var MobileNumber = $('#TextBoxMobileNumber').val();
    var EmailAddress = $('#TextBoxEmailAddress').val();
    var ParentName = $('#TextBoxParentName').val();
    var ParentNICNo = $('#TextBoxParentNICNo').val();
    var ParentStudyLevelId = $('#DropDownListParentStudyLevel :selected').val();
    var OccupationId = $('#DropDownListOccupation :selected').val();
    var RelationshipId = $('#DropDownListRelationship :selected').val();
    var MonthlyIncome = $('#TextBoxMonthlyIncome').val();
    var ClassId = $('#DropDownListClass :selected').val();
    var SessionId = $('#HiddenFieldSessionId').val();
    HiddenFieldSessionId

    var JsonArg = {
        FirstName: FirstName,
        LastName: LastName,
        DateofBirth: DateofBirth,
        CnicNo_FormBNo: CnicNo_FormBNo,
        GenderId: GenderId,
        MartialStatusId: MartialStatusId,
        ReligionId: ReligionId,
        NationalityId: NationalityId,
        ResedenitalAddress: ResedenitalAddress,
        MobileNumber: MobileNumber,
        EmailAddress: EmailAddress,
        ParentName: ParentName,
        ParentNICNo: ParentNICNo,
        ParentStudyLevelId: ParentStudyLevelId,
        OccupationId: OccupationId,
        RelationshipId: RelationshipId,
        MonthlyIncome: MonthlyIncome,
        ClassId: ClassId,
        SessionId: SessionId,
    }
    
    $.ajax({
        type: "POST",
        url: BasePath + "/AStudent/MEnrollmentUI/Insert_AppStudent",
        dataType: 'json',
        data: { 'PostedData': (JsonArg) },
        beforeSend: function () {
            debugger
            startLoading();
        },
        success: function (data) {
            //SAVE 
            if (OperationType == 1) {
                GetMessageBox(data.Message, data.Code);
                ClearInputFields();
            }
            //SAVE AND NEW
        },
        complete: function () {
            stopLoading();
        },
        error: function (jqXHR, error, errorThrown) {
            GetMessageBox("The Transaction Can Not Be Performed Due To Serve Activity", 500);

        },
    });
}

