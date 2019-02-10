$(document).ready(function () {
    $('.button.add[data-toggle="popover"]').popover({ html: true, content: function () { return $('#popover-add').html(); } });
    $('.button.options[data-toggle="popover"]').popover({ html: true, content: function () { return $('#popover-options').html(); } });
    $('.button.more[data-toggle="popover"]').popover({ html: true, content: function () { return $('#popover-more').html(); } });
    $('.button.act[data-toggle="popover"]').popover('disable');

    // Bootstrap Popover
    $('[data-toggle="popover"]').popover();

    $('body').on('click', function (e) {
        var toggleAttribute = $(e.target).data('toggle');
        var hasLaunchPopoverclass = $(e.target).hasClass('launch-popover');

        if (toggleAttribute !== 'popover' && hasLaunchPopoverclass === false && $(e.target).parents('.popover.in').length === 0) {
            $('.popover.editable-popover').popover('destroy');
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