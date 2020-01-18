namespace Stomp4Net.Server
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using Stomp4Net.Logging;
    using Stomp4Net.Model.Frames;

    public class StompTcpServer : StompServer
    {
        private static readonly ILog Log = LogProvider.For<StompTcpServer>();

        private TcpListener server = null;

        private Dictionary<string, TcpClient> tcpClients = new Dictionary<string, TcpClient>();

        public StompTcpServer(int port)
            : base(port)
        {
            IPAddress localAddr = IPAddress.Parse("0.0.0.0");
            this.server = new TcpListener(localAddr, port);
            this.server.Start();
            Thread connectionListenerThread = new Thread(this.StartListener);
            connectionListenerThread.Start();
        }

        public void StartListener()
        {
            try
            {
                while (true)
                {
                    Log.Debug($"TCP server listening on port {this.Port}");
                    TcpClient client = server.AcceptTcpClient();
                    var sessionId = Guid.NewGuid().ToString();
                    Log.Debug($"New client connected: {sessionId}");
                    Thread t = new Thread(this.HandleConnection(client, sessionId));
                    t.Start();
                }
            }
            catch (SocketException e)
            {
                Log.Info("SocketException: {0}", e);
                this.server.Stop();
            }
        }

        public ThreadStart HandleConnection(TcpClient client, string sessionId)
        {
            return () => {
                this.tcpClients.Add(sessionId, client);

                var stream = client.GetStream();
                string imei = string.Empty;
                string data = null;
                byte[] bytes = new byte[256];
                int i;
                try
                {
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        string hex = BitConverter.ToString(bytes);
                        data = Encoding.ASCII.GetString(bytes, 0, i);
                        this.ProcessMessage(sessionId, data);
                    }
                }
                catch (Exception e)
                {
                    Log.Debug($"Exception: {e.ToString()}");
                    client.
                    Close();
                }
            };
        }

        protected override void SendStompFrame(string sessionId, IStompFrame stompFrame)
        {
            var client = tcpClients[sessionId];
            var stream = client.GetStream();

            byte[] reply = Encoding.ASCII.GetBytes(stompFrame.Serialize());
            stream.Write(reply, 0, reply.Length);
        }
    }
}
