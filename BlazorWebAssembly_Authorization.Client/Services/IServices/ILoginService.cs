using BlazorWebAssembly_Authorization.Shared.Models;
using BlazorWebAssembly_Authorization.Shared.ViewModels;

namespace BlazorWebAssembly_Authorization.Client.Services.IServices
{
    public interface ILoginService
    {
        Task<Response> LoginAsync(LoginViewModel loginUser);
        Task LogoutAsync();
    }
}
