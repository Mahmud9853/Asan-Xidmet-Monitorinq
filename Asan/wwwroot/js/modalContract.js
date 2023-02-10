$(function () {
    $("#CustomerGrid .details").click(function () {
        var Id = $(this).data("id");
        console.log(customerId)
        $.ajax({
            type: "GET",
            url: "https://localhost:44325/Contract/Detail/" + Id,
            success: function (response) {
                $("#partialModal").find(".modal-body").html(response);
                $("#partialModal").modal('show');
               
            },

        });
    });
    $("#close").click(function () {
        $("#partialModal").modal('hide');
    });
});

//$("#CustomerGrid .detail").click(function () {
//    $.ajax({
//        url: '' + $(this).data("id"),
//        type: 'GET',
//        success: function (data) {
//            $("#partialModal .modal-body").html(data);
//            $("#partialModal").modal("show");
//        }
//    });
//});