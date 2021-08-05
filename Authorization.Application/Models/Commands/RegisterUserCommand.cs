using System;
using Authorization.Application.Models.Responses;
using MediatR;

namespace Authorization.Application.Models.Commands
{
    public class RegisterUserCommand : IRequest<RegisterUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public long ClientId { get; set; }
        public Guid ClientSecret { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
