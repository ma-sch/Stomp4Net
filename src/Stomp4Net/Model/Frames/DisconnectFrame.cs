namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#DISCONNECT">DISCONNECT frame</see>.
    /// </summary>
    public class DisconnectFrame : StompFrame
    {
        public DisconnectFrame()
            : base(StompCommand.Disconnect)
        {
        }

        public StompHeaders RequiredHeadersWithSamples { get; } = new StompHeaders()
        {
        };

        public StompHeaders OptionalHeadersWithSamples { get; } = new StompHeaders()
        {
            { StompHeaders.ContentLength, "123" },
            { StompHeaders.ContentType, "text/plain" },
            { StompHeaders.Receipt, "message-12345" },
        };
    }
}
