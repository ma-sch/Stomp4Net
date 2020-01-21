using System;

namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#CONNECT_or_STOMP_Frame">CONNECT frame</see>.
    /// </summary>
    public class ConnectFrame : BaseStompFrame<ConnectFrameHeaders>
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
            this.Headers.AcceptVersion = StompConfig.StompMinProtocolVersion;
            this.Headers.Host = host;
        }

        /// <summary>
        /// Gets the interval the client will send heartbeats.
        /// </summary>
        public TimeSpan ClientSendingHeartbeatInterval
        {
            get
            {
                var interval = int.Parse(this.Headers.Heartbeat.Split(",")[0]);
                return TimeSpan.FromMilliseconds(interval);
            }
        }

        /// <summary>
        /// Gets the heartbeat interval expected by the client.
        /// </summary>
        public TimeSpan ClientExpectedHeartbeatInterval
        {
            get
            {
                var interval = int.Parse(this.Headers.Heartbeat.Split(",")[1]);
                return TimeSpan.FromMilliseconds(interval);
            }
        }
    }
}
