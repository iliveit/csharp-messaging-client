using iliveit.MessagingAPI.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iliveit.MessagingAPI.Interfaces
{
    public interface IMessageData
    {
        [JsonProperty("message_type")]
        string MessageType { get; set; }
        /// <summary>
        /// The MSISDN for the message. Required.
        /// </summary>
        [JsonProperty("msisdn")]
        List<string> MSISDN {get; set;}
        /// <summary>
        /// The network to submit via. Leave blank to have the
        /// message API decide on the network based on portability and scrub
        /// </summary>
        [JsonProperty("network")]
        string Network { get; set; }
    }
}
