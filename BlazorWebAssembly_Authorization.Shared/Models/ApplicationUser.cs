using Microsoft.AspNetCore.Identity;

namespace BlazorWebAssembly_Authorization.Shared.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}