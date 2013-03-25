using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SecretCommunicator.SecretCommunicatorDAL;

namespace SecretCommunicator.PostableDocuments
{
    /// <summary>
    /// channel name, password, URL and description
    /// </summary>
    [DataContract]
    public class Link:Postable
    {
        [DataMember(Name = "linkUrl")]
        public string Url { get; set; }

        [DataMember(Name = "linkDescription")]
        public string Description { get; set; }

        public Link(string url, string description)
        {
            Url = url;
            Description = description;
            DocType = "link";
        }

        public override void Upload(string channelName)
        {
            var channel = ChannelsDAL.GetChannel(channelName);
            channel.Insert(this);
        }
    }
}