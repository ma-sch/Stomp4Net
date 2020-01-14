namespace Stomp4Net.Model
{
    using System;
    using Stomp4Net.Exceptions;
    using Stomp4Net.Model.Frames;

    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#MESSAGE">MESSAGE frame</see>.
    /// </summary>
    public class MessageFrame : StompFrame
    {
        public MessageFrame(SendFrame sendFrame, string subscriptionId)
            : base(StompCommand.Message, sendFrame.Body)
        {
            this.Headers["destination"] = sendFrame.Destination;
            this.Headers["content-type"] = sendFrame.ContentType;
            this.Headers["subscription"] = subscriptionId;
            this.Headers["message-id"] = Guid.NewGuid().ToString();
        }

        public MessageFrame(string body, StompHeaders headers)
            : base(StompCommand.Message, body)
        {
            this.Headers = headers;
            this.Destination = this.Headers.ContainsKey("destination") ? this.Headers["destination"] : throw new HeaderMissingException("Header 'destination' is missing in MESSAGE frame");
            this.MessageId = this.Headers.ContainsKey("message-id") ? this.Headers["message-id"] : throw new HeaderMissingException("Header 'message-id' is missing in MESSAGE frame");
            this.ContentType = this.Headers.ContainsKey("content-type") ? this.Headers["content-type"] : "Not provided by server";
            this.ContentLength = this.Headers.ContainsKey("content-length") ? this.Headers["content-length"] : "Not provided by server";
        }

        public string Destination { get; private set; }

        public string MessageId { get; private set; }

        public string ContentType { get; private set; }

        public string ContentLength { get; private set; }
    }
}