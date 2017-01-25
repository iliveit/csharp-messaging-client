using iliveit.MessagingAPI.Enums;
using iliveit.MessagingAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iliveit.MessagingAPI.Structs;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace iliveit.MessagingAPI.Structs
{
    [DataContract]
    public class MMSSlideContent
    {
        /// <summary>
        /// The type of slide
        /// </summary>
        [JsonProperty("type")]
        public MMSSlideTypes Type;
        /// <summary>
        /// The mime type of the content of the slide
        /// </summary>
        [JsonProperty("mime")]
        public string Mime;
        /// <summary>
        /// The data used in the slide, base64 encoded
        /// </summary>
        [JsonProperty("data")]
        public string Data;
        /// <summary>
        /// The name to be used for the slide content
        /// </summary>
        [JsonProperty("name")]
        public string Name;
        /// <summary>
        /// Constructor
        /// </summary>
        public MMSSlideContent()
        {
            
        }

    }
}
