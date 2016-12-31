$(document).ready(function () {
    $(".pagination .pagination-button").click(function (event) {
        var page = $(this).attr("data-page");
        var control = $(this).attr("data-type");

        $(".pagination .pagination-button[data-type='" + control + "']").removeClass("active");
        $(this).addClass("active");

        $(".pagination-wrapper[data-type='" + control + "'] .pagination-page").hide();
        $('#page-' + page).show();
    });
});