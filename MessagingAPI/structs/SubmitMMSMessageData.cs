using iliveit.MessagingAPI.Enums;
using iliveit.MessagingAPI.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace iliveit.MessagingAPI.Structs
{
    public class SubmitMMSMessageData : IMessageData
    {
        public string MessageType { get; set; }
        public List<string> MSISDN { get; set; }
        public string Network { get; set; }

        /// <summary>
        /// List of slides to use for the message
        /// </summary>
        [JsonProperty("slides")]
        public List<MMSSlide> Slides { get; set; }

        /// <summary>
        /// The MMS message subject
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public SubmitMMSMessageData()
        {
            this.MessageType = "mms";
            this.Network = "*";
            this.Subject = "";
            this.MSISDN = new List<string>();
            this.Slides = new List<MMSSlide>();
        }

    }
}
