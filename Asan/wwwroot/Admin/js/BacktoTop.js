////function scrollToTop() {
////    window.scrollTo(0, 500);
////    document.body.scrollIntoView({ behavior: 'auto', block: 'start' });
////}

window.onscroll = function () { scrollFunction() };
function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        document.getElementById("myBtn").style.display = "block";
    } else {
        document.getElementById("myBtn").style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}
//function scrollToTop() {
//    window.scrollBy(0, -10000);
//}
//function scrollToTopWithSpeed(duration) {
//    let cosParameter = window.scrollY / 6;
//    let scrollCount = 0;
//    let oldTimestamp = performance.now();

//    function step(newTimestamp) {
//        scrollCount += Math.PI / (duration / (newTimestamp - oldTimestamp));
//        if (scrollCount >= Math.PI) window.scrollTo(0, 500);
//        if (window.scrollY === 0) return;
//        window.scrollTo(0, Math.round(cosParameter + cosParameter * Math.cos(scrollCount)));
//        oldTimestamp = newTimestamp;
//        window.requestAnimationFrame(step);
//    }
//    window.requestAnimationFrame(step);
//}

//function scrollToTop() {
//    let scrollStep = -window.scrollY / (1 / 1000),
//        scrollInterval = setInterval(function () {
//            if (window.scrollY !== 0) {
//                window.scrollBy(0, scrollStep);
//            }
//            else clearInterval(scrollInterval);
//        }, 1000);
//}