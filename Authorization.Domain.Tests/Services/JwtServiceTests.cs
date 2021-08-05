using System;
using System.Collections.Generic;
using System.Linq;
using Authorization.Domain.ConfigurationClasses;
using Authorization.Domain.Models;
using Authorization.Domain.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

#pragma warning disable CA2211, SA1401

namespace Authorization.Domain.Tests.Services
{
    public class JwtServiceTests
    {
        public static IEnumerable<object[]> GetJwtStringTestData = new[]
        {
            new object[]
            {
                "some-very-long-test-symmetric-key",

                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0MiIsImVtYWlsIjoidGVzdEBleGFtcGxlLmNvbSIsInVua" +
                "XF1ZV9uYW1lIjoiSm9obiIsImZhbWlseV9uYW1lIjoiU21pdGgiLCJyb2xlIjoiVU5DT05GSVJNRURfVVNFUiIsIm5iZiI6MTU" +
                "5NDYzMzc0NSwiZXhwIjoxNTk0NjM3MzQ1LCJpYXQiOjE1OTQ2MzM3NDUsImlzcyI6InRlc3QtaXNzdWVyIiwiYXVkIjoidGVzd" +
                "C1hdXRob3JpdHkifQ.rqtsvpxdDqfodM9Kl9xWQfwib_wi79xsMDVB8bCUe48",
            },
            new object[]
            {
                "another-very-long-test-symmetric-key",

                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0MiIsImVtYWlsIjoidGVzdEBleGFtcGxlLmNvbSIsInVua" +
                "XF1ZV9uYW1lIjoiSm9obiIsImZhbWlseV9uYW1lIjoiU21pdGgiLCJyb2xlIjoiVU5DT05GSVJNRURfVVNFUiIsIm5iZiI6MTU" +
                "5NDYzMzc0NSwiZXhwIjoxNTk0NjM3MzQ1LCJpYXQiOjE1OTQ2MzM3NDUsImlzcyI6InRlc3QtaXNzdWVyIiwiYXVkIjoidGVzd" +
                "C1hdXRob3JpdHkifQ.Wg3ZdsE_j0_SYrYhVEUduFPw86o6_fyi6QFbvyIdlhI",
            },
        };

        private readonly IJwtConfiguration _configuration;
        private readonly IUser _user;
        private readonly JwtService _sut;

        public JwtServiceTests()
        {
            _configuration = MockData.SetupDefaultJwtConfiguration();
            _user = MockData.SetupDefaultUser();
            var timeProvider = MockData.SetupDefaultTimeProvider();

            _sut = new JwtService(_configuration, timeProvider);
        }

        [Theory]
        [InlineData("issuer1")]
        [InlineData("issuer2")]
        public void CreateJwt_ChangingIssuerInConfiguration_SetsProperIssuer(string issuer)
        {
            // Arrange
            _configuration.Issuer.Returns(issuer);

            // Act
            var jwt = _sut.CreateJwt(_user);

            // Assert
            jwt.Issuer.Should().Be(issuer);
        }

        [Theory]
        [InlineData("authority1")]
        [InlineData("authority2")]
        public void CreateJwt_ChangingAuthorityInConfiguration_SetsProperAuthority(string authority)
        {
            // Arrange
            _configuration.Authority.Returns(authority);

            // Act
            var jwt = _sut.CreateJwt(_user);

            // Assert
            jwt.Audiences.Should().ContainSingle();
            jwt.Audiences.Single().Should().Be(authority);
        }

        [Theory]
        [InlineData("some-very-long-test-symmetric-key", "clPb/HGK9Gx4ea+695l7Vf96Olze7TIUE/aO/9dqB48=")]
        [InlineData("another-very-long-test-symmetric-key", "DcOR8dPAMjE9hFPCDSLQjMlnviM9FAhOS/CSoszCyRo=")]
        public void CreateJwt_ChangingSymmetricKeyInConfiguration_SetsProperKey(string symmetricKey, string expectedThumbprint)
        {
            // Arrange
            _configuration.SymmetricKey.Returns(symmetricKey);

            // Act
            var jwt = _sut.CreateJwt(_user);
            var key = jwt.SigningCredentials.Key;
            var thumbprint = Convert.ToBase64String(key.ComputeJwkThumbprint());

            // Assert
            thumbprint.Should().Be(expectedThumbprint);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(200)]
        public void CreateJwt_ChangingExpirationTimeInConfiguration_SetsProperValidTo(int expirationTimeInHours)
        {
            // Arrange
            _configuration.ExpirationTimeInHours.Returns(expirationTimeInHours);

            // Act
            var jwt = _sut.CreateJwt(_user);

            // Assert
            (jwt.ValidTo - jwt.ValidFrom).TotalHours.Should().Be(expirationTimeInHours);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(200)]
        public void CreateJwt_ChangingExpirationTimeInConfiguration_SetsValidFromSameAsIssuedAt(int expirationTimeInHours)
        {
            // Arrange
            _configuration.ExpirationTimeInHours.Returns(expirationTimeInHours);

            // Act
            var jwt = _sut.CreateJwt(_user);

            // Assert
            jwt.ValidFrom.Should().Be(jwt.IssuedAt);
        }

        [Theory]
        [MemberData(nameof(GetJwtStringTestData))]
        public void GetJwtString_ChangingSymmetricKey_ChangesJwtString(string symmetricKey, string expectedJwt)
        {
            // Arrange
            _configuration.SymmetricKey.Returns(symmetricKey);

            // Act
            var jwtString = _sut.GetJwtString(_sut.CreateJwt(_user));

            // Assert
            jwtString.Should().Be(expectedJwt);
        }
    }
}

#pragma warning restore CA2211, SA1401
