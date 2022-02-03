using System.Net.Http.Json;

namespace WebStore_Edu.WebAPI.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected HttpClient Http;

        protected string Address;

        protected BaseClient(HttpClient HttpHttp, string Address)
        {
            Http = HttpHttp;
            this.Address = Address;
        }


        protected T? Get<T>(string url) => GetAsync<T>(url).Result;

        protected async Task<T?> GetAsync<T>(string url, CancellationToken Cancel = default)
        {
            var response = await Http.GetAsync(url, Cancel).ConfigureAwait(false);
            return await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancellationToken: Cancel)
                .ConfigureAwait(false);
        }

        protected HttpResponseMessage Post<T>(string url, T value) => PostAsync<T>(url, value).Result;

        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T value, CancellationToken Cancel = default)
        {
            var response = await Http.PostAsJsonAsync(url, value, Cancel).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }


        protected HttpResponseMessage Put<T>(string url, T value) => PutAsync(url, value).Result;

        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T value, CancellationToken Cancel = default)
        {
            var response = await Http.PutAsJsonAsync(url, value, Cancel).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }


        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;

        protected async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            var response = await Http.DeleteAsync(url).ConfigureAwait(false);
            return response;
        }

        public void Dispose() => Dispose(true);

        protected bool Disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (Disposed) return;
            Disposed = true;

            if (disposing)
            {
                // освобождаем управляемые ресурсы - обычные объекты с интерфейсом IDisposable
                //Http.Dispose(); // - не должны вызывать Dispose() потому, что не мы его создавали
            }

            // освобождаем неуправляемые ресурсы: COM-объекты на пример
        }
    }
}
