
namespace Stomp4Net.EventArguments
{
    using System;

    public class ClientDisconnectedEventArgs : EventArgs
    {
        public ClientDisconnectedEventArgs(string sessionId)
        {
            this.SessionId = sessionId;
        }

        /// <summary>
        /// Gets the id of the session for the disconnected client.
        /// </summary>
        public string SessionId { get; private set; }
    }
}