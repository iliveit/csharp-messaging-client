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
    public class NewMessage
    {
        /// <summary>
        /// The action to be taken by the API
        /// </summary>
        public APIActionTypes Action;
        /// <summary>
        /// The Operator this message belongs to
        /// </summary>
        public int MVNOID;
        /// <summary>
        /// The data for that is required for building or submitting the message
        /// </summary>
        public IMessageData Data;
        /// <summary>
        /// The Campaign id for tracking purposes (optional)
        /// </summary>
        public string Campaign;
        /// <summary>
        /// A URL where SMS replies to this message should be POSTed. Applies only to SMS messages (optional)
        /// </summary>
        public string PostbackReplyUrl;
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
        /// Message will not be submitted before this date and time
        /// </summary>
        public string SubmitNotBefore;
        /// <summary>
        /// Message will not be submitted after this date and time
        /// </summary>
        public string SubmitNotAfter;

        /// <summary>
        /// The last error that occurred within Validate()
        /// </summary>
        [JsonIgnore]
        public string Error {get; private set;}
        
        /// <summary>
        /// Constructor
        /// </summary>
        public NewMessage()
        {

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
            if (this.Action == 0)
            {
                this.Error = "Action must be set";
                return false;
            }
            if (this.MVNOID == 0)
            {
                this.Error = "MVNOID must be set and not zero";
                return false;
            }
            
            if (this.Action == APIActionTypes.SubmitMMS)
            {
                SubmitMMSMessageData data = (SubmitMMSMessageData)this.Data;
                if (data.Slides.Count == 0)
                {
                    this.Error = "MMS messages must have at least one slide";
                    return false;
                }
                if (data.Subject == "")
                {
                    this.Error = "MMS message must have a subject set";
                    return false;
                }
                if (this.Data.MSISDN.Count == 0)
                {
                    this.Error = "A message must have at least one recipient set in MSISDN";
                    return false;
                }
            }
            else if (this.Action == APIActionTypes.SubmitSMS)
            {
                SubmitSMSMessageData data = (SubmitSMSMessageData)this.Data;
                if (data.Message == "")
                {
                    this.Error = "SMS messages must have a Message set";
                    return false;
                }
                if (this.Data.MSISDN.Count == 0)
                {
                    this.Error = "A message must have at least one recipient set in MSISDN";
                    return false;
                }
            }
            else if (this.Action == APIActionTypes.SubmitEmail)
            {
                SubmitEmailMessageData data = (SubmitEmailMessageData)this.Data;
                if (data.Address.Count == 0)
                {
                    this.Error = "Email messages must have at least one recipient listed in Address";
                }
                if (data.HTML == "" && data.Text == "")
                {
                    this.Error = "Email messages must have either HTML or Text set, or both";
                    return false;
                }
                if (data.Subject == "")
                {
                    this.Error = "Email messages must have a subject";
                    return false;
                }
            }
            return true;
        }

    }
}
