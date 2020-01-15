namespace Stomp4Net.Model
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#RECEIPT">RECEIPT frame</see>.
    /// </summary>
    public class ReceiptFrame : StompFrame
    {
        public ReceiptFrame()
            : base(StompCommand.Receipt)
        {
        }

        public StompHeaders RequiredHeadersWithSamples { get; } = new StompHeaders()
        {
            { StompHeaders.ReceiptId, "receipt-12345" },
        };

        public StompHeaders OptionalHeadersWithSamples { get; } = new StompHeaders()
        {
            { StompHeaders.ContentLength, "123" },
            { StompHeaders.ContentType, "text/plain" },
            { StompHeaders.Receipt, "message-12345" },
        };
    }
}