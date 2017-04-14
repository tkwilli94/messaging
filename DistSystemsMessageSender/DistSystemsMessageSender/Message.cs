using System;

namespace DistSystemsMessageSender
{
    internal class Message
    {

        public string senderName { get; set; }
        public string senderNumber { get; set; }
        public string body { get; set; }
        public DateTime timestamp { get; set; }

        public Message(string senderName, string senderNumber, string body, DateTime timestamp)
        {
            this.senderName = senderName;
            this.senderNumber = senderNumber;
            this.body = body;
            this.timestamp = timestamp;
        }


    }
}