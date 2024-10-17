namespace ChatApp.Domain.Entities
{
    public class ChatMessage : BaseEntity
    {
        public int FromUserId { get; set; }
        public AppUser Sender { get; set; }
        public int ToUserId { get; set; }
        public AppUser Receiver { get; set; }
        public DateTime Timestamp { get; set; }
        public bool ReadStatus { get; set; }
        public DateTime? ReadDt { get; set; }
        public string MessageText { get; set; }
    }
}
