using iliveit.MessagingAPI.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Script.Serialization;

namespace iliveit.MessagingAPI.Structs
{
    public class ScreenSize
    {
        [JsonProperty("width")]
        public int Width;
        [JsonProperty("height")]
        public int Height;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public ScreenSize()
        {

        }
    }
}
