using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Authorization.Domain.Models
{
    public class User : IUser
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RemovedAt { get; set; }
        public ICollection<ConfirmationRequest> ConfirmationRequests { get; set; }
    }
}
