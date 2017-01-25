using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iliveit.MessagingAPI.Enums;
using iliveit.MessagingAPI.Helpers;
using iliveit.MessagingAPI.Structs;
using System.Net;
using System.IO;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace iliveit.MessagingAPI
{
    public class MessagingAPI
    {
        private APIConfig _config;

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="access_token">Your application access token</param>
        public MessagingAPI(APIConfig config)
        {
            string validation = config.Validate();
            if (validation != "true")
            {
                throw new MissingFieldException(validation + " can not be blank");
            }
            this._config = config;
        }

        /// <summary>
        /// Handles the response result
        /// </summary>
        /// <param name="result">The current result that will be modified in place</param>
        private void HandleExceptionResponse(ref APIResult result, WebException ex)
        {
            if (ex.Response != null)
            {
                HttpStatusCode statusCode = ((System.Net.HttpWebResponse)ex.Response).StatusCode;
                string response = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                int statusInt = (int)statusCode;
                if (statusCode != HttpStatusCode.BadGateway)
                {
                    dynamic obj = Json.Decode(response);
                    if (statusInt == 429)
                        result.StatusCode = APIResultStatuses.RateLimited;
                    else if (statusCode == HttpStatusCode.BadRequest)
                        result.StatusCode = APIResultStatuses.Error;
                    else result.StatusCode = APIResultStatuses.AuthFailed;
                    result.StatusDescription = obj.Error;
                }
                else
                {
                    result.StatusCode = APIResultStatuses.APIError;
                    result.StatusDescription = "An internal API error occurred. Please try again later.";
                }
            }
            else
            {
                result.StatusCode = APIResultStatuses.APIError;
                result.StatusDescription = "No connection could be made to the API.";
            }
        }

        /// <summary>
        /// Sends an authenticated request to the API to check if
        /// access token is working
        /// </summary>
        public APIResult Ping()
        {
            APIResult result = new APIResult();
            APIWebRequest r = new APIWebRequest(this._config, "ping", "GET");
            try
            {
                string response = r.GetResponse();
                result.StatusCode = APIResultStatuses.Ok;
                result.StatusDescription = "Ok";
            }
            catch (WebException ex)
            {
                HandleExceptionResponse(ref result, ex);
            }
            return result;
        }

        /// <summary>
        /// Requests a new message to be submitted via the API
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <returns>The API result of the operation (including message ID)</returns>
        public APIResult Create(NewMessage message)
        {
            APIResult result = new APIResult();

            if (message.Validate())
            {
                string messageJSON = message.Package();
                
                APIWebRequest r = new APIWebRequest(this._config, "message/send", "POST", messageJSON);
                try
                {
                    string response = r.GetResponse();
                    result.StatusCode = APIResultStatuses.Ok;
                    result.StatusDescription = "Ok";

                    try
                    {
                        dynamic obj = Json.Decode(response);
                        NewMessageResult message_result = new NewMessageResult();
                        message_result.MessageID = obj.MessageID;
                        result.MessageResult = message_result;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    
                }
                catch (WebException ex)
                {
                    HandleExceptionResponse(ref result, ex);
                }
            }
            else
            {
                throw new MissingFieldException(message.Error);
            }

            return result;
        }

        /// <summary>
        /// Requests a video to be generated via the API
        /// </summary>
        /// <param name="request">The request data to be used in building</param>
        /// <returns>The API result of the operation (including message ID)</returns>
        public APIResult Generate(BuildRequest request)
        {
            APIResult result = new APIResult();

            if (request.Validate())
            {
                string messageJSON = request.Package();
                

                APIWebRequest r = new APIWebRequest(this._config, "generate/video", "POST", messageJSON);
                try
                {
                    string response = r.GetResponse();
                    result.StatusCode = APIResultStatuses.Ok;
                    result.StatusDescription = "Ok";

                    try
                    {
                        dynamic obj = Json.Decode(response);
                        NewMessageResult message_result = new NewMessageResult();
                        message_result.MessageID = obj.MessageID;
                        result.MessageResult = message_result;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                catch (WebException ex)
                {
                    HandleExceptionResponse(ref result, ex);
                }
            }
            else
            {
                throw new MissingFieldException(request.Error);
            }

            return result;
        }

        /// <summary>
        /// Scrub determines if an MSISDN can receive an MMS
        /// </summary>
        /// <param name="MSISDN">The MSISDN to scrub</param>
        /// <returns>The result of the scrub</returns>
        public APIResult Scrub(string MSISDN)
        {
            APIResult result = new APIResult();

            if (string.IsNullOrEmpty(MSISDN))
                throw new MissingFieldException("You must supply a non-empty MSISDN");
            
            APIWebRequest r = new APIWebRequest(this._config, "scrub/" + MSISDN, "GET");
            try
            {
                string response = r.GetResponse();
                result.StatusCode = APIResultStatuses.Ok;
                result.StatusDescription = "Ok";

                try
                {
                    ScrubResult scrub_result = JsonConvert.DeserializeObject<ScrubResult>(response);
                    result.ScrubResult = scrub_result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (WebException ex)
            {
                HandleExceptionResponse(ref result, ex);
            }
            

            return result;
        }

        /// <summary>
        /// Requests a message to be resent, optionally to a different MSISDN
        /// </summary>
        /// <param name="message">The message to be resent</param>
        /// <returns>The API result of the operation (including message ID)</returns>
        public APIResult Resend(ResendMessageRequest message)
        {
            APIResult result = new APIResult();

            if (message.Validate())
            {
                string messageJSON = message.Package();

                APIWebRequest r = new APIWebRequest(this._config, "message/resend", "POST", messageJSON);
                try
                {
                    string response = r.GetResponse();
                    result.StatusCode = APIResultStatuses.Ok;
                    result.StatusDescription = "Ok";

                    try
                    {
                        dynamic obj = Json.Decode(response);
                        NewMessageResult message_result = new NewMessageResult();
                        message_result.MessageID = obj.MessageID;
                        result.MessageResult = message_result;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                catch (WebException ex)
                {
                    HandleExceptionResponse(ref result, ex);
                }
            }
            else
            {
                throw new MissingFieldException(message.Error);
            }

            return result;
        }

        /// <summary>
        /// GetMessageStatus retrieves the message's status
        /// </summary>
        /// <param name="message_id">The message_id to retrieve</param>
        /// <returns>The current status of the message</returns>
        public APIResult GetMessageStatus(string message_id)
        {
            APIResult result = new APIResult();

            if (string.IsNullOrEmpty(message_id))
                throw new MissingFieldException("You must supply a non-empty Message ID");

            APIWebRequest r = new APIWebRequest(this._config, "message/" + message_id + "/status", "GET");
            try
            {
                string response = r.GetResponse();
                result.StatusCode = APIResultStatuses.Ok;
                result.StatusDescription = "Ok";

                try
                {
                    MessageStatus status = JsonConvert.DeserializeObject<MessageStatus>(response);
                    result.MessageStatus = status;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (WebException ex)
            {
                HandleExceptionResponse(ref result, ex);
            }
            return result;
        }

        /// <summary>
        /// GetMessageStatus retrieves the message's status
        /// </summary>
        /// <param name="message_id">The message_id to retrieve</param>
        /// <returns>The current status of the message</returns>
        public APIResult GetMessageStatusExtended(string message_id)
        {
            APIResult result = new APIResult();

            if (string.IsNullOrEmpty(message_id))
                throw new MissingFieldException("You must supply a non-empty Message ID");

            APIWebRequest r = new APIWebRequest(this._config, "message/" + message_id + "/statusExtended", "GET");
            try
            {
                string response = r.GetResponse();
                result.StatusCode = APIResultStatuses.Ok;
                result.StatusDescription = "Ok";

                try
                {
                    MessageStatusExtended status = JsonConvert.DeserializeObject<MessageStatusExtended>(response);
                    result.MessageStatusExtended = status;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (WebException ex)
            {
                HandleExceptionResponse(ref result, ex);
            }
            return result;
        }

        /// <summary>
        /// GetMessage retrieves the message from the archive
        /// </summary>
        /// <param name="message_id">The message_id to retrieve</param>
        /// <returns>The full message that was sent</returns>
        public APIResult GetMessage(string message_id)
        {
            APIResult result = new APIResult();

            if (string.IsNullOrEmpty(message_id))
                throw new MissingFieldException("You must supply a non-empty Message ID");

            APIWebRequest r = new APIWebRequest(this._config, "message/" + message_id, "GET");
            try
            {
                string response = r.GetResponse();
                result.StatusCode = APIResultStatuses.Ok;
                result.StatusDescription = "Ok";

                try
                {
                    ArchiveResponse arcResponse = JsonConvert.DeserializeObject<ArchiveResponse>(response);
                    ArchivedMessage archive = arcResponse.Data;
                    if (arcResponse.Type == "sms")
                    {
                        archive.MessageType = "sms";
                        archive.Message = archive.Text;
                    }
                    else if (arcResponse.Type == "email")
                    {
                        archive.MessageType = "email";
                    }
                    result.ArchivedMessage = archive;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (WebException ex)
            {
                HandleExceptionResponse(ref result, ex);
            }


            return result;
        }

    }
}
