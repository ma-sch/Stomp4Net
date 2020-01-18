namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Headers for <see cref="AbortFrame"/>.
    /// </summary>
    public class StompFrameHeaders : BaseStompHeaders
    {
        /// <summary>
        /// Gets or sets the accept-version header of the frame.
        /// </summary>
        public string AcceptVersion
        {
            get { return this.GetHeaderValue(AcceptVersionKey); }
            set { this.SetHeaderValue(AcceptVersionKey, value); }
        }

        /// <summary>
        /// Gets or sets the host header of the frame.
        /// </summary>
        public string Host
        {
            get { return this.GetHeaderValue(HostKey); }
            set { this.SetHeaderValue(HostKey, value); }
        }

        /// <summary>
        /// Gets or sets the login header of the frame.
        /// </summary>
        public string Login
        {
            get { return this.GetHeaderValue(LoginKey); }
            set { this.SetHeaderValue(LoginKey, value); }
        }

        /// <summary>
        /// Gets or sets the passcode header of the frame.
        /// </summary>
        public string Passcode
        {
            get { return this.GetHeaderValue(PasscodeKey); }
            set { this.SetHeaderValue(PasscodeKey, value); }
        }

        /// <summary>
        /// Gets or sets the heart-beat header of the frame.
        /// </summary>
        public string Heartbeat
        {
            get { return this.GetHeaderValue(HeartbeatKey); }
            set { this.SetHeaderValue(HeartbeatKey, value); }
        }

        public BaseStompHeaders RequiredHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { BaseStompHeaders.AcceptVersionKey, "1.2" },
            { BaseStompHeaders.HostKey, "localhost" },
        };

        public BaseStompHeaders OptionalHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { BaseStompHeaders.LoginKey, "SampleUser" },
            { BaseStompHeaders.PasscodeKey, "SamplePassword" },
            { BaseStompHeaders.HeartbeatKey, "0,0" },
        };
    }
}
