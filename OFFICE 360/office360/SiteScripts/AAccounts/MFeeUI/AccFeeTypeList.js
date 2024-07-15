var table = "";

$(document).ready(function () {
    PopulateDropDownLists();
    InitDateTable();
});


function PopulateDropDownLists() {
    PopulateLKSearchParameterList();
}
//-----------ALL DATA TABLE LIST
function InitDateTable() {
    var GroupColumn_P1 = 2;
    var GroupColumn_P2 = 3;
    var JsonArg = {
        ActionCondition: PARAMETER.SESCondition.GET_MT_ACCFEESTRUCTURE_BYPARAMETER,
        ListCondition: PARAMETER.SPListCondition.STRUCTUREFEETYPE_LIST,
    }
    table = $('#MainTableFeeType').DataTable({
        "responsive": true,
        "ordering": true,
        "processing": true,
        "ajax": {
            "url": BasePath + "/AAccounts/MFeeUI/PopulateStructureFeeType_ListByParam_FORDT",
            "type": "POST",
            "data": { "PostedData": JsonArg },
            beforeSend: function () {
                startLoading();
            },
            complete: function () {
                stopLoading();
            },
        },
        "columns": [
            { "data": null,                 "title": "#"                    },
            { "data": "FeeName",            "title": "Fee"                  },
            { "data": "FeeCatagory",        "title": "Catagory"             },
            { "data": "ChargingMethod",     "title": "Method",                  "orderable": true,  },
            { "data": "IsOnAdmission",      "title": "On Admission"         },
            { "data": "IsSecurity",         "title": "Is Security"          },
            { "data": "IsRefundable",       "title": "Is Refundable"        },
            { "data": "IsDiscount",         "title": "Is Discountable"      },
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
//-----------ALL DROPDOWN LIST
function PopulateLKSearchParameterList() {
    var JsonArg = {
        DocType: PARAMETER.DocumentType.BRANCHES,
        ActionCondition: PARAMETER.LookUpCondition.GET_LK1_SEARCHPARAMETER_BYPARAMTER,
    }
    $.ajax({
        type: "POST",
        url: BasePath + "/ACompany/MBranchUI/GET_DATA_BY_PARAMETER",
        data: { 'PostedData': (JsonArg) },
        success: function (data) {
            var s = '<option  value="-1">Select an option</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<center><option  value="' + data[i].Id + '">' + data[i].Description + '' + '</option>';
            }
            $("#DropDownListSearchBy").html(s);
        },
    });
}





