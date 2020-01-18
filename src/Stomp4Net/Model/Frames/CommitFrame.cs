namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#COMMIT">COMMIT frame</see>.
    /// </summary>
    public class CommitFrame : BaseStompFrame<CommitFrameHeaders>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommitFrame"/> class.
        /// </summary>
        /// <param name="transaction">Specifies the identifier of the transaction to commit.</param>
        public CommitFrame(string transaction)
            : base(StompCommand.Commit)
        {
            this.Headers.Transaction = transaction;
        }
    }
}
