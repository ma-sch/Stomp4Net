namespace Stomp4Net.Model
{
    using System.IO;
    using System.Text;
    using Stomp4Net.Logging;
    using Stomp4Net.Model.Frames;

    public class StompFrame
    {
        private static readonly ILog Log = LogProvider.For<StompFrame>();

        public StompFrame(string command)
            : this(command, string.Empty)
        {
        }

        public StompFrame(string command, string body)
            : this(command, body, new StompHeaders())
        {
        }

        public StompFrame(string command, string body, StompHeaders headers)
        {
            this.Command = command;
            this.Body = body;
            this.Headers = headers;

            this.Headers["content-length"] = body.Length.ToString();
        }

        public StompHeaders Headers { get; protected set; }

        public string Body { get; protected set; }

        public string Command { get; protected set; }

        public static StompFrame Deserialize(string message)
        {
            var reader = new StringReader(message);

            var command = reader.ReadLine();

            var headers = new StompHeaders();

            var header = reader.ReadLine();
            while (!string.IsNullOrEmpty(header))
            {
                var split = header.Split(':');
                if (split.Length == 2)
                {
                    headers[split[0].Trim()] = split[1].Trim();
                }

                header = reader.ReadLine() ?? string.Empty;
            }

            var body = reader.ReadToEnd() ?? string.Empty;
            body = body.TrimEnd('\r', '\n', '\0');

            StompFrame stompFrame = null;
            switch (command)
            {
                case StompCommand.Connected:
                    stompFrame = new ConnectedFrame(headers);
                    break;

                case StompCommand.Error:
                    stompFrame = new ErrorFrame();
                    break;

                case StompCommand.Receipt:
                    stompFrame = new ReceiptFrame();
                    break;

                case StompCommand.Send:
                    stompFrame = new SendFrame(headers["destination"], body, headers["content-type"]);
                    break;

                case StompCommand.Message:
                    stompFrame = new MessageFrame(body, headers);
                    break;

                case StompCommand.Subscribe:
                    stompFrame = new SubscribeFrame(headers["destination"]);
                    break;

                default:
                    new StompFrame(command, body, headers);
                    break;
            }

            return stompFrame;
        }

        public string Serialize()
        {
            var buffer = new StringBuilder();

            buffer.Append(this.Command + "\n");

            if (this.Headers != null)
            {
                foreach (var header in this.Headers)
                {
                    buffer.Append(header.Key + ":" + header.Value + "\n");
                }
            }

            buffer.Append("\n");
            buffer.Append(this.Body);
            buffer.Append('\0');

            return buffer.ToString();
        }
    }
}
