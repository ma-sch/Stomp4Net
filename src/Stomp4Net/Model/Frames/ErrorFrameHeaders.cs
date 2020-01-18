namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Headers for <see cref="AbortFrame"/>.
    /// </summary>
    public class ErrorFrameHeaders : BaseStompHeaders
    {
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
        /// Gets or sets the message header of the frame.
        /// </summary>
        public string Message
        {
            get { return this.GetHeaderValue(MessageKey); }
            set { this.SetHeaderValue(MessageKey, value); }
        }

        public BaseStompHeaders RequiredHeadersWithSamples { get; } = new BaseStompHeaders()
        {
        };

        public BaseStompHeaders OptionalHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { ContentLengthKey, "123" },
            { ContentTypeKey, "text/plain" },
            { ReceiptKey, "message-12345" },
            { MessageKey, "error occured" },
        };
    }
}
