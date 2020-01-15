namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#ACK">ACK frame</see>.
    /// </summary>
    public class AckFrame : StompFrame
    {
        public AckFrame()
            : base(StompCommand.Ack)
        {
        }

        public StompHeaders RequiredHeadersWithSamples { get; } = new StompHeaders()
        {
            { StompHeaders.Id, "0" },
        };

        public StompHeaders OptionalHeadersWithSamples { get; } = new StompHeaders()
        {
            { StompHeaders.ContentLength, "123" },
            { StompHeaders.ContentType, "text/plain" },
            { StompHeaders.Receipt, "message-12345" },
            { StompHeaders.Transaction, "tx1" },
        };
    }
}
