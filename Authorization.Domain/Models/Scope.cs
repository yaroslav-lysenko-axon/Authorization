using System.Collections.Generic;

namespace Authorization.Domain.Models
{
    public class Scope
    {
        public string Id { get; set; }
        public ICollection<RoleScope> RoleScopes { get; set; }
    }
}
