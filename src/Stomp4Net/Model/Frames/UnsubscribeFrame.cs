namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#UNSUBSCRIBE">UNSUBSCRIBE frame</see>.
    /// </summary>
    public class UnsubscribeFrame : StompFrame
    {
        public UnsubscribeFrame()
            : base(StompCommand.Unsubscribe)
        {
        }
    }
}
