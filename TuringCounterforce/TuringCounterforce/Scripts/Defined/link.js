function connectToLinkChannel() {
    var channelName = htmlEncode($("#channelNameSpanLink").text());
    getChannelSalt(channelName);
}
function postLinkClicked() {
    var channelName = htmlEncode($("#channelNameSpanLink").text());
    var channelPassword = htmlEncode($("#linkPassword").val());
    var channelPasswordProtected = createProtectedPasswordForChannel(channelName, channelPassword);
    var linkUrl = htmlEncode($("#linkUrl").val());
    var linkDescription = htmlEncode($("#linkDescription").val());
    var linkUrlProtected = CryptoJS.AES.encrypt(linkUrl, channelPassword).toString();
    var linkDescriptionProtected = CryptoJS.AES.encrypt(linkDescription, channelPassword).toString();
    var request = {
        "channelName": channelName,
        "channelPassword": channelPasswordProtected,
        "linkUrl": linkUrlProtected,
        "linkDescription": linkDescriptionProtected
    };
    $.ajax({
        url: serviceBaseUrl + "/post-link",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(request),
        success: displayPostedLink(channelName, linkUrl, linkDescription),
        error: ajaxLinkCallError
    });
}
function ajaxLinkCallError(err) {
    $("#linkFormErrorConsole").text("Posting link failed");
}