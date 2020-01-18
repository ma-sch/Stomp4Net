namespace Stomp4Net.Test.Frames
{
    using FluentAssertions;
    using Stomp4Net.Model;
    using Stomp4Net.Model.Frames;
    using Xunit;

    public class ConnectFrameTests
    {
        [Fact]
        public void CheckCommand()
        {
            var connectFrame = new ConnectFrame("TestHost");
            connectFrame.Command.Should().Be(StompCommand.Connect);
        }

        [Fact]
        public void CheckRequiredHeaders()
        {
            var connectFrame = new ConnectFrame("TestHost");
            connectFrame.Headers.Should().Contain(BaseStompHeaders.AcceptVersionKey, StompConfig.StompMinProtocolVersion);
            connectFrame.Headers.Should().ContainKey(BaseStompHeaders.HostKey, "TestHost");
        }

        [Fact]
        public void CheckOptionalHeaders()
        {
            var connectFrame = new ConnectFrame("TestHost");
            connectFrame.Headers.Should().Contain(BaseStompHeaders.LoginKey, "User");
            connectFrame.Headers.Should().ContainKey(BaseStompHeaders.PasscodeKey, "Password");
            connectFrame.Headers.Should().ContainKey(BaseStompHeaders.HeartbeatKey, "1000");
        }
    }
}
