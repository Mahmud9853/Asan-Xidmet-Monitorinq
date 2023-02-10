$(document).ready(function () {
    $('#example').dataTable({
        dom: 'B',
        buttons: [
            //{extend: 'copy', className: 'btn  btn-square btn - sm - square btn - lg - square ' },
            //{ extend: 'csv', className: 'btn btn-square btn - sm - square btn - lg - square'},
            //{ extend: 'excel', className: 'btn btn-square btn - sm - square btn - lg - square' },
            //{ extend: 'pdf', className: 'btn btn-square btn - sm - square btn - lg - square'},
            { extend: 'print', className: 'btn btn-square btn - sm - square btn - lg - square'}
        ]

    });

});