using Blazored.LocalStorage;
using BlazorWebAssembly_Authorization.Client;
using BlazorWebAssembly_Authorization.Client.Extensions;
using BlazorWebAssembly_Authorization.Client.Services;
using BlazorWebAssembly_Authorization.Client.Services.IServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7143/")
});

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

await builder.Build().RunAsync();
