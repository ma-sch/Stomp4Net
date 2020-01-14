namespace Stomp4Net.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Headers for Stomp frames.
    /// </summary>
    public class StompHeaders : Dictionary<string, string>
    {
        public const string AcceptVersion = "accept-version";

        public const string Ack = "ack";

        public const string ContentLength = "content-length";

        public const string ContentType = "content-type";

        public const string Destination = "destination";

        public const string Heartbeat = "heart-beat";

        public const string Host = "host";

        public const string Id = "id";

        public const string Login = "login";

        public const string Message = "message";

        public const string MessageId = "message-id";

        public const string Passcode = "passcode";

        public const string Receipt = "receipt";

        public const string ReceiptId = "receipt-id";

        public const string Server = "server";

        public const string Session = "session";

        public const string Subscription = "subscription";

        public const string Transaction = "transaction";

        public const string Version = "version";
    }
}
