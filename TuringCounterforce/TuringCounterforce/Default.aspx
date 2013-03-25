<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SecretCommunicator.Main" %>

<!DOCTYPE html>
<html>
    <head>
        <title>
            SecretCommunicator
        </title>
        <link href="Content/themes/Main.css" rel="stylesheet" type="text/css" />
        <link href="Content/themes/jquery-ui-theme.css" rel="stylesheet" type="text/css" />
        <link href="Content/kendo/2012.2.710/kendo.blueopal.min.css" rel="stylesheet"
              type="text/css" />
        <!--Factory Scripts--> 
        <script src="Scripts/JQuery/jquery-1.7.2.min.js" type="text/javascript"></script>
        <script src="Scripts/JQuery/jquery-ui-1.8.22.min.js" type="text/javascript"></script>
        <script src="Scripts/kendo/2012.2.710/kendo.upload.min.js" type="text/javascript"></script>
        <script src="Scripts/Security/aes.js" type="text/javascript"></script>
        <script src="Scripts/Security/md5.js" type="text/javascript"></script>
        <!--Own Scripts-->
        <script src="Scripts/Defined/channels.js" type="text/javascript"></script>
        <script src="Scripts/Defined/interface.js" type="text/javascript"></script>
        <script src="Scripts/Defined/link.js" type="text/javascript"></script>
        <script src="Scripts/Defined/message.js" type="text/javascript"></script>
        <script src="Scripts/Defined/list.js" type="text/javascript"></script>
    </head>
    <body>
        <header id="applicationName">
            <span id="heading">Turing Counterforce</span>
            <br />
            <span id="subHeading">Encrypted Communication Service</span>
        </header>
        <section id="leftContent">
            <nav id="channelsMenu">
                <div id="menuAccordion">
                    <h3><a href="#">Channels</a></h3>
                    <div id="addedChannels">
                        <ul id="addedChannelsList">
                            <li>Add Channels</li>
                        </ul>
                    </div>
                    <h3><a href="#" id="addExisting">Add Existing</a></h3>
                    <div>
                        <span id="addExistingError" class="errorConsole"></span>
                        <span>Channel name:</span> 
                        <br />
                        <input type="text" id="addExistingChannelInput"/>
                        <br />
                        <button id="addExistingChannel" onclick="addExistingChannelClicked()">Add</button>
                        
                    </div>
                    <h3><a href="#" id="createNew">Create New</a></h3>
                    <div>
                        <span>Channel name:</span> 
                        <br />
                        <input type="text" id="newChannelName"/>
                        <br />
                        <span>Password</span>
                        <input type="password" id="newChannelPassword"/>
                        <br />
                        <button id="createNewChannel" onclick="createNewChannelButtonClicked()">Create</button>
                    </div>
                    <h3><a href="#" id="aboutButton">About</a></h3>
                </div>
            </nav>    
            <aside id="imageLogo">  
                <img width="100%" src="Content/enigma.jpg" />
            </aside>   
        </section>
        <section id="mainContent">
            <article id="loadedChannelsContent">
                <div id="loadedChannelsTabs">
                    <ul id="loadedChannelsNamesList">
                        <li><a href="#emptyDiv">Select a channel</a></li>
                    </ul>
                    <div id="emptyDiv">
                        <p>
                            Please note that this product does not have
                            access to your data or password as plaintext
                            which means that it can be decrypted only locally,
                            leading to an exceptionally high level of data privacy.
                            For more information refer to the About>Technology tab.
                        </p>
                    </div>
                </div>
            </article>
            <article id="controlsMenu" class="controls">
                <button id="listPostsButton">List last posts</button>
                <button id="postMessageButton">Post a message</button>
                <button id="postLinkButton">Post a link</button>
                <button id="postFileButton">Post a file</button>
            </article>
        </section>
        <footer id="credits">
            <span> Copyright © 2012 Mario Dimitrov </span>
        </footer>
        <div id="aboutContentWindow"  >
            <div id="aboutContent" title="About" >
                <ul>
                    <li><a href="#technology">Technology</a></li>
                    <li><a href="#history">Alan Turing</a></li>
                    <li><a href="#contacts">Contacts</a></li>
                </ul>
                <div id="technology">
                    <img alt="architecture" src="Content/architecture.png" />
                </div>
                <div id="history">
                    <iframe width="400px" height="400px" src="http://en.wikipedia.org/wiki/Alan_Turing"></iframe>
                </div>
                <div id="contacts">
                    <span>
                        Mario Dimitrov
                    </span>
                    <br />
                    <span>
                        eAddress: mario_dmtrv[at]yahoo[dot]com
                    </span>
                    <br />
                    <span>
                        FMI, Sofia University
                    </span>
                    <br />
                    <img alt="Contact" src="Content/contact.png" />
                </div>
            </div>
        </div>

        <div id="listPostsForm" title="List most recent posts">
            <span>Channel Name: </span>
            <span id="channelNameSpanList" class="channelNameInForm"></span>
            <button onclick="connectToListChannel()">Connect</button>
            <br />
            <span>Password: </span><input type="password" required="required" id="listPassword"/>
            <br />
            <p>
                <label for="amount">Load posts:</label>
                <input type="number" min="1" max="150" readonly="readonly" required="required" id="postsCountAmount"/>
            </p>

            <div id="postsCountSlider"></div>
            <button id="listPosts" value="List" onclick="listPostsButtonClicked()">List</button>
            <p> <span id="postsFormErrorConsole" class="errorConsole"></span></p> 
        </div>
        <div id="postMessageForm" title="Post a message">
            <span>Channel Name: </span>
            <span id="channelNameSpanMessage" class="channelNameInForm"></span>
            <button onclick="connectToMessageChannel()">Connect</button>
            <br />
            <span>Password: </span><input type="password" id="messagePassword"/>
            <br />
            
            <textarea id="messageContent" rows="5" cols="30" placeholder="Message content here" required="required"></textarea>
            <br />
            <button id="postMessage" value="Post" onclick="postMessageClicked()">Post</button>
            <p> <span id="messagesFormErrorConsole" class="errorConsole"></span></p> 
        </div>
        <div id="postLinkForm" title="Post a link">
            <span>Channel Name: </span>
            <span id="channelNameSpanLink" class="channelNameInForm"></span>
            <button onclick="connectToLinkChannel()">Connect</button>
            <br />
            <span>Password: </span><input type="password" id="linkPassword"/>
            <br />
            <span>URL: </span>
            <input type="url" id="linkUrl"/>
            <br />
            <textarea id="linkDescription" rows="5" cols="30" placeholder="Link description here" required="required"></textarea>
            <br />
            <button id="postLink" value="Post" onclick="postLinkClicked()">Post</button>
            <p> <span id="linksFormErrorConsole" class="errorConsole"></span></p> 
        </div>
        <div id="postFileForm" title="Post an image or a file">
       <script>
           function connectToFileChannel() {
               var channelName = htmlEncode(selectedChannelName());
               getChannelSalt(channelName);
           }
           
           function performAction() {
               $("#<%=channelNameSpanFileSecret.ClientID%>").val(htmlEncode($("#channelNameSpanFile").text()));
               var pass = htmlEncode($("#filePassword").val());
               $("#<%=filePasswordSecret.ClientID%>").val(protectPassword(pass, channelSalt));
           }
       </script>
           <span>Channel Name: </span>
            <span id="channelNameSpanFile" class="channelNameInForm"></span>
            <button onclick="connectToFileChannel()">Connect</button>
            <br />
            <span>Password: </span><input type="password" id="filePassword"/>
            <br />
           <form id="form1" runat="server" enableviewstate="true">
            <div style="visibility: hidden">
            <asp:TextBox Visible="True" id="channelNameSpanFileSecret" runat="server"></asp:TextBox>
           <br />
          <asp:TextBox runat="server" Visible="True" ID="filePasswordSecret" TextMode="Password"></asp:TextBox>
            <br/>
            </div>
            <asp:FileUpload  ID="FileUploadControl" runat="server"/>
                 <asp:Button runat="server" ID="UploadButton" Text="Upload" OnClientClick="performAction()" OnClick="UploadButton_Click" /><br /><br />
            <asp:Label  runat="server" ID="notifyMessage" ForeColor="Red" Font-Bold="true"></asp:Label>
            <asp:HiddenField runat="server" ID="hiddenUploadPhotoUrl" />
            </form>
        <!--  <button id="postFile" value="Post" onclick="postFileClicked()">Post</button> -->
            <p> <span id="fileFormErrorConsole" class="errorConsole"></span></p> 
        </div>
    </body>
</html>

