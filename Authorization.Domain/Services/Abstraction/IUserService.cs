using System.Threading.Tasks;
using Authorization.Domain.Models;

namespace Authorization.Domain.Services.Abstraction
{
    public interface IUserService
    {
        Task<User> RegisterUser(string email, string password, string firstName, string lastName);
        Task<User> GetUser(string email, string password);
    }
}
