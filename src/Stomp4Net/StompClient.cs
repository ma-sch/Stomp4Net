namespace Stomp4Net
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Stomp4Net.EventArguments;
    using Stomp4Net.Logging;
    using Stomp4Net.Model;
    using Stomp4Net.Model.Frames;
    using SuperSocket.ClientEngine;
    using WebSocket4Net;

    public class StompClient
    {
        private static readonly ILog Log = LogProvider.For<StompClient>();

        private WebSocket websocket;

        private bool autoSendPingEnabled = true;

        private int autoSendPingIntervalInMilliseconds = 50;

        private Dictionary<string, Action<object, EventArguments.MessageReceivedEventArgs>> subscriptionCallbacks
            = new Dictionary<string, Action<object, EventArguments.MessageReceivedEventArgs>>();

        public StompClient(string webSocketUrl)
        {
            this.WebSocketUrl = $"ws://{webSocketUrl}";
            this.websocket = new WebSocket(this.WebSocketUrl);

            this.websocket.MessageReceived += this.OnWebsocketMessageReceived;
            this.websocket.Opened += this.OnWebsocketOpend;
            this.websocket.Error += this.OnWebsocketError;
            this.websocket.Closed += this.OnWebsocketClosed;
        }

        public event EventHandler<ConnectedEventArgs> Connected;

        public event EventHandler<EventArguments.MessageReceivedEventArgs> MessageReceived;

        public event EventHandler<EventArguments.ReceiptReceivedEventArgs> ReceiptReceived;

        public event EventHandler<EventArguments.ErrorReceivedEventArgs> ErrorReceived;

        public string WebSocketUrl { get; private set; }

        public bool AutoReconnectEnabled { get; private set; } = true;

        public int AutoReconnectInterval { get; private set; } = 5000;

        public bool AutoSendPingEnabled
        {
            get
            {
                return this.autoSendPingEnabled;
            }

            set
            {
                this.autoSendPingEnabled = value;
                this.websocket.EnableAutoSendPing = this.autoSendPingEnabled;
            }
        }

        public int AutoSendPingIntervalInMilliseconds
        {
            get
            {
                return this.autoSendPingIntervalInMilliseconds;
            }

            set
            {
                this.autoSendPingIntervalInMilliseconds = value;
                this.websocket.AutoSendPingInterval = this.autoSendPingIntervalInMilliseconds;
            }
        }

        public void Connect()
        {
            this.ConnectAsync().Wait();
        }

        public async Task ConnectAsync()
        {
            await this.websocket.OpenAsync();
        }

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

        private void OnWebsocketClosed(object sender, EventArgs e)
        {
            Log.Info($"Websocket connection closed");
            if (this.AutoReconnectEnabled)
            {
                Thread.Sleep(this.AutoReconnectInterval);
                this.Connect();
            }
        }

        private void OnWebsocketError(object sender, ErrorEventArgs e)
        {
            Log.Error(e.Exception.ToString());
        }

        private void OnWebsocketOpend(object sender, EventArgs e)
        {
            Log.Info($"Websocket connection opend");
            var connectFrame = new ConnectFrame("localhost");
            this.SendStompFrame(connectFrame);
        }

        private void OnWebsocketMessageReceived(object sender, WebSocket4Net.MessageReceivedEventArgs e)
        {
            Log.Info($"Websocket message received: {e.Message}");

            var stompFrame = StompFrame.Deserialize(e.Message);
            switch (stompFrame)
            {
                case ConnectedFrame frame:
                    this.Connected?.Invoke(this, new ConnectedEventArgs(frame));
                    break;

                case ErrorFrame frame:
                    this.ErrorReceived?.Invoke(this, new ErrorReceivedEventArgs(frame));
                    break;

                case ReceiptFrame frame:
                    this.ReceiptReceived?.Invoke(this, new ReceiptReceivedEventArgs(frame));
                    break;

                case MessageFrame frame:
                    var eventArguments = new EventArguments.MessageReceivedEventArgs(frame);
                    if (this.subscriptionCallbacks.ContainsKey(frame.Destination))
                    {
                        var parameters = new object[]
                        {
                            this,
                            eventArguments,
                        };
                        this.subscriptionCallbacks[frame.Destination].DynamicInvoke(parameters);
                    }
                    else
                    {
                        this.MessageReceived?.Invoke(
                            this,
                            eventArguments);
                    }

                    break;

                default:
                    Log.Info($"Unkown stomp frame: {stompFrame.Body}");
                    break;
            }
        }

        private void SendStompFrame(StompFrame stompFrame)
        {
            var serializedStompFrame = stompFrame.Serialize();
            this.websocket.Send(serializedStompFrame);
        }
    }
}
