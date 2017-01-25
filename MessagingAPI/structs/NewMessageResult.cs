using iliveit.MessagingAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Script.Serialization;

namespace iliveit.MessagingAPI.Structs
{
    public class NewMessageResult
    {
        /// <summary>
        /// The MessageID assigned by the API
        /// </summary>
        public string MessageID;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public NewMessageResult()
        {

        }

    }
}
