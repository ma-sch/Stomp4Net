
namespace Stomp4Net.EventArguments
{
    using System;

    public class ClientConnectedEventArgs : EventArgs
    {
        public ClientConnectedEventArgs(string sessionId)
        {
            this.SessionId = sessionId;
        }

        /// <summary>
        /// Gets the id of the session for the newly connected client.
        /// </summary>
        public string SessionId { get; private set; }
    }
}