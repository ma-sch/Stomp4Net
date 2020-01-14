namespace Stomp4Net.EventArguments
{
    using System;
    using Stomp4Net.Model.Frames;

    public class ConnectedEventArgs : EventArgs
    {
        public ConnectedEventArgs(ConnectedFrame connectedFrame)
        {
            this.ConnectedFrame = connectedFrame;
        }

        public ConnectedFrame ConnectedFrame { get; private set; }
    }
}
