using System.IO;

namespace Stomp4Net.Model.Frames
{
    public interface IStompFrame
    {
        BaseStompHeaders Headers { get; set; }

        string Body { get; set; }

        string Command { get; set; }

        string Serialize();

        static IStompFrame Deserialize(string message)
        {
            var reader = new StringReader(message);

            var command = reader.ReadLine();

            var headers = new BaseStompHeaders();

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

            IStompFrame stompFrame = null;
            switch (command)
            {
                case StompCommand.Connect:
                    return new ConnectFrame(headers[BaseStompHeaders.HostKey]);

                case StompCommand.Stomp:
                    return new StompFrame(headers[BaseStompHeaders.HostKey]);

                case StompCommand.Connected:
                    var connectedFrame = new ConnectedFrame(headers[BaseStompHeaders.VersionKey]);
                    connectedFrame.Headers.Session = headers.ContainsKey(BaseStompHeaders.SessionKey) ? headers[BaseStompHeaders.SessionKey] : string.Empty;
                    connectedFrame.Headers.Server = headers.ContainsKey(BaseStompHeaders.ServerKey) ? headers[BaseStompHeaders.ServerKey] : string.Empty;
                    connectedFrame.Headers.Heartbeat = headers.ContainsKey(BaseStompHeaders.HeartbeatKey) ? headers[BaseStompHeaders.HeartbeatKey] : "0,0";
                    return connectedFrame;

                case StompCommand.Error:
                    return new ErrorFrame(headers[BaseStompHeaders.MessageKey], body);

                case StompCommand.Receipt:
                    return new ReceiptFrame(headers[BaseStompHeaders.ReceiptIdKey]);

                case StompCommand.Send:
                    return new SendFrame(headers[BaseStompHeaders.DestinationKey], body, headers[BaseStompHeaders.ContentTypeKey]);

                case StompCommand.Message:
                    return new MessageFrame(headers[BaseStompHeaders.DestinationKey], headers[BaseStompHeaders.SubscriptionKey], body, headers[BaseStompHeaders.ContentTypeKey]);

                case StompCommand.Subscribe:
                    return new SubscribeFrame(headers[BaseStompHeaders.DestinationKey]);
            }

            return stompFrame;
        }
    }
}
