using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SecretCommunicator.SecretCommunicatorDAL;

namespace SecretCommunicator
{
    [DataContract]
    public class Channel
    {
        public ObjectId Id { get; set; }

        [DataMember(Name = "channelName")]
        public string Name { get; set; }

        [DataMember(Name = "channelPassword")]
        public string Password { get; set; }

    /*    [DataMember(Name = "postsCount")]
        public string PostsCount { get; set; }*/

        [DataMember(Name = "channelSalt")]
        public string Salt { get; set; }

        public Channel()
        {
            Name = "";
            Password = "";
            Salt = Guid.NewGuid().ToString();
            ChannelsDAL.CreateEmptyChannel(this);
        }
        public static string CreateEmpty()
        {
            Channel newChannel=new Channel();
            return newChannel.Salt;
        }
        
    }
}