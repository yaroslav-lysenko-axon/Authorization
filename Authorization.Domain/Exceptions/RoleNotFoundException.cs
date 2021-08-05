using System.Globalization;
using System.Net;
using Authorization.Domain.Models;

namespace Authorization.Domain.Exceptions
{
    public class RoleNotFoundException : ApplicationException
    {
        private const string MessageTemplate = "Role with name '{0}' not found.";

        public RoleNotFoundException(string roleName)
            : base(ErrorCode.RoleNotFound, HttpStatusCode.NotFound, string.Format(CultureInfo.InvariantCulture, MessageTemplate, roleName))
        {
        }
    }
}
