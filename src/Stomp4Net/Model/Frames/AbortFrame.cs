namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#ABORT">ABORT frame</see>.
    /// </summary>
    public class AbortFrame : StompFrame
    {
        public AbortFrame()
            : base(StompCommand.Abort)
        {
        }
    }
}
