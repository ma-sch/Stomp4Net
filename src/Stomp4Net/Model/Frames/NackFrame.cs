namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#NACK">NACK frame</see>.
    /// </summary>
    public class NackFrame : BaseStompFrame<NackFrameHeaders>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NackFrame"/> class.
        /// </summary>
        /// <param name="id">Id matching the ack header of the message being not acknowledged.</param>
        public NackFrame(string id)
            : base(StompCommand.Ack)
        {
            this.Headers.Id = id;
        }
    }
}
