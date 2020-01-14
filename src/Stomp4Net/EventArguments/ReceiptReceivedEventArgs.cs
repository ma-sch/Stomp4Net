namespace Stomp4Net.EventArguments
{
    using System;
    using Stomp4Net.Model;

    public class ReceiptReceivedEventArgs : EventArgs
    {
        public ReceiptReceivedEventArgs(ReceiptFrame receiptFrame)
        {
            this.ReceiptFrame = receiptFrame;
        }

        public ReceiptFrame ReceiptFrame { get; private set; }
    }
}