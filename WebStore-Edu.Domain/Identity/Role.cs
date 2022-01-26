using Microsoft.AspNetCore.Identity;

namespace WebStore_Edu.Domain.Identity
{
    public class Role : IdentityRole
    {
        public const string Administrators = "Administrators";
        public const string Users = "Users";

    }
}
