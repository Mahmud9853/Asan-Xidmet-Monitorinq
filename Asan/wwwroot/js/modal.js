$(function () {
    $("#CustomerGrid .details").click(function () {
        var customerId = $(this).data("id");
        console.log(customerId)
        $.ajax({
            type: "GET", 
            url: "https://localhost:44325/Home/Detail/" + customerId,
           
            success: function (response) {
                $("#partialModal").find(".modal-body").html(response);
                $("#partialModal").modal('show');
           
            },
        });
    });
    $("#close").click(function () {
        $("#partialModal").modal('hide');
    });
            console.log()
});