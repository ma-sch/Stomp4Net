namespace Stomp4Net.Model
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#ERROR">ERROR frame</see>.
    /// </summary>
    public class ErrorFrame : StompFrame
    {
        public ErrorFrame()
            : base(StompCommand.Error)
        {
        }
    }
}