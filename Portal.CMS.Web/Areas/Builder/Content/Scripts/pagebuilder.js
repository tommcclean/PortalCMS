$(document).ready(function () {
    $(".add-component").click(function (event) {
        var sectionId = $(this).attr("data-sectionid");

        var targetContainer = $("#section-" + sectionId + " .component-container.selected:first").attr("id");

        if (targetContainer === undefined) {
            var href = "/Builder/Component/Add?pageSectionId=" + sectionId + "&elementId=section-" + sectionId;
            showModalEditor("Add Component", href);
        }
        else {
            var href = "/Builder/Component/Add?pageSectionId=" + sectionId + "&elementId=" + targetContainer;
            showModalEditor("Add Component", href);
        }
    });

    $(".admin .component-editor").click(function (event) {
        var elementId = $(this).attr("id");
        var sectionId = ExtractSectionId($(this));

        var targetContainer = $("#section-" + sectionId + " .component-container.selected:first").attr("id");

        var href = "/Builder/Container/Edit?pageSectionId=" + sectionId + "&elementId=" + targetContainer;
        showModalEditor("Edit Container Properties", href);
    });

    $(".admin section a").click(function (event) {
        var elementId = $(this).attr("id");
        var sectionId = ExtractSectionId($(this));

        var href = "/Builder/Component/Anchor?pageSectionId=" + sectionId + "&elementId=" + elementId;
        showModalEditor("Edit Anchor Properties", href);
    });

    $(".admin section p, .admin section h1, .admin section h2, .admin section h3, .admin section h4").click(function (event) {
        var elementId = event.target.id;
        var sectionId = ExtractSectionId($(this));

        var href = "/Builder/Component/Element?sectionId=" + sectionId + "&elementId=" + elementId;
        showModalEditor("Edit Element Properties", href);
    });

    $(".admin section .image").click(function (event) {
        var elementId = event.target.id;
        var sectionId = ExtractSectionId($(this));

        var href = "/Builder/Image/Edit?pageSectionId=" + sectionId + "&elementId=" + elementId;
        showModalEditor("Edit Image Properties", href);
    });

    $(".admin .component-container").click(function (event) {
        var elementId = $(this).attr("id");
        var sectionId = ExtractSectionId($(this));

        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            $('#container-editor-' + sectionId).fadeOut();
        }
        else {
            $('.component-container').removeClass('selected');
            $('.component-editor').fadeOut(200);
            $('#container-editor-' + sectionId).fadeIn(200);
            $(this).addClass('selected');
        }
    }).children().click(function (e) {
        return false;
    });;

    $(".admin section").click(function (event) {
        var elementId = $(this).attr("id");
        var sectionId = ExtractSectionId($(this));
        $(this).find('.component-container').removeClass('selected');
        $('#container-editor-' + sectionId).fadeOut();
    }).children().click(function (e) {
        return false;
    });
});

function ExtractSectionId(element) {
    var elementId = $(element).attr("id");
    var elementParts = elementId.split('-');
    var sectionId = elementParts[elementParts.length - 1];
    return sectionId;
}