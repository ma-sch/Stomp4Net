namespace Stomp4Net.Model
{
    using System;

    public class StompSubscription
    {
        internal StompSubscription(string id, string sessionId)
        {
            this.Id = id;
            this.SessionId = sessionId;
        }

        public string Id { get; private set; }

        public string SessionId { get; private set; }
    }
}
