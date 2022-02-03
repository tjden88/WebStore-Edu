using Microsoft.AspNetCore.Identity;
using WebStore_Edu.Domain.Identity;

namespace WebStore_Edu.Interfaces.Identity;

public interface IRoleClient : IRoleStore<Role>
{

}