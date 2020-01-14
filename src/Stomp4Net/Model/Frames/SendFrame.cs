namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#SEND">SEND frame</see>.
    /// </summary>
    public class SendFrame : StompFrame
    {
        public SendFrame(string destination, string body, string contentType)
            : base(StompCommand.Send, body)
        {
            this.Headers["destination"] = destination;
            this.Headers["content-type"] = contentType;
            this.Headers["content-length"] = body.Length.ToString();
        }

        public string Destination
        {
            get { return this.Headers["destination"]; }
        }

        public string ContentType
        {
            get { return this.Headers["content-type"]; }
        }

        public string ContentLength
        {
            get { return this.Headers["content-length"]; }
        }
    }
}
