namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Headers for <see cref="AbortFrame"/>.
    /// </summary>
    public class UnsubscribeFrameHeaders : BaseStompHeaders
    {
        /// <summary>
        /// Gets or sets the id header of the frame.
        /// </summary>
        public string Id
        {
            get { return this.GetHeaderValue(IdKey); }
            set { this.SetHeaderValue(IdKey, value); }
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
            { BaseStompHeaders.IdKey, "0" },
        };

        public BaseStompHeaders OptionalHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { BaseStompHeaders.ReceiptKey, "message-12345" },
        };
    }
}
