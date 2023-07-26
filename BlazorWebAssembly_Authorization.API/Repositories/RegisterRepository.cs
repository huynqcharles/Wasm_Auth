using BlazorWebAssembly_Authorization.API.Repositories.Contracts;
using BlazorWebAssembly_Authorization.Shared.Models;
using BlazorWebAssembly_Authorization.Shared.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BlazorWebAssembly_Authorization.API.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RegisterRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<Response> RegisterAsync(RegisterViewModel registerUser, string role)
        {
            // Check user is exists or not
            if (await this.userManager.FindByEmailAsync(registerUser.Email) != null)
            {
                return new Response
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "This email is already exists!"
                };
            }
            else if (await this.userManager.FindByNameAsync(registerUser.UserName) != null)
            {
                return new Response
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "This username is already exists!"
                };
            }

            // Create an identity user
            ApplicationUser identityUser = new()
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName
            };

            // Check if roles is exist or not
            if (await this.roleManager.RoleExistsAsync(role))
            {
                // Add user to the database
                var result = await this.userManager.CreateAsync(identityUser, registerUser.Password);

                // Check if register is fail
                if (!result.Succeeded)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "This username is already exists!"
                    };
                }

                // Add role to the user
                await this.userManager.AddToRoleAsync(identityUser, role);
                return new Response
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Register successfully!"
                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "This role doesn't exists!"
                };
            }
        }
    }
}
