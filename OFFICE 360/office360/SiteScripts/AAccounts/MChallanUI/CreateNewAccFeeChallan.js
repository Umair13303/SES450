var table = "";
var NumberOfChallan;
var OperationType;
$(document).ready(function () {
    PopulateDropDownLists();
    ChangeCase();
    PickerDate();
    Picker_DataTable();
    KeyUpFunctions();
});
function PickerDate() {
    $('.date_masking').datepicker({
        format: 'dd/mm/yyyy',
        todayHighlight: true,
        autoclose: true,
        orientation: "bottom"
    }).datepicker("setDate", 'now');
}
function PopulateDropDownLists(){
    PopulateAppSessions();
    PopulateRegistrationTypes();
}
function ChangeCase() {
    $('#DropDownListSession').change(function (event) {
        PopulateAppClassBySessionId();
    });
    $('#DropDownListRegistrationType').change(function (event) {
        PopulateAppClassRegistrationsByParams();
    });
    $('#DropDownListClass').change(function (event) {
        PopulateFeeStructureByParams();
    });
    $("#DropDownListFeeStructure").change(function () {
        debugger
        NumberOfChallan = $(this).find('DropDownListFeeStructure :selected').data("NumberOfChallan");
        alert(NumberOfChallan);

        PopulateFeeStructureDetailByParams();
    });
 }
function PopulateAppSessions() {
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/FeeChallanUI/PopulateAppSessions",
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListSession").html(s);
        },
    });
}
function PopulateAppClassBySessionId() {
    var JsonArg = {
        SessionId: $('#DropDownListSession :selected').val(),
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/FeeChallanUI/PopulateAppClassBySessionId",
        data: { PostedData: (JsonArg) },
        success: function (data) {
            debugger
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + ' - ' + data[i].Code + '[ ' + data[i].StudyLevel + ' ( ' + data[i].StudyGroup + ' ) ]' + '</option>';
            }
            $("#DropDownListClass").html(s);
        },
    });
}
function PopulateRegistrationTypes() {
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/FeeChallanUI/PopulateRegistrationTypes",
        data: "{}",
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListRegistrationType").html(s);
        },
    });
}
function PopulateAppClassRegistrationsByParams() {
    var JsonArg = {
        SessionId: $('#DropDownListSession :selected').val(),
        ClassId: $('#DropDownListClass :selected').val(),
        RegistrationTypeId: $('#DropDownListRegistrationType :selected').val(),
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/FeeChallanUI/PopulateAppClassRegistrationsByParams",
        data: { PostedData: (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListClassRegistration").html(s);
        },
    });
}
function PopulateFeeStructureByParams(){
    var JsonArg = {
        SessionId: $('#DropDownListSession :selected').val(),
        ClassId: $('#DropDownListClass :selected').val(),
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/FeeChallanUI/PopulateFeeStructureByParams",
        data: { PostedData: (JsonArg) },
        success: function (data) {
            var s = '<option value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                debugger;
                alert(data[i].NumberOfChallan);
                s += '<option value="' + data[i].Id + '" data-NumberOfChallan="' + data[i].NumberOfChallan + '">' + data[i].Description + '</option>';
            }
            $("#DropDownListFeeStructure").html(s);
        }

    });
}

function Picker_DataTable() {
    table = $('#TableFeeInformation').DataTable({
        "responsive": true,
        "ordering": false,
        "searching": false,
        "createdRow": function (row, data, index) {
          //  AddRow(row);
        },
        "bPaginate": false,
        "responsive": true,
        "columns": [
            { "title": "#" }, /*0*/
            { "title": "Fee Name(s)" },  /*1*/
            { "title": "Charging Method" },  /*2*/
            { "title": "Asset Account" },  /*3*/
            { "title": "Liability Account" }, /*4*/
            { "title": "Revenue Account" }, /*5*/
            { "title": "Amount" }, /*6*/

            { "title": "FeeTypeId" }, /*7*/
            { "title": "FeeAmount" }, /*8*/
            { "title": "AssetAccountId" }, /*9*/
            { "title": "LiabilityAccountId" }, /*10*/
            { "title": "RevenueAccountId" }, /*11*/
            { "title": "OnAdmissionOnly" }, /*12*/
            { "title": "Recurring" }, /*13*/
            { "title": "Refundable" }, /*14*/
            { "title": "Security" }, /*15*/
        ],
        "columnDefs": [
            { targets: [7,8,9,10,11,12,13,14,15], 'visible': false },
        ],
        "order": [[0, 'desc']],
    });
}
function PopulateFeeStructureDetailByParams() {
    var JsonArg = {
        FeeStructureId: $('#DropDownListFeeStructure :selected').val(),
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/FeeChallanUI/PopulateFeeStructureDetailByParams",
        data: { PostedData: (JsonArg) },
        dataType: "json",
        beforeSend: function () {
            startLoading();
            table.clear().draw();
        },
        success: function (response) {
            var jsondata = response.data;
            for (var i = 0; i < jsondata.length; ++i) {
                var FeeAmount;
                if (jsondata[i].Recurring == true) {
                    FeeAmount = jsondata[i].FeeAmount / NumberOfChallan;
                }
                else {
                    FeeAmount = jsondata[i].FeeAmount
                }
                var row_data = [];
                row_data[0] = i + 1;
                row_data[1]  = jsondata[i].FeeName;
                row_data[2]  = jsondata[i].ChargingMethod;
                row_data[3]  = jsondata[i].AssetAccount;
                row_data[4]  = jsondata[i].LiabilityAccount;
                row_data[5]  = jsondata[i].RevenueAccount;
                row_data[6]  = "<input Id='FeeType" + jsondata[i].FeeTypeId + "' type='text' class='form-control col-md-12' Value='" + FeeAmount + "'/>";
                row_data[7]  = jsondata[i].FeeTypeId;
                row_data[8] =   FeeAmount;
                row_data[9]  = jsondata[i].AssetAccountId;
                row_data[10] = jsondata[i].LiabilityAccountId;
                row_data[11] = jsondata[i].RevenueAccountId;
                row_data[12] = jsondata[i].OnAdmissionOnly;
                row_data[13] = jsondata[i].Recurring;
                row_data[14] = jsondata[i].Refundable;
                row_data[15] = jsondata[i].Security;
                table.row.add(row_data);
            }

            table.draw();

            //DISPLAY TOTAL AMOUNT
            UpdateCalculator();
        },
        complete: function (response) {
            stopLoading();
        },
        error: function (jqXHR, error, errorThrown) {
       //     errors(jqXHR, error, errorThrown);
        }
    });
}


function KeyUpFunctions() {
    $('#TableFeeInformation').on('keyup', 'tbody tr td:nth-child(7) input', function () {
        UpdateCalculator();
    });
    $('#TextBoxLateFee').on('keyup', function () {
        UpdateCalculator();
    });
    $('#TextBoxDiscount').on('keyup', function () {
        UpdateCalculator();
    });
}
function UpdateCalculator() {

    var GrossReceivable = calculateSumOfColumn('TableFeeInformation', 6);
    $('#TextBoxGrossRecievable').val(GrossReceivable.toFixed(2));

    // Parse the values as floats to ensure proper arithmetic operations
    var GrossReceivable = parseFloat($('#TextBoxGrossRecievable').val()) || 0;
    var LateFee = parseFloat($('#TextBoxLateFee').val()) || 0;
    var Discount = parseFloat($('#TextBoxDiscount').val()) || 0;

    // Calculate net receivable
    var NetReceivable = GrossReceivable + LateFee;
    NetReceivable = NetReceivable - Discount;
    // Set the net receivable value in the appropriate textbox
    $('#TextBoxNetRecievable').val(NetReceivable.toFixed(2));
}
function ValidateInputFields() {
    debugger
    if ($('#DropDownListSession').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListClass').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListRegistrationType').RequiredDropdown() == false) {
        return false;
    }
    if ($('#DropDownListClassRegistration').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxTransactionDate').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxDueDate').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#TextBoxExpiryDate').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    if ($('#DropDownListFeeStructure').RequiredDropdown() == false) {
        return false;
    }
    if ($('#TextBoxGrossRecievable').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    //if ($('#TextBoxLateFee').RequiredTextBoxInputGroup() == false) {
    //    return false;
    //}
    //if ($('#TextBoxDiscount').RequiredTextBoxInputGroup() == false) {
    //    return false;
    //}
    if ($('#TextBoxNetRecievable').RequiredTextBoxInputGroup() == false) {
        return false;
    }
    return true;
}
function ClearInputFields() {
    $('.form-control').val('');
    $('.select2').val('-1').change();
    $('form').removeClass('Is-Valid');
}
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
$('#ButtonSubmitDownNew').click(function (event) {
    event.preventDefault();
    var IsValid = ValidateInputFields();
    if (IsValid) {
        try {
            var OperationType = 2;
            InsertData(OperationType);
        }
        catch (err) {
            GetMessageBox(err, 500);

        }
    }
});
function InsertData(OperationType) {
    debugger
    var SessionId = $('#DropDownListSession :selected').val();
    var ClassId = $('#DropDownListClass :selected').val();
    var RegistrationTypeId = $('#DropDownListRegistrationType :selected').val();
    var ClassRegistrationId = $('#DropDownListClassRegistration :selected').val();
    var TransactionDate = $('#TextBoxTransactionDate').val();
    var DueDate = $('#TextBoxDueDate').val();
    var ExpiryDate = $('#TextBoxExpiryDate').val();
    var FeeStructureId = $('#DropDownListFeeStructure :selected').val();
    var GrossRecievable = $('#TextBoxGrossRecievable').val();
    var LateFee = $('#TextBoxLateFee').val();
    var Discount = $('#TextBoxDiscount').val();
    var NetRecievable = $('#TextBoxNetRecievable').val();
    var JsonArg = {
        SessionId: SessionId,
        ClassId: ClassId,
        RegistrationTypeId: RegistrationTypeId,
        ClassRegistrationId: ClassRegistrationId,
        TransactionDate: TransactionDate,
        DueDate: DueDate,
        ExpiryDate: ExpiryDate,
        FeeStructureId: FeeStructureId,
        GrossRecievable: GrossRecievable,
        LateFee: LateFee,
        Discount: Discount,
        NetRecievable: NetRecievable,
    }
    var FeeChallanDetails = [];
    debugger
    table.rows().every(function (rowIdx, tableLoop, rowLoop) {
        debugger
        var $row = $(this.node());
        var FeeTypeId = table.cell({ row: rowIdx, column: 7 }).data();
        var ActualFeeAmount = $row.find("td:eq(6) input[type='text']").val();
        var ChargedFeeAmount = table.cell({ row: rowIdx, column: 8 }).data();
        var AssetAccountId = table.cell({ row: rowIdx, column: 9 }).data();
        var LiabilityAccountId = table.cell({ row: rowIdx, column: 10 }).data();
        var RevenueAccountId = table.cell({ row: rowIdx, column: 11 }).data();
        var JsonArg_FeeChallanDetails = {
            FeeTypeId: FeeTypeId,
            ActualFeeAmount: ActualFeeAmount,
            ChargedFeeAmount: ChargedFeeAmount,
            AssetAccountId: AssetAccountId,
            LiabilityAccountId: LiabilityAccountId,
            RevenueAccountId: RevenueAccountId,
        }
        debugger
        FeeChallanDetails.push(JsonArg_FeeChallanDetails);
    });
    debugger

    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/FeeChallanUI/Insert_AccFeeChallan",
        dataType: 'json',
        data: { 'PostedData': (JsonArg), 'PostedDataDetail': (FeeChallanDetails) },
        beforeSend: function () {
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



