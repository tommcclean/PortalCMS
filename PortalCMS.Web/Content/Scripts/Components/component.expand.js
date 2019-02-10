$(document).ready(function () {
    $(".visitor .component-expand").click(function (event) {
        var componentBody = $(this).find('.component-body');

        if ($(this).hasClass('active')) {
            componentBody.slideUp(300);

            $(this).removeClass("active");
        }
        else {
            $('.visitor .component-expand.active .component-body').slideUp();
            $('.visitor .component-expand').removeClass("active");

            componentBody.slideDown(300);

            $(this).addClass("active");
        }
    });
});