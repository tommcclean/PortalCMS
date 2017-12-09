function EditablePopover_OnSuccess(popoverTitle, actionIcon, pageAssociationId) {
    $('.popover').popover('hide');

    var buttonElement = $('.action[data-title="' + popoverTitle + '"][data-association=' + pageAssociationId + ']');
    var iconElement = $('.action[data-title="' + popoverTitle + '"][data-association=' + pageAssociationId + '] span');

    iconElement.removeClass(actionIcon);
    iconElement.addClass('fa-check');
    buttonElement.addClass('green');

    setTimeout(function () {
        iconElement.addClass(actionIcon);
        iconElement.removeClass('fa-check');
        buttonElement.removeClass('green');
    }, 2500);
}

function EditablePopover_OnError(popoverTitle, actionIcon, pageAssociationId) {
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
}