$(function () {
    $(".header_nav>ul>li>a").on("click",function () {
        $(this).addClass("nav_current").parent("li").siblings("li").children("a").removeClass("nav_current");
    })

    $(".header_nav>ul>li").hover(function () {
        $(this).children("ul").toggle();
    })

    $(".header>.header_nav>ul>li>ul>li").hover(function () {
        $(this).children("ul").toggle();
    })


})
