$(document).ready(function () {
    $(".image-selector.multiple img.thumbnail").click(function () {
        $(this).toggleClass("selected");

        var parentControl = $(this).attr("data-parent");
        var targetControl = $(this).attr("data-target");

        OutputImageSelections(parentControl, targetControl);
    });

    $(".image-selector.single img.thumbnail").click(function () {
        $(".image-selector.single img.thumbnail").removeClass("selected");
        $(this).toggleClass("selected");

        var parentControl = $(this).attr("data-parent");
        var targetControl = $(this).attr("data-target");

        OutputImageSelections(parentControl, targetControl);
    });
});

function OutputImageSelections(imageSelector, outputTextbox) {
    var selectedItemList = [];

    $('.' + imageSelector + ' img.thumbnail.selected').each(function (i, obj) {
        var identifier = $(this).attr("data-identifier");

        selectedItemList.push(identifier);
    });

    $('#' + outputTextbox).val(selectedItemList);
}