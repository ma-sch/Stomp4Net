namespace Stomp4Net.Client
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Timers;
    using Stomp4Net.EventArguments;
    using Stomp4Net.Logging;
    using Stomp4Net.Model.Frames;
    using SuperSocket.ClientEngine;
    using WebSocket4Net;

    public class StompWebsocketClient : StompClient
    {
        private static readonly ILog Log = LogProvider.For<StompWebsocketClient>();

        private readonly WebSocket websocket;

        private bool websocketHeartbeatEnabled = false;

        private TimeSpan websocketHeartbeatInterval;

        /// <summary>
        /// Initializes a new instance of the <see cref="StompWebsocketClient"/> class.
        /// </summary>
        /// <param name="webSocketUrl">URL of WebSocket server to which the client should connect.</param>
        public StompWebsocketClient(string webSocketUrl)
        {
            this.WebSocketUrl = webSocketUrl;
            this.websocket = new WebSocket(this.WebSocketUrl);

            this.websocket.MessageReceived += this.OnWebsocketMessageReceived;
            this.websocket.Opened += this.OnWebsocketOpend;
            this.websocket.Error += this.OnWebsocketError;
            this.websocket.Closed += this.OnWebsocketClosed;
        }

        /// <summary> Gets the URL of the WebSocket server to which the client connects.</summary>
        public string WebSocketUrl { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether WebSocket heatbeat is used instead of STOMP heartbeat.
        /// </summary>
        public bool WebsocketHeartbeatEnabled
        {
            get
            {
                return this.websocketHeartbeatEnabled;
            }

            set
            {
                this.websocketHeartbeatEnabled = value;
                this.websocket.EnableAutoSendPing = value;
            }
        }

        /// <summary>
        /// Gets or sets the interval for Websocket heartbeat. 
        /// Websocket heartbeat is only used when <see cref="WebsocketHeartbeatEnabled"/> is set.
        /// </summary>
        public TimeSpan WebsocketHeartbeatInterval
        {
            get
            {
                return this.websocketHeartbeatInterval;
            }

            set
            {
                this.websocketHeartbeatInterval = value;
                this.websocket.AutoSendPingInterval = (int)value.TotalMilliseconds;
            }
        }

        /// <summary>
        /// Connect to Websocket server.
        /// </summary>
        public override void Connect()
        {
            this.ConnectAsync().Wait();
        }

        public override async Task ConnectAsync()
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
            this.HandleLostConnection();
            if (this.AutoReconnectEnabled)
            {
                Thread.Sleep(this.AutoReconnectInterval);
                this.Connect();
            }
        }

        private void OnWebsocketError(object sender, ErrorEventArgs e)
        {
            Log.Error(e.Exception.ToString());
            this.HandleLostConnection();
        }

        private void OnWebsocketOpend(object sender, EventArgs e)
        {
            Log.Debug($"Websocket connection opend");
            var connectFrame = new ConnectFrame("localhost")
            {
                Headers = new ConnectFrameHeaders()
                {
                    AcceptVersion = "1.2",
                    Host = "localhost",
                    Heartbeat =
                        $"{this.StompHeartbeatIntervalFromClientToServer.TotalMilliseconds}," +
                        $"{this.StompHeartbeatIntervalFromServerToClient.TotalMilliseconds}",
                },
            };
            this.SendStompFrame(connectFrame);
        }

        private void OnWebsocketMessageReceived(object sender, WebSocket4Net.MessageReceivedEventArgs e)
        {
            if (e.Message == "\r\n" || e.Message == "\n")
            {
                Log.Trace("EOL received");
                this.StompHeartbeatReceiveTimer?.Stop();
                this.StompHeartbeatReceiveTimer?.Start();
                return;
            }

            Log.Info($"Websocket message received: {e.Message}");

            var stompFrame = IStompFrame.Deserialize(e.Message);
            switch (stompFrame)
            {
                case ConnectedFrame frame:
                    if (frame.ServerExpectedHeartbeatInterval.TotalMilliseconds != 0)
                    {
                        this.StompHeartbeatSendTimer = new System.Timers.Timer(frame.ServerExpectedHeartbeatInterval.TotalMilliseconds);
                        this.StompHeartbeatSendTimer.Elapsed += this.HeartbeatSendTimerElapsed;
                        this.StompHeartbeatSendTimer.Start();
                    }

                    if (frame.ServerSendingHeartbeatInterval.TotalMilliseconds != 0)
                    {
                        this.StompHeartbeatReceiveTimer = new System.Timers.Timer((this.StompHeartbeatIntervalFromServerToClient + this.StompHeartbeatTimeout).TotalMilliseconds);
                        this.StompHeartbeatReceiveTimer.Elapsed += this.HeartbeatReceivedTimerElapsed;
                        this.StompHeartbeatReceiveTimer.Start();
                    }

                    this.InvokeConnectedEvent(new ConnectedEventArgs(frame));
                    break;

                case ErrorFrame frame:
                    this.InvokeErrorReceivedEvent(new ErrorReceivedEventArgs(frame));
                    break;

                case ReceiptFrame frame:
                    this.InvokeReceiptReceivedEvent(new ReceiptReceivedEventArgs(frame));
                    break;

                case MessageFrame frame:
                    var eventArguments = new EventArguments.MessageReceivedEventArgs(frame);
                    if (this.subscriptionCallbacks.ContainsKey(frame.Headers.Destination))
                    {
                        var parameters = new object[]
                        {
                            this,
                            eventArguments,
                        };
                        this.subscriptionCallbacks[frame.Headers.Destination].DynamicInvoke(parameters);
                    }
                    else
                    {
                        this.InvokeMessageReceivedEvent(eventArguments);
                    }

                    break;

                default:
                    Log.Info($"Unkown stomp frame: {stompFrame.Body}");
                    break;
            }

            this.StompHeartbeatReceiveTimer?.Stop();
            this.StompHeartbeatReceiveTimer?.Start();
        }

        private void HeartbeatSendTimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.SendEOL();
        }

        private void HeartbeatReceivedTimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.HandleLostConnection();
        }

        protected override void SendStompFrame(IStompFrame stompFrame)
        {
            var serializedStompFrame = stompFrame.Serialize();
            this.websocket.Send(serializedStompFrame);
        }

        protected override void SendEOL()
        {
            Log.Trace("Sending EOL");
            this.websocket.Send("\r\n");
        }

        private void HandleLostConnection()
        {
            Log.Error("Connection to server lost");
            this.StompHeartbeatSendTimer?.Stop();
            this.StompHeartbeatReceiveTimer?.Stop();
            this.InvokeDisconnectedEvent(new DisconnectedEventArgs());
        }
    }
}
