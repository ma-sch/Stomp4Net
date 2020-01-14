namespace Stomp4Net.Model
{
    /// <summary>
    /// Stomp <see cref="https://stomp.github.io/stomp-specification-1.2.html#RECEIPT">RECEIPT frame</see>.
    /// </summary>
    public class ReceiptFrame : StompFrame
    {
        public ReceiptFrame()
            : base(StompCommand.Receipt)
        {
        }
    }
}