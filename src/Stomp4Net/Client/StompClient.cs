namespace Stomp4Net.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Stomp4Net.EventArguments;
    using Stomp4Net.Logging;
    using Stomp4Net.Model.Frames;
    using SuperSocket.ClientEngine;
    using WebSocket4Net;

    public abstract class StompClient
    {
        private static readonly ILog Log = LogProvider.For<StompClient>();

        protected bool autoSendPingEnabled = true;

        protected int autoSendPingIntervalInMilliseconds = 50;

        protected Dictionary<string, Action<object, EventArguments.MessageReceivedEventArgs>> subscriptionCallbacks
            = new Dictionary<string, Action<object, EventArguments.MessageReceivedEventArgs>>();

        public StompClient()
        {
        }

        public event EventHandler<ConnectedEventArgs> Connected;

        public event EventHandler<EventArguments.MessageReceivedEventArgs> MessageReceived;

        public event EventHandler<ReceiptReceivedEventArgs> ReceiptReceived;

        public event EventHandler<ErrorReceivedEventArgs> ErrorReceived;

        public bool AutoReconnectEnabled { get; private set; } = true;

        public int AutoReconnectInterval { get; private set; } = 5000;

        public virtual bool AutoSendPingEnabled { get; set; }

        public virtual int AutoSendPingIntervalInMilliseconds { get; set; }

        public abstract void Connect();

        public abstract Task ConnectAsync();

        public void Send(string topic, string body, string contentType)
        {
            var sendFrame = new SendFrame(topic, body, contentType);
            this.SendStompFrame(sendFrame);
        }

        public void Subscribe(string topic)
        {
            var subscribeFrame = new SubscribeFrame(topic);
            this.SendStompFrame(subscribeFrame);
        }

        public void Subscribe(string topic, Action<object, EventArguments.MessageReceivedEventArgs> callback)
        {
            this.Subscribe(topic);
            this.subscriptionCallbacks[topic] = callback;
        }

        protected abstract void SendStompFrame(IStompFrame stompFrame);
    }
}
