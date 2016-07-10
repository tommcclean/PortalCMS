$(document).ready(function ()
{
    $(".add-component").click(function (event) {
        var sectionId = $(this).attr("data-sectionid");

        var targetContainer = $("#section-" + sectionId + " .component-container.selected:first").attr("id");

        if (targetContainer === undefined)
        {
            // GENERATE: Modal URL
            var href = "/Builder/Component/Add?pageSectionId=" + sectionId + "&elementId=section-" + sectionId;

            // OPEN: Modal Editor
            showModalEditor("Add Component", href);
        }
        else
        {
            // GENERATE: Modal URL
            var href = "/Builder/Component/Add?pageSectionId=" + sectionId + "&elementId=" + targetContainer;

            // OPEN: Modal Editor
            showModalEditor("Add Component", href);
        }
    });

    $(".admin .component-editor").click(function (event) {
        var elementId = $(this).attr("id");
        var elementParts = elementId.split('-');
        var sectionId = elementParts[elementParts.length - 1];

        var targetContainer = $("#section-" + sectionId + " .component-container.selected:first").attr("id");

        // GENERATE: Modal URL
        var href = "/Builder/Container/Edit?pageSectionId=" + sectionId + "&elementId=" + targetContainer;

        // OPEN: Modal Editor
        showModalEditor("Edit Container", href);
    });

$(".admin section p, .admin section h1, .admin section h2, .admin section h3, .admin section h4").click(function(event)
{
    // GET: Element ID
    var elementId = event.target.id;

    var elementParts = elementId.split('-');

    // GENERATE: Modal URL
    var href = "/Builder/Build/Element?sectionId=" + elementParts[elementParts.length - 1] + "&elementId=" + elementId;

    // OPEN: Modal Editor
    showModalEditor("Edit Element Content", href);
});

$(".admin section .image").click(function (event) {

    // GET: Element ID
    var elementId = event.target.id;

    var elementParts = elementId.split('-');

    // GENERATE: Modal URL
    var href = "/Builder/Image/Edit?pageSectionId=" + elementParts[elementParts.length - 1] + "&elementId=" + elementId;

    // OPEN: Modal Editor
    showModalEditor("Edit Element Content", href);
});

$(".admin section a").click(function (event) {

    // GET: Element ID
    var elementId = $(this).attr("id");
    var elementParts = elementId.split('-');
    var sectionId = elementParts[elementParts.length - 1]

    // GENERATE: Modal URL
    var href = "/Builder/Component/Anchor?pageSectionId=" + sectionId + "&elementId=" + elementId;

    // OPEN: Modal Editor
    showModalEditor("Edit Anchor", href);
});

$(".admin .component-container").click(function (event)
{
    var elementId = $(this).attr("id");
    var elementParts = elementId.split('-');
    var sectionId = elementParts[elementParts.length - 1];

    if ($(this).hasClass('selected'))
    {
        $(this).removeClass('selected');
        $('#container-editor-' + sectionId).fadeOut();
    }
    else
    {
        $('.component-container').removeClass('selected');
        $('.component-editor').fadeOut();
        $('#container-editor-' + sectionId).fadeIn();
        $(this).addClass('selected');
    }
}).children().click(function (e) {
    return false;
});;

$(".admin section").click(function (event)
{
    var elementId = $(this).attr("id");
    var elementParts = elementId.split('-');
    var sectionId = elementParts[elementParts.length - 1];
    $(this).find('.component-container').removeClass('selected');
    $('#container-editor-' + sectionId).fadeOut();
}).children().click(function (e) {
    return false;
});
});