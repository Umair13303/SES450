var table = "";

$(document).ready(function () {
    ChangeCase();
    InitDateTable();
});

function ChangeCase() {
    $('#DropDownListSearchBy').change(function (event) {
        $('#TextBoxQueryString').val('');
        event.preventDefault();
        var SearchBy = $('#DropDownListSearchBy :selected').val();
        if (SearchBy == 1) {
            $('#TextBoxQueryString').prop('disabled',true);
        }
        else {
            $('#TextBoxQueryString').prop('disabled',false);
        }
    });
}

//-----------ALL DATA TABLE LIST
function InitDateTable() {
    var GroupColumn_P1 = 2;
    var GroupColumn_P2 = 3;
    
    table = $('#MainTableFeeStructure').DataTable({
        "responsive": true,
        "ordering": true,
        "processing": true,
        "columns": [
            { "data": null,                 "title": "#"             }, 
            { "data": "Code",               "title": "Code"          },
            { "data": "Session",            "title": "Session",      },
            { "data": "Class",              "title": "Class"         },
            { "data": "WHTaxPolicy",        "title": "WH Tax Policy" },
            { "data": "TotalFee",           "title": "TotalFee"      },
            {
                "data": null, "title": "Status",                "defaultContent": "",
                "render": function (data, type, full, meta)
                {
                    return GetStatus(data["DocumentStatus"]);
                }
            },
            {
                "data": null, "title": "Action(s)",                "defaultContent": "",
                "render": function (data, type, full, meta)
                {
                    var UpdateDocumentStatus;
                    var DocumentStatus = parseInt(data["DocumentStatus"], 10); // Convert to integer
                    if (DocumentStatus == (PARAMETER.DocStatus.Active_FEE_STRUCTURE)) 
                    {
                        alert(1);
                        UpdateDocumentStatus = PARAMETER.DocStatus.InActive_FEE_STRUCTURE;
                    }
                    else if (DocumentStatus ==(PARAMETER.DocStatus.InActive_FEE_STRUCTURE)) 
                    {
                        alert(2);

                        UpdateDocumentStatus = PARAMETER.DocStatus.Active_FEE_STRUCTURE;
                    }
                    return GetEditbtn(AccFeeStructureUpdateStatus(data["GuID"], UpdateDocumentStatus), "Fee Structure", "Edit");
                }
            },
            { "data": "Id",                 "title": "Id"            },
            { "data": "GuID",               "title": "GuID"          },

        ],
        columnDefs: [
            { visible: false, targets: [GroupColumn_P1,8,9] },
            { "orderable": false, targets: [0, 1, 2, 4, 5, 6,7,8,9] }
        ],
        order: [[GroupColumn_P1, 'asc']],
        displayLength: 10,
        drawCallback: function (settings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            var last = null;

            api.column(GroupColumn_P1, { page: 'current' })
                .data()
                .each(function (AdmissionFall, i) {
                    if (last !== AdmissionFall) {
                        var rowData = table.row(i).data();
                        var CampusName = rowData ? rowData["Campus"] : '';
                        $(rows)
                            .eq(i)
                            .before(
                                '<tr class="group"><td colspan="5" style="background-color:lightseagreen;color:white"><b>' +
                                'Branch :- ' + CampusName +' </b> For : '+ AdmissionFall +
                                '</td></tr>'
                            );

                        last = AdmissionFall;
                    }
                });
        }
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();/*for serial No*/
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
    debugger
    table.ajax.url((BasePath + "/AAccounts/MFeeUI/AccFeeStructure_ListByParam_FORDT?" + queryString)).load();
}

function AccFeeStructureUpdateStatus(GuID, UpdateDocumentStatus) {
    var FeeStructureGuID        =   GuID;
    var FeeStructureStatus = UpdateDocumentStatus;
    return
    var JsonArg = {
        GuID                : FeeStructureGuID,
        DocumentStatus      : FeeStructureStatus
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/AAccounts/MFeeUI/Update_Insert_AccFeeStructure",
        dataType: 'json',
        data: { 'PostedData': (JsonArg), },
        beforeSend: function () {
            startLoading();
        },
        success: function (data) {
            GetMessageBox(data.Message, data.Code);

        },
        complete: function () {
            stopLoading();
        },
        error: function (jqXHR, error, errorThrown) {
            GetMessageBox("The Transaction Can Not Be Performed Due To Serve Activity", 500);

        },
    });

}







