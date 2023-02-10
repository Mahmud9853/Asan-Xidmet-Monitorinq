//function search() {
//    var searchTerm = $("#searchTerm").val();
//    $.ajax({
//        type: "GET",
//        url: "/Admin/Legislation/Search",
//        data: { searchTerm: searchTerm },
//        success: function (data) {
//            $("#searchResults").html(data);
// 
//    }
//});

//$(document).on("click", "#searchTerm", function () {

//    if ($("#searchTerm").val().length > 0) {
//        $.ajax({
//            url: "/Admin/Legislation/Search",
//            type: "get",
//            data: {
//                "key": $("#searchTerm").val()
//            },
//            success: function (response) {
//                $("#searchResults").empty()
//                $("#searchResults").append(response)
//            }
//        });
//    } else {
//        $("#searchResults").empty()
//    }
//});

//$(document).ready(function () {
//    $("#searchTerm").keyup(function () {
//        var searchTerm = $("#searchTerm").val();
//        $.ajax({
//            url: "/Admin/Search",
//            type: "GET",
//            data: { searchTerm: searchTerm },
//            success: function (result) {
//                $("#searchResult").html(result);
//            }
//        });
//    });
//});

//$(document).ready(function () {
//    $("#searchBtn").click(function () {
//        var searchTerm = $("#searchTerm").val();

//        $.ajax({
//            url: "/Admin/Search",
//            data: { searchTerm: searchTerm },
//            type: "GET",
//            success: function (result) {
//                $("#searchResult").html(result);
//            }
//        });
//    });
//});

$(document).ready(function () {
    $('#searchForm').submit(function (e) {
        e.preventDefault();
        var searchTerm = $('input[name="SearchTerm"]').val();

        $.ajax({
            type: "POST",
            url: "/Admin/Dashboard/Search",
            data: { searchTerm: searchTerm },
            success: function (result) {
                $('#searchResult').show(result);
            }
        });
    });
});
