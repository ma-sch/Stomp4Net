namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#UNSUBSCRIBE">UNSUBSCRIBE frame</see>.
    /// </summary>
    public class UnsubscribeFrame : BaseStompFrame<UnsubscribeFrameHeaders>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsubscribeFrame"/> class.
        /// </summary>
        /// <param name="id">Identifies the subscription that should be removed. It must match the subscription identifier of an existing subscription.</param>
        public UnsubscribeFrame(string id)
            : base(StompCommand.Unsubscribe)
        {
            this.Headers.Id = id;
        }
    }
}
