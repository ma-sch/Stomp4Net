namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#CONNECT_or_STOMP_Frame">CONNECT frame</see>.
    /// </summary>
    public class ConnectFrame : StompFrame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectFrame"/> class.
        /// </summary>
        /// <param name="host">The name of a virtual host that the client wishes to connect to.
        /// It is recommended clients set this to the host name that the socket was established against,
        /// or to any name of their choosing. If this header does not match a known virtual host, servers
        /// supporting virtual hosting MAY select a default virtual host or reject the connection.
        /// </param>
        public ConnectFrame(string host)
            : base(StompCommand.Connect)
        {
            this.Headers[StompHeaders.AcceptVersion] = StompConfig.StompMinProtocolVersion;
            this.Headers[StompHeaders.Host] = host;
        }
    }
}
