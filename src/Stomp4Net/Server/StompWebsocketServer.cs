namespace Stomp4Net.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Fleck;
    using Stomp4Net.Logging;
    using Stomp4Net.Model.Frames;

    public class StompWebsocketServer : StompServer
    {
        private static readonly ILog Log = LogProvider.For<StompWebsocketServer>();

        private WebSocketServer server;

        private IWebSocketConnection websocket;

        private Dictionary<string, IWebSocketConnection> clients = new Dictionary<string, IWebSocketConnection>();

        public StompWebsocketServer(int port)
            : base(port)
        {
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
            });
            Log.Info($"WebSocket server listening on port '{port}'");
        }

        public void ConnectionOpend(IWebSocketConnection newConnection)
        {
            Log.Debug($"New connection opend from '{newConnection.ConnectionInfo.ClientIpAddress}'");
            var clientId = newConnection.ConnectionInfo.Id.ToString();
            this.clients.Add(clientId, newConnection);
        }

        public void ConnectionClosed(IWebSocketConnection closedConnection)
        {
        }

        public void MessageReceived(IWebSocketConnection receivingConnection, string messageString)
        {
            var sessionId = receivingConnection.ConnectionInfo.Id;
            Log.Trace($"Websocket message received from client '{sessionId}': {messageString}");
            this.ProcessMessage(sessionId.ToString(), messageString);
        }

        protected override void SendStompFrame(string sessionId, IStompFrame stompFrame)
        {
            var websocketConnection = this.clients[sessionId];
            websocketConnection.Send(stompFrame.Serialize());
        }

        protected override void SendEOL(string sessionId)
        {
            Log.Trace($"Sending EOL to '{sessionId}'");
            var websocketConnection = this.clients[sessionId];
            websocketConnection.Send("\r\n");
        }
    }
}
