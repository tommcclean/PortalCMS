"use strict";

$(document).ready(function () {
    $('body').on('click', '.launch-modal', function (e) {
        e.preventDefault();
        showModalEditor($(this).data('title'), $(this).attr('href'));
    });
});

function showModalEditor(title, url) {
    if ($('#ContentEditor').length == 0) {
        var html = [];
        html.push('<div class="modal" id="ContentEditor" tabindex="-1" role="dialog" aria-hidden="true"><div class="modal-dialog"><div class="modal-content">');
        html.push('<div class="modal-header"><button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button><h4 class="modal-title">Edit content</h4></div>');
        html.push('<div class="modal-data">');
        html.push('<div class="modal-body"></div>');
        html.push('</div></div></div></div>');

        $('body').append(html.join(''));
    }

    $('#ContentEditor .modal-title').text(title);
    setModalEditorContent('<p>Loading Please wait...</p>');
    $('#ContentEditor').modal('show');

    $.ajax({
        type: 'GET',
        url: url,
        cache: false,
        success: function (data) {
            setModalEditorContent(data);
            $('legend').remove();
        },
        error: function () {
            $('#ContentEditor .modal-body').html('<p>An error occurred. Please try again.</p>');
        }
    });

    return;
};

function setModalEditorContent(content) {
    if (~content.indexOf('modal-footer')) {
        $('#ContentEditor .modal-data').html(content);
    } else {
        var html = [];
        html.push('<div class="modal-body">' + content + '</div>');
        $('#ContentEditor .modal-data').html(html.join(''));
    }

    if ($('#ContentEditor .datepicker').length > 0) {
        $('#ContentEditor .datepicker').each(function () {
            $(this).datepicker({
                format: "yyyy-mm-dd",
                weekStart: 1,
                startView: 1,
                autoclose: true
            });
        });
    }

    bindModalEditor();
};

function bindModalEditor() {
    if ($('#ContentEditor .tag-editor').length > 0) {
        loadTags();
    }

    if ($('#ContentEditor .refresh-page').length > 0) {
        $('#ContentEditor .refresh-page').on('click', function (e) {
            e.preventDefault();
            window.location.reload();
        });
    }

    if ($('#ContentEditor a.switch-modal').length > 0) {
        $('#ContentEditor a.switch-modal').on('click', function (e) {
            e.preventDefault();
            setModalEditorContent('<p>Please wait...</p>');
            $.ajax({
                type: 'GET',
                url: $(this).attr('href'),
                cache: false,
                success: function (data) {
                    setModalEditorContent(data);
                },
                error: function () {
                    $('#ContentEditor .modal-data').html('<p>An error occurred.</p>');
                }
            });
        });
    }

    $('#ContentEditor form').submit(function (e) {
        e.preventDefault();

        var formType = $(this).attr('method');
        var formUrl = $(this).attr('action');
        var formData;

        if ($(this).find('input[type="file"]').length > 0) {
            if (window.FormData === null) {
                alert('Your browser does not support this admin feature. Please update to a modern browser.');
                return;
            }

            formData = new FormData();
            $(this).find('input[type!="file"]').each(function () {
                formData.append($(this).attr('name'), $(this).val());
            });
            $(this).find('select').each(function () {
                formData.append($(this).attr('name'), $(this).val());
            });
            $(this).find('input[type="file"]').each(function () {
                formData.append($(this).attr('name'), $(this)[0].files[0]);
            });

            $.ajax({
                type: formType,
                url: formUrl,
                cache: false,
                contentType: false,
                processData: false,
                data: formData,
                beforeSend: function () {
                    setModalEditorContent('<p>Please wait...</p>');
                },
                success: function (data) {
                    if (data == 'Refresh') {
                        window.location.reload();
                    } else {
                        setModalEditorContent(data);
                    }
                },
                error: function () {
                    $('#ContentEditor .modal-data').html('<p>An error occurred.</p>');
                }
            });
        } else {

            // No files, so just use serialize()
            formData = $(this).serialize();
            $.ajax({
                type: formType,
                url: formUrl,
                cache: false,
                data: formData,
                beforeSend: function () {
                    setModalEditorContent('<p>Please wait...</p>');
                },
                success: function (data) {
                    if (data == 'Refresh') {
                        window.location.reload();
                    } else {
                        setModalEditorContent(data);
                    }
                },
                error: function () {
                    $('#ContentEditor .modal-data').html('<p>An error occurred.</p>');
                }
            });
        }

        return;
    });
};