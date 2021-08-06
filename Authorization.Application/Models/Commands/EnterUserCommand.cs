using Authorization.Application.Models.Responses;
using MediatR;

namespace Authorization.Application.Models.Commands
{
    public class EnterUserCommand : IRequest<GetUserTokenResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
