using iliveit.MessagingAPI.Enums;
using iliveit.MessagingAPI.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Script.Serialization;

namespace iliveit.MessagingAPI.Structs
{
    public class EmailAttachment
    {
        /// <summary>
        /// The name you wish to appear in the email
        /// </summary>
        [JsonProperty("filename")]
        public string Filename { get; set; }
        /// <summary>
        /// Base64 converted data of the attachment
        /// </summary>
        [JsonProperty("data")]
        public string Base64Data { get; set; }
    }
}
