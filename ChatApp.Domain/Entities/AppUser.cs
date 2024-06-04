namespace ChatApp.Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string? ProfilePicturePath { get; set; }
        public string IP { get; set; }
        public string? Status { get; set; }
        public DateTime? LastSeen { get; set; }
        //public DateTime AuthenticationTime { get; set; }
        //public string UserKey { get; set; }
        //public int Port { get; set; }

        public ICollection<FriendRequest> SentFriendRequests { get; set; }
        public ICollection<FriendRequest> ReceivedFriendRequests { get; set; }
        public ICollection<ChatMessage> SentMessages { get; set; }
        public ICollection<ChatMessage> ReceivedMessages { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
