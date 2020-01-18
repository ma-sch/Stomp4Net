namespace Stomp4Net.EventArguments
{
    using System;
    using Stomp4Net.Model.Frames;

    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(MessageFrame messageFrame)
        {
            this.MessageFrame = messageFrame;
        }

        public MessageFrame MessageFrame { get; private set; }

        public string Topic
        {
            get
            {
                return this.MessageFrame.Headers.Destination;
            }
        }

        public string Message
        {
            get
            {
                return this.MessageFrame.Body;
            }
        }
    }
}
