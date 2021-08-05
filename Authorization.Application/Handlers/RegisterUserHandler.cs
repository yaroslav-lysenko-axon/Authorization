using System.Threading;
using System.Threading.Tasks;
using Authorization.Application.Models.Commands;
using Authorization.Application.Models.Responses;
using Authorization.Domain.ConfigurationClasses;
using Authorization.Domain.Repositories;
using Authorization.Domain.Services.Abstraction;
using MediatR;

namespace Authorization.Application.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IClientService _clientService;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokensConfiguration _tokensConfiguration;

        public RegisterUserHandler(
            IClientService clientService,
            IUserService userService,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            ITokensConfiguration tokensConfiguration)
        {
            _clientService = clientService;
            _userService = userService;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _tokensConfiguration = tokensConfiguration;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientService.AuthenticateClient(request.ClientId, request.ClientSecret);
            var user = await _userService.RegisterUser(request.Email, request.Password, request.FirstName, request.LastName);

            var accessToken = _tokenService.IssueAccessToken(user);
            var refreshToken = _tokensConfiguration.ShouldIssueRefreshTokens
                ? await _tokenService.IssueRefreshToken(user, client)
                : null;

            var response = new RegisterUserResponse
            {
                AccessToken = accessToken.Token,
                TokenType = accessToken.Type,
                RefreshToken = refreshToken?.Token,
            };

            await _unitOfWork.Commit();

            return response;
        }
    }
}
