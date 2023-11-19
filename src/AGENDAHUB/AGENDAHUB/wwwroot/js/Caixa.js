$(document).ready(function () {
    var table = new DataTable('#tabelaCaixa', {
        "language": {
            "url": "/lib/dataTable_PT_BR.json"
        },
        columnDefs: [
            {
                targets: 0,
                data: null,
                defaultContent: '',
                orderable: false,
                className: 'select-checkbox'
            }
        ],
        select: {
            info: false,
            style: 'multi',
            selector: 'td:first-child'
        },
        order: [[1, 'asc']],
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copy',
                text: 'Copiar Selecionados'
            },
            {
                extend: 'selectNone',
                text: 'Desmarcar Selecionados'
            },
            'csv', 'excel', 'pdf', 'print'
        ],
    });
});
