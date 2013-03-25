using System.IO;
using System.Runtime.Serialization;
using SecretCommunicator.SecretCommunicatorDAL;

namespace SecretCommunicator.PostableDocuments
{
    [DataContract]
    public class File : Postable
    {
        [DataMember(Name = "fileUrl")]
        public string Url { get; set; }

        [DataMember(Name = "fileMimeType")]
        public string MimeType { get; set; }

        public File(string url, string mimeType)
        {
            Url = url;
            DocType = "file";
            MimeType = mimeType;
        }

        public override void Upload(string channelName)
        {
            var channel = ChannelsDAL.GetChannel(channelName);
            channel.Insert(this);
        }
    }
}