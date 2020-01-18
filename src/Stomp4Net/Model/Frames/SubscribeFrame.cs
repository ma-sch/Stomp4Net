namespace Stomp4Net.Model.Frames
{
    using System;

    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#SUBSCRIBE">SUBSCRIBE frame</see>.
    /// </summary>
    public class SubscribeFrame : BaseStompFrame<SubscribeFrameHeaders>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscribeFrame"/> class with ack type set to 'auto'.
        /// </summary>
        /// <param name="destination">Destination for which the client wants to subscribe.</param>
        public SubscribeFrame(string destination)
            : this(destination, "auto")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscribeFrame"/> class.
        /// </summary>
        /// <param name="destination">Destination for which the client wants to subscribe.</param>
        /// <param name="ackType">Message acknowledgment mode.</param>
        public SubscribeFrame(string destination, string ackType)
            : base(StompCommand.Subscribe)
        {
            this.Headers.Destination = destination;
            this.Headers.Id = Guid.NewGuid().ToString();
            this.Headers.Ack = ackType;
        }
    }
}
