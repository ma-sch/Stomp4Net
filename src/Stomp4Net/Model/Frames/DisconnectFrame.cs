namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#DISCONNECT">DISCONNECT frame</see>.
    /// </summary>
    public class DisconnectFrame : StompFrame
    {
        public DisconnectFrame()
            : base(StompCommand.Disconnect)
        {
        }
    }
}
