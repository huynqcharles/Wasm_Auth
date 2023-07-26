using BlazorWebAssembly_Authorization.Client.Services.IServices;
using Microsoft.AspNetCore.Components;

namespace BlazorWebAssembly_Authorization.Client.Pages
{
    public class LogoutBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILoginService LoginService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoginService.LogoutAsync();
            NavigationManager.NavigateTo("/");
        }
    }
}
