namespace WebStore_Edu.WebAPI.Clients.Base
{
    public abstract class BaseClient
    {
        protected HttpClient Client;

        protected string Address;

        protected BaseClient(HttpClient HttpClient, string Address)
        {
            Client = HttpClient;
            this.Address = Address;
        }
    }
}
