using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iliveit.MessagingAPI.Enums
{
    public enum APIResultStatuses
    {
        /// <summary>
        /// No error occurred
        /// </summary>
        Ok = 0,
        /// <summary>
        /// Generic error occurred. See description for details
        /// </summary>
        Error = 1,
        /// <summary>
        /// Returned when you supply the incorrect access token or
        /// have an invalid authentication implementation
        /// </summary>
        AuthFailed = 2,
        /// <summary>
        /// Returned when you issue a POST to a GET endpoint and vice versa
        /// </summary>
        InvalidMethod = 3,
        /// <summary>
        /// HTTP 5XX errors return this code indicating an issue with the API
        /// </summary>
        APIError = 4,
        /// <summary>
        /// Returned when you exceeded your maximum messages per second
        /// </summary>
        RateLimited = 5
    }
}
