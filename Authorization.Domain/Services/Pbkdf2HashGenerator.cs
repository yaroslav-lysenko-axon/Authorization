using System;
using System.Security.Cryptography;
using Authorization.Domain.Services.Abstraction;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Authorization.Domain.Services
{
    public class Pbkdf2HashGenerator : IHashGenerator
    {
        public const int SaltLength = 128 / 8;
        public const int SubKeyLength = 256 / 8;
        public const int IterationsCount = 10000;
        public const KeyDerivationPrf KeyDerivationAlgorithm = KeyDerivationPrf.HMACSHA256;

        public string CreateSalt()
        {
            var saltBytes = new byte[SaltLength];
            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(saltBytes);

            var salt = Convert.ToBase64String(saltBytes);

            return salt;
        }

        // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-3.1
        public string GenerateHash(string valueToHash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: valueToHash,
                salt: saltBytes,
                prf: KeyDerivationAlgorithm,
                iterationCount: IterationsCount,
                numBytesRequested: SubKeyLength));

            return hashed;
        }
    }
}
