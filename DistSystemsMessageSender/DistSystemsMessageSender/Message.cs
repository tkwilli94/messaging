using System;

namespace DistSystemsMessageSender
{
    internal class Message
    {

        public string sender { get; set; }
        public string body { get; set; }
        public DateTime timestamp { get; set; }

        public Message(string sender, string body, DateTime timestamp)
        {
            this.sender = sender;
            this.body = body;
            this.timestamp = timestamp;
        }
    }
}