namespace Stomp4Net.Model.Frames
{
    using System;

    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#MESSAGE">MESSAGE frame</see>.
    /// </summary>
    public class MessageFrame : BaseStompFrame<MessageFrameHeaders>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageFrame"/> class.
        /// </summary>
        /// <param name="destination">Destination where the message is sent to.</param>
        /// <param name="subscriptionId">Id of the subscription that is receiving the message.</param>
        /// <param name="body">Content of the message-</param>
        /// <param name="contentType">Content type of the message</param>
        public MessageFrame(string destination, string subscriptionId, string body, String contentType)
            : base(StompCommand.Message, body)
        {
            this.Headers.Destination = destination;
            this.Headers.ContentType = contentType;
            this.Headers.Subscription = subscriptionId;
            this.Headers.MessageId = Guid.NewGuid().ToString();
        }
    }
}