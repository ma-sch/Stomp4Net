namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#ACK">ACK frame</see>.
    /// </summary>
    public class AckFrame : BaseStompFrame<AckFrameHeaders>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AckFrame"/> class.
        /// </summary>
        /// <param name="id">Id matching the ack header of the message being acknowledged.</param>
        public AckFrame(string id)
            : base(StompCommand.Ack)
        {
            this.Headers.Id = id;
        }
    }
}
