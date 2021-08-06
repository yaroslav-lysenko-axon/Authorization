using System.Threading;
using System.Threading.Tasks;
using Authorization.Application.Models.Commands;
using Authorization.Application.Models.Responses;
using Authorization.Domain.ConfigurationClasses;
using Authorization.Domain.Services.Abstraction;
using MediatR;

namespace Authorization.Application.Handlers
{
    public class GetUserHandler : IRequestHandler<EnterUserCommand, GetUserTokenResponse>
    {
        private readonly IClientService _clientService;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly ITokensConfiguration _tokensConfiguration;

        public GetUserHandler(
            IClientService clientService,
            IUserService userService,
            ITokenService tokenService,
            ITokensConfiguration tokensConfiguration)
        {
            _clientService = clientService;
            _userService = userService;
            _tokenService = tokenService;
            _tokensConfiguration = tokensConfiguration;
        }

        public async Task<GetUserTokenResponse> Handle(EnterUserCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientService.FindClientByClientId(request.ClientId);
            var user = await _userService.GetUser(request.Email, request.Password);
            var accessToken = _tokenService.IssueAccessToken(user);
            var refreshToken = _tokensConfiguration.ShouldIssueRefreshTokens
                    ? await _tokenService.IssueRefreshToken(user, client)
                    : null;

            var response = new GetUserTokenResponse()
            {
                AccessToken = accessToken.Token,
                TokenType = accessToken.Type,
                RefreshToken = refreshToken?.Token,
            };

            return response;
        }
    }
}
