using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace iliveit.MessagingAPI.Structs
{
    /// <summary>
    /// Message slide types
    /// </summary>
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MMSSlideTypes
    {
        [EnumMember(Value = "text")]
        Text,
        [EnumMember(Value = "image")]
        Image,
        [EnumMember(Value = "video")]
        Video,
        [EnumMember(Value = "audio")]
        Audio
    }
}
