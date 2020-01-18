namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Headers for <see cref="AbortFrame"/>.
    /// </summary>
    public class MessageFrameHeaders : BaseStompHeaders
    {
        /// <summary>
        /// Gets or sets the destination header of the frame.
        /// </summary>
        public string Destination
        {
            get { return this.GetHeaderValue(DestinationKey); }
            set { this.SetHeaderValue(DestinationKey, value); }
        }

        /// <summary>
        /// Gets or sets the message-id header of the frame.
        /// </summary>
        public string MessageId
        {
            get { return this.GetHeaderValue(MessageIdKey); }
            set { this.SetHeaderValue(MessageIdKey, value); }
        }

        /// <summary>
        /// Gets or sets the subscription header of the frame.
        /// </summary>
        public string Subscription
        {
            get { return this.GetHeaderValue(SubscriptionKey); }
            set { this.SetHeaderValue(SubscriptionKey, value); }
        }

        /// <summary>
        /// Gets or sets the content-length header of the frame.
        /// </summary>
        public string ContentLength
        {
            get { return this.GetHeaderValue(ContentLengthKey); }
            set { this.SetHeaderValue(ContentLengthKey, value); }
        }

        /// <summary>
        /// Gets or sets the content-type header of the frame.
        /// </summary>
        public string ContentType
        {
            get { return this.GetHeaderValue(ContentTypeKey); }
            set { this.SetHeaderValue(ContentTypeKey, value); }
        }

        /// <summary>
        /// Gets or sets the receipt header of the frame.
        /// </summary>
        public string Receipt
        {
            get { return this.GetHeaderValue(ReceiptKey); }
            set { this.SetHeaderValue(ReceiptKey, value); }
        }

        /// <summary>
        /// Gets or sets the ack header of the frame.
        /// </summary>
        public string Ack
        {
            get { return this.GetHeaderValue(AckKey); }
            set { this.SetHeaderValue(AckKey, value); }
        }

        public BaseStompHeaders RequiredHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { DestinationKey, "/queue/a" },
            { MessageIdKey, "007" },
            { SubscriptionKey, "0" },
        };

        public BaseStompHeaders OptionalHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { ContentLengthKey, "123" },
            { ContentTypeKey, "text/plain" },
            { ReceiptKey, "message-12345" },
            { AckKey, "text/plain" },
        };
    }
}
