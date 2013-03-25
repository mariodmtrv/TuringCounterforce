function createNewChannelButtonClicked() {
    $.ajax({
        url: serviceBaseUrl + "/get-newSalt",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        success: setNewChannelData,
        error: ajaxChannelCallError
    });
}

function ajaxChannelCallError() {
    alert("Connection failed");
}

function setNewChannelData(salt) {
    var channelName = htmlEncode($("#newChannelName").val());
    var channelPassword = htmlEncode($("#newChannelPassword").val());
    var protectedChannelPassword = protectPassword(channelPassword, salt);
    var request = {
        "channelName": channelName,
        "channelPassword": protectedChannelPassword,
        "channelSalt": salt
    };
    $.ajax({
        url: serviceBaseUrl + "/set-up-channel",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(request),
        success: addCreatedChannel(channelName),
        error: ajaxChannelCreationError
    });
}

function ajaxChannelCreationError() {
    alert("Channel creation failed");
}
function getChannelSalt(channelName) {
    var request = {
        "channelName": htmlEncode(channelName)
    };
    $.ajax({
        url: serviceBaseUrl + "/get-channel-salt",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(request),
        success: function (salt) {
            channelSalt = salt;
        },
        error: ajaxGetSaltCallError
    });
}
function ajaxGetSaltCallError(err) {
    alert("gettingSaltFailed");
}
function createProtectedPasswordForChannel(channelName, password) {
    var s = channelSalt;
    var protectedPassword = protectPassword(htmlEncode(password), s);
    return protectedPassword;
}
function protectPassword(password, salt) {
    var doc = password + salt;
    var hash = CryptoJS.MD5(doc);
    var result = hash.toString();
    return result;
}