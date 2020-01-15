namespace Stomp4Net.Model.Frames
{
    using Stomp4Net.Exceptions;

    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#CONNECTED_Frame">CONNECTED frame</see>.
    /// </summary>
    public class ConnectedFrame : StompFrame
    {
        public ConnectedFrame(StompHeaders headers)
            : base(StompCommand.Connected)
        {
            this.Headers = headers;
            this.Version = headers.ContainsKey(StompHeaders.Version) ? headers[StompHeaders.Version] : throw new HeaderMissingException("Header 'version' is missing in CONNECTED frame");
            this.Session = headers.ContainsKey(StompHeaders.Session) ? headers[StompHeaders.Session] : "Not provided by server";
            this.Server = headers.ContainsKey(StompHeaders.Server) ? headers[StompHeaders.Server] : "Not provided by server";
        }


        public StompHeaders RequiredHeadersWithSamples { get; } = new StompHeaders()
        {
            { StompHeaders.Version, "1.2" },
        };

        public StompHeaders OptionalHeadersWithSamples { get; } = new StompHeaders()
        {
            { StompHeaders.ContentLength, "123" },
            { StompHeaders.ContentType, "text/plain" },
            { StompHeaders.Receipt, "message-12345" },
            { StompHeaders.Session, "session-12345" },
            { StompHeaders.Server, "localhost" },
            { StompHeaders.Heartbeat, "0,0" },
        };

        public string Version
        {
            get
            {
                return this.Headers[StompHeaders.Version];
            }

            private set
            {
                this.Headers[StompHeaders.Version] = value;
            }
        }

        public string Session
        {
            get
            {
                return this.Headers[StompHeaders.Session];
            }

            private set
            {
                this.Headers[StompHeaders.Session] = value;
            }
        }

        public string Server
        {
            get
            {
                return this.Headers[StompHeaders.Server];
            }

            private set
            {
                this.Headers[StompHeaders.Server] = value;
            }
        }
    }
}
