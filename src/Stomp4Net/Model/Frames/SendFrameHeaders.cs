namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Headers for <see cref="AbortFrame"/>.
    /// </summary>
    public class SendFrameHeaders : BaseStompHeaders
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
        /// Gets or sets the transaction header of the frame.
        /// </summary>
        public string Transaction
        {
            get { return this.GetHeaderValue(TransactionKey); }
            set { this.SetHeaderValue(TransactionKey, value); }
        }

        public BaseStompHeaders RequiredHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { BaseStompHeaders.DestinationKey, "/queue/a" },
        };

        public BaseStompHeaders OptionalHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { BaseStompHeaders.ContentLengthKey, "123" },
            { BaseStompHeaders.ContentTypeKey, "text/plain" },
            { BaseStompHeaders.ReceiptKey, "message-12345" },
            { BaseStompHeaders.TransactionKey, "message-12345" },
        };
    }
}
