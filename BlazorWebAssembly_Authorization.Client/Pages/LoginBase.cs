using BlazorWebAssembly_Authorization.Client.Services.IServices;
using BlazorWebAssembly_Authorization.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace BlazorWebAssembly_Authorization.Client.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILoginService LoginService { get; set; }
        public LoginViewModel LoginViewModel { get; set; } = new LoginViewModel();

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected async Task HandleSubmitForm()
        {
            var result = await LoginService.LoginAsync(LoginViewModel);
            if (result.IsSuccess)
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
