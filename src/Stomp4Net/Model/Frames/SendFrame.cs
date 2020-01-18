namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#SEND">SEND frame</see>.
    /// </summary>
    public class SendFrame : BaseStompFrame<SendFrameHeaders>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendFrame"/> class.
        /// </summary>
        /// <param name="destination">Destination where the message should send to.</param>
        /// <param name="body">Content of the message.</param>
        /// <param name="contentType">Content type of the message.</param>
        public SendFrame(string destination, string body, string contentType)
            : base(StompCommand.Send, body)
        {
            this.Headers.Destination = destination;
            this.Headers.ContentType = contentType;
        }
    }
}
