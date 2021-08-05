namespace Authorization.Domain.Models
{
    public class RoleScope
    {
        public long RoleId { get; set; }
        public string ScopeId { get; set; }
        public Role Role { get; set; }
        public Scope Scope { get; set; }
    }
}
