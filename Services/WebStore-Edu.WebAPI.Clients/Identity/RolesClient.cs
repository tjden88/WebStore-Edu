using WebSore_Edu.WebAPI;
using WebStore_Edu.WebAPI.Clients.Base;

namespace WebStore_Edu.WebAPI.Clients.Identity;

public class RolesClient : BaseClient
{
    public RolesClient(HttpClient HttpHttp) : base(HttpHttp, ApiAddresses.Roles)
    {
    }
}