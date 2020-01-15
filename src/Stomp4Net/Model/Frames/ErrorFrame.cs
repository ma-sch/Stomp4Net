namespace Stomp4Net.Model
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#ERROR">ERROR frame</see>.
    /// </summary>
    public class ErrorFrame : StompFrame
    {
        public ErrorFrame()
            : base(StompCommand.Error)
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
            { StompHeaders.Message, "error occured" },
        };
    }
}