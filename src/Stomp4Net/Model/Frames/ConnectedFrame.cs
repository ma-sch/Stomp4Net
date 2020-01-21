namespace Stomp4Net.Model.Frames
{
    using System;

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

        /// <summary>
        /// Gets the the interval the server will send heartbeats.
        /// </summary>
        public TimeSpan ServerSendingHeartbeatInterval
        {
            get
            {
                var interval = int.Parse(this.Headers.Heartbeat.Split(",")[0]);
                return TimeSpan.FromMilliseconds(interval);
            }
        }

        /// <summary>
        /// Gets the heartbeat interval expected by the server.
        /// </summary>
        public TimeSpan ServerExpectedHeartbeatInterval
        {
            get
            {
                var interval = int.Parse(this.Headers.Heartbeat.Split(",")[1]);
                return TimeSpan.FromMilliseconds(interval);
            }
        }
    }
}
