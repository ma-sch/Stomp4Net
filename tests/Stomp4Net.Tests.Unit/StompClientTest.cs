namespace Stomp4Net.Test
{
    using System.Threading;
    using Stomp4Net.EventArguments;
    using Xunit;
    using Xunit.Abstractions;

    public class StompClientTest
    {
        public class Connect : BaseTest
        {
            public Connect(ITestOutputHelper output)
            : base(output)
            {
            }

            [Fact]
            public void ServerAvailable()
            {
                var stompServer = new StompServer(5555);
                var stompClient = new StompClient("127.0.0.1:5555");

                var raisedEvent = Assert.RaisesAny<ConnectedEventArgs>(
                    handler => stompClient.Connected += handler,
                    handler => stompClient.Connected -= handler,
                    () =>
                    {
                        stompClient.Connect();
                        Thread.Sleep(100);
                    });
            }

            [Fact]
            public void ServerNotAvailable()
            {
            }
        }

        public class Send : BaseTest
        {
            public Send(ITestOutputHelper output)
            : base(output)
            {
            }
        }

        public class Subscribe : BaseTest
        {
            public Subscribe(ITestOutputHelper output)
            : base(output)
            {
            }
        }
    }
}
