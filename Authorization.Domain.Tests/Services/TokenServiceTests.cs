using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Authorization.Domain.ConfigurationClasses;
using Authorization.Domain.Models;
using Authorization.Domain.Repositories;
using Authorization.Domain.Services;
using Authorization.Domain.Services.Abstraction;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Authorization.Domain.Tests.Services
{
    public class TokenServiceTests
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokensConfiguration _configuration;
        private readonly IJwtService _jwtService;
        private readonly IUser _user;
        private readonly TokenService _sut;

        public TokenServiceTests()
        {
            _refreshTokenRepository = Substitute.For<IRefreshTokenRepository>();
            _configuration = Substitute.For<ITokensConfiguration>();
            _jwtService = Substitute.For<IJwtService>();
            _user = MockData.SetupDefaultUser();
            var timeProvider = MockData.SetupDefaultTimeProvider();

            _sut = new TokenService(_refreshTokenRepository, _configuration, timeProvider, _jwtService);
        }

        [Fact]
        public void IssueAccessToken_WithDefaultValues_CallsCreateJwt()
        {
            // Act
            _sut.IssueAccessToken(_user);

            // Assert
            _jwtService.Received(1).CreateJwt(_user);
        }

        [Fact]
        public void IssueAccessToken_WithDefaultValues_CallsGetJwtString()
        {
            // Act
            _sut.IssueAccessToken(_user);

            // Assert
            _jwtService.Received(1).GetJwtString(Arg.Any<JwtSecurityToken>());
        }

        [Fact]
        public async Task IssueRefreshToken_WithDefaultValues_CallsRevokeRefreshTokens()
        {
            // Arrange
            var user = new User();
            var client = new Client();

            // Act
            await _sut.IssueRefreshToken(user, client);

            // Assert
            await _refreshTokenRepository.Received(1).RevokeRefreshTokens(user, client, RefreshTokenRevokeReason.Refresh);
        }

        [Fact]
        public async Task IssueRefreshToken_WithDefaultValues_CallsRepositoryInsert()
        {
            // Arrange
            var user = new User();
            var client = new Client();

            // Act
            await _sut.IssueRefreshToken(user, client);

            // Assert
            await _refreshTokenRepository.Received(1).Insert(Arg.Any<RefreshToken>());
        }

        [Fact]
        public async Task IssueRefreshToken_WithDefaultValues_SetsProperUserAndClientPropertiesInRefreshToken()
        {
            // Arrange
            var user = new User();
            var client = new Client();

            // Act
            var refreshToken = await _sut.IssueRefreshToken(user, client);

            // Assert
            refreshToken.User.Should().Be(user);
            refreshToken.Client.Should().Be(client);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(200)]
        public async Task IssueRefreshToken_WithDefaultValues_SetsProperExpireAt(int expirationTimeInHours)
        {
            // Arrange
            var user = new User();
            var client = new Client();
            _configuration.RefreshTokenExpirationTimeInHours.Returns(expirationTimeInHours);

            // Act
            var refreshToken = await _sut.IssueRefreshToken(user, client);

            // Assert
            (refreshToken.ExpireAt - refreshToken.CreatedAt).TotalHours.Should().Be(expirationTimeInHours);
        }
    }
}
