using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iliveit.MessagingAPI.Structs
{
    /// <summary>
    /// Holds the API configuration
    /// </summary>
    public class APIConfig
    {
        public string Endpoint;
        public string AccessToken;

        /// <summary>
        /// Constructor
        /// </summary>
        public APIConfig()
        {

        }

        /// <summary>
        /// Validates that all fields required are provided
        /// </summary>
        /// <returns>The fields required, "true" otherwise</returns>
        public string Validate()
        {
            if (this.AccessToken == null || this.AccessToken == "")
            {
                return "Access Token";
            }
            if (this.Endpoint == null || this.Endpoint == "")
            {
                return "Endpoint";
            }

            // Add trailing slash if not present
            if (this.Endpoint[this.Endpoint.Length - 1] != '/')
            {
                this.Endpoint = this.Endpoint + "/";
            }
            return "true";
        }
    }
}
