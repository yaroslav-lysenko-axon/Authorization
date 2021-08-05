using System.Collections.Generic;

namespace Authorization.Domain.Models
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<RoleScope> RoleScopes { get; set; }
    }
}
