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

        protected Dictionary<string, Timer> sessionIdToHeartbeatSendTimers = new Dictionary<string, Timer>();
        protected Dictionary<Timer, string> heartbeatSendTimersToSessionId = new Dictionary<Timer, string>();

        protected Dictionary<string, Timer> sessionIdToHeartbeatReceivedTimers = new Dictionary<string, Timer>();
        protected Dictionary<Timer, string> heartbeatReceivedTimersToSessionId = new Dictionary<Timer, string>();

        public StompServer(int port)
        {
            this.Port = port;
        }

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;

        public event EventHandler<SendReceivedEventArgs> SendReceived;

        public int Port { get; set; }

        public TimeSpan StompHeartbeatIntervalFromClientToServer { get; set; } = TimeSpan.FromMilliseconds(10000);

        public TimeSpan StompHeartbeatIntervalFromServerToClient { get; set; } = TimeSpan.FromMilliseconds(10000);

        public TimeSpan StompHeartbeatTimeout { get; set; } = TimeSpan.FromMilliseconds(5000);

        public void ProcessMessage(string sessionId, string messageString)
        {
            if (messageString == "\r\n")
            {
                Log.Trace("EOL received");
                if (this.sessionIdToHeartbeatReceivedTimers.ContainsKey(sessionId))
                {
                    var heartbeatReceiveTimerToResett = this.sessionIdToHeartbeatReceivedTimers[sessionId];
                    heartbeatReceiveTimerToResett.Stop();
                    heartbeatReceiveTimerToResett.Start();
                }
                return;
            }

            Log.Debug($"Message received from client '{sessionId}': {messageString}");

            var stompFrame = IStompFrame.Deserialize(messageString);
            switch (stompFrame)
            {
                case ConnectFrame frame:
                    var version = "1.2";
                    var connectedFrame = new ConnectedFrame(version)
                    {
                        Headers = new ConnectedFrameHeaders()
                        {
                            Version = version,
                            Session = sessionId,
                            Server = "Stomp4Net/1.0.0",
                            Heartbeat = $"{this.StompHeartbeatIntervalFromServerToClient.TotalMilliseconds},{this.StompHeartbeatIntervalFromClientToServer.TotalMilliseconds}",
                        },
                    };

                    if (frame.ClientExpectedHeartbeatInterval.TotalMilliseconds != 0)
                    {
                        var heartbeatSendTimer = new Timer(this.StompHeartbeatIntervalFromServerToClient.TotalMilliseconds);
                        heartbeatSendTimer.Elapsed += this.SendHeartbeatTimerElapsed;
                        heartbeatSendTimer.Start();
                        this.sessionIdToHeartbeatSendTimers.Add(sessionId, heartbeatSendTimer);
                        this.heartbeatSendTimersToSessionId.Add(heartbeatSendTimer, sessionId);
                    }

                    if (frame.ClientExpectedHeartbeatInterval.TotalMilliseconds != 0)
                    {
                        var heartbeatReceiveTimer = new Timer(
                            (this.StompHeartbeatIntervalFromServerToClient + this.StompHeartbeatTimeout).TotalMilliseconds);
                        heartbeatReceiveTimer.Elapsed += this.ReceiveHeartbeatTimerElapsed;
                        heartbeatReceiveTimer.Start();
                        this.sessionIdToHeartbeatReceivedTimers.Add(sessionId, heartbeatReceiveTimer);
                        this.heartbeatReceivedTimersToSessionId.Add(heartbeatReceiveTimer, sessionId);
                    }

                    this.SendStompFrame(sessionId, connectedFrame);
                    this.ClientConnected?.Invoke(this, new ClientConnectedEventArgs(sessionId));
                    break;

                case StompFrame frame:
                    version = "1.2";
                    connectedFrame = new ConnectedFrame(version)
                    {
                        Headers = new ConnectedFrameHeaders()
                        {
                            Version = version,
                            Session = sessionId,
                            Server = "Stomp4Net/1.0.0",
                            Heartbeat = $"{this.StompHeartbeatIntervalFromServerToClient.TotalMilliseconds},{this.StompHeartbeatIntervalFromClientToServer.TotalMilliseconds}",
                        },
                    };

                    if (frame.ClientExpectedHeartbeatInterval.TotalMilliseconds != 0)
                    {
                        var heartbeatSendTimer = new Timer(this.StompHeartbeatIntervalFromServerToClient.TotalMilliseconds);
                        heartbeatSendTimer.Elapsed += this.SendHeartbeatTimerElapsed;
                        heartbeatSendTimer.Start();
                        this.sessionIdToHeartbeatSendTimers.Add(sessionId, heartbeatSendTimer);
                        this.heartbeatSendTimersToSessionId.Add(heartbeatSendTimer, sessionId);
                    }

                    if (frame.ClientExpectedHeartbeatInterval.TotalMilliseconds != 0)
                    {
                        var heartbeatReceiveTimer = new Timer(
                            (this.StompHeartbeatIntervalFromServerToClient + this.StompHeartbeatTimeout).TotalMilliseconds);
                        heartbeatReceiveTimer.Elapsed += this.ReceiveHeartbeatTimerElapsed;
                        heartbeatReceiveTimer.Start();
                        this.sessionIdToHeartbeatReceivedTimers.Add(sessionId, heartbeatReceiveTimer);
                        this.heartbeatReceivedTimersToSessionId.Add(heartbeatReceiveTimer, sessionId);
                    }

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

            if (this.sessionIdToHeartbeatReceivedTimers.ContainsKey(sessionId))
            {
                var heartbeatReceiveTimerToReset = this.sessionIdToHeartbeatReceivedTimers[sessionId];
                heartbeatReceiveTimerToReset.Stop();
                heartbeatReceiveTimerToReset.Start();
            }
        }

        private void SendHeartbeatTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var timer = (Timer)sender;
            var sessionId = this.heartbeatSendTimersToSessionId[timer];
            this.SendEOL(sessionId);
        }

        private void ReceiveHeartbeatTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var heartbeatReceiveTimer = (Timer)sender;
            var sessionId = this.heartbeatReceivedTimersToSessionId[heartbeatReceiveTimer];
            heartbeatReceiveTimer.Stop();
            this.heartbeatReceivedTimersToSessionId.Remove(heartbeatReceiveTimer);
            this.sessionIdToHeartbeatReceivedTimers.Remove(sessionId);

            var heartbeatSendTimer = this.sessionIdToHeartbeatSendTimers[sessionId];
            heartbeatSendTimer.Stop();
            this.heartbeatSendTimersToSessionId.Remove(heartbeatSendTimer);
            this.sessionIdToHeartbeatSendTimers.Remove(sessionId);
            Log.Error($"Connection to client '{sessionId}' lost");
            this.ClientDisconnected?.Invoke(this, new ClientDisconnectedEventArgs(sessionId));
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

        public void PublishMessageForSession(string sessionId, string destination, string message, string subscriptionId = "")
        {
            var messageFrame = new MessageFrame(destination, subscriptionId, message, ContentType.ApplicationJson);
            this.SendStompFrame(sessionId, messageFrame);
        }

        protected abstract void SendStompFrame(string sessionId, IStompFrame stompFrame);

        protected abstract void SendEOL(string sessionId);

        protected void InvokeClientDisconnected(string sessionId)
        {
            this.ClientDisconnected?.Invoke(this, new ClientDisconnectedEventArgs(sessionId));
        }
    }
}
