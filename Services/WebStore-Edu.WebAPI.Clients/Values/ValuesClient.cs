using System.Net.Http.Json;
using WebSore_Edu.WebAPI;
using WebStore_Edu.Interfaces.TestApi;
using WebStore_Edu.WebAPI.Clients.Base;

namespace WebStore_Edu.WebAPI.Clients.Values
{
    public class ValuesClient : BaseClient, IValuesApiService
    {
        public ValuesClient(HttpClient HttpHttp) : base(HttpHttp, ApiAddresses.Values)
        {
        }
        public IEnumerable<string> GetAll()
        {
            var response = Http.GetAsync(Address).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result!;
            }

            return Enumerable.Empty<string>();
        }

        public string? Get(int Id)
        {
            var response = Http.GetAsync($"{Address}/{Id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<string>().Result!;
            }

            return null;
        }

        public int Count()
        {
            var response = Http.GetAsync($"{Address}/count").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<int>().Result;
            }

            return -1;
        }

        public void Add(string Value)
        {
            var response = Http.PostAsJsonAsync(Address, Value).Result;
            response.EnsureSuccessStatusCode();
        }

        public void Update(int Id, string Value)
        {
            var response = Http.PutAsJsonAsync($"{Address}/{Id}", Value).Result;
            response.EnsureSuccessStatusCode();
        }

        public bool Delete(int Id)
        {
            var response = Http.DeleteAsync($"{Address}/{Id}").Result;
           return response.IsSuccessStatusCode;
        }


    }
}
