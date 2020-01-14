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
    }
}
