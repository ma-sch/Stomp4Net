namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Headers for <see cref="AbortFrame"/>.
    /// </summary>
    public class BeginFrameHeaders : BaseStompHeaders
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

        public BaseStompHeaders RequiredHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { BaseStompHeaders.TransactionKey, "tx1" },
        };

        public BaseStompHeaders OptionalHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { BaseStompHeaders.ReceiptKey, "message-12345" },
        };
    }
}
