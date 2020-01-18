namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#BEGIN">BEGIN frame</see>.
    /// </summary>
    public class BeginFrame : BaseStompFrame<BeginFrameHeaders>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BeginFrame"/> class.
        /// </summary>
        /// <param name="transaction">The transaction identifier which will be used for SEND, COMMIT, ABORT, ACK, and 
        /// NACK frames to bind them to the named transaction. Within the same connection, different transactions 
        /// MUST use different transaction identifiers.
        /// </param>
        public BeginFrame(string transaction)
            : base(StompCommand.Begin)
        {
            this.Headers.Transaction = transaction;
        }
    }
}
