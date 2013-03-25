function connectToMessageChannel() {
    var channelName = htmlEncode($("#channelNameSpanMessage").text());
    getChannelSalt(channelName);
}
function postMessageClicked() {
    var channelName = htmlEncode($("#channelNameSpanMessage").text());
    var channelPassword = htmlEncode($("#messagePassword").val());
    var channelPasswordProtected = createProtectedPasswordForChannel(channelName, channelPassword);
    var messageContent = htmlEncode($("#messageContent").val());
    var messageContentProtected = CryptoJS.AES.encrypt(messageContent, channelPassword).toString();

    var request = {
        "channelName": channelName,
        "channelPassword": channelPasswordProtected,
        "messageContent": messageContentProtected
    };
    $.ajax({
        url: serviceBaseUrl + "/post-message",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(request),
        success: displayPostedMessage(channelName, messageContent),
        error: ajaxMessageCallError
    });
}
function ajaxMessageCallError(err) {
    $("#messagesFormErrorConsole").text("Posting message failed");
}