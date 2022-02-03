using System.Net.Http.Json;

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


        protected T? Get<T>(string url) => GetAsync<T>(url).Result;

        protected async Task<T?> GetAsync<T>(string url)
        {
            var response = await Http.GetAsync(url).ConfigureAwait(false);
            return await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>()
                .ConfigureAwait(false);
        }

        protected HttpResponseMessage Post<T>(string url, T value) => PostAsync<T>(url, value).Result;

        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T value)
        {
            var response = await Http.PostAsJsonAsync(url, value).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }


        protected HttpResponseMessage Put<T>(string url, T value) => PutAsync(url, value).Result;

        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T value)
        {
            var response = await Http.PutAsJsonAsync(url, value).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }


        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;

        protected async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            var response = await Http.DeleteAsync(url).ConfigureAwait(false);
            return response;
        }
    }
}
