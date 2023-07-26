using BlazorWebAssembly_Authorization.API.Repositories.Contracts;
using BlazorWebAssembly_Authorization.Shared.Models;
using BlazorWebAssembly_Authorization.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssembly_Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterRepository registerRepository;

        public RegisterController(IRegisterRepository registerRepository)
        {
            this.registerRepository = registerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerUser, string role)
        {
            var result = await this.registerRepository.RegisterAsync(registerUser, role);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
