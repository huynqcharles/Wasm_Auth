using BlazorWebAssembly_Authorization.Shared.Models;
using BlazorWebAssembly_Authorization.Shared.ViewModels;

namespace BlazorWebAssembly_Authorization.API.Repositories.Contracts
{
    public interface ILoginRepository
    {
        Task<Response> LoginAsync(LoginViewModel loginUser);
    }
}
