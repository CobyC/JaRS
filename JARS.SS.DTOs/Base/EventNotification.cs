using System;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{
    /// <summary>
    /// This is a jars helper class and is used to determine if the message that was sent represents an appointment booked in the scheduler.
    /// </summary>
    public class ServerEventMessageData
    {
        public ServerEventMessageData()
        {
            Meta = new Dictionary<string, object>();
        }        

        public string jsonDataString { get; set; }

        public string From { get; set; }

        public Guid FromClientGuid { get; set; }

        public Dictionary<string, object> Meta { get; set; }
    }
}
