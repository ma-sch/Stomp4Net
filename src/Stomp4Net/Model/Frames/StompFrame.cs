namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#CONNECT_or_STOMP_Frame">STOMP frame</see>.
    /// </summary>
    public class StompFrame : BaseStompFrame<ConnectFrameHeaders>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StompFrame"/> class.
        /// </summary>
        /// <param name="host">The name of a virtual host that the client wishes to connect to.
        /// It is recommended clients set this to the host name that the socket was established against,
        /// or to any name of their choosing. If this header does not match a known virtual host, servers
        /// supporting virtual hosting MAY select a default virtual host or reject the connection.
        /// </param>
        public StompFrame(string host)
            : base(StompCommand.Stomp)
        {
            this.Headers.AcceptVersion = StompConfig.StompMinProtocolVersion;
            this.Headers.Host = host;
        }
    }
}
