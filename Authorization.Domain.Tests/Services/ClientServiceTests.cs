using System;
using System.Threading.Tasks;
using Authorization.Domain.Exceptions;
using Authorization.Domain.Repositories;
using Authorization.Domain.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Authorization.Domain.Tests.Services
{
    public class ClientServiceTests
    {
        private readonly ClientService _sut;

        public ClientServiceTests()
        {
            var repository = Substitute.For<IClientRepository>();
            _sut = new ClientService(repository);
        }

        [Fact]
        public void AuthenticateClient_CantFindAClient_ThrowsClientNotAuthorizedException()
        {
            // Arrange
            long clientId = 0;
            Guid clientSecret = Guid.NewGuid();

            // Act
            Func<Task> action = () => _sut.AuthenticateClient(clientId, clientSecret);

            // Assert
            action.Should().ThrowExactly<ClientNotAuthorizedException>();
        }
    }
}
