namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Headers for <see cref="AbortFrame"/>.
    /// </summary>
    public class ReceiptFrameHeaders : BaseStompHeaders
    {
        /// <summary>
        /// Gets or sets the receipt-id header of the frame.
        /// </summary>
        public string ReceiptId
        {
            get { return this.GetHeaderValue(ReceiptIdKey); }
            set { this.SetHeaderValue(ReceiptIdKey, value); }
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
            { ReceiptIdKey, "receipt-12345" },
        };

        public BaseStompHeaders OptionalHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { ReceiptKey, "message-12345" },
        };
    }
}
