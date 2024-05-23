namespace ChatApp.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int UserId { get; set; }
        public bool Revoked { get; set; }
        public AppUser User { get; set; }

    }
}






