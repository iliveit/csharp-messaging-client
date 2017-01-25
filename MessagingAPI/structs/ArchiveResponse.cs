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
    public class ArchiveResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }
        [JsonProperty("data")]
        public ArchivedMessage Data { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ArchiveResponse()
        {
            this.Type = "mms";
        }

    }
}
