namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Headers for <see cref="AbortFrame"/>.
    /// </summary>
    public class CommitFrameHeaders : BaseStompHeaders
    {
        /// <summary>
        /// Gets or sets the transaction header of the frame.
        /// </summary>
        public string Transaction
        {
            get { return this.GetHeaderValue(TransactionKey); }
            set { this.SetHeaderValue(TransactionKey, value); }
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
        /// Gets or sets the content-type header of the frame.
        /// </summary>
        public string ContentType
        {
            get { return this.GetHeaderValue(ContentTypeKey); }
            set { this.SetHeaderValue(ContentTypeKey, value); }
        }

        /// <summary>
        /// Gets or sets the content-leng´th header of the frame.
        /// </summary>
        public string ContentLength
        {
            get { return this.GetHeaderValue(ContentLengthKey); }
            set { this.SetHeaderValue(ContentLengthKey, value); }
        }

        public BaseStompHeaders RequiredHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { BaseStompHeaders.TransactionKey, "tx1" },
        };

        public BaseStompHeaders OptionalHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { BaseStompHeaders.ContentLengthKey, "123" },
            { BaseStompHeaders.ContentTypeKey, "text/plain" },
            { BaseStompHeaders.ReceiptKey, "message-12345" },
        };
    }
}
