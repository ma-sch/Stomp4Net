namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Headers for <see cref="AbortFrame"/>.
    /// </summary>
    public class ConnectedFrameHeaders : BaseStompHeaders
    {
        /// <summary>
        /// Gets or sets the version header of the frame.
        /// </summary>
        public string Version
        {
            get { return this.GetHeaderValue(VersionKey); }
            set { this.SetHeaderValue(VersionKey, value); }
        }

        /// <summary>
        /// Gets or sets the session header of the frame.
        /// </summary>
        public string Session
        {
            get { return this.GetHeaderValue(SessionKey); }
            set { this.SetHeaderValue(SessionKey, value); }
        }

        /// <summary>
        /// Gets or sets the server header of the frame.
        /// </summary>
        public string Server
        {
            get { return this.GetHeaderValue(ServerKey); }
            set { this.SetHeaderValue(ServerKey, value); }
        }

        /// <summary>
        /// Gets or sets the heart-beat header of the frame.
        /// </summary>
        public string Heartbeat
        {
            get { return this.GetHeaderValue(HeartbeatKey); }
            set { this.SetHeaderValue(HeartbeatKey, value); }
        }

        /// <summary>
        /// Gets or sets the receipt header of the frame.
        /// </summary>
        public string Receipt
        {
            get { return this.GetHeaderValue(ReceiptKey); }
            set { this.SetHeaderValue(ReceiptKey, value); }
        }

        public BaseStompHeaders RequiredHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { BaseStompHeaders.VersionKey, "1.2" },
        };

        public BaseStompHeaders OptionalHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { BaseStompHeaders.ReceiptKey, "message-12345" },
            { BaseStompHeaders.SessionKey, "session-12345" },
            { BaseStompHeaders.ServerKey, "localhost" },
            { BaseStompHeaders.HeartbeatKey, "0,0" },
        };
    }
}
