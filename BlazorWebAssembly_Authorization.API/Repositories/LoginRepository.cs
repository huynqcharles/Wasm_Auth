using BlazorWebAssembly_Authorization.API.Repositories.Contracts;
using BlazorWebAssembly_Authorization.Shared.Models;
using BlazorWebAssembly_Authorization.Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorWebAssembly_Authorization.API.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public LoginRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<Response> LoginAsync(LoginViewModel loginUser)
        {
            // Check user is exists
            var user = await this.userManager.FindByNameAsync(loginUser.UserName);
            if (user == null)
            {
                return new Response()
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "This user not exist!"
                };
            }
            else if (!await this.userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                return new Response()
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Invalid password!"
                };
            }
            else /*(user != null && await this.userManager.CheckPasswordAsync(user, loginUser.Password))*/
            {
                // Get list roles of user
                var roles = await this.userManager.GetRolesAsync(user);

                // Create list of claims
                var claims = new List<Claim>()
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                };

                // Create JWT Token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JWT:SecretKey"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(this.configuration["JWT:Issuer"], this.configuration["JWT:Audience"],
                    claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                return new Response()
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Valid user!",
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }
            
        }
    }
}
