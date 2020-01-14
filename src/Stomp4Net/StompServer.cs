namespace Stomp4Net
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Timers;
    using Fleck;
    using Stomp4Net.Logging;
    using Stomp4Net.Model;
    using Stomp4Net.Model.Frames;

    public class StompServer
    {
        private const double ConnectionTimeout = 5000;

        private static readonly ILog Log = LogProvider.For<StompServer>();

        private WebSocketServer server;

        private IWebSocketConnection websocket;

        private int port;

        private Dictionary<string, List<StompSubscription>> stompSubscriptions = new Dictionary<string, List<StompSubscription>>();

        private Dictionary<string, Action<object, EventArguments.MessageReceivedEventArgs>> notificationForDestinationCallbacks
            = new Dictionary<string, Action<object, EventArguments.MessageReceivedEventArgs>>();

        private Dictionary<Guid, IWebSocketConnection> clients = new Dictionary<Guid, IWebSocketConnection>();

        private Dictionary<Guid, Timer> pingTimers = new Dictionary<Guid, Timer>();

        public StompServer(int port)
        {
            this.port = port;
            this.server = new WebSocketServer($"ws://0.0.0.0:{port}");
            this.server.Start(socket =>
            {
                this.websocket = socket;
                socket.OnOpen = () =>
                {
                    this.ConnectionOpend(socket);
                };
                socket.OnClose = () =>
                {
                    this.ConnectionClosed(socket);
                };
                socket.OnMessage = (string message) =>
                {
                    this.MessageReceived(socket, message);
                };
                socket.OnError = (Exception e) =>
                {
                    if (e.GetType() != typeof(IOException))
                    {
                        Log.Error($"Exception for client '{socket.ConnectionInfo.Id}': {e}");
                    }

                    this.ConnectionClosed(socket);
                };
                socket.OnPing = (byte[] message) =>
                {
                    Log.Info("Ping received");
                    var pingTimer = this.pingTimers[socket.ConnectionInfo.Id];
                    pingTimer.Stop();
                    pingTimer.Start();
                };
            });
            Log.Info($"Server listening on port '{port}'");
        }

        public delegate void SendReceivedEventHandler(object sender, EventArguments.SendReceivedEventArgs eventArgs);

        public event SendReceivedEventHandler SendReceived;

        public void ConnectionOpend(IWebSocketConnection newConnection)
        {
            Log.Debug($"New connection opend from '{newConnection.ConnectionInfo.ClientIpAddress}'");

            var clientId = newConnection.ConnectionInfo.Id;
            this.clients.Add(clientId, newConnection);

            var pingTimer = new Timer(ConnectionTimeout);
            pingTimer.Elapsed += this.PingResponseTimeExceeded;
            pingTimer.Start();
            this.pingTimers.Add(newConnection.ConnectionInfo.Id, pingTimer);

            var stompHeaders = new StompHeaders();
            stompHeaders["version"] = "1.2";
            stompHeaders["session"] = clientId.ToString();
            stompHeaders["server"] = "Stomp4Net/1.0.0";
            var connectedFrame = new ConnectedFrame(stompHeaders);

            this.SendStompFrame(newConnection, connectedFrame);
        }

        public void ConnectionClosed(IWebSocketConnection closedConnection)
        {
        }

        public void MessageReceived(IWebSocketConnection receivingConnection, string messageString)
        {
            var sessionId = receivingConnection.ConnectionInfo.Id;
            Log.Debug($"Websocket message received from client '{sessionId}': {messageString}");

            var stompFrame = StompFrame.Deserialize(messageString);
            switch (stompFrame)
            {
                case SendFrame frame:
                    var eventArguments = new EventArguments.SendReceivedEventArgs(sessionId, frame);
                    if (this.notificationForDestinationCallbacks.ContainsKey(frame.Destination))
                    {
                        var parameters = new object[]
                        {
                            this,
                            eventArguments,
                        };
                        this.notificationForDestinationCallbacks[frame.Destination].DynamicInvoke(parameters);
                    }
                    else
                    {
                        this.SendReceived?.Invoke(
                            this,
                            eventArguments);
                    }

                    if (this.stompSubscriptions.ContainsKey(frame.Destination))
                    {
                        var subscriptionsToNotify = this.stompSubscriptions[frame.Destination];
                        foreach (var subscriptionToNotify in subscriptionsToNotify)
                        {
                            var messageFrame = new MessageFrame(frame, subscriptionToNotify.Id);
                            this.clients[subscriptionToNotify.SessionId].Send(messageFrame.Serialize());
                        }
                    }

                    break;

                case SubscribeFrame frame:
                    List<StompSubscription> stompSubscriptionsForDestination;
                    if (this.stompSubscriptions.ContainsKey(frame.Id))
                    {
                        stompSubscriptionsForDestination = this.stompSubscriptions[frame.Destination];
                    }
                    else
                    {
                        stompSubscriptionsForDestination = new List<StompSubscription>();
                    }

                    stompSubscriptionsForDestination.Add(new StompSubscription(frame.Id, sessionId));
                    this.stompSubscriptions[frame.Destination] = stompSubscriptionsForDestination;
                    break;

                default:
                    Log.Warn($"Unkown frame: {messageString}");
                    break;
            }
        }

        public void PingResponseTimeExceeded(object sender, ElapsedEventArgs e)
        {
        }

        public void PublishMessageForClient(Guid clientId, object message)
        {
        }

        public void EnableNotificationForDestination(string topic, Action<object, EventArguments.MessageReceivedEventArgs> callback)
        {
            this.notificationForDestinationCallbacks[topic] = callback;
        }

        private void SendStompFrame(IWebSocketConnection webSocketConnection, StompFrame stompFrame)
        {
            webSocketConnection.Send(stompFrame.Serialize());
        }
    }
}
