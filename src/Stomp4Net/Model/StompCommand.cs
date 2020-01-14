namespace Stomp4Net.Model
{
    public static class StompCommand
    {
        #region Client Frames

        // https://stomp.github.io/stomp-specification-1.2.html#Client_Frames
        public const string Abort = "ABORT";
        public const string Ack = "ACK";
        public const string Begin = "BEGIN";
        public const string Commit = "COMMIT";
        public const string Connect = "CONNECT";
        public const string Disconnect = "DISCONNECT";
        public const string Nack = "NACK";
        public const string Send = "SEND";
        public const string Stomp = "STOMP";
        public const string Subscribe = "SUBSCRIBE";
        public const string Unsubscribe = "UNSUBSCRIBE";
        #endregion Client Commands

        #region Server Frames

        // https://stomp.github.io/stomp-specification-1.2.html#Server_Frames
        public const string Connected = "CONNECTED";
        public const string Message = "MESSAGE";
        public const string Error = "ERROR";
        public const string Receipt = "RECEIPT";
        #endregion Server Frames
    }
}