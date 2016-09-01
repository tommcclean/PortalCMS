$(document).ready(function () {
    SetupComponentEvents();
    LoadWidgets();

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
            data: dataParams,
            type: 'POST',
            cache: false,
            url: '/Builder/Component/Delete',
            success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
        });
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

    if ($('#section-panel').hasClass('visible')) {
        $('#section-panel').slideUp(300);
        $('#section-panel').toggleClass('visible');
    }

    if ($('#component-panel').hasClass('visible')) {
        $('#component-panel').slideUp(300);
        $('#component-panel').toggleClass('visible');
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

function ToggleSectionPanel() {
    if ($('#component-panel').hasClass('visible')) {
        $('#component-panel').slideUp(300);
        $('#component-panel').toggleClass('visible');
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

    if ($('#component-panel').hasClass('visible')) {
        $('#component-panel').slideUp(300);
        $('#component-panel').toggleClass('visible');
    }
    else {
        $('#component-panel').slideDown(300);
        $('#component-panel').toggleClass('visible');
    }
}

function LoadWidgets() {
    if ($('.post-list-wrapper').length) {
        $.get("/Builder/Widget/RecentPostList", function (data) {
            $(".post-list-wrapper").html(data);
        });
    }
}

function SetupComponentEvents() {
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

    // Edit Text Elements
    tinymce.init({
        selector: '.admin section p, .admin section h1, .admin section h2, .admin section h3, .admin section h4, .admin section code',
        menubar: false,
        inline: true,
        plugins: ['advlist textcolor colorpicker link'],
        toolbar: 'bold italic underline | link | forecolor backcolor | delete',
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
        toolbar: 'bold italic underline | link | forecolor backcolor | delete',
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

    // Edit Buttons and Links
    tinymce.init({
        selector: '.admin section .freestyle',
        menubar: true,
        inline: true,
        plugins: [
         'advlist autolink lists link image charmap print preview anchor',
         'searchreplace visualblocks code fullscreen',
         'insertdatetime media table contextmenu paste textcolor colorpicker'
        ],
        toolbar: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | forecolor backcolor | bullist numlist outdent indent | link image | delete',
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
                    url: '/Builder/Component/Freestyle',
                    success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
                });
            });
        }
    });
}

function SetupAddComponentDrawer() {
    $("section").droppable({
        accept: function () {
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
        },
        tolerance: "pointer",
        activeClass: "ui-state-default",
        hoverClass: "ui-state-hover",
        drop: function (event, ui) {
            var newElement = $(ui.draggable).clone();

            var sectionId = ExtractSectionId($(this));
            var newElementId = newElement.attr("id");

            newElementId = newElementId.replace('<sectionId>', sectionId);
            newElementId = newElementId.replace('<componentStamp>', new Date().valueOf());
            newElement.attr("id", newElementId);

            $(this).append(newElement);

            $('#' + newElementId).removeClass("ui-draggable");
            $('#' + newElementId).removeClass("ui-draggable-handle");
            $('#' + newElementId).unbind();

            ReplaceChildTokens(newElementId, sectionId);

            var newElementContent = $('#' + newElementId)[0].outerHTML;

            SetupComponentEvents();
            LoadWidgets();

            if (newElement.hasClass("component-container")) {
                SetupControlContainer(newElementId);
            }

            var dataParams = { "pageSectionId": sectionId, "containerElementId": $(this).attr("id"), "elementBody": newElementContent };
            $.ajax({
                data: dataParams,
                type: 'POST',
                cache: false,
                url: '/Builder/Component/Add',
                success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
            });
        }
    });
    $(".component-container").droppable({
        accept: function () {
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
        },
        tolerance: "pointer",
        activeClass: "ui-state-default",
        hoverClass: "ui-state-hover",
        greedy: "true",
        drop: function (event, ui) {
            var newElement = $(ui.draggable).clone();
            var sectionId = ExtractSectionId($(this));
            var newElementId = newElement.attr("id");

            newElementId = newElementId.replace('<sectionId>', sectionId);
            newElementId = newElementId.replace('<componentStamp>', new Date().valueOf());
            newElement.attr("id", newElementId);

            $(this).append(newElement);

            $('#' + newElementId).removeClass("ui-draggable");
            $('#' + newElementId).removeClass("ui-draggable-handle");
            $('#' + newElementId).unbind();

            ReplaceChildTokens(newElementId, sectionId);

            var newElementContent = $('#' + newElementId)[0].outerHTML;

            SetupComponentEvents();
            LoadWidgets();

            if (newElement.hasClass("component-container")) {
                SetupControlContainer(newElementId);
            }

            var dataParams = { "pageSectionId": sectionId, "containerElementId": $(this).attr("id"), "elementBody": newElementContent };
            $.ajax({
                data: dataParams,
                type: 'POST',
                cache: false,
                url: '/Builder/Component/Add',
                success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
            });
        }
    });
}

function ReplaceChildTokens(parentElementId, sectionId)
{
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

function SetupControlContainer(elementId) {
    $("#" + elementId).droppable({
        tolerance: "intersect",
        activeClass: "ui-state-default",
        hoverClass: "ui-state-hover",
        greedy: "true",
        drop: function (event, ui) {
            var newElement = $(ui.draggable).clone();
            var sectionId = ExtractSectionId($(this));
            var newElementId = newElement.attr("id");

            newElementId = newElementId.replace('<sectionId>', sectionId);
            newElementId = newElementId.replace('<componentStamp>', new Date().valueOf());
            newElement.attr("id", newElementId);

            $(this).append(newElement);

            $('#' + newElementId).removeClass("ui-draggable");
            $('#' + newElementId).removeClass("ui-draggable-handle");
            $('#' + newElementId).unbind();

            ReplaceChildTokens(newElementId, sectionId);

            SetupComponentEvents();
            LoadWidgets();

            if (newElement.hasClass("component-container")) {
                SetupControlContainer(newElementId);
            }

            var dataParams = { "pageSectionId": sectionId, "containerElementId": $(this).attr("id"), "elementBody": $('#' + newElementId)[0].outerHTML };
            $.ajax({
                data: dataParams,
                type: 'POST',
                cache: false,
                url: '/Builder/Component/Add',
                success: function (data) { if (data.State === false) { alert("Error: The Page has lost synchronisation. Reloading Page..."); location.reload(); } }
            });
        }
    });
}