namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#COMMIT">COMMIT frame</see>.
    /// </summary>
    public class CommitFrame : StompFrame
    {
        public CommitFrame()
            : base(StompCommand.Commit)
        {
        }
    }
}
