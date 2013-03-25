$(document).ready(function () {
    $(function () {
        $("#listPostsForm").dialog({
            autoOpen: false,
            width: 370,
            height: 300,
            show: "blind",
            hide: "explode"
        });

        $("#listPostsButton").click(function () {
            $("#postsFormErrorConsole").text("");
            $("#linkPassword").val("");
            $("#listPostsForm").dialog("open");
            $("#channelNameSpanList").text(selectedChannelName());
            return false;
        });
    });
    $(function () {
        $("#postsCountSlider").slider({
            value: 0,
            min: 1,
            max: 50,
            step: 3,
            slide: function (event, ui) {
                $("#postsCountAmount").val(ui.value);
            }
        });
        $("#postsCountAmount").val($("#postsCountSlider").slider("value"));
    });
    $(function () {
        $("#postMessageForm").dialog({
            autoOpen: false,
            width: 370,
            height: 300,
            show: "blind",
            hide: "explode"
        });

        $("#postMessageButton").click(function () {

            $("#postMessageForm").dialog("open");
            $("#channelNameSpanMessage").text(selectedChannelName());
            return false;
        });
    });
    $(function () {
        $("#postLinkForm").dialog({
            autoOpen: false,
            width: 370,
            height: 300,
            show: "blind",
            hide: "explode"
        });

        $("#postLinkButton").click(function () {
            $("#postLinkForm").dialog("open");
            $("#channelNameSpanLink").text(selectedChannelName());
            return false;
        });
    });
    $(function () {
        $("#postFileForm").dialog({
            autoOpen: false,
            width: 370,
            height: 300,
            show: "blind",
            hide: "explode"
        });

        $("#postFileButton").click(function () {
            $("#postFileForm").dialog("open");
            $("#channelNameSpanFile").text(selectedChannelName());
            return false;
        });
    });
    $.fx.speeds._default = 50;
    $(function () {
        $(function () {
            $("#aboutContent").tabs();
        });

        $("#aboutContent").dialog({
            height: 500,
            width: 700,
            autoOpen: false,
            show: "blind",
            hide: "explode"
        });

        $("#aboutButton").click(function () {
            $("#aboutContent").dialog("open");
            return false;
        });
    });
    $(function () {
        $(".controls button").button({
            icons: {
                primary: 'ui-icon-gear'
            }
        });
    });
    $(function () {
        $("#loadedChannelsTabs").tabs();
    });
    $(function () {
        $("#menuAccordion").accordion();
    });
});

var loadedChannelsTabs = new Array();
var addedChannelsList = new Array();

function selectedChannelName() {
    var curTab = $('.loadedChannelsTabContent:not(.ui-tabs-hide)');
    var curTabId = curTab.attr("id");
    var name = curTabId.substring(7);
    return name;
}

function addCreatedChannel(name) {
    var newLiName =htmlEncode(name);
    var newLi = '<li class="addedChannelsListItem" id="' + newLiName + '" onclick="channelListItemClicked(this.id)">' + newLiName + '</li>';
    $('#addedChannelsList').append(newLi);
    $('#newChannelName').val("");
    $('#newChannelPassword').val("");
    
    addedChannelsList.push(newLiName);
}
function addExistingChannelClicked() {
    var newLiName = htmlEncode(htmlEncode(document.getElementById('addExistingChannelInput').value));
    $("#addExistingError").text("");
    if (addedChannelsList.indexOf(newLiName) == -1) {
        if ((newLiName[0] >= 'a' && newLiName[0] <= 'z') || (newLiName[0] >= 'A' && newLiName[0] <= 'Z')) {
            var newLi = '<li class="addedChannelsListItem" id="' + newLiName + '" onclick="channelListItemClicked(this.id)">' + newLiName + '</li>';
            $('#addedChannelsList').append(newLi);
            addedChannelsList.push(newLiName);
            $('#addExistingChannelInput').val("");
        } else {
            $("#addExistingError").text("Channel name should start with a letter [a-z] or [A-Z]");
        }
    } else {
        $("#addExistingError").text("Channel already added");
    }
}

function channelListItemClicked(id1) {
    var id = htmlEncode(id1);
    if (loadedChannelsTabs.indexOf(id) == -1) {
        var idName = 'Channel' + id;
        $("#loadedChannelsTabs").append('<div class="loadedChannelsTabContent" id="' + idName + '"></div>');
        $("#loadedChannelsTabs").tabs('add', '#' + idName, id);
        $("#loadedChannelsTabs").tabs('select', id);
        loadedChannelsTabs.push(id);
    }
}

function displayLoadedLink(channelName, linkUrl, linkDescription) {
    var channel = '#Channel' + channelName;
    $(channel).append('<div class="loadedLinkDiv" ><p>'+ 
        linkUrl + '<p>'+linkDescription+'</p></div>');
}
function displayLoadedMessage(channelName, content) {
    var channel = '#Channel' + channelName;
    $(channel).append('<div class="loadedMessageDiv" ><p>' + content + '</p></div>');
}
function displayLoadedFile(channelName, url, mimetype) {
    var channel = '#Channel' + channelName;
    $(channel).append('<div class="loadedMessageDiv" >' + '<a href="' + url +'">'+url+'</a><p>'+ mimetype+' </p></div>');
}
function displayPostedMessage(channelName, content) {
    $("#messagePassword").val("");
    $("#messageContent").val("");
    $("#messagesFormErrorConsole").text("");
    var channel = '#Channel' + channelName;
    $(channel).prepend('<div class="loadedMessageDiv" ><p>' + content + '</p></div>');
}
function displayPostedLink(channelName, linkUrl, linkDescription) {
    $("#linkPassword").val("");
    $("#linkDescription").val("");
    $("#linkUrl").val("");
    $("#linksFormErrorConsole").text("");
    var channel = '#Channel' + channelName;
    $(channel).prepend('<div class="loadedLinkDiv" ><a href="' + linkUrl + '" >' +
        linkUrl + '</a><p>' + linkDescription + '</p></div>');
}