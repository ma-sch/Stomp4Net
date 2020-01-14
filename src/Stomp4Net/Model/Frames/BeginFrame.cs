namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#BEGIN">BEGIN frame</see>.
    /// </summary>
    public class BeginFrame : StompFrame
    {
        public BeginFrame()
            : base(StompCommand.Begin)
        {
        }
    }
}
