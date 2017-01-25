using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using iliveit.MessagingAPI;
using iliveit.MessagingAPI.Enums;
using iliveit.MessagingAPI.Structs;
using System.Net;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Uncomment the sample you want to use

            //SamplePing();
            //SampleScrub();
            //SampleGetMessageStatus();
            //SampleGetMessage();
            //SampleJSONString();
            //SamplePostBack();
            //SampleResubmitMessage();

            //SampleSubmitSMS();
            //SampleSubmitMMS();
            //SampleSubmitEmail();
            //SampleSubmitEmailWithAttachments();
            
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        /// <summary>
        /// This sample shows a basic call to the API. The Ping
        /// route checks the access token, so this is regarded as the
        /// easiest way to verify your application correctly authenticates
        /// </summary>
        static void SamplePing()
        {
            APIConfig config = new APIConfig();
            config.AccessToken = ConfigurationManager.AppSettings["APIAccessToken"];
            config.Endpoint = ConfigurationManager.AppSettings["APIEndpoint"];

            MessagingAPI api = new MessagingAPI(config);

            APIResult result = api.Ping();
            if (result.StatusCode != APIResultStatuses.Ok)
            {
                Console.WriteLine(result.StatusCode);
                Console.WriteLine(result.StatusDescription);
            }
            else
            {
                Console.WriteLine("Ping success");
            }
        }

        /// <summary>
        /// This sample shows how to scrub an MSISDN
        /// </summary>
        static void SampleScrub()
        {
            APIConfig config = new APIConfig();
            config.AccessToken = ConfigurationManager.AppSettings["APIAccessToken"];
            config.Endpoint = ConfigurationManager.AppSettings["APIEndpoint"];

            MessagingAPI api = new MessagingAPI(config);

            APIResult result = api.Scrub("XXXXXX");
            if (result.StatusCode != APIResultStatuses.Ok)
            {
                Console.WriteLine(result.StatusCode);
                Console.WriteLine(result.StatusDescription);
            }
            else
            {
                Console.WriteLine("Scrub result");

                Console.WriteLine("MSISDN: " + result.ScrubResult.MSISDN);
                Console.WriteLine("Network: " + result.ScrubResult.Network);
                Console.WriteLine("Handset");
                Console.WriteLine("\tMake: " + result.ScrubResult.HandsetMake);
                Console.WriteLine("\tModel: " + result.ScrubResult.HandsetModel);
                Console.WriteLine("Is MMS Provisioned: " + result.ScrubResult.IsMMSProvisioned);
                Console.WriteLine("Is MMS Capable: " + result.ScrubResult.IsMMSCapable);
                // tl;dr
                // If AllowSend is true, regard the MSISDN as being capable of receiving MMS
                //
                // AllowSend can be confusing. You should always react to what AllowSend is telling
                // you. The other fields can be regarded as metadata. AllowSend can be true even
                // when Provisioned and Capable is set to false. This is due to internal API
                // rules that determine that an MMS has a high probability of being delivered
                Console.WriteLine("Is Allowed to Send: " + result.ScrubResult.AllowSend);
                Console.WriteLine("Device Screen");
                Console.WriteLine("\tWidth: " + result.ScrubResult.ScreenSize.Width);
                Console.WriteLine("\tHeight: " + result.ScrubResult.ScreenSize.Height);
                if (result.ScrubResult.ErrorCode != 0)
                {
                    Console.WriteLine("ErrorCode: " + result.ScrubResult.ErrorCode);
                    Console.WriteLine("ErrorDescription: " + result.ScrubResult.Error);
                }

            }
        }

        /// <summary>
        /// This sample shows how to retrieve a message's status
        /// </summary>
        static void SampleGetMessageStatus()
        {
            APIConfig config = new APIConfig();
            config.AccessToken = ConfigurationManager.AppSettings["APIAccessToken"];
            config.Endpoint = ConfigurationManager.AppSettings["APIEndpoint"];

            MessagingAPI api = new MessagingAPI(config);
            
            // You must retrieve a message using the same application (access token) that
            // was used to submit it
            APIResult result = api.GetMessageStatus("Message-ID-From-Send-Call");
            if (result.StatusCode != APIResultStatuses.Ok)
            {
                Console.WriteLine(result.StatusCode);
                Console.WriteLine(result.StatusDescription);
            }
            else
            {
                Console.WriteLine("Message Status");

                /*
                 * Any status field can be used against iliveit.MessagingAPI.Enums.MessageStatuses
                 */
                Console.WriteLine("Type: " + result.MessageStatus.Type);
                Console.WriteLine("Campaign: " + result.MessageStatus.Campaign);
                Console.WriteLine("Template: " + result.MessageStatus.Template);
                Console.WriteLine("Network: " + result.MessageStatus.Network);
                /*
                 * When result.MessageStatus.Type is 'mms' or 'sms' MSISDN will be
                 * available. If result.MessageStatus.Type is 'email' Email will be
                 * available
                 */
                Console.WriteLine("MSISDN: " + result.MessageStatus.MSISDN);
                Console.WriteLine("Email: " + result.MessageStatus.Email);
                Console.WriteLine("MVNO: " + result.MessageStatus.MVNO);
                Console.WriteLine("DateReceived: " + result.MessageStatus.DateReceived);
                // If a video was built
                Console.WriteLine("BuildStatus: " + result.MessageStatus.BuildStatus);
                Console.WriteLine("BuildStatusDescription: " + result.MessageStatus.BuildStatusDescription);
                Console.WriteLine("BuildTimestamp: " + result.MessageStatus.BuildTimestamp);
                // If message was archived
                Console.WriteLine("ArchiveStatus: " + result.MessageStatus.ArchiveStatus);
                Console.WriteLine("ArchiveStatusDescription: " + result.MessageStatus.ArchiveStatusDescription);
                Console.WriteLine("ArchiveTimestamp: " + result.MessageStatus.ArchiveTimestamp);
                // If message was submitted to backend for submission to network
                Console.WriteLine("SubmitStatus: " + result.MessageStatus.SubmitStatus);
                Console.WriteLine("SubmitStatusDescription: " + result.MessageStatus.SubmitStatusDescription);
                Console.WriteLine("SubmitTimestamp: " + result.MessageStatus.SubmitTimestamp);
                // If message was submitted to the network
                Console.WriteLine("SentStatus: " + result.MessageStatus.SentStatus);
                Console.WriteLine("SentStatusDescription: " + result.MessageStatus.SentStatusDescription);
                Console.WriteLine("SentTimestamp: " + result.MessageStatus.SentTimestamp);
                // If message was delivered. If a message was sent more than 72 hours ago
                // and no failed status has been received, then the delivery status
                // will be returned as Assumed Delivered (12)
                Console.WriteLine("DeliveredStatus: " + result.MessageStatus.DeliveredStatus);
                Console.WriteLine("DeliveredStatusDescription: " + result.MessageStatus.DeliveredStatusDescription);
                Console.WriteLine("DeliveredTimestamp: " + result.MessageStatus.DeliveredTimestamp);

            }
        }

        /// <summary>
        /// This sample shows how to retrieve a message from
        /// the archive
        /// </summary>
        static void SampleGetMessage()
        {
            APIConfig config = new APIConfig();
            config.AccessToken = ConfigurationManager.AppSettings["APIAccessToken"];
            config.Endpoint = ConfigurationManager.AppSettings["APIEndpoint"];

            MessagingAPI api = new MessagingAPI(config);

            // You must retrieve a message using the same application (access token) that
            // was used to submit it
            APIResult result = api.GetMessage("Message-ID-From-Send-Call");
            if (result.StatusCode != APIResultStatuses.Ok)
            {
                Console.WriteLine(result.StatusCode);
                Console.WriteLine(result.StatusDescription);
            }
            else
            {
                Console.WriteLine("Message Archive");

                if (result.ArchivedMessage.MessageType == "mms")
                {
                    // When Message Type is MMS, the following fields will
                    // be available
                    Console.WriteLine("MSISDN Total: {0}", result.ArchivedMessage.MSISDN.Count);
                    Console.WriteLine("Network: {0}", result.ArchivedMessage.Network);
                    Console.WriteLine("Subject: {0}", result.ArchivedMessage.Subject);
                    Console.WriteLine("Total Slides: {0}", result.ArchivedMessage.Slides.Count);

                    foreach (MMSSlide slide in result.ArchivedMessage.Slides)
                    {
                        Console.WriteLine(" New Slide");
                        // Duration in seconds
                        Console.WriteLine("  Duration: {0}", slide.Duration);
                        foreach (MMSSlideContent content in slide.Content)
                        {
                            Console.WriteLine("  Content Name: {0}", content.Name);
                            // The MIME type of the content
                            Console.WriteLine("  Content Type: {0}", content.Type);
                            // Data is the base64 content of the slide. When content type is text/plain
                            // the data will be in plan text and not base64 
                            //Console.WriteLine(" Content Data: {0}", content.Data);
                        }
                    }
                }
                else if (result.ArchivedMessage.MessageType == "sms")
                {
                    // When Message Type is SMS, the following fields will
                    // be available
                    Console.WriteLine("MSISDN Total: {0}", result.ArchivedMessage.MSISDN.Count);
                    Console.WriteLine("Network: {0}", result.ArchivedMessage.Network);
                    Console.WriteLine("Message: {0}", result.ArchivedMessage.Message);
                }
                else if (result.ArchivedMessage.MessageType == "email")
                {
                    Console.WriteLine("MSISDN Total: {0}", result.ArchivedMessage.MSISDN.Count);
                    Console.WriteLine("Network: {0}", result.ArchivedMessage.Network);
                    Console.WriteLine("Subject: {0}", result.ArchivedMessage.Subject);
                    Console.WriteLine("HTML Content: {0}", result.ArchivedMessage.HTML);
                    Console.WriteLine("Text Content: {0}", result.ArchivedMessage.Text);
                }
            }
        }
        
        /// <summary>
        /// This sample show how to pass a string to the API instead of an object.
        /// NOTE: This sample does not actually work, but rather shows how to do it.
        /// </summary>
        static void SampleJSONString()
        {
            APIConfig config = new APIConfig();
            config.AccessToken = ConfigurationManager.AppSettings["APIAccessToken"];
            config.Endpoint = ConfigurationManager.AppSettings["APIEndpoint"];

            MessagingAPI api = new MessagingAPI(config);

            BuildRequest msg = new BuildRequest();
            msg.MVNOID = 29;
            // Assign a group to the message to report on the messages
            // in an easier way (reporting coming soon)
            //msg.Campaign = "Testing-Group";
            msg.Campaign = "Testing JSON";
            msg.BuildTemplate = 0;

            /*
             * This is where you add a JSON string instead of an object. This can be
             * (obviously) pulled from a DB and merged with the actual data.
             */
            // Build your JSON 
            string json = "{\"Name\": \"John\", \"Surname\": \"Doe\"}";
            // Add your JSON as the data for the template
            msg.Data = json;


            // Specify what the API needs to do when the build is complete
            msg.AfterBuildAction = APIActionTypes.SubmitMMS;

            // You need to supply data for the post build action
            SubmitMMSMessageData postBuildData = new SubmitMMSMessageData();

            // A network with a value of '*' will tell the API to
            // determine the network using the number ranges and portability
            // database. '*' is preferred unless you know exactly which network
            // to use.
            postBuildData.Network = "*";
            List<string> msisdnList = new List<string>();
            msisdnList.Add("XXXXXX");
            postBuildData.MSISDN = msisdnList;
            //
            // No need to include the Slide content or Subject, that is handled
            // by the templating system that generates
            // the MMS images and videos
            //
            msg.AfterBuildData = postBuildData;


            APIResult result = api.Generate(msg);
            if (result.StatusCode != APIResultStatuses.Ok)
            {
                Console.WriteLine(result.StatusCode);
                Console.WriteLine(result.StatusDescription);
            }
            else
            {
                Console.WriteLine("Message created");
                // This message id can be used to query message status using the previous
                // reporting API
                Console.WriteLine("Message ID:" + result.MessageResult.MessageID);
            }
        }
        
        /// <summary>
        /// This sample shows how to implement postback functionality using the JSON string supplied
        /// for MTN payment reminders
        /// </summary>
        static void SamplePostBack()
        {
            APIConfig config = new APIConfig();
            config.AccessToken = ConfigurationManager.AppSettings["APIAccessToken"];
            config.Endpoint = ConfigurationManager.AppSettings["APIEndpoint"];

            MessagingAPI api = new MessagingAPI(config);

            BuildRequest msg = new BuildRequest();
            msg.MVNOID = 29;
            msg.Campaign = "Postback Test";
            msg.BuildTemplate = 0;

            msg.Data = "{\"CustomerName\":\"Mr John Doe\",\"AccountNumber\":\"A00000000\",\"AmountDue\":400.02}";
            // Specify what the API needs to do when the build is complete
            msg.AfterBuildAction = APIActionTypes.SubmitMMS;

            // for the sample, we subscribe to all possible types
            msg.PostBackStatusTypes = "build,submit,archive,sent,delivered";
            // Specify the postback location. You can specify unique URLs for every message
            // if you want ie. http://10.0.2.2:9090/status/update/27424-3423525-32525266
            // The message ID and postback type will be supplied in the postback packet
            msg.PostbackStatusUrl = "http://10.0.2.2:9100";

            /*
             *  Use the MSISDN 27760000000 to get scrub failed notifications on build
             */

            // You need to supply data for the post build action
            SubmitMMSMessageData postBuildData = new SubmitMMSMessageData();

            // A network with a value of '*' will tell the API to
            // determine the network using the number ranges and portability
            // database. '*' is preferred unless you know exactly which network
            // to use.
            postBuildData.Network = "*";
            List<string> msisdnList = new List<string>();
            msisdnList.Add("XXXXX");
            postBuildData.MSISDN = msisdnList;
            //
            // No need to include the Slide content or Subject, that is handled
            // by the templating system that generates
            // the MMS images and videos
            //
            msg.AfterBuildData = postBuildData;

            // Construct a simple web server before we send a message.
            // If you get a Access Denied message when trying to run this sample, run is as
            // Administrator from your command line
            SimpleWebServer ws = new SimpleWebServer(HandlePostBack, "http://127.0.0.1:9100/");
            ws.Run();


            APIResult result = api.Generate(msg);
            if (result.StatusCode != APIResultStatuses.Ok)
            {
                Console.WriteLine(result.StatusCode);
                Console.WriteLine(result.StatusDescription);
            }
            else
            {
                Console.WriteLine("Message created");
                // This message id can be used to query message status using the previous
                // reporting API
                Console.WriteLine("Message ID:" + result.MessageResult.MessageID);

            }
            // Wait for postbacks
            Console.WriteLine("Waiting for postbacks. Press a key to quit.");
            Console.ReadKey();
            ws.Stop();
        }

        /// <summary>
        /// Resubmits a message with given message ID and MSISDN (or Email address).
        /// Returns a new Message ID, which is the original message id with the unix timestamp appended.
        /// Note: Only messages that have successfully been archived can be resent. OTPs can not be resent.
        /// Note2: You can also specify postbacks for the resubmit, but do note that video messages will not be rebuilt
        /// </summary>
        static void SampleResubmitMessage()
        {
            APIConfig config = new APIConfig();
            config.AccessToken = ConfigurationManager.AppSettings["APIAccessToken"];
            config.Endpoint = ConfigurationManager.AppSettings["APIEndpoint"];

            MessagingAPI api = new MessagingAPI(config);

            ResendMessageRequest msg = new ResendMessageRequest();
            msg.MessageID = "Message-ID-From-Send-Call";
            msg.MSISDN = "xxxx";
            //msg.Email = "test@example.com";

            APIResult result = api.Resend(msg);
            if (result.StatusCode != APIResultStatuses.Ok)
            {
                Console.WriteLine(result.StatusCode);
                Console.WriteLine(result.StatusDescription);
            }
            else
            {
                Console.WriteLine("Message created");
                Console.WriteLine("Message ID:" + result.MessageResult.MessageID);
            }
        }

        public static string HandlePostBack(HttpListenerRequest request)
        {
            string content = GetRequestPostData(request);

            // From here it is your job to deserialize the content into MessageStatus
            Console.WriteLine(content);
            Console.WriteLine("=====");

            return "Ok";
        }

        /// <summary>
        /// I didn't want to spend time on this, so got it from http://stackoverflow.com/questions/5197579/getting-form-data-from-httplistenerrequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetRequestPostData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (System.IO.Stream body = request.InputStream) // here we have data
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// This sample shows how to submit an SMS message using the client library
        /// </summary>
        static void SampleSubmitSMS()
        {
            APIConfig config = new APIConfig();
            config.AccessToken = ConfigurationManager.AppSettings["APIAccessToken"];
            config.Endpoint = ConfigurationManager.AppSettings["APIEndpoint"];

            MessagingAPI api = new MessagingAPI(config);

            NewMessage msg = new NewMessage();
            msg.Action = APIActionTypes.SubmitSMS;
            msg.MVNOID = 29;
            msg.Campaign = "Your campaign name";
            
            SubmitSMSMessageData msgData = new SubmitSMSMessageData();
            msgData.MSISDN.Add("XXXXX");
            msgData.Network = "*";
            msgData.Message = "Sample SMS";
            
            msg.Data = msgData;

            APIResult result = api.Create(msg);
            if (result.StatusCode != APIResultStatuses.Ok)
            {
                Console.WriteLine(result.StatusCode);
                Console.WriteLine(result.StatusDescription);
            }
            else
            {
                Console.WriteLine("Message created");

                Console.WriteLine("Message ID:" + result.MessageResult.MessageID);
            }
        }

        /// <summary>
        /// This sample shows how to submit an MMS message using the client library
        /// </summary>
        static void SampleSubmitMMS()
        {
            APIConfig config = new APIConfig();
            config.AccessToken = ConfigurationManager.AppSettings["APIAccessToken"];
            config.Endpoint = ConfigurationManager.AppSettings["APIEndpoint"];

            MessagingAPI api = new MessagingAPI(config);

            NewMessage msg = new NewMessage();
            msg.Action = APIActionTypes.SubmitMMS;
            msg.MVNOID = 29;
            msg.Campaign = "Sample MMS";

            SubmitMMSMessageData msgData = new SubmitMMSMessageData();
            msgData.MSISDN.Add("XXXX");
            msgData.Subject = "My Test MMS";
            msgData.Network = "*";
            MMSSlideContent slideContent = new MMSSlideContent();
            slideContent.Type = MMSSlideTypes.Text;
            slideContent.Mime = "text/plain";
            slideContent.Name = "TextDocument1.txt";
            slideContent.Data = "My Plain Text";
            msgData.Slides.Add(new MMSSlide(5, slideContent));
            msg.Data = msgData;

            APIResult result = api.Create(msg);
            if (result.StatusCode != APIResultStatuses.Ok)
            {
                Console.WriteLine(result.StatusCode);
                Console.WriteLine(result.StatusDescription);
            }
            else
            {
                Console.WriteLine("Message created");

                Console.WriteLine("Message ID:" + result.MessageResult.MessageID);
            }
        }

        /// <summary>
        /// This sample shows how to submit an Email message using the client library
        /// </summary>
        static void SampleSubmitEmail()
        {
            APIConfig config = new APIConfig();
            config.AccessToken = ConfigurationManager.AppSettings["APIAccessToken"];
            config.Endpoint = ConfigurationManager.AppSettings["APIEndpoint"];

            MessagingAPI api = new MessagingAPI(config);

            NewMessage msg = new NewMessage();
            msg.Action = APIActionTypes.SubmitEmail;
            msg.MVNOID = 8;
            msg.Campaign = "Test Email";

            SubmitEmailMessageData msgData = new SubmitEmailMessageData();
            msgData.Address.Add("test@example.com");
            msgData.Network = "iliveit_will_supply_network";
            msgData.Subject = "Testing Email";
            msgData.HTML = "<h1>This is the heading</h1>";
            msg.Data = msgData;

            APIResult result = api.Create(msg);
            if (result.StatusCode != APIResultStatuses.Ok)
            {
                Console.WriteLine(result.StatusCode);
                Console.WriteLine(result.StatusDescription);
            }
            else
            {
                Console.WriteLine("Message created");

                Console.WriteLine("Message ID:" + result.MessageResult.MessageID);
            }
        }

        /// <summary>
        /// This sample shows how to submit an Email with attachments using the client library
        /// </summary>
        static void SampleSubmitEmailWithAttachments()
        {
            APIConfig config = new APIConfig();
            config.AccessToken = ConfigurationManager.AppSettings["APIAccessToken"];
            config.Endpoint = ConfigurationManager.AppSettings["APIEndpoint"];

            MessagingAPI api = new MessagingAPI(config);

            NewMessage msg = new NewMessage();
            msg.Action = APIActionTypes.SubmitEmail;
            msg.MVNOID = 29;
            msg.Campaign = "Email Attachments Test";

            SubmitEmailMessageData msgData = new SubmitEmailMessageData();
            msgData.Address.Add("test@example.com");
            msgData.Network = "iliveit_will_supply_network";
            msgData.Subject = "Sample Email Attachment 2";
            msgData.HTML = "<h1>This is a test attachment</h1>";
            msg.Data = msgData;

            EmailAttachment attachment = new EmailAttachment();
            attachment.Filename = "TestDocument.pdf";
            attachment.Base64Data = "JVBERi0xLjQKJcOkw7zDtsOfCjIgMCBvYmoKPDwvTGVuZ3RoIDMgMCBSL0ZpbHRlci9GbGF0ZURlY29kZT4+CnN0cmVhbQp4nFWMOwsCQQyE+/yK1MKuybqvgyXg+SjsDhYsxM7TTvCa+/u3j0oCmcl8Q0gzrvBDQirODU4bjJZ1xGWG+w6/nZVZPjBmcL6gEGwp5xfur4xsML8fiVhMIkOHKlZU2e7/8MJVQs1i9R0EUa7mg/hExxaNolr11B+cWyPKM9/gkmGCCTfbnCbZCmVuZHN0cmVhbQplbmRvYmoKCjMgMCBvYmoKMTM2CmVuZG9iagoKNSAwIG9iago8PC9MZW5ndGggNiAwIFIvRmlsdGVyL0ZsYXRlRGVjb2RlL0xlbmd0aDEgOTU5Mj4+CnN0cmVhbQp4nOVZe1BbV3o/jyuhB+gBkuAiGV358hYgjIwBG4MMkhDGNuKVCBxAMohHbANGwnk4KewmTrI4XrvZbB61u3bStM0kmfriZFtnk43JTHZ2Onk4md10N9082G467TRm7aZJZicO0O9cCfzYZHem25n+0Yu493uf7/ud75x7EPHJqShKRTOIIs/A/siEi7ekI4TeQAinDxyMCysNHgHoBYSIZWhieH+h+1e/RYj+DqEUxfC+O4beLnz3JYS04KJ7bSQaGVTcVelGiH8NBJtGQNC5fEcK8F8BnzuyP377q0pVMULZEBNV7xsfiIxk/zIL+BDwOfsjt0/8lfI7HPC3Ay+MRfZHd0WffBj4xxBS7ZgYj8UHUe4KQus/ZvqJyejE5z2PqhASwYfEQYbhh12pQCoZTyinUKao1Bptahr6f3gpjiIzCii2Ij2akO/XXfQ5xLPnysXr78s7Vr7838xClXg8hv4GvYCOovdQb1LhR0E0iqZAcu31KnoHpOwKoh70DJr9hrDPoXOgT9iF0TH0+DfYBdGj6Hn00+tGCaL96BDk8kP0Ht6A/hFaZRx9ilXoW+gnEPVTkO38ulBEB7chmRy6RvordIIcQdsJ68vHmYa4iAG9hk7iPogchzqPrlVc+3tB70d3w70DjaCDQMuXYutX/4zUK/8FVd2NtqNvo21o3zUeL+NTVAPz14lOAaavyjLXqjIlQG8lf0/I0veA+XM0DL8RDLWTo3Qb8iqM+AWEPL7uUFdnR3tbsHXXzh0t25sDTX6ft7Fhm6e+bmvtls011VWbKjeUu8pKSwoL8vNyxfUOe5bJaNDr0rQatSpFqeAowajEJ/rDgpQflrh8MRAoZbwYAUHkGkFYEkDkv95GEsKymXC9pQcsh26w9CQsPWuW2CDUotrSEsEnCtKbXlE4h3vaQkAf9YrdgrQo0ztlmsuXmTRgHA7wEHxZI15BwmHBJ/kPjsz6wl6IN6fVNIqNUU1pCZrTaIHUAiUVihNzuLAOywQp9G2eI0iVxoaVaJ4vMigF20I+r9Xh6C4taZZ0oldWoUY5pKRslFLkkMIoSx0dEeZK5mcfPGdAe8LO1EFxMHJLSKIR8J2lvtnZ+yWjUyoSvVLRnR9nQeVRqUT0+iQni9rSvjZOy9UhsaTIM4jC7OcIyhEXL14viSQlyjzD54iRfoB3dtYvCv7Z8Gzk3MrMHlEwiLNzqamzEz5AGAVD4HVu5UdHrJL/wW7JEB7Bm5PF+ttbpIy23SGJ5PmFkQhI4FMvOqqtDmP3qk3wm9QIgAA4AFOHgxV+5JwH7QFGmmkLJXgB7bGeRR6Xs1siYaaZX9WYu5hmZlWz5h4WYTZbOkKzEpfXPCj6AOMjEWlmD/TTrWwqRIOk+8LqEGfTjUKNq1u2FSCr5sFRQVLkAyzgda0DdApzmTXIjO6LxGPRCgPkG9OFGhHCsDg+0RdOfg6OZEEAobRECjgTU98ZkjxeIDyR5Bz55spd4BEJwxSNeuXpk1zihGQSG9bmk6XlG+0IyS5JN8nUKKHwQNJLcvm8bGTBNxv2JlJgscS20IvIvbIwt1GwPu9GG1G3lxlbGqGv8n2zocEhyR62DsJKGxJCVofk6YYJ7hZD0W7WaIBQ0QIM55BHlEhjZ6ilQ2xp6wlVJxNJKFg4Ls93QxgxZE2EgZaTVHkqIUSstBsMDSAQ/ECIDbVwl1LyVPBrAMBlKWvVhlohhK1o1RrSkIoEX9SbtGP8dUEVrJ0aA6vRlIyFOI0Bq6PbkbhKSwioheTA4KFioAZWVTQPdgKQEQgjixiWWaznhZAYFbvFEUHyBEOsNgaPjHISDBnz5Fx1XsddAxbAhBygXmUYmJLfab0WXKlJ5tfYwA3q5lW1MKsSWzpmWXAxGRBB5s0SYi3sqTZa5dXP1rPoj8AihhUtr+fZOY+HreURtmxnxebBWbEjVCtbww5yt/VONlY6asEtnQ2lJbCZNcyJ+IG2OQ9+oKMn9KIBjlQPdIbOEkwaww3dc7mgC70owLtClhImZULGCIxhkdqBUcn21hc9CM3IWk4WyPzAOYxkmWpVhtHAOZKQGVZlBGRcQuaRZeyCWcoaAYxh//YJg2x+7uoemQ13sx5HFkAEPljCYh2gI9bNYaJMlTRitEHSig1MXs/k9Qm5kslToDOwBZeW3Dlr8ImfZ5XKr27khdugogtOwCmobA4jV+3ZFE61WDGnVLxfe5YSINEcZWIFE59NUaq/qj2LmdxtdBjzHEaHlwjLufix5RFF15fPerk3ETuJ5iHEvQpnrkz8n54VRZo5LS+NalTZKqLW83hZz7fy/fw0f4w/z3/Er/Cqyzw+xp/iL/B0gsd63g56egFUl3gq8fgUj2d4bOdd4EQRj98a58+A5yWeCzJrF1/P0xUev83j8zw+zeN6cJ/mqcDjaQh6HsKu8Iowj1t5XM4c8F9ekq1d/DjYneE5A/O8AAFXeO44f5on0zwOM8t6niyweKvJKgTZfy/ke0Ee6hiPr2ackELC/RCY1cOV8x6eeO638xjS/oiVIfGkn3HlPNkCOS+sujBAjvG0nDEL/GWeJiLLtgJYs+AQYF5GY4Kf4Yk9UTgEDqbOpEqp86lcKulXH1OfV19Qc2pzD0lDaqxWm2hYQ82kH6Wj+sUK+LhdvW7sWnqj1/BGb/I6wK5J+epb439fssb1run7rgYAekM58I7KKqO4XqnHInSIWFBGndiYacZb3nXfczbP2sid9FrTm/rGN294t9LKPZqqegdvWf7JO5xSQa/stVYiuX9McL77Vzi/FyLpRZS2Mu9xqgyBIlONiWSZsJp9zE16A7YYik8XY1RsKJ4vXijmak4XXy4mxefA3uQsD7iKsaEYB4vxRPFM8fFiyhTP29cHZANnhiWA7E0zuRjlGnKF3Pnct3MXcpWq3LxgIbKbDbnBjPXmHIWCb9cYADi30V3vXqyoAACxq6/3wGKF09kLADih6gOG9/t6FysYFBvKndikIyl11F2RQ8xQN0NjY764HoRJrqDSgf0YU2IL3nxz7qaebXmTy3vvbuuy1ddtSp9eHrztQVxBv9AVOgvTDLk5GTkNt7YsPcKXlvKkr6NbqdJySxmMUxB5ERPkhFu6YgfKQOvQSU8H2q7VnNA8q6GfaK5oyL0arOGbtCanibSYdptOmK6YOMZtMT1resn0iUlpMHlqtgZMds5uspOaz+z4uB2ToP20XbLP27njQBA7w620PCA/s6zy02NIMwQUHXouO7hOb+KDmeZEdwE8ToZMP2sNw/tOQGdy6efQIIsbyhky4voyUrmxjjB46FU8DhlzCi2WghyjMafAYinMMWqeWOZPH8ZO7qNrpWB1pY0VD4u7lPVJ58pF8jP6E1SJnvDkbq+YrSB3mR80k82W7ZY7LbMWTuE2u/PctDZ7R/Zd2Q9mc+Tcyi89meq0QE6WOjWQ5zGYA3l5GX5UJVThKlZWeY4j0FrVX3Wmipb6bVqtLaNUURx0bMz35pP8fIfBEFRs1Hq1T2mpoMVarcKC6mE99RoW2cOwmF5Tg11ul2Gx13nA8OGi2+WEjnD2IrZGVssvqHRn5mB3xabKjWXKBBaWTDMsFAyrxmzKUZKfFXQe7nPt3rU5rXSDfU9Db7TYe/Pum73FZR0xn/fbta7i7B53W1exL3RLyFeMVfWjLUVavUHxb/fYCtu6KraVrMvJr+1p9Ax6xYzUN/dnZgW9ZVuKcoQizy2sXwIrF+kB+ioqRpvQox7H3nxszXRmEp2lzkLSBa0+sC69NJ2kpuM0I8YcpudWFjzr1MYAplhl02xqUlbPVOP+auypxkBsaDIVMODsGl2goKDVhE35+eudQZsNbXK3afQWZVBtXh9EbA3BDywhY40LYHJh2IVcTgAKFpHhfVhZbO9wMpzYDa+ungIdTbYMV48BMJLYWSrrcEaKjppNAGMVfsczFiydWl7O0LsD/Zu9vdVZOZuau/rLj+oc1cXle/LWV2878k/3bLmp2nbMO1BBX83aPNCydJgv7dMXilnFLcO1dbvrCiwqzH2v2FdhyzZPvakzL+dwJKMsWCfZs9h+BH1Wyn0L8ehmTw2pVhkDnBKfseJ5K663tlqJRtdEg6awiZhMKYgaqECpinKpQbVHrQuoU7R6s7ENWdg+Uu9+y7lYwfYQt9wxFb29kxvKe52K9fmVRrGyHkPHmkWjycIqM+so3hXuP3R3tP4Xv9hSntds12/Y0mCaHCbfKy14993OpeltDRrlNo1Jr0m8z4Mwt35YD3aY3bin9Dsm/FgG1mYcySAWa76VqLP4rKKsx7M4VX7ArtXaS1AJrpspOV1yuYSWsNXduD3Anp7M4rJAHg48YMEWFMzLUwpB3qBsM1rkWYTZg02wl22BB2CRv1UB/S7v/GzioIGhoesIyz0xc5vYWl+HMWyBZgfMIObM9ZO9OQ0NddmZ23aFSqeeGCx563zLPXtqlh+tbqvk8UNGZwC/l9583/BWhUqjrNZbLWmeP/vRHV98Wtj3g4Pt+KTrpkM7dhy6yZWoeQucNRxw1khFDvSU5+6njC8YicKOD2d/P5so+MM8UWlINiE6TVaqPoB6bKJedInj4rR4TFS4xHqxFZhT4nnxIzFFL/YDcwHIFVFZzUSEGU+DltOLdjCeBtMzolKVousJZuAMVTgtzagIm/otVJfRb0y+YheNqwix7Y/9wl4AG0FfrwwSBhDgnchQwlffCazj2Q5Be8TWmb49I33TO4XlXe8uvX7qOfzl0R9PlrvGfzRLpWC8JXfpcGnnncvPLjdYK+EV+oPsyq17j3e0Pxb3J/BY+UoxBXgYUD663dM5xeMp6AGdVbdXR3vpfkpqaDMlWmqlRG0lWA0fhHVoN7LgTEshKsT1nkIsFOKJwtOFC4U0pScoLohEzO1XhvOp2J8atrIyoQ/kEuUCDR/CC5C9/nt7kweCZI05VK6ygsuBxtBhjnWEE8sFc0+1P/7Lwy/k+Le35E7/MF619Lu/xWmvDHc+s7x0pub+b08VPPfcc+Tph3/xoPfKIUIobvn+h7TI/4Ov/uGp5b/rwQQnKoe1yea/COrNRr/2bGcnzco0ak7NS61MpbzZZ+4yE2rOzITNGqu0trjtXtvDNhq2Ya+t00Y+tuG3bbgTxC/YXrNxHhvOtW20EcmGbWwZZNT5AshmsAk2uoUDv6dsVJZvqdocmLdhZmfCPUHlgpIo62GtWFDYBEc4nneZ+k3jJmoyKTPCapSKU1NT+tUUK/u5ZIsk3xZsE2SbgKtXbpcD/QfYgcK5dtwyvNXXC83k7u81uhm61/TL6tHKUeUGEj/90dKrp56jv20QhN19nZm/wkfsW7faSc/SF6tNsnz+PU5J8dI7p5cHnwTc6qFZnlE8iRz4jCdNreSVRUqq0op4SWQl9v3uSuCIiDfCH8uDIr1X/Ln4sfiZyE2I2ASiThBy7BYXX5AVSq1oFckbl0X8mmxKZV+mp0+t+ibsGamQh9BIzwdkt5Mym/rYicAJEcfFe6HbmGDDd44GnhUxc7tXpFYR2gd/JuKXRMziyCKnSEC4lxk8LFLZ63h0JNCyavus+JJIHhaxU9zNLE0iYZLXRcpoVkZcVGy+IuIXIEdyWsS5Iis4LodTGkRMkIgFsVwMijPicVGChXBZVBlEAdh5kctKS7M1UeQwOATHjINTOWyOoN2MsoOU16cH1f06rNOpMUocDdi7rz6x81fAhFfAwulfPV8np9t5zenaCazcCVdNZAnbaDPEyqobDg462GHZe0Nuj18/+aSzbaoZDjAbSg35NrEkW/Pll68vc0doaENBw61P7K/Wqt48pNHatw36T3Z+9YWjtNSROHcXQU8Y4NytRvd7ChVNToS1CG/ejfaiQ+gE4qywS7yEXkcc456Fvxq1r8EpiL046v0B9vTYqrcEjmsBN61BG9Se1kraea3yOBCXtVSbPEfKhqlwfoSzAUKKIE2eHXHi4Oh0woEgsW/Cppl39ZA4zs6Ep7Hff83xV/6fBzZ+MjR9eF+/vvZzZE98337h6PrvXv3KeOUi7IhPIvZlPEmKwC/FsexDN68Z4Ru+mdaRi8jL/QblcQiZSA1y0nWok7nToygAvAmeQS6Gtih+irawJ3kG1YMcMITX6g78LyRMviRf0ofpv3N7uP9Q7FEsp6hS9iRH0qGqZC4EdmoX+y6f0yprAFMmteGb1vIJr+WGwTKcpAn8xT6RpCmyotuSNAc2DyVpBYzyZJJWIj2SknQKuhOdT9IqZMI1SVqNdHhnktZCDrvX/qNUhlfjp6Fx/NdJWofqiAlGxxzMI5on7UkaI4GmJ2mCdLQiSVO0iXqSNAc2B5O0AtnoI0laiXLo2SSdgj6jbydpFSrkXkvSamTjLiZpLapWqJJ0KrpFsRo/DX2oOJmkdegu5Z2N4xN3TI4Oj8SFwoEioaK8vEpojw4KgUi8RGgeGygTtu3bJ8gGMWEyGotOHowOlgk7mht87ds6m1t3CaMxISLEJyOD0f2Ryb3C+ND1/jtG90QnI/HR8TGhIzo5OtQeHZ7aF5ncFhuIjg1GJ4VS4UaLG/mbopMxxmwoK68q23hVe6PxH0kEsh8ejcWjkyAcHRO6yjrKhGAkHh2LC5GxQaFzzbF1aGh0ICoLB6KT8QgYj8dHINVbpyZHY4OjA2y0WNlaBY3jkxPjyZTi0YNRYWckHo/GxsdG4vGJzS7XbbfdVhZJGg+AbdnA+H7XH9LF75iIDkZjo8NjUHnZSHz/vh2Q0FgMEp+SR4RsrkXNPz4Gk7MvYVMixKJRgYWPQfyh6CCkNjE5fmt0IF42Pjnsum1076grEW90bNh1NQyLkhznT/NGjWgc1uAdaBKNomE0guJIQIVoAHYAAVWgcvipAqodRdEgPAMoAhYlQDWjMbAqA4r9Z2sfPK9GiMlcFJ5ReB6UfZnlDvBqQD6Itg11At2KdoF0VLaPwG8crCNgG0X74TkJO7YA2Q39wfF3gP8eeRymGQX7MdB2yJJR8GWew2gKMmQRt8FYAyAZk0eZBMtSOa8/HOOP6W+SqdiaZgPkxXArQxu/1vePRf7TEElgPyxHicuxE5ajcuwusOiQrYKyJ8MiLo82Jlt1fs2IrTDiEPgz5K5aDsix48AnIo8DPZJE9VZAfFLOYFD2W60tBiP//hywHpyELhy/ASWW3UF5zJ2yPC73FNONyNwE2gxvHRe8N9hPGdhcH3kgGbdMpvaD5f/ULw4rZELGMSrP8zDYJua8TI65H/prRxKhMbnvGUJT19SYwOabes0vPxMrZ991cdjMsifzXc0+lsx/SB4ngdoE3McB96iMdpksHZZrHIU5HAXq2vzYjA0nZTdms5rL9fX8X45NkycgB4z4NdecOvwKTmF/Dcj385jzdOOFJXxhCQtLePoKDl7BM58e/5T85+Ui+5nL5y+T1kv9l85couWXsP4SVqFFw2JwMbw4sXh6UanRX8Sp6BNs/M1Ctf0j9wddH7rf70If4NrgBzMfSB9Qdu7r+UCl9X+Aadf71GI3zAvz5fMT8zPzb88vzF+eV828cvwV8uOXXXb9y/aXif351uenn6fhp7H+afvTJHgifIIcP4n1J+0nXSfpXzxeZn+8Kcf+6CMF9oVHLj9CWPjKR9KM/v7v4+mHjj1EJu6bue/4fXTm8PHD5MzB8wdJLFhkHx9z2seaiu28O6srxU27lHRF/kLTuyev0B/u99j7wWh3T7m9p6nInuFO71JAshwY6qmd1tNWOk6P0fM0RdUezLG3we9C8HKQ6Fvtra5W+buySIsDAm2f2D6znTb7i+yBpmq7vsne5Gq60PRR06UmZX8TPgUf/xn/eT/1+Itcfo8/x+G3BaxdFre5y+DWdxGMurAbdbn0K3qi1/frp/VUD3+xkRkLVuBz+PhcZ4fT2XIuZaW9RVIFd0v4ASmvg909bT2S8gEJdfXsDs1h/N3uw0ePooZ1LVJFR0gKr+tukQaB8DBiBgjDujkLauiOxeJOdsERHMgpuCPnFIj6Ygkhcq6qkTOGYzEUi2En08kkSFDMycRMwnwwePbFELsxrVO2YlQsltX33yEy1OQKZW5kc3RyZWFtCmVuZG9iagoKNiAwIG9iago2MDM5CmVuZG9iagoKNyAwIG9iago8PC9UeXBlL0ZvbnREZXNjcmlwdG9yL0ZvbnROYW1lL0JBQUFBQStMaWJlcmF0aW9uU2VyaWYKL0ZsYWdzIDQKL0ZvbnRCQm94Wy0xNzYgLTMwMyAxMDA1IDk4MV0vSXRhbGljQW5nbGUgMAovQXNjZW50IDg5MQovRGVzY2VudCAtMjE2Ci9DYXBIZWlnaHQgOTgxCi9TdGVtViA4MAovRm9udEZpbGUyIDUgMCBSCj4+CmVuZG9iagoKOCAwIG9iago8PC9MZW5ndGggMjgyL0ZpbHRlci9GbGF0ZURlY29kZT4+CnN0cmVhbQp4nF2Ry27DIBBF93wFy3QRgV95SJal1FEkL/pQ3X6ADWMXqcYI44X/vjCkrdQF6Izn3tH4wurm2mjl2KudRQuODkpLC8u8WgG0h1FpkqRUKuHuFd5i6gxh3ttui4Op0cNcloS9+d7i7EZ3Fzn38EDYi5VglR7p7qNufd2uxnzBBNpRTqqKShj8nKfOPHcTMHTtG+nbym17b/kTvG8GaIp1ElcRs4TFdAJsp0cgJecVLW+3ioCW/3pJFi39ID4766WJl3Je5JXnFPlwCpxFPgfOkY9Z4AI55YEPUZMEPkYNzjnF70Xgc5yP+gtyjprHyIfAddQjXyPXuPx9y/AbIeefeKhYrfXR4GNgJiENpeH3vcxsggvPN8jqiY8KZW5kc3RyZWFtCmVuZG9iagoKOSAwIG9iago8PC9UeXBlL0ZvbnQvU3VidHlwZS9UcnVlVHlwZS9CYXNlRm9udC9CQUFBQUErTGliZXJhdGlvblNlcmlmCi9GaXJzdENoYXIgMAovTGFzdENoYXIgMTMKL1dpZHRoc1szNjUgNjEwIDUwMCAyNzcgMzg5IDI1MCA0NDMgMjc3IDQ0MyA1NTYgNzIyIDU1NiAzMzMgMjc3IF0KL0ZvbnREZXNjcmlwdG9yIDcgMCBSCi9Ub1VuaWNvZGUgOCAwIFIKPj4KZW5kb2JqCgoxMCAwIG9iago8PC9GMSA5IDAgUgo+PgplbmRvYmoKCjExIDAgb2JqCjw8L0ZvbnQgMTAgMCBSCi9Qcm9jU2V0Wy9QREYvVGV4dF0KPj4KZW5kb2JqCgoxIDAgb2JqCjw8L1R5cGUvUGFnZS9QYXJlbnQgNCAwIFIvUmVzb3VyY2VzIDExIDAgUi9NZWRpYUJveFswIDAgNTk1IDg0Ml0vR3JvdXA8PC9TL1RyYW5zcGFyZW5jeS9DUy9EZXZpY2VSR0IvSSB0cnVlPj4vQ29udGVudHMgMiAwIFI+PgplbmRvYmoKCjQgMCBvYmoKPDwvVHlwZS9QYWdlcwovUmVzb3VyY2VzIDExIDAgUgovTWVkaWFCb3hbIDAgMCA1OTUgODQyIF0KL0tpZHNbIDEgMCBSIF0KL0NvdW50IDE+PgplbmRvYmoKCjEyIDAgb2JqCjw8L1R5cGUvQ2F0YWxvZy9QYWdlcyA0IDAgUgovT3BlbkFjdGlvblsxIDAgUiAvWFlaIG51bGwgbnVsbCAwXQovTGFuZyhlbi1aQSkKPj4KZW5kb2JqCgoxMyAwIG9iago8PC9DcmVhdG9yPEZFRkYwMDU3MDA3MjAwNjkwMDc0MDA2NTAwNzI+Ci9Qcm9kdWNlcjxGRUZGMDA0QzAwNjkwMDYyMDA3MjAwNjUwMDRGMDA2NjAwNjYwMDY5MDA2MzAwNjUwMDIwMDAzNTAwMkUwMDMxPgovQ3JlYXRpb25EYXRlKEQ6MjAxNjA1MjQxMDA2NTErMDInMDAnKT4+CmVuZG9iagoKeHJlZgowIDE0CjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwNzIzNSAwMDAwMCBuIAowMDAwMDAwMDE5IDAwMDAwIG4gCjAwMDAwMDAyMjYgMDAwMDAgbiAKMDAwMDAwNzM3OCAwMDAwMCBuIAowMDAwMDAwMjQ2IDAwMDAwIG4gCjAwMDAwMDYzNjkgMDAwMDAgbiAKMDAwMDAwNjM5MCAwMDAwMCBuIAowMDAwMDA2NTg1IDAwMDAwIG4gCjAwMDAwMDY5MzYgMDAwMDAgbiAKMDAwMDAwNzE0OCAwMDAwMCBuIAowMDAwMDA3MTgwIDAwMDAwIG4gCjAwMDAwMDc0NzcgMDAwMDAgbiAKMDAwMDAwNzU3NCAwMDAwMCBuIAp0cmFpbGVyCjw8L1NpemUgMTQvUm9vdCAxMiAwIFIKL0luZm8gMTMgMCBSCi9JRCBbIDwxNkQ0OEQwMzcyN0UwNkIwMzNCQ0M4MjRFMjNFQTgwOD4KPDE2RDQ4RDAzNzI3RTA2QjAzM0JDQzgyNEUyM0VBODA4PiBdCi9Eb2NDaGVja3N1bSAvRTZBOTE0QjJENDUzQTQwNjQ0OTlFQjJDRkQ5MjAxNjUKPj4Kc3RhcnR4cmVmCjc3NDkKJSVFT0YK";

            msgData.Attachments.Add(attachment);

            attachment = new EmailAttachment();
            attachment.Filename = "TestImage.png";
            attachment.Base64Data = "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";

            msgData.Attachments.Add(attachment);

            APIResult result = api.Create(msg);
            if (result.StatusCode != APIResultStatuses.Ok)
            {
                Console.WriteLine(result.StatusCode);
                Console.WriteLine(result.StatusDescription);
            }
            else
            {
                Console.WriteLine("Message created");

                Console.WriteLine("Message ID:" + result.MessageResult.MessageID);
            }
        }
    }
}
