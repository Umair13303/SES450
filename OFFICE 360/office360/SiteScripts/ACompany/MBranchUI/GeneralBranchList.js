var table = "";
$(document).ready(function () {
    InitDataTable();
    PopulateDropDownLists();
    ChangeCase();
});
//-----------ALL DATA TABLE LIST
function InitDataTable() {
    table = $('#MainTable').DataTable({
        "responsive": true,
        "ordering": true,
        "processing": true,
        "columns": [
            { "data": null,             "title": "#"            },
            { "data": "GuID",           "title": "GuID"         },
            { "data": "Country",        "title": "Country"      },
            { "data": "City",           "title": "City"         },
            { "data": "Code",           "title": "Code"         },
            { "data": "Description",    "title": "Name"         },
            { "data": "CampusType",     "title": "Branch Type"  },
        ],
        columnDefs: [
            { visible: false, targets: 1 },
            { "orderable": false, targets: [0, 1, 2, 3, 4, 5, 6] },
            { className: PARAMETER.CustomerCSSClass.DynamicGroupBy, targets:[2,3,6] }
        ],
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });

    }).draw();
}

function PopulateDropDownLists() {
    PopulateLK_SearchParameter_List();
    PopulateDTGroupByList('MainTable', PARAMETER.CustomerCSSClass.DynamicGroupBy, 'DropDownListGroupBy');
}

//-----------ALL CHANGE CASES
function ChangeCase() {
    $('#DropDownListSearchBy').change(function (event) {
        event.preventDefault();
        var SearchBy = $('#DropDownListSearchBy :selected').val();
        if (SearchBy == 1) {
            $('#TextBoxQueryString').prop('disabled', true);
        }
        else {
            $('#TextBoxQueryString').prop('disabled', false);
        }
    });
}

//-----------ALL DROPDOWN LIST
function PopulateLK_SearchParameter_List() {
    var JsonArg = {
        DocType: PARAMETER.DocumentType.BRANCHES,
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_SEARCHPARAMETER_BYPARAMTER,
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
                s += '<option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListSearchBy").html(s);
        },
        complete: function () {
            stopLoading();
        },
    });
}

//-----------DB OPERATION CALL
$('#ButtonSearch').click(function (event) {
    event.preventDefault();
    var IsValid = true;
    if (IsValid) {
        try {
            DrawDataTable();

        }
        catch {
            GetMessageBox(err, 500);
        }
    }
});
function DrawDataTable() {
    var SearchById = $('#DropDownListSearchBy :selected').val();
    var InputText = $('#TextBoxQueryString').val();
    var JsonArg = {
        SearchById: SearchById,
        InputText: InputText,
    };
    var queryString = $.param(JsonArg);
    table.ajax.url((BasePath + "/ACompany/MBranchUI/PopulateGeneralBranchList_FORDT?" + queryString)).load();
}










