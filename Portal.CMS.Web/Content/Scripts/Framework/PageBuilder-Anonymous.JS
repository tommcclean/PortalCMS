$(document).ready(function () {
    InitialiseWidgets();
});

function InitialiseWidgets() {
    if ($('.post-list-wrapper').length) {
        $.get("/PageBuilder/Widget/RecentPostList", function (data) {
            $(".post-list-wrapper").html(data);
        });
    }
}