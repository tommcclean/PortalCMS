$(document).ready(function () {
    // Bootstrap Popover
    $('[data-toggle="popover"]').popover();

    $('body').on('click', function (e) {
        if ($(e.target).data('toggle') !== 'popover'
            && $(e.target).parents('.popover.in').length === 0) {
            $('[data-toggle="popover"]').popover('hide');
        }
    });

    // Bootstrap Confirmation
    $('[data-toggle="confirmation"]').confirmation({
        href: function (elem) {
            return $(elem).attr('href');
        }
    });
});