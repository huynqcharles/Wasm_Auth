using BlazorWebAssembly_Authorization.Shared.Models;

namespace BlazorWebAssembly_Authorization.API.Repositories.Contracts
{
    public interface IUsersRepository
    {
        Task<IEnumerable<ApplicationUser>> GetUsers();
    }
}
