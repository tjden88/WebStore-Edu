using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace WebStore_Edu.Domain.DTO.Identity
{
    public class UserDTO
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public UserLoginInfo LoginInfo { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
    }
}
