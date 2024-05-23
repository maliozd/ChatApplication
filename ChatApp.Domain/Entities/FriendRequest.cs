namespace ChatApp.Domain.Entities
{
    public class FriendRequest : BaseEntity
    {
        public int SenderUserId { get; set; }
        public AppUser SenderUser { get; set; }
        public int ReceiverUserId { get; set; }
        public AppUser ReceiverUser { get; set; }
        public bool IsApproved { get; set; }
    }
}