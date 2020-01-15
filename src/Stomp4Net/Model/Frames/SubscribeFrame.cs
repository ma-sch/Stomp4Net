namespace Stomp4Net.Model.Frames
{
    using System;

    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#SUBSCRIBE">SUBSCRIBE frame</see>.
    /// </summary>
    public class SubscribeFrame : StompFrame
    {
        public SubscribeFrame(string destination)
            : this(destination, "auto")
        {
        }

        public SubscribeFrame(string destination, string ackType)
            : base(StompCommand.Subscribe)
        {
            this.Headers["destination"] = destination;
            this.Headers["id"] = Guid.NewGuid().ToString();
            this.Headers["ack"] = ackType;
        }

        public StompHeaders RequiredHeadersWithSamples { get; } = new StompHeaders()
        {
            { StompHeaders.Id, "0" },
            { StompHeaders.Destination, "/queue/a" },
        };

        public StompHeaders OptionalHeadersWithSamples { get; } = new StompHeaders()
        {
            { StompHeaders.ContentLength, "123" },
            { StompHeaders.ContentType, "text/plain" },
            { StompHeaders.Receipt, "message-12345" },
            { StompHeaders.Ack, "client" },
        };

        public string Destination
        {
            get
            {
                return this.Headers["destination"];
            }
        }

        public string Id
        {
            get
            {
                return this.Headers["id"];
            }
        }

        public string Ack
        {
            get
            {
                return this.Headers["ack"];
            }
        }
    }
}
