namespace Stomp4Net.Model.Frames
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#RECEIPT">RECEIPT frame</see>.
    /// </summary>
    public class ReceiptFrame : BaseStompFrame<ReceiptFrameHeaders>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiptFrame"/> class.
        /// </summary>
        /// <param name="receiptId">Value of the receipt header in the frame which this is a receipt for.</param>
        public ReceiptFrame(string receiptId)
            : base(StompCommand.Receipt)
        {
            this.Headers.ReceiptId = receiptId;
        }
    }
}