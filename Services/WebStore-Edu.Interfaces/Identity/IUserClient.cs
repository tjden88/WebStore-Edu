using Microsoft.AspNetCore.Identity;
using WebStore_Edu.Domain.Identity;

namespace WebStore_Edu.Interfaces.Identity
{
    public interface IUserClient : 
        IUserRoleStore<User>,
        IUserPasswordStore<User>,
        IUserEmailStore<User>,
        IUserPhoneNumberStore<User>,
        IUserTwoFactorStore<User>,
        IUserLoginStore<User>,
        IUserClaimStore<User>

    {

    }
}
