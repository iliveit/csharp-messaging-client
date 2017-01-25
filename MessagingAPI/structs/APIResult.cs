using iliveit.MessagingAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iliveit.MessagingAPI.Structs
{
    public class APIResult
    {
        public APIResultStatuses StatusCode;
        public string StatusDescription;
        public NewMessageResult MessageResult;
        public ScrubResult ScrubResult;
        public MessageStatus MessageStatus;
        public MessageStatusExtended MessageStatusExtended;
        public ArchivedMessage ArchivedMessage;
    }
}
