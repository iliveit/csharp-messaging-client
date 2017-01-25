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
    public class SubmitSMSMessageData : IMessageData
    {
        public string MessageType { get; set; }
        public List<string> MSISDN { get; set; }
        public string Network { get; set; }

        /// <summary>
        /// Message is the actual message content of the SMS
        /// </summary>
        [JsonProperty("text")]
        public string Message { get; set; }

        /// <summary>
        /// Extra digits to append to sender address, when allowed
        /// </summary>
        [JsonProperty("extra_digits")]
        public string ExtraDigits { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public SubmitSMSMessageData()
        {
            this.MessageType = "sms";
            this.Network = "any";
            this.MSISDN = new List<string>();
            this.Message = "";
        }

    }
}
