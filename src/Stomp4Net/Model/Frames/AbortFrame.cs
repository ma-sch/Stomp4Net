namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#ABORT">ABORT frame</see>.
    /// </summary>
    public class AbortFrame : StompFrame
    {
        public AbortFrame()
            : base(StompCommand.Abort)
        {
        }

        public StompHeaders RequiredHeadersWithSamples { get; } = new StompHeaders()
        {
            { StompHeaders.Transaction, "tx1" },
        };

        public StompHeaders OptionalHeadersWithSamples { get; } = new StompHeaders()
        {
            { StompHeaders.ContentLength, "123" },
            { StompHeaders.ContentType, "text/plain" },
            { StompHeaders.Receipt, "message-12345" },
        };
    }
}
