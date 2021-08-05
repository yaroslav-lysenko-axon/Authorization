using System.Net;
using Authorization.Domain.Models;

namespace Authorization.Domain.Exceptions
{
    public class DuplicateEmailException : ApplicationException
    {
        public DuplicateEmailException(string roleName)
            : base(ErrorCode.DuplicateMail, HttpStatusCode.BadRequest, roleName)
        {
        }
    }
}
