namespace Stomp4Net.Sample
{
    using System.Threading;
    using Serilog;
    using Stomp4Net.EventArguments;
    using Stomp4Net.Model;

    public class StompSample
    {
        private StompServer stompServer;

        private StompClient stompClient;

        public StompSample()
        {
        }

        public void StartServer()
        {
            this.stompServer = new StompServer(5555);
        }

        public void StartClient()
        {
            this.stompClient = new StompClient("localhost:5555/test");
            this.stompClient.Connected += this.Connected;

            this.stompClient.Connect();
        }

        private void Connected(object sender, ConnectedEventArgs eventArgs)
        {
            Log.Information("Client connected");
            this.stompClient.Subscribe("/test/", this.NewMessageOnSubscribedTopic);
            Thread.Sleep(100);
            this.stompClient.Send("/test/", "asdf", ContentType.ApplicationJson);
        }

        private void NewMessageOnSubscribedTopic(object sender, MessageReceivedEventArgs eventArgs)
        {
            Log.Information($"New message on topic '{eventArgs.Topic}': {eventArgs.Message}");
        }
    }
}
