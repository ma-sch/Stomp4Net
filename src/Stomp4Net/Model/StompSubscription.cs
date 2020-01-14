namespace Stomp4Net.Model
{
    using System;

    internal class StompSubscription
    {
        internal StompSubscription(string id, Guid sessionId)
        {
            this.Id = id;
            this.SessionId = sessionId;
        }

        public string Id { get; private set; }

        public Guid SessionId { get; private set; }
    }
}
