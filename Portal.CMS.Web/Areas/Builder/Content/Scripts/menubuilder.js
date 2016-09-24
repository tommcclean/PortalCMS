$(document).ready(function () {
    $("nav li a.custom").click(function (event) {
        event.preventDefault();

        var menuItemId = $(this).attr("data-item");

        showModalEditor("Edit Menu Item", "/Admin/MenuItem/Edit?menuItemId=" + menuItemId);
    });
});