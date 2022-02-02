namespace WebStore_Edu.WebAPI.Clients.Base
{
    public abstract class BaseClient
    {
        protected HttpClient Http;

        protected string Address;

        protected BaseClient(HttpClient HttpHttp, string Address)
        {
            Http = HttpHttp;
            this.Address = Address;
        }
    }
}
