$("a.admin-item, .action").click(function (event) {
    var fontAwesomeIcon = $(this).find('.fa')
    fontAwesomeIcon.addClass('fa-spin');

    setTimeout(function () {
        fontAwesomeIcon.removeClass('fa-spin');
    }, 2000);
});