$(document).ready(function () {
    $(".visitor .component-expand").click(function (event) {
        var componentBody = $(this).find('.component-body');

        if (componentBody.hasClass('active'))
        {
            componentBody.slideUp();

            componentBody.removeClass("active");
        }
        else
        {
            componentBody.slideDown();

            componentBody.addClass("active");
        }

        
    });
});