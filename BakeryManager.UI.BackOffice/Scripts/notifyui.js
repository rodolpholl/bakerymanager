// Mensagens de notificação / interação com usuário

var confirmCallBackOk, confirmCallBackCancel = null;

function toast(message, messageType) {
    buildToastNotification(message, messageType);
}

function inlineAlert(message, messageType, messageHost) {
    buildInlineNotification(message, messageType, messageHost);
}

function alert(message, callBackOk, messageType) {

    if (messageType == null || messageType == undefined || messageType.toString() == "")
        messageType = 0;

    confirmCallBackOk = callBackOk;

    buildModalNotification(message, messageType, "<a class='btn btn-success' href='javascript:void(0);' onclick='javascript:handleCallBackOk();'>ok</a>");

}

function confirm(message, callBackOk, callBackCancel) {

    confirmCallBackOk = callBackOk;
    confirmCallBackCancel = callBackCancel;

    buildModalNotification(message, "2", "<a class='btn btn-success' href='javascript:void(0);' onclick='javascript:handleCallBackOk();'>Sim</a><a class='btn btn-default' href='javascript:void(0);' onclick='javascript:handleCallBackCancel();'>Não</a>");

    $("#myModal").modal('show');

}

function buildToastNotification(message, messageType) {

    var messageIcon, messageTitle, messageClass = "";

    //    alert 0
    //    error 1
    //    confirm 2
    //    ok 3
    //    information 4

    if (messageType == "0") {
        messageIcon = "<span class='glyphicon glyphicon-exclamation-sign'></span>&nbsp;";
        messageTitle = "Alerta";
        messageClass = "growl-warning";
    } else if (messageType == "1") {
        messageIcon = "<span class='glyphicon glyphicon-warning-sign'></span>&nbsp;";
        messageTitle = "Erro";
        messageClass = "growl-danger";
    } else if (messageType == "3") {
        messageIcon = "<span class='glyphicon glyphicon-ok'></span>&nbsp;";
        messageTitle = "Ok";
        messageClass = "growl-primary";
    } else if (messageType == "4") {
        messageIcon = "<span class='glyphicon glyphicon-info-sign'></span>&nbsp;";
        messageTitle = "Informação";
        messageClass = "growl-info";
    } else if (messageType == "5") {
        messageIcon = "<span class='glyphicon glyphicon-info-ok'></span>&nbsp;";
        messageTitle = "Sucesso";
        messageClass = "growl-success";
    }

    $.gritter.add({
        title: messageIcon + messageTitle,
        text: message,
        class_name: messageClass,
        sticky: false,
        time: ''
    });

}

function buildInlineNotification(message, messageType, messageHost) {

    var messageIcon, messageTitle, messageClass = "";

    //    alert 0
    //    error 1
    //    confirm 2
    //    ok 3
    //    information 4
    //    success 5

    if (messageType == "0") {
        messageIcon = "<span class='glyphicon glyphicon-exclamation-sign'></span>&nbsp;";
        messageTitle = "Atenção!";
        messageClass = "alert-warning";
    } else if (messageType == "1") {
        messageIcon = "<span class='glyphicon glyphicon-warning-sign'></span>&nbsp;";
        messageTitle = "Erro!";
        messageClass = "alert-danger";
    } else if (messageType == "3") {
        messageIcon = "<span class='glyphicon glyphicon-ok'></span>&nbsp;";
        messageTitle = "Ok!";
        messageClass = "alert-info";
    } else if (messageType == "4") {
        messageIcon = "<span class='glyphicon glyphicon-info-sign'></span>&nbsp;";
        messageTitle = "Informação!";
        messageClass = "alert-info";
    } else if (messageType == "5") {
        messageIcon = "<span class='glyphicon glyphicon-ok'></span>&nbsp;";
        messageTitle = "Sucesso!";
        messageClass = "alert-success";
    }

    var html = "<div class=\"alert " + messageClass + "\">";
    html += "<a type=\"button\" class=\"close btn btn-default btn-sm\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</a>";
    html += messageIcon + "<strong>" + messageTitle + "</strong><br />" + message;
    html += "</div>";

    $("#" + messageHost).html(html);

}

function buildModalNotification(message, messageType, buttons) {

    $('body').removeClass('modal-open');
    $("#myModal").remove();
    $("body").remove("#myModal");

    var messageIcon, messageTitle = "";

    var htmlContent = "";

    htmlContent += "<div class='modal fade' tabindex='-1' role='dialog' aria-hidden='true' id='myModal'>";
    htmlContent += "<div class='modal-dialog'>";
    htmlContent += "<div class='modal-content'>";

    //    alert 0
    //    error 1
    //    confirm 2
    //    ok 3
    //    information 4

    if (messageType == 0) {
        messageIcon = "<span class='glyphicon glyphicon-exclamation-sign'></span>&nbsp;";
        messageTitle = "Alerta";
    } else if (messageType == 1) {
        messageIcon = "<span class='glyphicon glyphicon-warning-sign'></span>&nbsp;";
        messageTitle = "Erro";
    } else if (messageType == 2) {
        messageIcon = "<span class='glyphicon glyphicon-question-sign'></span>&nbsp;";
        messageTitle = "Confirmação";
    } else if (messageType == 3) {
        messageIcon = "<span class='glyphicon glyphicon-ok'></span>&nbsp;";
        messageTitle = "Ok";
    } else if (messageType == 4) {
        messageIcon = "<span class='glyphicon glyphicon-info-sign'></span>&nbsp;";
        messageTitle = "Informação";
    }

    htmlContent += "<div class='modal-header'><button class='close' data-dismiss='modal'>&times;</button><h4 class='modal-title'>" + messageIcon + messageTitle + "</h4></div>";
    htmlContent += "<div class='modal-body'><p>" + message + "</p></div>";
    htmlContent += "<div class='modal-footer'> " + buttons + " </div>";

    htmlContent += "<div/>";
    htmlContent += "<div/>";
    htmlContent += "<div/>";

    if ($("#myModal").length == 0) {
        $(htmlContent).appendTo('body');
    }

    $("#myModal").modal('show');

}

function handleCallBackOk() {
    callBackHandle(confirmCallBackOk);
}

function handleCallBackCancel() {
    callBackHandle(confirmCallBackCancel);
}

function callBackHandle(callBack) {

    $('body').modal('hide');
    $('body').removeClass('modal-open');

    $('#myModal').modal('hide');
    $("#myModal").remove();

    $('.modal-backdrop').modal('hide');
    $(".modal-backdrop").remove();

    $("body").remove("#myModal");

    if (!callBack) {
        callBack = function () {
            return false;
        };
    }

    if (typeof (callBack) == 'function') {
        callBack();
    }

}
// Mensagens de notificação / interação com usuário - Fim