$(document).ready(function () {

    // Bootstrap Popover
    $('[data-toggle="popover"]').popover();

    $('body').on('click', function (e) {
        if ($(e.target).data('toggle') !== 'popover'
            && $(e.target).parents('.popover.in').length === 0) {
            $('[data-toggle="popover"]').popover('hide');
        }
    });

    // FancyBox Image Viewer
    $('.fancybox').fancybox();
    $(".fancybox-thumb").fancybox({
        prevEffect: 'none',
        nextEffect: 'none',
        helpers: {
            title: {
                type: 'outside'
            },
            thumbs: {
                width: 50,
                height: 50
            }
        }
    });

    // Bootstrap Confirmation
    $('[data-toggle="confirmation"]').confirmation({
        href: function (elem) {
            return $(elem).attr('href');
        }
    });
});