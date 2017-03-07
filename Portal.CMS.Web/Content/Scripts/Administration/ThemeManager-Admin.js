$(function () {
    $(".jquery-slider-control").slider({
        range: "min",
        value: 0,
        min: 10,
        max: 60,
        slide: function (event, ui) {
            var parentControl = $(this).attr("data-parent");
            $("#" + parentControl).val(ui.value);
        }
    });

    $(".jquery-slider-value").on('input', function () {
        var sliderControl = $(this).attr("data-slider");
        $("#" + sliderControl).slider({
            value: $(this).val(),
            min: 10,
            max: 60
        });
    });

    $(".jquery-slider-control").each(function (index) {
        var parentControl = $(this).attr("data-parent");

        $(this).slider({
            value: $("#" + parentControl).val(),
            min: 10,
            max: 60
        });
    });
});