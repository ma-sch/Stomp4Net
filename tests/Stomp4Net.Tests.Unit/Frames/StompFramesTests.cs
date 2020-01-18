namespace Stomp4Net.Test
{
    using FluentAssertions;
    using Stomp4Net.Model;
    using Stomp4Net.Model.Frames;
    using Xunit;

    public class StompFramesTests
    {
        public class ReceiptFrameTests
        {
            [Fact]
            public void CheckCommand()
            {
                //var connectFrame = new ReceiptFrame();
                //connectFrame.Command.Should().Be(StompCommand.Receipt);
            }
        }

        public class ErrorFrameTests
        {
            [Fact]
            public void CheckCommand()
            {
                //var connectFrame = new ErrorFrame();
                //connectFrame.Command.Should().Be(StompCommand.Error);
            }
        }

        public class SendFrameTests
        {
            [Fact]
            public void CheckCommand()
            {
                // var connectFrame = new SendFrame();
                // connectFrame.Command.Should().Be(StompCommand.Send);
            }
        }

        public class SubscribeFrameTests
        {
            [Fact]
            public void CheckCommand()
            {
                // var connectFrame = new SubscribeFrame();
                // connectFrame.Command.Should().Be(StompCommand.Subscribe);
            }
        }

        public class UnsubscribeFrameTests
        {
            [Fact]
            public void CheckCommand()
            {
                //var connectFrame = new UnsubscribeFrame();
                //connectFrame.Command.Should().Be(StompCommand.Unsubscribe);
            }
        }

        public class AckFrameTests
        {
            [Fact]
            public void CheckCommand()
            {
                //var connectFrame = new AckFrame();
                //connectFrame.Command.Should().Be(StompCommand.Ack);
            }
        }

        public class NackFrameTests
        {
            [Fact]
            public void CheckCommand()
            {
                //var connectFrame = new NackFrame();
                //connectFrame.Command.Should().Be(StompCommand.Nack);
            }
        }

        public class BeginFrameTests
        {
            [Fact]
            public void CheckCommand()
            {
                //var connectFrame = new BeginFrame();
                //connectFrame.Command.Should().Be(StompCommand.Begin);
            }
        }

        public class CommitFrameTests
        {
            [Fact]
            public void CheckCommand()
            {
                //var connectFrame = new CommitFrame();
                //connectFrame.Command.Should().Be(StompCommand.Commit);
            }
        }

        public class AbortFrameTests
        {
            [Fact]
            public void CheckCommand()
            {
                //var connectFrame = new AbortFrame();
                //connectFrame.Command.Should().Be(StompCommand.Abort);
            }
        }

        public class DisconnectFrameTests
        {
            [Fact]
            public void CheckCommand()
            {
                var connectFrame = new DisconnectFrame();
                connectFrame.Command.Should().Be(StompCommand.Disconnect);
            }
        }
    }
}
