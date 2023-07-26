using BlazorWebAssembly_Authorization.API.Repositories.Contracts;
using BlazorWebAssembly_Authorization.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebAssembly_Authorization.API.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await this.userManager.Users.ToListAsync();
        }
    }
}
