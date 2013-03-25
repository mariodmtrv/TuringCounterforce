using System;
using System.IO;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using SecretCommunicator.SecretCommunicatorDAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SecretCommunicator.PostableDocuments
{
    /// <summary>
    /// channel name, password and message text
    /// </summary>
    [DataContract]
    public class Message:Postable
    {
        [DataMember(Name = "messageContent")]
        public string MessageContent { get; set; }

        public Message(string messageContent)
        {
            MessageContent = messageContent;
            DocType = "message";
        }

        public override void Upload(string channelName)
        {
            var channel = ChannelsDAL.GetChannel(channelName);
            channel.Insert(this);
        }
    }
}