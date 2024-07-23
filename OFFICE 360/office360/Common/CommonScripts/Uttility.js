
$.fn.RequiredTextBoxInputGroup = function () {
    $(this).removeClass('is-invalid is-valid');
    $(this).css('border', ''); // Reset the border
    var labelText = '';
    var inputId = $(this).attr('id');
    var labelForInput = $('label[for="' + inputId + '"]');
    if (labelForInput.length) {
        labelText = labelForInput.text();
    }
    if ($(this).val() == null || $(this).val() == '' || $(this).val() == undefined) {
        var errorMessage = labelText + ' is required.';
        var $errorDiv = $('<div class="col-sm-12 error text-danger">' + errorMessage + '</div>');
        $(this).parent().after($errorDiv);
        $(this).addClass('is-invalid');

        // Scroll to the input field
        $(this)[0].scrollIntoView({ behavior: 'smooth', block: 'center' });

        setTimeout(function () {
            $errorDiv.remove();
        }, 5000);
        return false;
    } else {
        $(this).parent().next('.error').remove();
        $(this).addClass('is-valid');
        return true;
    }
};




// DataTables column definition



$.fn.RequiredDropdown = function () {
    // Remove any existing validation messages and icons
    $(this).removeClass('is-invalid is-valid');
    $(this).css('border', ''); // Reset the border

    var labelText = '';
    // Find the label associated with this dropdown
    var inputId = $(this).attr('id');
    var labelForInput = $('label[for="' + inputId + '"]');
    if (labelForInput.length) {
        labelText = labelForInput.text();
    }

    if (!$(this).val() || $(this).val() == '-1') {
        // Create and display an error message dynamically
        var errorMessage = 'Please select a ' + labelText;
        var $errorDiv = $('<div class="col-sm-12 error text-danger">' + errorMessage + '</div>');

        // Insert the error message after the parent div of the dropdown
        $(this).closest('.form-group').after($errorDiv);

        $(this).addClass('is-invalid');

        // Scroll to the dropdown field
        $(this)[0].scrollIntoView({ behavior: 'smooth', block: 'center' });

        // Remove the error message after 5 seconds
        setTimeout(function () {
            $errorDiv.remove();
        }, 5000);

        return false;
    } else {
        // Add success class
        $(this).addClass('is-valid');

        // Remove any existing error messages after success
        $(this).closest('.form-group').next('.error').remove();

        return true;
    }
};



function GetHRAreaButton( title, url, text) {
    return '<a title = "' + title + '" class="view text-primary btn btn-lg"  href = "' + url + '" ><i class="fa fa-print" aria-hidden="true"></i></a >';
}

function GetStatus(Status) {
    var Badge = ""; // Initialize Badge variable to an empty string
    switch (Status) {
        case "Open":
            Badge = "orange";
            break;
        case "Processed":
            Badge = "blue";
            break;
        case "Closed":
            Badge = "green";
            break;
        case "Deleted":
            Badge = "grey";
            break;
        case "Rejected":
            Badge = "red";
            break;
        case "Disbursed":
            Badge = "purple";
            break;
        case "Active":
            Badge = "teal";
            break;
        case "In-Active":
            Badge = "maroon";
            break;
        default:
            Badge = "default"; // Set a default class name for unknown statuses
            break;
    }
    var Label = '<td> <span class="custom-badge status-' + Badge + '">' + Status + '</span></td>';
    return Label;
}
//function GetViewbtn(title, url, icon) {
//    return '<a title="' + title + '" class="view text-primary btn btn-lg" href="'+url+ '"><i class="fa fa-'+icon+'" aria-hidden="true"></i></a>';
//}
function GetViewbtn(url, title, text) {
    return "<td class='center'><a onclick=" + url + "  title='Click here to View " + title + "' class='btn btn-sm view'><i class='far fa-eye'></i> " + '' + "</a></td>";
}
function GetEditbtn(url, title, text) {
    return "<td class='center'><a onclick=" + url + "  title='Click here to View " + title + "' class='btn btn-sm edit'><i class='far fa-edit'></i> " + '' + "</a></td>";
}
function GetDeletebtn(url, title, text) {
    return "<td class='center'><a onclick=" + url + "  title='Click here to View " + title + "' class='btn btn-sm delete'><i class='far fa-trash'></i> " + '' + "</a></td>";
}
function OpenReport(response, status, xhr) {
    var filename = "";
    var disposition = xhr.getResponseHeader('Content-Disposition');
    if (disposition && disposition.indexOf('attachment') !== -1) {
        var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
        var matches = filenameRegex.exec(disposition);
        if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
    }
    var type = xhr.getResponseHeader('Content-Type');
    var blob = new Blob([response], { type: type });
    if (typeof window.navigator.msSaveBlob !== 'undefined') {
        /*IE workaround for "HTML7007: One or more blob URLs were revoked by closing the blob for which they were created. These URLs will no longer resolve as the data backing the URL has been freed."*/
        window.navigator.msSaveBlob(blob, filename);
    } else {
        var URL = window.URL || window.webkitURL;
        var downloadUrl = URL.createObjectURL(blob);
        if (filename) {
            /*use HTML5 a[download] attribute to specify filename*/
            var a = document.createElement("a");
            /*safari doesn't support this yet*/
            if (typeof a.download === 'undefined') {
                window.location = downloadUrl;
            } else {
                a.href = downloadUrl;
                a.download = filename;
                document.body.appendChild(a);
                a.click();
            }
        } else {
            var windowVal = window.open(downloadUrl);/*openNewTabOrNewWindow(downloadUrl);*/
            //windowVal.document.write('<html><head><title>EXP-220601-0001</title></head><body height="100%" width="100%"><iframe src="' + downloadUrl + '" height="100%" width="100%"></iframe></body></html>');
            //if (windowVal.document) { }
            windowVal.opener = null;
            if (!windowVal) {
                mesgboxshow("error", "Please allow pop-ups for view report. <strong>How to allow pop-ups : <a target='_blank' href='https://support.mozilla.org/en-US/kb/pop-blocker-settings-exceptions-troubleshooting'><span>Click here<span></a></strong>");
            }
        }
        setTimeout(function () { URL.revokeObjectURL(downloadUrl); }, 100); /*cleanup*/
    }
}

function Select2() {
    $('.select2').select2({
        placeholder: "Select a state"
    });

}
$(function () {   
    $('.select2').select2();
    $('.js-basic-single').select2();
        $('.js-basic-multiple').select2();
        $(".bs-select-1").val()
   
});

$("#logoutLink").click(function (e) {
    e.preventDefault(); 
    $.ajax({
        type: "POST",
        url: BasePath+'/Home/Logout',
        dataType: "json",
        success: function () {
        },

        error: function () {
        }
    });
});
function GetMessageBox(message, status) {
    const messageContainer = $('#messageContainer');
    let alertClass, iconClass;
    switch (status) {
        case 200:
            alertClass = 'alert-primary';
            iconClass = 'success';
            break;
        case 404:
            alertClass = 'alert-info';
            iconClass = 'error';
            break;
        case 505:
            alertClass = 'alert-danger';
            iconClass = 'info';
            break;
        default:
            alertClass = 'alert-secondary';
            iconClass = 'info';
            break;
    }

    Swal.fire({
        text: message,
        icon: iconClass
    });


   // const alertHtml = '<div class="alert border-0'+alertClass+' bg-gradient m-b-30 alert-dismissible fade show border-radius-none" role="alert"><strong style="color:white">'+message+'</strong><button type="button" class="close" data-dismiss="alert" aria-label="Close"><i class="${'+iconClass+'}"></i></button></div>';

   // messageContainer.html(alertHtml).fadeIn('slow').delay(7000).fadeOut();
}
function stopLoading() {
    setTimeout(function () {
        $.unblockUI();
    }, 1000);
}
function startLoading() {
    $.blockUI({
        message: 'Loading Please Wait',
        overlayCSS: {
            backgroundColor: '#1b2024',
            opacity: 0.8,
            zIndex: 1200,
            cursor: 'wait'
        },
        css: {
            border: 0,
            color: '#fff',
            zIndex: 1201,
            padding: 0,
            backgroundColor: 'transparent'
        }
    });
}
function ErrorMessage(Message) {
    $.blockUI({
        message: Message,
        fadeIn: 800,
        timeout: 8000,
        overlayCSS: {
            backgroundColor: '#1b2024',
            opacity: 0.8,
            zIndex: 1200,
            cursor: 'wait'
        },
        css: {
            border: 0,
            color: '#fff',
            zIndex: 1201,
            padding: 0,
            backgroundColor: 'transparent'
        }
    });
}
function GetCheckBox_row(Id) {
    return '<td><div class="form-group"><div class="checkbox checbox-switch switch-success"><label><input type="checkbox" Id="IsChecked' + Id +'" /><span></span></label></div></div></td>';
}
    function GetTextBox(Id) {
    debugger
    var inputElement = document.createElement('input');
    inputElement.type = 'text';
    inputElement.className = 'form-control date_masking';
    inputElement.placeholder = 'Please Enter End Date Here!';
    inputElement.id = Id;

    return inputElement; // Return the inputElement, not DatePicker
}
//function _InlineDate() {
//    $('.date_masking').datepicker({
//        format: 'dd/mm/yyyy',
//        todayHighlight: true,
//        autoclose: true,
//        orientation: "bottom"
//    }).datepicker("setDate", 'now');
/*}*/

function calculateSumOfColumn(TableId, ColumnIndex) {
    var TotalSum = 0;
    $('#' + TableId + ' tbody tr').each(function () {
        var value = $(this).find('td').eq(ColumnIndex).find('input').val();
        TotalSum += parseFloat(value) || 0; // The "|| 0" ensures that NaN is treated as 0
    });
    return TotalSum;
}

// Usage example:
var sum = calculateSumOfColumn('myTable', 5); // Replace 'myTable' with your table ID and 5 with your column index
console.log(sum); // Outputs the sum to the console

function ParseData(dotNetDate) {
    if (!dotNetDate) {
        return ''; // Return an empty string if the date is null or undefined
    }

    // Extract the timestamp from the format /Date(1673463600000)/
    var timestamp = parseInt(dotNetDate.substr(6));

    // Create a new Date object using the extracted timestamp
    var date = new Date(timestamp);

    // Define an array of month names
    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

    // Format the date as 13-Jan-2023
    var day = ("0" + date.getDate()).slice(-2); // Add leading 0 if necessary
    var month = months[date.getMonth()]; // Get the month name
    var year = date.getFullYear();

    return day + '-' + month + '-' + year;
}
function GetDecimalValue(number) {
    return number.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
function PopulateDTGroupByList(DataTableId, ListCondition,HTMLAttribute) {
    var Table = $('#' + DataTableId).DataTable();
    var DATA = [];

    Table.columns().every(function () {
        var column = this;
        var header = $(column.header());

        if (header.hasClass(ListCondition)) {
            DATA.push({
                Description: header.text().trim(),
                Id: column.index() // Adjust index if needed
            });
        }
    });
    if (HTMLAttribute) {
        var dropdown = $('#' + HTMLAttribute);
        dropdown.empty();
        dropdown.append('<option value="-1">Select an option</option>');

        DATA.forEach(function (header) {
            dropdown.append('<option value="' + header.Id + '">' + header.Description + '</option>');
        });
    }
}
function AppendTableFooterTotals(TableId, ColumnSpan, TableDivId, Header) {
    var Footer = $('#' + TableId).append('<tfoot><tr><th>' + Header + '</th><td class="Headings" colspan="' + ColumnSpan + '"></td><td id="' + TableDivId + '"></td><td colspan="6"></td></tr></tfoot>');

    return Footer;
}
