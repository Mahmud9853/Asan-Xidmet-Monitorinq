////let skip = 6;
////let showCount = $("#Showscount").val();
////$(document).on("click", "#loadmorebtn", function () {
////    $.ajax({
////        url: "/Contract/ShowLoad",
////        type: "get",
////        data: {
////            "skip": skip
////        },
////        success: function (response) {
////            $("#myList").append(response);
////            skip += 6;
////            if (skip >= showCount) {
////                $("#loadmorebtn").remove()

////            }
////        }
////    });
////});

//const toggle = document.getElementById('toggleDark');
//const body = document.querySelector('.active');
///*var line = document.getElementsByClassName("product-item ");*/
///*const line = document.querySelector('.news');*/
//toggle.addEventListener('click', function () {
//    this.classList.toggle('bi-moon');
//    if (this.classList.toggle('bi-brightness-high-fill')) {
//        body.style.background = 'white';
//        body.style.color = 'black';
//        body.style.transition = '2s';
//        /*  document.getElementsByClassName("product-item ").style.border = 'none';*/

//    } else {
//        /*    line.style.border = '1px solid yellow ';*/
//        body.style.background = 'black';
//        body.style.color = 'yellow';
//        body.style.transition = '2s';
//    }
//});

//$(document).ready(function () {
//    $("#showMoreButton").click(function () {
//        $.ajax({
//            url: "/Home/ShowLoad",
//            type: "GET",
//            success: function (data) {
//                $("#additionalDataContainer").html(data);
//            }
//        });
//    });
//});

//$(document).ready(function () {
//    var skip = 0;
//    var take = 4;
//    var showMoreButton = $("#showMoreButton");
//    var showLessButton = $("#showLessButton");
//    var productsContainer = $("#productsContainer");

//    showMoreButton.click(function () {
//        skip += take;
//        $.ajax({
//            url: '/Home/ShowLoad',
//            type: 'GET',
//            data: { skip: skip, take: take },
//            success: function (result) {
//                productsContainer.append(result);
//                showLessButton.show();
//                showMoreButton.remove();
//            }
//        });
//    });
//    showLessButton.click(function () {
//        skip -= take;
//        $.ajax({
//            url: '/Home/ShowLoad',
//            type: 'GET',
//            data: { skip: skip, take: take },
//            success: function (result) {
//                productsContainer.append(result);
//                showLessButton.remove();
//                showMoreButton.show();
//            }
//        });
//    });
//});