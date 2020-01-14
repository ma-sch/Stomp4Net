namespace Stomp4Net.Model
{
    /// <summary>
    /// Acknowledgment types for Stomp subscriptions.
    /// </summary>
    public static class SubscribeAckType
    {
        public const string Client = "client";

        public const string Auto = "auto";

        public const string ClientIndividual = "client-individual";
    }
}
