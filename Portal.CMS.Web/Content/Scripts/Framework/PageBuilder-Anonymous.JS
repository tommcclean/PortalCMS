$(document).ready(function () {
    InitialiseWidgets();
});

function InitialiseWidgets() {
    $(".widget-wrapper").each(function () {
        var self = $(this);

        $(self).empty();
        $(self).append('<div class="spinner"></div>');

        var areaName = $(self).attr("data-area");
        var controllerName = $(self).attr("data-controller");
        var actionName = $(self).attr("data-action");

        if (areaName !== undefined) {
            $.get("/" + areaName + "/" + controllerName + "/" + actionName, function (data) {
                $(self).empty();
                $(self).append(data);
            });
        }
    });
}