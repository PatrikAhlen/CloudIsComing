using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
           


            for (int i = 0; i < 100; i++)
            {
                var response = await client.GetAsync("http://localhost:50150/api/appinfo");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }

            Console.ReadLine();
        }
    }
}
