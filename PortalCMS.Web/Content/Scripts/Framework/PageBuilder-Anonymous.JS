$(document).ready(function () {
    InitialiseWidgets();
});

function InitialiseWidgets() {
    $(".widget-wrapper").each(function () {
        var self = $(this);

        var areaName = $(self).attr("data-area");
        var controllerName = $(self).attr("data-controller");
        var actionName = $(self).attr("data-action");

        if (areaName !== undefined) {
            $(self).empty();
            $(self).append('<div class="spinner"></div>');

            $.get("/" + areaName + "/" + controllerName + "/" + actionName, function (data) {
                $(self).empty();
                $(self).append(data);
            });
        }
    });
}