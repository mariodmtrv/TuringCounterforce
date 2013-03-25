$(document).ready(function () {
    //  $('#postMessage').click(buttonPostMessageClicked);
    // $("#fileUpload").kendoUpload();
});

function htmlEncode(value) {
    return $('<div/>').text(value).html();
}
var serviceBaseUrl = "SecretCommunicatorService.svc";
var channelSalt;
function connectToListChannel() {
    var channelName = htmlEncode($("#channelNameSpanList").text());
    getChannelSalt(channelName);
}

function listPostsButtonClicked() {
    var channelName = htmlEncode($("#channelNameSpanList").text());
    var channelPassword = htmlEncode($("#listPassword").val());
    var channelPasswordProtected = createProtectedPasswordForChannel(channelName, channelPassword);
    var postsCount = $("#postsCountAmount").val();
    var request = {
        "channelName": channelName,
        "channelPassword": channelPasswordProtected,
        "postsCount": postsCount
    };
    $.ajax({
        url: serviceBaseUrl + "/get-posts",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(request),
        success: displayPosts,
        error: ajaxPostsCallError
    });
}
function ajaxPostsCallError(err) {
    $("#postsFormErrorConsole").text("Loading posts failed");
}

function decryptData(content,password) {
    return CryptoJS.AES.decrypt(content, password).toString(CryptoJS.enc.Utf8);
}
function displayPosts(posts) {
    var channelName = htmlEncode($("#channelNameSpanList").text());
    var channelPassword = htmlEncode($("#listPassword").val());
    for (i in posts) {
        var p = jQuery.parseJSON(posts[i]);
        if (p.docType == "message") {
            displayLoadedMessage(
                channelName, decryptData(p.messageContent, channelPassword));
                     } else if (p.docType == "link") {
            displayLoadedLink(
                channelName,
                decryptData(p.linkUrl,channelPassword),
                decryptData(p.linkDescription,channelPassword));
        }
        else if(p.docType=="file") {
            displayLoadedFile(channelName, p.fileUrl, p.fileMimeType);
        }
    }
}


