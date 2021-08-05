using System;
using System.Threading.Tasks;
using Authorization.Domain.Models;

namespace Authorization.Domain.Services.Abstraction
{
    public interface IClientService
    {
        Task<Client> AuthenticateClient(long clientId, Guid clientSecret);
        Task<Client> FindClientByName(string name);
    }
}
