namespace Stomp4Net.Model
{
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
