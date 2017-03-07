$(document).ready(function () {
    $(".admin-wrapper .button").click(function (event) {
        var href = $(this).attr("href");

        if (href === undefined) {
            $('.admin-wrapper .button').popover('hide');
        }
    });
});

function TogglePanel(focusPanel) {
    $('.admin-wrapper .button').popover('hide');

    var isActive = $('#' + focusPanel).hasClass('visible');

    $('.panel-overlay').slideUp(300);
    $('.panel-overlay').removeClass('visible');

    if (isActive != true) {
        $('#' + focusPanel).removeClass('left');
        $('#' + focusPanel).slideDown(300);
        $('#' + focusPanel).addClass('visible');
    }
}

function ClosePanels() {
    $('.panel-overlay').slideUp(300);
    $('.panel-overlay').removeClass('visible');
}

function FloatPanel(elementId) {
    $('#' + elementId).toggleClass('left');
}