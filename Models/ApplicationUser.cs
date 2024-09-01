using Microsoft.AspNetCore.Identity;

namespace Atula_dotnet_test.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

