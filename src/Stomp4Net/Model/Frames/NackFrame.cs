namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#NACK">NACK frame</see>.
    /// </summary>
    public class NackFrame : StompFrame
    {
        public NackFrame()
            : base(StompCommand.Nack)
        {
        }
    }
}
