using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace feladat
{
    class Connection
    {
        HttpClient client = new HttpClient();
        string serverUrl = "";

        public Connection(string serverUrl)
        {
            this.serverUrl = serverUrl;
        }

        public async void Create(string name, float score, int price)
        {
            string url = serverUrl + "/createKolbasz";

            try
            {
                var jsonInfo = new
                {
                    name = name,
                    score = score,
                    price = price
                };
                string jsonStringified = JsonConvert.SerializeObject(jsonInfo);
                HttpContent send = new StringContent(jsonStringified, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, send);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Sikeres létrehozás");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<List<string>> All()
        {
            string url = serverUrl + "/kolbaszok";
            List<string> list = new List<string>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<JsonData>>(result).Select(item => $"{item.name},{item.score},{item.price}").ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return list;
        }

        public async void Delete(int id)
        {
            string url = $"{serverUrl}/deleteKolbasz/{id}";

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Sikeres törlés");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
