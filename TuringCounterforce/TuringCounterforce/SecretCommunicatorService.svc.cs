using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using SecretCommunicator.PostableDocuments;
using SecretCommunicator.SecretCommunicatorDAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web.Script.Serialization;
using File = SecretCommunicator.PostableDocuments.File;

namespace SecretCommunicator
{

    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SecretCommunicatorService
    {
        [WebInvoke(
                    Method = "POST",
                    UriTemplate = "get-newSalt",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string GetNewSalt()
        {
            string newSalt=Channel.CreateEmpty();
            return newSalt;
        }
        [WebInvoke(
                    Method = "POST",
                    UriTemplate = "set-up-channel",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public void SetChannelData(string channelName, string channelPassword, string channelSalt)
        {
            bool created = ChannelsDAL.SetChannelData(channelName, channelPassword, channelSalt);
            if(!created)
            {
                throw new FileLoadException("Channel setup unsuccessful");
            }
        }


        [WebInvoke(
                    Method = "POST",
                    UriTemplate = "get-channel-salt",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public string GetChannelSalt(string channelName)
        {
            string salt=ChannelsDAL.GetChannelSalt(channelName);
            return salt;
        }


        [WebInvoke(
                    Method = "POST",
                    UriTemplate = "get-posts",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest)]
         public List<string> GetPosts(string channelName, string channelPassword, int postsCount)
        {
            bool isLogged = true;
            isLogged = ChannelsDAL.LogIntoChannel(channelName, channelPassword);

            if (isLogged)
            {
                List<Postable> resultList = PostsDAL.GetPosts(channelName, postsCount);
                List<string> clientResult = new List<string>();

                foreach (var postable in resultList)
                {
                    clientResult.Add(postable.ToJson());
                }
                
                return clientResult;
            }
            return null;
        }

        [WebInvoke(
                    Method = "POST",
                    UriTemplate = "post-message",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest)] 
        public void PostMessage(string channelName, string channelPassword, string messageContent)
        {
            bool isLogged = true;
            isLogged = ChannelsDAL.LogIntoChannel(channelName, channelPassword);

            if (isLogged)
            {
                Message newMessage = new Message(messageContent);
                newMessage.Upload(channelName);
            }
        }
        [WebInvoke(
                    Method = "POST",
                    UriTemplate = "post-link",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest)]

        public void PostLink(string channelName, string channelPassword, string linkUrl, string linkDescription)
        {
            bool isLogged = true;
            isLogged = ChannelsDAL.LogIntoChannel(channelName, channelPassword);

            if (isLogged)
            {
                Link newLink = new Link(linkUrl, linkDescription);
                newLink.Upload(channelName);
            }
        }

        //uses Dropbox file
        
        [WebInvoke(
                    Method = "POST",
                    UriTemplate = "post-file",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        
        public void PostFile(string channelName, string password)
        {
            bool isLogged = true;
            isLogged = ChannelsDAL.LogIntoChannel(channelName, password);

            if (isLogged)
            {
                //
            }
        }

        //post file...
    }
}

