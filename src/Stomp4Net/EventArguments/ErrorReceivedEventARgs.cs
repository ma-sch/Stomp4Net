namespace Stomp4Net.EventArguments
{
    using System;
    using Stomp4Net.Model;

    public class ErrorReceivedEventArgs : EventArgs
    {
        public ErrorReceivedEventArgs(ErrorFrame errorFrame)
        {
            this.ErrorFrame = errorFrame;
        }

        public ErrorFrame ErrorFrame { get; private set; }
    }
}