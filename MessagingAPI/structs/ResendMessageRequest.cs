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
    public class ResendMessageRequest
    {
        /// <summary>
        /// The MessageID to resubmit
        /// </summary>
        public string MessageID;
        /// <summary>
        /// The MSISDN to submit to
        /// </summary>
        public string MSISDN;
        /// <summary>
        /// The Email to submit to
        /// </summary>
        public string Email;
        /// <summary>
        /// A URL where status updates for this message should be POSTed (optional)
        /// </summary>
        public string PostbackStatusUrl;
        /// <summary>
        /// Which updates you want to receive in the postback
        /// Possible values of "build", "submit", "archive", "sent", "delivery"
        /// comma delimited - i.e. "build,submit,delivery"
        /// </summary>
        public string PostBackStatusTypes;
        /// <summary>
        /// The last error that occurred within Validate()
        /// </summary>
        [JsonIgnore]
        public string Error {get; private set;}
        
        /// <summary>
        /// Constructor
        /// </summary>
        public ResendMessageRequest()
        {
            this.MSISDN = "";
        }

        /// <summary>
        /// Packages the message into JSON
        /// </summary>
        /// <returns>The JSON string</returns>
        public string Package()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }

        /// <summary>
        /// Validates the message before sending via the API
        /// </summary>
        /// <returns>true if valid, false otherwise</returns>
        public bool Validate()
        {
            if (this.MessageID == "")
            {
                this.Error = "MessageID must be specified";
                return false;
            }
            if (this.MSISDN == "" && this.Email == "")
            {
                this.Error = "You must specify an MSISDN or Email address to resend to";
                return false;
            }
            
            return true;
        }

    }
}
