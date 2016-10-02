$("a.admin-item, .action").click(function (event) {
    var fontAwesomeIcon = $(this).find('.fa')
    fontAwesomeIcon.addClass('fa-spin');

    setTimeout(function () {
        fontAwesomeIcon.removeClass('fa-spin');
    }, 2000);
});

function ToggleThemeManager() {
    if ($('#component-panel').hasClass('visible')) {
        $('#component-panel').slideUp(300);
        $('#component-panel').toggleClass('visible');
    }

    if ($('#pages-panel').hasClass('visible')) {
        $('#pages-panel').slideUp(300);
        $('#pages-panel').toggleClass('visible');
    }

    if ($('#section-panel').hasClass('visible')) {
        $('#section-panel').slideUp(300);
        $('#section-panel').toggleClass('visible');
    }

    if ($('#theme-manager-panel').hasClass('visible')) {
        $('#theme-manager-panel').slideUp(300);
        $('#theme-manager-panel').toggleClass('visible');
    }
    else {
        $('#theme-manager-panel').slideDown(300);
        $('#theme-manager-panel').toggleClass('visible');
    }
}

function TogglePageList() {
    if ($('#section-panel').hasClass('visible')) {
        $('#section-panel').slideUp(300);
        $('#section-panel').toggleClass('visible');
    }

    if ($('#theme-manager-panel').hasClass('visible')) {
        $('#theme-manager-panel').slideUp(300);
        $('#theme-manager-panel').toggleClass('visible');
    }

    if ($('#component-panel').hasClass('visible')) {
        $('#component-panel').slideUp(300);
        $('#component-panel').toggleClass('visible');
    }

    if ($('#pages-panel').hasClass('visible')) {
        $('#pages-panel').slideUp(300);
        $('#pages-panel').toggleClass('visible');
    }
    else {
        $('#pages-panel').slideDown(300);
        $('#pages-panel').toggleClass('visible');
    }
}