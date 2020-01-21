namespace Stomp4Net.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Stomp4Net.EventArguments;
    using Stomp4Net.Logging;
    using Stomp4Net.Model.Frames;

    public abstract class StompClient
    {
        private static readonly ILog Log = LogProvider.For<StompClient>();

        /// <summary> Gets or sets the timer which triggers send of STOMP heartbeat.</summary>
        protected System.Timers.Timer StompHeartbeatSendTimer { get; set;  }

        /// <summary> Gets or sets the timer which will elapse if STOMP heartbeat is missing.</summary>
        protected System.Timers.Timer StompHeartbeatReceiveTimer { get; set; }

        protected Dictionary<string, Action<object, EventArguments.MessageReceivedEventArgs>> subscriptionCallbacks
            = new Dictionary<string, Action<object, EventArguments.MessageReceivedEventArgs>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="StompClient"/> class.
        /// </summary>
        public StompClient()
        {
        }

        public event EventHandler<ConnectedEventArgs> Connected;

        public event EventHandler<DisconnectedEventArgs> Disconnected;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public event EventHandler<ReceiptReceivedEventArgs> ReceiptReceived;

        public event EventHandler<ErrorReceivedEventArgs> ErrorReceived;

        public bool AutoReconnectEnabled { get; private set; } = true;

        public int AutoReconnectInterval { get; private set; } = 5000;

        public TimeSpan StompHeartbeatIntervalFromClientToServer { get; set; } = TimeSpan.FromMilliseconds(10000);


        public TimeSpan StompHeartbeatIntervalFromServerToClient { get; set; } = TimeSpan.FromMilliseconds(10000);

        public TimeSpan StompHeartbeatTimeout { get; set; } = TimeSpan.FromMilliseconds(5000);

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

        public void Subscribe(string topic, Action<object, MessageReceivedEventArgs> callback)
        {
            this.Subscribe(topic);
            this.subscriptionCallbacks[topic] = callback;
        }

        protected abstract void SendStompFrame(IStompFrame stompFrame);

        protected abstract void SendEOL();

        protected void InvokeConnectedEvent(ConnectedEventArgs eventArgs)
        {
            this.Connected?.Invoke(this, eventArgs);
        }

        protected void InvokeDisconnectedEvent(DisconnectedEventArgs eventArgs)
        {
            this.Disconnected?.Invoke(this, eventArgs);
        }

        protected void InvokeMessageReceivedEvent(MessageReceivedEventArgs eventArgs)
        {
            this.MessageReceived?.Invoke(this, eventArgs);
        }

        protected void InvokeReceiptReceivedEvent(ReceiptReceivedEventArgs eventArgs)
        {
            this.ReceiptReceived?.Invoke(this, eventArgs);
        }

        protected void InvokeErrorReceivedEvent(ErrorReceivedEventArgs eventArgs)
        {
            this.ErrorReceived?.Invoke(this, eventArgs);
        }
    }
}
