'use strict';

$(document).ready(function () {
    $('body #page-wrapper.admin').on('click', '.launch-popover', function (e) {
        EditablePopover.Load($(this));
    });
});

var EditablePopover = {
    Load: function (triggerElement) {
        var actionTitle = $(triggerElement).attr("data-action");
        var associationId = $(triggerElement).attr("data-association");
        var editablePopoverIdentifier = '.popover.editable-popover[data-association=' + associationId + '][data-action="' + actionTitle + '"]';

        if ($(editablePopoverIdentifier).length) {
            EditablePopover.Destroy();
        }
        else {
            EditablePopover.Destroy();

            $(triggerElement).popover({
                template:
                '<div class="popover editable-popover" data-association=' + associationId + ' data-action="' + actionTitle + '">' +
                '<div class="arrow"></div>' +
                '<h3 class="popover-title"></h3>' +
                '<div class="popover-content"></div>' +
                '</div>'
            }).popover('show');

            $.ajax({
                type: 'GET',
                url: $(triggerElement).attr("data-url"),
                cache: false,
                success: function (data) {
                    $(editablePopoverIdentifier + ' .popover-content').empty();
                    $(editablePopoverIdentifier + ' .popover-content').parent().addClass("dynamic");
                    $(editablePopoverIdentifier + ' .popover-content').parent().attr("data-association", associationId);
                    $(editablePopoverIdentifier + ' .popover-content').html(data);
                },
                error: function () {
                    EditablePopover.Destroy();
                }
            });
        }
    },
    GenerateAntiForgeryHeader: function () {
        var headers = {};
        headers['__RequestVerificationToken'] = $('input[name="__RequestVerificationToken"]').val();
        return headers;
    },
    ShowSpinner: function (pageAssociationId) {
        $('#editable-popover-spinner[data-association="' + pageAssociationId + '"]').show();
        $('#editable-popover-content[data-association="' + pageAssociationId + '"]').hide();
    },
    HideSpinner: function (pageAssociationId) {
        $('#editable-popover-content[data-association="' + pageAssociationId + '"]').show();
        $('#editable-popover-spinner[data-association="' + pageAssociationId + '"]').hide();
    },
    OnSuccess: function (popoverTitle, actionIcon, pageAssociationId) {
        EditablePopover.Destroy();

        var buttonElement = $('.action[data-action="' + popoverTitle + '"][data-association=' + pageAssociationId + ']');
        var iconElement = $('.action[data-action="' + popoverTitle + '"][data-association=' + pageAssociationId + '] span');

        iconElement.removeClass(actionIcon);
        iconElement.addClass('fa-check');
        buttonElement.addClass('green');

        setTimeout(function () {
            iconElement.addClass(actionIcon);
            iconElement.removeClass('fa-check');
            buttonElement.removeClass('green');
        }, 2500);
    },
    OnError: function (popoverTitle, actionIcon, pageAssociationId) {
        $('#editable-popover-info[data-association=' + pageAssociationId + ']').removeClass("alert-warning");
        $('#editable-popover-info[data-association=' + pageAssociationId + ']').addClass("alert-danger");
        $('#editable-popover-info[data-association=' + pageAssociationId + ']').text("An error occured, please try again");

        var buttonElement = $('.action[data-title="' + popoverTitle + '"][data-association=' + pageAssociationId + ']');
        var iconElement = $('.action[data-title="' + popoverTitle + '"][data-association=' + pageAssociationId + '] span');

        iconElement.removeClass(actionIcon);
        iconElement.addClass('fa-exclamation');
        buttonElement.addClass('red');

        setTimeout(function () {
            iconElement.addClass(actionIcon);
            iconElement.removeClass('fa-exclamation');
            buttonElement.removeClass('red');
        }, 2500);
    },
    Destroy: function () {
        $('.popover.editable-popover').popover('destroy');
    }
};