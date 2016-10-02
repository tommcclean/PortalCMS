$(document).ready(function () {
    InitialiseWidgets();

    if ($('#page-wrapper.admin').length) {
        InitialiseEditor();
        InitialiseWidgets();

        $(".admin .component-editor").click(function (event) {
            var elementId = $(this).attr("id");
            var sectionId = ExtractSectionId($(this));

            var targetContainer = $("#section-" + sectionId + " .component-container.selected:first").attr("id");

            if (targetContainer === undefined) {
                targetContainer = $("#section-" + sectionId + " .widget-wrapper.selected:first").attr("id");
            }

            $('#' + targetContainer).remove();
            $('.component-editor').fadeOut(200);

            var dataParams = { "pageSectionId": sectionId, "elementId": targetContainer };
            $.ajax({
                data: dataParams, type: 'POST', cache: false, url: '/Builder/Component/Delete',
                success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
            });
        });

        var tour = new Tour({
            container: 'body',
            backdrop: true,
            template: "<div class='popover tour'><div class='arrow'></div><h3 class='popover-title'></h3><div class='popover-content'></div><div class='popover-navigation'><button class='btn btn-default' data-role='prev'>«</button><button class='btn btn-default' data-role='next'>»</button><button class='btn btn-default' data-role='end'>Exit</button></div></div>",
            steps:
            [
                {
                    element: "#tour-add-section",
                    placement: "top",
                    title: "Add a Section",
                    content: "<p>Pages are made up of Sections.</p><p>Add a Section to add new content to your page.</p><p>You can choose a templated Section, or a blank one if you want to add your own components.</p>"
                },
                {
                    element: "#tour-add-component",
                    placement: "top",
                    title: "Add a Component",
                    content: "<p>Components are elements you can add to a Section; like text, buttons and images.</p><p>Drag and drop your selected components to where you want them, then click or tap it to change it right away.</p>"
                },
                {
                    element: "#tour-page-manager",
                    placement: "top",
                    title: "Page Manager",
                    content: "<p>Open the Page Manager to create a new Page</p><p>You can also use this to quickly navigate to your other Pages</p>"
                },
                {
                    element: "#tour-theme-manager",
                    placement: "top",
                    title: "Theme Manager",
                    content: "<p>The Theme Manager allows you to make changes to the look and feel of your website</p><p>Edit your theme to change things like fonts, colours and text sizes</p>"
                },
                {
                    element: "#tour-edit-section",
                    placement: "bottom",
                    title: "Edit Section",
                    content: "<p>Every Section has a button allowing you to modify it.</p><p>You can change things like the background image or the size of a section or you can delete a section entirely.</p>"
                },
                {
                    element: "#tour-edit-element",
                    placement: "bottom",
                    title: "Edit Components",
                    content: "<p>Simply click or tap on any component in order to change it. You can change all components.</p>"
                },
            ]
        });

        tour.init();

        tour.start();
    }

    $('.edit-section').first().attr('id', 'tour-edit-section');
    $('#page-wrapper h1').first().attr('id', 'tour-edit-element');
});

function InitialiseWidgets() {
    if ($('.post-list-wrapper').length) {
        $.get("/Builder/Widget/RecentPostList", function (data) {
            $(".post-list-wrapper").html(data);
        });
    }
}

function InitialiseEditor() {
    $('.admin section').unbind();
    $('.admin .component-container').unbind();
    $('.admin .widget-wrapper').unbind();

    $(".admin section").click(function (event) {
        var elementId = $(this).attr("id");
        var sectionId = ExtractSectionId($(this));

        $(this).find('.component-container').removeClass('selected');
        $(this).find('.widget-wrapper').removeClass('selected');

        $('#container-editor-' + sectionId).fadeOut();
    }).children().click(function (e) {
        return false;
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
            $('.widget-wrapper').removeClass('selected');
            $('.component-editor').fadeOut(200);
            $('#container-editor-' + sectionId).fadeIn(200);
            $(this).addClass('selected');
        }
    }).children().click(function (e) {
        return false;
    });

    $(".admin .widget-wrapper").click(function (event) {
        var elementId = $(this).attr("id");
        var sectionId = ExtractSectionId($(this));

        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            $('#container-editor-' + sectionId).fadeOut();
        }
        else {
            $('.component-container').removeClass('selected');
            $('.widget-wrapper').removeClass('selected');
            $('.component-editor').fadeOut(200);
            $('#container-editor-' + sectionId).fadeIn(200);
            $(this).addClass('selected');
        }
    }).children().click(function (e) {
        return false;
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

    tinymce.init({
        selector: '.admin section p, .admin section h1, .admin section h2, .admin section h3, .admin section h4, .admin section code',
        menubar: false, inline: true,
        plugins: ['advlist textcolor colorpicker link'],
        toolbar: 'bold italic underline | link | forecolor backcolor | delete',
        setup: function (ed) {
            ed.addButton('delete', { icon: 'trash', onclick: function () { DeleteInlineComponent(tinyMCE.activeEditor.id); } }),
            ed.on('change', function (e) { EditInlineText(tinyMCE.activeEditor.id, ed.getContent()); });
        }
    });

    tinymce.init({
        selector: '.admin section a, .admin section .btn',
        menubar: false, inline: true,
        plugins: ['advlist textcolor colorpicker link'],
        toolbar: 'bold italic underline | link | forecolor backcolor | delete',
        setup: function (ed) {
            ed.addButton('delete', { icon: 'trash', onclick: function () { DeleteInlineComponent(tinyMCE.activeEditor.id); } }),
            ed.on('change', function (e) { EditInlineAnchor(tinyMCE.activeEditor.id, ed.getContent()); });
        }
    });

    tinymce.init({
        selector: '.admin section .freestyle',
        menubar: true, inline: true,
        plugins: ['advlist autolink lists link image charmap print preview anchor searchreplace visualblocks code fullscreen insertdatetime media table contextmenu paste textcolor colorpicker'],
        toolbar: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | forecolor backcolor | bullist numlist outdent indent | link image | delete',
        setup: function (ed) {
            ed.addButton('delete', { icon: 'trash', onclick: function () { DeleteInlineComponent(tinyMCE.activeEditor.id); } }),
            ed.on('change', function (e) { EditInlineFreestyle(tinyMCE.activeEditor.id, ed.getContent()); });
        }
    });
}

function InitialiseContainer(elementId) {
    $("#" + elementId).droppable({
        tolerance: "intersect", activeClass: "ui-state-default", hoverClass: "ui-state-hover", greedy: "true", drop: function (event, ui) { DropComponent(this, event, ui); }
    });
}

function InitialiseDroppables() {
    $("section").droppable({
        accept: function () { return PreventAppDrawerDrop(); },
        tolerance: "pointer",
        activeClass: "ui-state-default",
        hoverClass: "ui-state-hover",
        drop: function (event, ui) { DropComponent(this, event, ui); }
    });
    $(".component-container").droppable({
        accept: function () { return PreventAppDrawerDrop(); },
        tolerance: "pointer",
        activeClass: "ui-state-default",
        hoverClass: "ui-state-hover",
        greedy: "true",
        drop: function (event, ui) { DropComponent(this, event, ui); }
    });
}

function ExtractSectionId(element) {
    var elementId = $(element).attr("id");
    var elementParts = elementId.split('-');
    var sectionId = elementParts[elementParts.length - 1];
    return sectionId;
}

function EditInlineText(editorId, editorContent) {
    var elementId = editorId;
    var sectionId = ExtractSectionId($('#' + editorId));

    editorContent = RemoveTinyMCEAttributes(editorContent);

    var dataParams = { "pageSectionId": sectionId, "elementId": elementId, "elementHtml": editorContent };
    $.ajax({
        data: dataParams,
        type: 'POST',
        cache: false,
        url: '/Builder/Component/Edit',
        success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
    });
}

function RemoveTinyMCEAttributes(htmlContent) {
    htmlContent = htmlContent.replace('mce-content-body', '');
    htmlContent = htmlContent.replace('position: relative;', '');
    htmlContent = htmlContent.replace('contenteditable="true" ', '');

    return htmlContent;
}

function EditInlineFreestyle(editorId, editorContent) {
    var elementId = editorId;
    var sectionId = ExtractSectionId($('#' + editorId));

    var dataParams = { "pageSectionId": sectionId, "elementId": elementId, "elementHtml": editorContent };
    $.ajax({
        data: dataParams,
        type: 'POST',
        cache: false,
        url: '/Builder/Component/Freestyle',
        success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
    });
}

function EditInlineAnchor(editorId, editorContent) {
    var elementId = editorId;
    var sectionId = ExtractSectionId($('#' + editorId));

    var href = $('#' + elementId).attr("href");
    var target = $('#' + elementId).attr("target");

    var dataParams = { "pageSectionId": sectionId, "elementId": elementId, "elementHtml": editorContent, "elementHref": href, "elementTarget": target };
    $.ajax({
        data: dataParams,
        type: 'POST',
        cache: false,
        url: '/Builder/Component/Link',
        success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
    });
}

function DeleteInlineComponent(editorId) {
    var elementId = editorId;
    var sectionId = ExtractSectionId($('#' + editorId));

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

function PreventAppDrawerDrop() {
    if (window.innerHeight < 701 && window.innerWidth < 601) {
        return true;
    }

    var tray = $("#component-panel").offset();
    var trayWidth = $("#component-panel").width();
    var trayHeight = $("#component-panel").height();

    var trayTop = (tray.top - $(document).scrollTop());

    var x = event.clientX;
    var y = event.clientY;

    if (x >= tray.left && x <= (tray.left + trayWidth) && y >= trayTop && y <= (trayTop + trayHeight)) {
        return false;
    }

    return true;
}

function DropComponent(control, event, ui) {
    var newElement = $(ui.draggable).clone();

    var sectionId = ExtractSectionId($(control));
    var newElementId = newElement.attr("id");

    newElementId = newElementId.replace('<sectionId>', sectionId);
    newElementId = newElementId.replace('<componentStamp>', new Date().valueOf());
    newElement.attr("id", newElementId);

    $(control).append(newElement);

    $('#' + newElementId).removeClass("ui-draggable");
    $('#' + newElementId).removeClass("ui-draggable-handle");
    $('#' + newElementId).unbind();

    ReplaceChildTokens(newElementId, sectionId);

    var newElementContent = $('#' + newElementId)[0].outerHTML;

    InitialiseEditor();
    InitialiseWidgets();

    if (newElement.hasClass("component-container")) {
        InitialiseContainer(newElementId);
    }

    var dataParams = { "pageSectionId": sectionId, "containerElementId": $(control).attr("id"), "elementBody": newElementContent };
    $.ajax({
        data: dataParams,
        type: 'POST',
        cache: false,
        url: '/Builder/Component/Add',
        success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
    });
}

function ReplaceChildTokens(parentElementId, sectionId) {
    $('#' + parentElementId).children().each(function () {
        var childId = $(this).attr("id");

        if (childId != undefined) {
            childId = childId.replace("<sectionId>", sectionId);
            childId = childId.replace("<componentStamp>", new Date().valueOf());

            $(this).attr("id", childId);

            ReplaceChildTokens(childId, sectionId);
        }
    });
}

function ChangeOrder() {
    $('#page-wrapper').toggleClass("zoom");
    $('#page-wrapper').toggleClass("change-order");
    $('#page-wrapper.change-order').sortable({ placeholder: "ui-state-highlight", helper: 'clone' });
    $('.action-container.global').fadeOut();
    $('.action-container.section-order').fadeIn();

    if ($('#section-panel').hasClass('visible')) {
        $('#section-panel').slideUp(300);
        $('#section-panel').toggleClass('visible');
    }

    if ($('#component-panel').hasClass('visible')) {
        $('#component-panel').slideUp(300);
        $('#component-panel').toggleClass('visible');
    }

    if ($('#pages-panel').hasClass('visible')) {
        $('#pages-panel').slideUp(300);
        $('#pages-panel').toggleClass('visible');
    }

    if ($('#theme-manager-panel').hasClass('visible')) {
        $('#theme-manager-panel').slideUp(300);
        $('#theme-manager-panel').toggleClass('visible');
    }
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

function ToggleSectionPanel() {
    if ($('#component-panel').hasClass('visible')) {
        $('#component-panel').slideUp(300);
        $('#component-panel').toggleClass('visible');
    }

    if ($('#pages-panel').hasClass('visible')) {
        $('#pages-panel').slideUp(300);
        $('#pages-panel').toggleClass('visible');
    }

    if ($('#theme-manager-panel').hasClass('visible')) {
        $('#theme-manager-panel').slideUp(300);
        $('#theme-manager-panel').toggleClass('visible');
    }

    if ($('#section-panel').hasClass('visible')) {
        $('#section-panel').slideUp(300);
        $('#section-panel').toggleClass('visible');
    }
    else {
        $('#section-panel').slideDown(300);
        $('#section-panel').toggleClass('visible');
    }
}

function ToggleComponentPanel() {
    if ($('#section-panel').hasClass('visible')) {
        $('#section-panel').slideUp(300);
        $('#section-panel').toggleClass('visible');
    }

    if ($('#theme-manager-panel').hasClass('visible')) {
        $('#theme-manager-panel').slideUp(300);
        $('#theme-manager-panel').toggleClass('visible');
    }

    if ($('#pages-panel').hasClass('visible')) {
        $('#pages-panel').slideUp(300);
        $('#pages-panel').toggleClass('visible');
    }

    if ($('#component-panel').hasClass('visible')) {
        $('#component-panel').slideUp(300);
        $('#component-panel').toggleClass('visible');
    }
    else {
        $('#component-panel').slideDown(300);
        $('#component-panel').toggleClass('visible');
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