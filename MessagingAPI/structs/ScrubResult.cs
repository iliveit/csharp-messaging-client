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
    public class ScrubResult
    {
        [JsonProperty("msisdn")]
        public string MSISDN;
        [JsonProperty("network")]
        public string Network;
        [JsonProperty("handset_make")]
        public string HandsetMake;
        [JsonProperty("handset_model")]
        public string HandsetModel;
        [JsonProperty("is_mms_provisioned")]
        public bool IsMMSProvisioned;
        [JsonProperty("is_mms_capable")]
        public bool IsMMSCapable;
        [JsonProperty("allow_send")]
        public bool AllowSend;
        [JsonProperty("screen_size")]
        public ScreenSize ScreenSize;
        [JsonProperty("error_code")]
        public int ErrorCode;
        [JsonProperty("error")]
        public string Error;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public ScrubResult()
        {

        }
    }
}
