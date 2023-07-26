using BlazorWebAssembly_Authorization.Shared.Models;
using BlazorWebAssembly_Authorization.Shared.ViewModels;

namespace BlazorWebAssembly_Authorization.API.Repositories.Contracts
{
    public interface IRegisterRepository
    {
        Task<Response> RegisterAsync(RegisterViewModel registerUser, string role);
    }
}
