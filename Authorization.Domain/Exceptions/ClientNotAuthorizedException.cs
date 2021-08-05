using System.Net;
using Authorization.Domain.Models;

namespace Authorization.Domain.Exceptions
{
    public class ClientNotAuthorizedException : ApplicationException
    {
        private const string MessageTemplate = "Client auth failed ('client_id' or 'client_secret' is invalid).";

        public ClientNotAuthorizedException()
            : base(ErrorCode.ClientAuthorizationFailed, HttpStatusCode.Unauthorized, MessageTemplate)
        {
        }
    }
}
