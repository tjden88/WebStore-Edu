using WebSore_Edu.WebAPI;
using WebStore_Edu.WebAPI.Clients.Base;

namespace WebStore_Edu.WebAPI.Clients.Identity
{
    public class UsersClient : BaseClient
    {
        public UsersClient(HttpClient HttpHttp) : base(HttpHttp, ApiAddresses.Users)
        {
        }
    }
}
