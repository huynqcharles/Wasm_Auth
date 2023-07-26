using Blazored.LocalStorage;
using BlazorWebAssembly_Authorization.Client.Extensions;
using BlazorWebAssembly_Authorization.Client.Services.IServices;
using BlazorWebAssembly_Authorization.Shared.Models;
using BlazorWebAssembly_Authorization.Shared.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorWebAssembly_Authorization.Client.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public LoginService(HttpClient httpClient, ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Response> LoginAsync(LoginViewModel loginUser)
        {
            var result = await this.httpClient.PostAsJsonAsync("/api/login", loginUser);
            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine(content.ToString());
            var loginResponse = JsonSerializer.Deserialize<Response>(content,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            if (!result.IsSuccessStatusCode)
            {
                return loginResponse;
            }
            await this.localStorageService.SetItemAsync("authToken", loginResponse.Token);
            ((CustomAuthenticationStateProvider)this.authenticationStateProvider).MarkUserAsAuthenticated(loginUser.UserName);
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);
            return loginResponse;
        }

        public async Task LogoutAsync()
        {
            await this.localStorageService.RemoveItemAsync("authToken");

            // Mark as user is logged out
            ((CustomAuthenticationStateProvider)this.authenticationStateProvider).MarkUserAsLoggedOut();

            // Remove Authorization JWT from header
            this.httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
