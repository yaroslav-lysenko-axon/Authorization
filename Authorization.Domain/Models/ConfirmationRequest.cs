using System;

namespace Authorization.Domain.Models
{
    public class ConfirmationRequest
    {
        public long Id { get; set; }
        public User User { get; set; }
        public string Key { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public bool Confirmed { get; set; }
        public string RequestType { get; set; }
    }
}
