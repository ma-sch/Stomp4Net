namespace Stomp4Net.EventArguments
{
    using System;
    using Stomp4Net.Model.Frames;

    public class SendReceivedEventArgs : EventArgs
    {
        public SendReceivedEventArgs(string sessionId, SendFrame sendFrame)
        {
            this.SessionId = sessionId;
            this.SendFrame = sendFrame;
        }

        public SendFrame SendFrame { get; private set; }

        public string SessionId
        {
            get;
        }

        public string Destination
        {
            get
            {
                return this.SendFrame.Headers.Destination;
            }
        }

        public string Content
        {
            get
            {
                return this.SendFrame.Body;
            }
        }
    }
}
