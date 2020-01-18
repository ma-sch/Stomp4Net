namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#ERROR">ERROR frame</see>.
    /// </summary>
    public class ErrorFrame : BaseStompFrame<ErrorFrameHeaders>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorFrame"/> class.
        /// </summary>
        /// <param name="message">Message header with a short description of the error.</param>
        /// <param name="body">Body with detailed information about the error.</param>
        public ErrorFrame(string message, string body)
            : base(StompCommand.Error)
        {
            this.Headers.Message = message;
        }
    }
}