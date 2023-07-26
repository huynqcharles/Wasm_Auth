using BlazorWebAssembly_Authorization.API.Repositories.Contracts;
using BlazorWebAssembly_Authorization.Shared.Models;
using BlazorWebAssembly_Authorization.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssembly_Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
        {
            var result = await this.loginRepository.LoginAsync(loginUser);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return StatusCode(result.StatusCode, result);
        }
    }
}
