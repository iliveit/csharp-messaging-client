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
    public class SubmitEmailMessageData : IMessageData
    {
        /// <summary>
        /// MessageType for Emails are always 'email'
        /// </summary>
        public string MessageType { get; set; }
        /// <summary>
        /// A list of email addresses to send to
        /// </summary>
        [JsonProperty("address")]
        public List<string> Address { get; set; }
        /// <summary>
        /// MSISDN is not used for email messages
        /// </summary>
        public List<string> MSISDN { get; set; }
        /// <summary>
        /// The network (or account) is the account used when sending the email
        /// </summary>
        public string Network { get; set; }

        /// <summary>
        /// The Email message subject
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// The HTML body of the email
        /// </summary>
        [JsonProperty("html")]
        public string HTML { get; set; }
        /// <summary>
        /// The plain-text body of the email
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
        /// <summary>
        /// Attachments foor the email
        /// </summary>
        [JsonProperty("attachments")]
        public List<EmailAttachment> Attachments { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public SubmitEmailMessageData()
        {
            this.MessageType = "email";
            this.Network = "any";
            this.Subject = "";
            this.Address = new List<string>();
            this.HTML = "";
            this.Text = "";
            this.Attachments = new List<EmailAttachment>();
        }

    }
}
