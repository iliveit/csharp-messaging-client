using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iliveit.MessagingAPI.Enums
{
    public enum APIActionTypes
    {
        /// <summary>
        /// Submit the message as MMS
        /// </summary>
        SubmitMMS = 1,
        /// <summary>
        /// Submit the message as SMS
        /// </summary>
        SubmitSMS = 2,
        /// <summary>
        /// Submit the message as Email
        /// </summary>
        SubmitEmail = 3,
        /// <summary>
        /// Archive will store the result for later retrieval
        /// </summary>
        Archive = 4
    }
}
