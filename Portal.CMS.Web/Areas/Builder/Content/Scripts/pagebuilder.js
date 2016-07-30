$(document).ready(function () {
    $(".add-component").click(function (event) {
        var sectionId = $(this).attr("data-sectionid");

        var targetContainer = $("#section-" + sectionId + " .component-container.selected:first").attr("id");

        if (targetContainer === undefined) {
            var href = "/Builder/Component/Add?pageSectionId=" + sectionId + "&elementId=section-" + sectionId;
            showModalEditor("Add Component", href);
        }
        else {
            var globalhref = "/Builder/Component/Add?pageSectionId=" + sectionId + "&elementId=" + targetContainer;
            showModalEditor("Add Component", globalhref);
        }
    });

    $(".admin section div.image").click(function (event) {
        var elementId = event.target.id;
        var sectionId = ExtractSectionId($(this));

        var href = "/Builder/Component/Image?pageSectionId=" + sectionId + "&elementId=" + elementId + "&elementType=div";
        showModalEditor("Edit Image Properties", href);
    });

    $(".admin section img.image").click(function (event) {
        var elementId = event.target.id;
        var sectionId = ExtractSectionId($(this));

        var href = "/Builder/Component/Image?pageSectionId=" + sectionId + "&elementId=" + elementId + "&elementType=img";
        showModalEditor("Edit Image Properties", href);
    });

    $(".admin section").click(function (event) {
        var elementId = $(this).attr("id");
        var sectionId = ExtractSectionId($(this));
        $(this).find('.component-container').removeClass('selected');
        $('#container-editor-' + sectionId).fadeOut();
    }).children().click(function (e) {
        return false;
    });

    $(".admin .component-editor").click(function (event) {
        var elementId = $(this).attr("id");
        var sectionId = ExtractSectionId($(this));

        var targetContainer = $("#section-" + sectionId + " .component-container.selected:first").attr("id");

        $('#' + targetContainer).remove();

        var dataParams = { "pageSectionId": sectionId, "elementId": targetContainer };
        $.ajax({
            data: dataParams,
            type: 'POST',
            cache: false,
            url: '/Builder/Component/Delete',
            success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
        });
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
    });

    // Edit Text Elements
    tinymce.init({
        selector: '.admin section p, .admin section h1, .admin section h2, .admin section h3, .admin section h4, .admin section code',
        menubar: false,
        inline: true,
        plugins: ['advlist textcolor colorpicker link'],
        toolbar: 'undo redo | bold italic underline | link | forecolor backcolor | delete',
        setup: function (ed) {
            ed.addButton('delete', {
                text: 'Delete',
                icon: false,
                onclick: function () {
                    var elementId = tinyMCE.activeEditor.id;
                    var elementParts = elementId.split('-');
                    var sectionId = elementParts[elementParts.length - 1];
                    var dataParams = { "pageSectionId": sectionId, "elementId": elementId };
                    $('#' + elementId).remove();
                    $.ajax({
                        data: dataParams,
                        type: 'POST',
                        cache: false,
                        url: '/Builder/Component/Delete',
                        success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
                    });
                }
            }),
            ed.on('change', function (e) {
                var elementId = tinyMCE.activeEditor.id;
                var elementParts = elementId.split('-');
                var sectionId = elementParts[elementParts.length - 1];

                var dataParams = { "pageSectionId": sectionId, "elementId": elementId, "elementHtml": ed.getContent() };
                $.ajax({
                    data: dataParams,
                    type: 'POST',
                    cache: false,
                    url: '/Builder/Component/Edit',
                    success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
                });
            });
        }
    });

    // Edit Buttons and Links
    tinymce.init({
        selector: '.admin section a, .admin section .btn',
        menubar: false,
        inline: true,
        plugins: [
          'advlist textcolor colorpicker link'
        ],
        toolbar: 'undo redo | bold italic underline | link | forecolor backcolor | delete',
        setup: function (ed) {
            ed.addButton('delete', {
                text: 'Delete',
                icon: false,
                onclick: function () {
                    var elementId = tinyMCE.activeEditor.id;
                    var elementParts = elementId.split('-');
                    var sectionId = elementParts[elementParts.length - 1];
                    var dataParams = { "pageSectionId": sectionId, "elementId": elementId };
                    $('#' + elementId).remove();
                    $.ajax({
                        data: dataParams,
                        type: 'POST',
                        cache: false,
                        url: '/Builder/Component/Delete',
                        success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
                    });
                }
            }),
            ed.on('change', function (e) {
                var elementId = tinyMCE.activeEditor.id;
                var elementParts = elementId.split('-');
                var sectionId = elementParts[elementParts.length - 1];
                var href = $('#' + elementId).attr("href");
                var target = $('#' + elementId).attr("target");

                var dataParams = { "pageSectionId": sectionId, "elementId": elementId, "elementHtml": ed.getContent(), "elementHref": href, "elementTarget": target };
                $.ajax({
                    data: dataParams,
                    type: 'POST',
                    cache: false,
                    url: '/Builder/Component/Link',
                    success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
                });
            });
        }
    });
});

function ExtractSectionId(element) {
    var elementId = $(element).attr("id");
    var elementParts = elementId.split('-');
    var sectionId = elementParts[elementParts.length - 1];
    return sectionId;
}

function ChangeOrder() {
    $('#page-wrapper').toggleClass("zoom");
    $('#page-wrapper').toggleClass("change-order");
    $('#page-wrapper.change-order').sortable({ placeholder: "ui-state-highlight", helper: 'clone' });
    $('.action-container.global').fadeOut();
    $('.action-container.section-order').fadeIn();
}

function SaveOrder() {
    var sectionList = [];
    var orderId = 1;
    $("#page-wrapper .sortable").each(function (index) {
        var sectionId = $(this).attr("data-section");
        sectionList.push(orderId + "-" + sectionId);
        orderId += 1;
    });
    $('#order-list').val(sectionList);
    $('#order-submit').click();
}