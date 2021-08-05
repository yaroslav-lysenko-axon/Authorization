using System.Threading.Tasks;
using Authorization.Domain.Exceptions;
using Authorization.Domain.Models;
using Authorization.Domain.Repositories;
using Authorization.Domain.Services.Abstraction;

namespace Authorization.Domain.Services
{
    public class UserService : IUserService
    {
        private const string UserRegistrationRole = "UNCONFIRMED_USER";

        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHashGenerator _hashGenerator;

        public UserService(
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            IHashGenerator hashGenerator)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _hashGenerator = hashGenerator;
        }

        public async Task<User> RegisterUser(string email, string password, string firstName, string lastName)
        {
            var role = await _roleRepository.FindByName(UserRegistrationRole);
            if (role == null)
            {
                throw new RoleNotFoundException(UserRegistrationRole);
            }

            var existingUser = await _userRepository.FindByEmail(email);
            if (existingUser != null)
            {
                throw new DuplicateEmailException(email);
            }

            var salt = _hashGenerator.CreateSalt();
            var passwordHash = _hashGenerator.GenerateHash(password, salt);
            var user = new User
            {
                Email = email,
                PasswordHash = passwordHash,
                Salt = salt,
                FirstName = firstName,
                LastName = lastName,
                Active = false,
                Role = role,
            };

            await _userRepository.Insert(user);

            return user;
        }

        public async Task<User> GetUserAndPassword(string email, string password)
        {
            var existingUser = await _userRepository.FindByEmail(email);
            if (existingUser != null)
            {
                var role = await _roleRepository.FindByName(UserRegistrationRole);
                if (role == null)
                {
                    throw new RoleNotFoundException(UserRegistrationRole);
                }

                var salt = _hashGenerator.CreateSalt();
                var passwordHash = _hashGenerator.GenerateHash(password, salt);
                var user = new User
                {
                    FirstName = existingUser.FirstName,
                    LastName = existingUser.LastName,
                    Email = email,
                    PasswordHash = passwordHash,
                    Salt = salt,
                    Active = false,
                    Role = role,
                };

                return user;
            }

            return null;
        }
    }
}
