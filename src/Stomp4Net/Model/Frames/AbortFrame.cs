namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#ABORT">ABORT frame</see>.
    /// </summary>
    public class AbortFrame : BaseStompFrame<AbortFrameHeaders>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbortFrame"/> class.
        /// </summary>
        /// <param name="transaction">Identifier of the transaction to abort.</param>
        public AbortFrame(string transaction)
            : base(StompCommand.Abort)
        {
            this.Headers.Transaction = transaction;
        }
    }
}
