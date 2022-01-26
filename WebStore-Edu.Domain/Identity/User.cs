using Microsoft.AspNetCore.Identity;

namespace WebStore_Edu.Domain.Identity
{
    public class User : IdentityUser
    {
        public const string Administrator = "Admin";
        public const string DefaultAdminPassword = "Admin";

    }
}
