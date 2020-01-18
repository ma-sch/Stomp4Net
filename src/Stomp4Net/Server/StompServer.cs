namespace Stomp4Net.Server
{
    using System;
    using System.Collections.Generic;
    using System.Timers;
    using Stomp4Net.EventArguments;
    using Stomp4Net.Logging;
    using Stomp4Net.Model;
    using Stomp4Net.Model.Frames;

    public abstract class StompServer
    {
        private const double ConnectionTimeout = 5000;

        private static readonly ILog Log = LogProvider.For<StompServer>();

        protected Dictionary<string, List<StompSubscription>> stompSubscriptions = new Dictionary<string, List<StompSubscription>>();

        protected Dictionary<string, Action<object, SendReceivedEventArgs>> notificationForDestinationCallbacks
            = new Dictionary<string, Action<object, SendReceivedEventArgs>>();

        public StompServer(int port)
        {
            this.Port = port;
        }

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;

        public event EventHandler<SendReceivedEventArgs> SendReceived;

        public int Port { get; set; }

        public void ProcessMessage(string sessionId, string messageString)
        {
            Log.Debug($"Message received from client '{sessionId}': {messageString}");

            var stompFrame = IStompFrame.Deserialize(messageString);
            switch (stompFrame)
            {
                case ConnectFrame frame:
                case StompFrame frame1:
                    var version = "1.2";
                    var connectedFrame = new ConnectedFrame(version)
                    {
                        Headers = new ConnectedFrameHeaders()
                        {
                            Version = "1.2",
                            Session = sessionId,
                            Server = "Stomp4Net/1.0.0",
                        },
                    };

                    this.SendStompFrame(sessionId, connectedFrame);
                    this.ClientConnected?.Invoke(this, new ClientConnectedEventArgs(sessionId));
                    break;

                case SendFrame frame:
                    var eventArguments = new EventArguments.SendReceivedEventArgs(sessionId, frame);
                    if (this.notificationForDestinationCallbacks.ContainsKey(frame.Headers.Destination))
                    {
                        var parameters = new object[]
                        {
                            this,
                            eventArguments,
                        };
                        this.notificationForDestinationCallbacks[frame.Headers.Destination].DynamicInvoke(parameters);
                    }
                    else
                    {
                        this.SendReceived?.Invoke(
                            this,
                            eventArguments);
                    }

                    if (this.stompSubscriptions.ContainsKey(frame.Headers.Destination))
                    {
                        var subscriptionsToNotify = this.stompSubscriptions[frame.Headers.Destination];
                        foreach (var subscriptionToNotify in subscriptionsToNotify)
                        {
                            var messageFrame = new MessageFrame(frame.Headers.Destination, subscriptionToNotify.Id, frame.Body, frame.Headers.ContentType);
                            this.SendStompFrame(subscriptionToNotify.SessionId, messageFrame);
                        }
                    }

                    break;

                case SubscribeFrame frame:
                    List<StompSubscription> stompSubscriptionsForDestination;
                    if (this.stompSubscriptions.ContainsKey(frame.Headers.Id))
                    {
                        stompSubscriptionsForDestination = this.stompSubscriptions[frame.Headers.Destination];
                    }
                    else
                    {
                        stompSubscriptionsForDestination = new List<StompSubscription>();
                    }

                    stompSubscriptionsForDestination.Add(new StompSubscription(frame.Headers.Id, sessionId));
                    this.stompSubscriptions[frame.Headers.Destination] = stompSubscriptionsForDestination;
                    break;

                case DisconnectFrame frame:
                    this.ClientDisconnected?.Invoke(this, new ClientDisconnectedEventArgs(sessionId));
                    break;

                default:
                    Log.Warn($"Unkown frame: {messageString}");
                    break;
            }
        }

        public void EnableNotificationForDestination(string topic, Action<object, SendReceivedEventArgs> callback)
        {
            this.notificationForDestinationCallbacks[topic] = callback;
        }

        public void DisableNotificationForDestination(string topic)
        {
            if (this.notificationForDestinationCallbacks.ContainsKey(topic))
            {
                this.notificationForDestinationCallbacks.Remove(topic);
            }
        }

        public void PingResponseTimeExceeded(object sender, ElapsedEventArgs e)
        {
        }

        public void PublishMessageForSession(string sessionId, string message, string destination = "", string subscriptionId = "")
        {
            var messageFrame = new MessageFrame(destination, subscriptionId, message, ContentType.ApplicationJson);
            this.SendStompFrame(sessionId, messageFrame);
        }

        protected abstract void SendStompFrame(string sessionId, IStompFrame stompFrame);
    }
}
