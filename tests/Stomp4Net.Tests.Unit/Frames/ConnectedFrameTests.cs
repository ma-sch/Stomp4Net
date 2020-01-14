namespace Stomp4Net.Test.Frames
{
    using FluentAssertions;
    using Stomp4Net.Model;
    using Stomp4Net.Model.Frames;
    using Xunit;

    public class ConnectedFrameTests
    {
        [Fact]
        public void CheckCommand()
        {
            // var connectFrame = new ConnectedFrame();
            // connectFrame.Command.Should().Be(StompCommand.Connected);
        }

        [Fact]
        public void CheckRequiredHeaders()
        {
            // var connectedFrame = new ConnectedFrame();
            // connectedFrame.Headers.Should().Contain(StompHeaders.Version, StompConfig.StompProtocolVersion);
        }

        [Fact]
        public void CheckOptionalHeaders()
        {
            // var connectFrame = new ConnectedFrame();
            // connectFrame.Headers.Should().Contain(StompHeaders.Heartbeat, "1000");
            // connectFrame.Headers.Should().ContainKey(StompHeaders.Session, "");
            // connectFrame.Headers.Should().ContainKey(StompHeaders.Server, "");
        }
    }
}
