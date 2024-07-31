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
    var GroupColumn_P1 = 3;
    var GroupColumn_P2 = 3;
    
    table = $('#MainTableFeeStructure').DataTable({
        "responsive": true,
        "ordering": true,
        "processing": true,
        "columns": [
            { "data": null,                 "title": "#"             }, 
            { "data": "Campus",             "title": "Campus"        },
            { "data": "Code",               "title": "Code"          },
            { "data": "Session",            "title": "Session",      },
            { "data": "Class",              "title": "Class"         },
            { "data": "WHTaxPolicy",        "title": "WH Tax Policy" },
            { "data": "TotalFeeExclusive",  "title": "Fee (Excl.)"   },
            { "data": "WHTAmount",          "title": "Tax"           },
            { "data": "TotalFee",           "title": "TotalFee"      },
        ],
        columnDefs: [
            { visible: false, targets: GroupColumn_P1 },
            { "orderable": false, targets: [0, 1, 2, 4, 5, 6,7] }
        ],
        order: [[GroupColumn_P1, 'asc']],
        displayLength: 10,
        drawCallback: function (settings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            var last = null;

            api.column(GroupColumn_P1, { page: 'current' })
                .data()
                .each(function (group, i) {
                    if (last !== group) {
                        $(rows)
                            .eq(i)
                            .before(
                                '<tr class="group"><td colspan="5" style="background-color:lightseagreen;color:white">' +
                                group +
                                '</td></tr>'
                            );

                        last = group;
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







