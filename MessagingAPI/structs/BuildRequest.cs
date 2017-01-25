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
    public class BuildRequest
    {
        /// <summary>
        /// The Operator this message belongs to
        /// </summary>
        public int MVNOID;
        /// <summary>
        /// The data for that is required for building the message
        /// </summary>
        public object Data;
        /// <summary>
        /// The Campaign id for tracking purposes (optional)
        /// </summary>
        public string Campaign;
        /// <summary>
        /// The template to be used when building the message
        /// </summary>
        public int BuildTemplate;
        /// <summary>
        /// The action to be taken by the API after the message has been built
        /// </summary>
        public APIActionTypes AfterBuildAction;
        /// <summary>
        /// The data required for the AfterBuildAction
        /// </summary>
        public object AfterBuildData;
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
        /// This will force the message to be built as a certain size.
        /// Options: High, Low
        /// </summary>
        public string ForcedSize;
        /// <summary>
        /// The last error that occurred within Validate()
        /// </summary>
        [JsonIgnore]
        public string Error {get; private set;}
        /// <summary>
        /// Message will not be submitted before this date and time
        /// </summary>
        public string SubmitNotBefore;
        /// <summary>
        /// Message will not be submitted after this date and time
        /// </summary>
        public string SubmitNotAfter;

        /// <summary>
        /// Constructor
        /// </summary>
        public BuildRequest()
        {
            this.Data = "";
        }

        /// <summary>
        /// Packages the message into JSON
        /// </summary>
        /// <returns>The JSON string</returns>
        public string Package()
        {
            if (!(this.Data is string))
            {
                this.Data = JsonConvert.SerializeObject(this.Data);
            }
            this.AfterBuildData = JsonConvert.SerializeObject(this.AfterBuildData);
            string json = JsonConvert.SerializeObject(this);
            return json;
        }

        /// <summary>
        /// Validates the message before sending via the API
        /// </summary>
        /// <returns>true if valid, false otherwise</returns>
        public bool Validate()
        {
            if (this.AfterBuildAction == 0)
            {
                this.Error = "AfterBuildAction must be set";
                return false;
            }
            if (this.MVNOID == 0)
            {
                this.Error = "MVNOID must be set and not zero";
                return false;
            }
            if (this.Data == "")
            {
                this.Error = "A build request must have data";
                return false;
            }
            if (this.AfterBuildAction != APIActionTypes.Archive)
            {
                if (this.AfterBuildData == null)
                {
                    this.Error = "If the AfterBuildAction is not Archive, AfterBuildData needs to be specified";
                    return false;
                }

                if (this.AfterBuildAction == APIActionTypes.SubmitMMS)
                {
                    if (!(this.AfterBuildData is SubmitMMSMessageData))
                    {
                        this.Error = "Using AfterAction of SubmitMMS requires AfterBuildData to be of type SubmitMMSMessageData";
                        return false;
                    }
                }
                
            }
            if (this.BuildTemplate == 0)
            {
                this.Error = "A build template must be selected";
                return false;
            }
            return true;
        }

        /// <summary>
        /// CreateDynamicSlide is a placeholder used by the API to
        /// insert all dynamically generated slide content into this position
        /// </summary>
        /// <returns>The MMSSlide placeholder</returns>
        public MMSSlide CreateDynamicSlide()
        {
            MMSSlide slide = new MMSSlide();

            return slide;
        }

    }
}
