$(document).ready(function () {
    $("nav li a.custom").click(function (event) {
        event.preventDefault();

        var menuItemId = $(this).attr("data-item");

        showModalEditor("Edit Menu Item: " + $(this).attr("data-itemname"), "/Admin/MenuItem/Edit?menuItemId=" + menuItemId);
    });
});