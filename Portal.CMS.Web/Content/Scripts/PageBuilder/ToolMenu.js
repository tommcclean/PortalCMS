$(document).ready(function () {
    $(".admin-wrapper .button").click(function (event) {
        $('.admin-wrapper .button').popover('hide');
    });
});

function TogglePanel(focusPanel) {
    $('.admin-wrapper .button').popover('hide');

    var isActive = $('#' + focusPanel).hasClass('visible');

    $('.panel-overlay').slideUp(300);
    $('.panel-overlay').removeClass('visible');

    if (isActive != true) {
        $('#' + focusPanel).slideDown(300);
        $('#' + focusPanel).addClass('visible');
    }
}

function ClosePanels() {
    $('.panel-overlay').slideUp(300);
    $('.panel-overlay').removeClass('visible');
}