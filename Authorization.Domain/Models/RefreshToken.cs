using System;

namespace Authorization.Domain.Models
{
    public class RefreshToken
    {
        public long Id { get; set; }
        public Client Client { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public string RevokeReason { get; set; }
    }
}
