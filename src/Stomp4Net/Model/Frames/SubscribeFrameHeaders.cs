namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Headers for <see cref="AbortFrame"/>.
    /// </summary>
    public class SubscribeFrameHeaders : BaseStompHeaders
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
            { BaseStompHeaders.IdKey, "0" },
            { BaseStompHeaders.DestinationKey, "/queue/a" },
        };

        public BaseStompHeaders OptionalHeadersWithSamples { get; } = new BaseStompHeaders()
        {
            { BaseStompHeaders.ReceiptKey, "message-12345" },
            { BaseStompHeaders.AckKey, "client" },
        };
    }
}
