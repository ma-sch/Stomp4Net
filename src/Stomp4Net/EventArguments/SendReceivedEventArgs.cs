namespace Stomp4Net.EventArguments
{
    using System;
    using Stomp4Net.Model.Frames;

    public class SendReceivedEventArgs : EventArgs
    {
        public SendReceivedEventArgs(Guid sessionId, SendFrame sendFrame)
        {
            this.SessionId = sessionId;
            this.SendFrame = sendFrame;
        }

        public SendFrame SendFrame { get; private set; }

        public Guid SessionId
        {
            get;
        }

        public string Destination
        {
            get
            {
                return this.SendFrame.Destination;
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
