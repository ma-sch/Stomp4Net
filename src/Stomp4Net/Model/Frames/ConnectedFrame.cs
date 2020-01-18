namespace Stomp4Net.Model.Frames
{
    using Stomp4Net.Exceptions;

    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#CONNECTED_Frame">CONNECTED frame</see>.
    /// </summary>
    public class ConnectedFrame : BaseStompFrame<ConnectedFrameHeaders>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectedFrame"/> class.
        /// </summary>
        /// <param name="version">The version of the STOMP protocol the session will be using.</param>
        public ConnectedFrame(string version)
            : base(StompCommand.Connected)
        {
            this.Headers.Version = version;
        }
    }
}
