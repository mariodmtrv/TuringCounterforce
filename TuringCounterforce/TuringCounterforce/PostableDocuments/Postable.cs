using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace SecretCommunicator.PostableDocuments
{
    [DataContract]
    public abstract class Postable
    { [DataMember(Name = "docType")]
        public string DocType { get; set; }
        
        public abstract void Upload(string channelName);

       public string ToJson()
       {
           object obj = (object) this;
           return JsonConvert.SerializeObject(obj);
       }
    }
}
