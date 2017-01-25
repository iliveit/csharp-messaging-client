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
    public class MessageStatusExtended
    {
        [JsonProperty("type")]
        public string Type;
        [JsonProperty("campaign")]
        public string Campaign;
        [JsonProperty("message_id")]
        public string MessageID;
        [JsonProperty("template")]
        public int Template;
        [JsonProperty("network")]
        public string Network;
	    [JsonProperty("msisdn")]
        public string MSISDN;
	    [JsonProperty("email")]
        public string Email;
	    [JsonProperty("mvno")]
        public int MVNO;
	    [JsonProperty("date_received")]
        public DateTime DateReceived;
	    [JsonProperty("build_status")]
        public int BuildStatus;
	    [JsonProperty("build_status_description")]
        public string BuildStatusDescription;
	    [JsonProperty("build_timestamp")]
        public DateTime BuildTimestamp;
	    [JsonProperty("archive_status")]
        public int ArchiveStatus;
	    [JsonProperty("archive_status_description")]
        public string ArchiveStatusDescription;
	    [JsonProperty("archive_timestamp")]
        public DateTime ArchiveTimestamp;
	    [JsonProperty("submit_status")]
        public int SubmitStatus;
	    [JsonProperty("submit_status_description")]
        public string SubmitStatusDescription;
	    [JsonProperty("submit_timestamp")]
        public DateTime SubmitTimestamp;
	    [JsonProperty("sent_status")]
        public int SentStatus;
	    [JsonProperty("sent_status_description")]
        public string SentStatusDescription;
	    [JsonProperty("sent_timestamp")]
        public DateTime SentTimestamp;
	    [JsonProperty("delivered_status")]
        public int DeliveredStatus;
	    [JsonProperty("delivered_status_description")]
        public string DeliveredStatusDescription;
        [JsonProperty("delivered_timestamp")]
        public DateTime DeliveredTimestamp;
        [JsonProperty("postback_type")]
        public string PostbackType;
        [JsonProperty("network_reference")]
        public string NetworkReference;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public MessageStatusExtended()
        {

        }
    }
}
