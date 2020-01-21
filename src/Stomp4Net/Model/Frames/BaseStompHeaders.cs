namespace Stomp4Net.Model.Frames
{
    using System.Collections.Generic;

    /// <summary>
    /// Headers for Stomp frames.
    /// </summary>
    public class BaseStompHeaders : Dictionary<string, string>, IStompHeaders
    {
        public const string AcceptVersionKey = "accept-version";

        public const string AckKey = "ack";

        public const string ContentLengthKey = "content-length";

        public const string ContentTypeKey = "content-type";

        public const string DestinationKey = "destination";

        public const string HeartbeatKey = "heart-beat";

        public const string HostKey = "host";

        public const string IdKey = "id";

        public const string LoginKey = "login";

        public const string MessageKey = "message";

        public const string MessageIdKey = "message-id";

        public const string PasscodeKey = "passcode";

        public const string ReceiptKey = "receipt";

        public const string ReceiptIdKey = "receipt-id";

        public const string ServerKey = "server";

        public const string SessionKey = "session";

        public const string SubscriptionKey = "subscription";

        public const string TransactionKey = "transaction";

        public const string VersionKey = "version";

        protected string GetHeaderValue(string headerKey)
        {
            if (this.ContainsKey(headerKey))
            {
                switch (headerKey)
                {
                    case HeartbeatKey:
                        var heartbeat = this[headerKey].Equals(string.Empty) ? "0,0" : this[headerKey];
                        return heartbeat;

                    default:
                        return this[headerKey];
                }
            }
            else
            {
                switch (headerKey)
                {
                    case HeartbeatKey:
                        return "0,0";

                    default:
                        return null;
                        //throw new HeaderNotSetException();
                }
            }
        }

        protected void SetHeaderValue(string headerKey, string value)
        {
            this[headerKey] = value;
        }
    }
}
