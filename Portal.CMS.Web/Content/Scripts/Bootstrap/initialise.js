$(document).ready(function () {
    $('.button.add[data-toggle="popover"]').popover({ html: true, content: function () { return $('#popover-add').html(); } });
    $('.button.options[data-toggle="popover"]').popover({ html: true, content: function () { return $('#popover-options').html(); } });
    $('.button.more[data-toggle="popover"]').popover({ html: true, content: function () { return $('#popover-more').html(); } });
    $('.button.act[data-toggle="popover"]').popover('disable');

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

    $('body').on('hidden.bs.popover', function (e) {
        $(e.target).data("bs.popover").inState.click = false;
    });
});