namespace Stomp4Net.Model
{
    using System.Text;
    using Stomp4Net.Logging;
    using Stomp4Net.Model.Frames;

    public class BaseStompFrame<T> : IStompFrame
        where T : BaseStompHeaders, new()
    {
        private static readonly ILog Log = LogProvider.For<BaseStompFrame<T>>();

        public BaseStompFrame(string command)
            : this(command, string.Empty)
        {
        }

        public BaseStompFrame(string command, string body)
            : this(command, body, new T())
        {
        }

        public BaseStompFrame(string command, string body, T headers)
        {
            this.Command = command;
            this.Body = body;
            this.Headers = headers;
            if (body.Length > 0)
            {
                this.Headers["content-length"] = body.Length.ToString();
            }
        }

        public T Headers { get; set; }

        public string Body { get; set; }

        public string Command { get; set; }

        BaseStompHeaders IStompFrame.Headers { get => Headers; set => this.Headers = (T)value; }

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
