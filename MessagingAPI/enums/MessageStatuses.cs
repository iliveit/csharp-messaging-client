using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iliveit.MessagingAPI.Enums
{
    public enum MessageStatuses
    {
        /// <summary>
        /// Received by API
        /// </summary>
        MessageStatusReceived = 1,
        /// <summary>
        /// Submitted to Beam
        /// </summary>
        MessageStatusSubmitted = 2,
        /// <summary>
        /// Submission to Beam failed
        /// </summary>
        MessageStatusSubmitFailed = 3,
        /// <summary>
        /// Archived
        /// </summary>
        MessageArchived = 4,
        /// <summary>
        /// Archived because of network action
        /// </summary>
        MessageArchivedNetworkRule = 5,
        /// <summary>
        /// Invalid JSON output
        /// </summary>
        MessageStatusFailedMarshal = 6,
        /// <summary>
        /// Failed due to bad JSON
        /// </summary>
        MessageStatusFailedUnmarshal = 7,
        /// <summary>
        /// When the scrub service is unavailable or unable to scrub an MSISDN
        /// </summary>
        MessageStatusScrubUnavailable = 8,
        /// <summary>
        /// Status when the MSISDN is not MMS capable
        /// </summary>
        MessageStatusNotMMS = 9,
        /// <summary>
        /// When failed to build message from a template
        /// </summary>
        MessageStatusTemplateError = 10,
        /// <summary>
        /// No network found for this message, cannot be sent
        /// </summary>
        MessageStatusNoNetwork = 11,
        /// <summary>
        /// Error submitting to renderfarm
        /// </summary>
        MessageStatusRenderFarmFailed = 12,
        /// <summary>
        /// Sent to for rendering
        /// </summary>
        MessageStatusRenderFarmSent = 13,
        /// <summary>
        /// Success render
        /// </summary>
        MessageStatusRenderFarmSuccess = 14,
        /// <summary>
        /// Unable to read file
        /// </summary>
        MessageStatusReadFileFailed = 15,
        /// <summary>
        /// Invalid data received
        /// </summary>
        MessageInvalid = 16,
        /// <summary>
        /// No handset information available for MSISDN
        /// </summary>
        MessageStatusNoHandsetInfo = 17,
        /// <summary>
        /// MSISDN is not provisioned for MMS
        /// </summary>
        MessageStatusNotMMSProvisioned = 18
    }
}
