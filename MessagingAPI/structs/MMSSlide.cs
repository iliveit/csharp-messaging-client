using iliveit.MessagingAPI.Enums;
using iliveit.MessagingAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iliveit.MessagingAPI.Structs;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using iliveit.MessagingAPI.Helpers;

namespace iliveit.MessagingAPI.Structs
{
    public class MMSSlide
    {
        /// <summary>
        /// The duration of the slide in seconds
        /// </summary>
        [JsonProperty("duration")]
        [JsonConverter(typeof(IntStringConverter))]
        public int Duration;
        /// <summary>
        /// The content for the slide
        /// </summary>
        [JsonProperty("content")]
        public List<MMSSlideContent> Content;
       
        /// <summary>
        /// Constructor
        /// </summary>
        public MMSSlide()
        {
            this.Duration = 5;
            this.Content = new List<MMSSlideContent>();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="duration">The duration of the slide in seconds</param>
        /// <param name="content">The content of the slide as a single slide content</param>
        public MMSSlide(int duration, MMSSlideContent content)
        {
            this.Duration = duration;
            this.Content = new List<MMSSlideContent>();
            this.Content.Add(content);
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="duration">The duration of the slide in seconds</param>
        /// <param name="content">The content of the slide as a list</param>
        public MMSSlide(int duration, List<MMSSlideContent> content)
        {
            this.Duration = duration;
            this.Content = content;
        }
    }
}
