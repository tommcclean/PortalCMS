$(document).ready(function () {
    $(".visitor .component-expand .component-body").first().addClass("active").slideDown();

    $(".visitor .component-expand").click(function (event) {
        var componentBody = $(this).find('.component-body');

        if (componentBody.hasClass('active'))
        {
            componentBody.slideUp(300);

            componentBody.removeClass("active");
        }
        else
        {
            $('.visitor .component-expand .component-body.active').removeClass("active").slideUp();

            componentBody.slideDown(300);

            componentBody.addClass("active");
        }

    });
});