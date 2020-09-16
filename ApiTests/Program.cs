using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTests
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new HttpClient();

            Console.Write("Enter amount of http-requests you want to make:");
            var amount = Console.ReadLine();

            int.TryParse(amount, out int result);
            if (result >0)
            {
                Console.Write("Do you want parralell requests? (y/n)");
                var beAggressive = Console.ReadKey();
                if (beAggressive.KeyChar.ToString().ToLower() == "y")
                {
                    BeAggressive(client, result);
                }
                else
                {
                    BeKind(client, result);
                }
            }
            else
            {
                Console.WriteLine("No?");
            }

            
            Console.ReadLine();
        }

        private static void BeKind(HttpClient client, in int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                RequestAppInfoAsync(client, i);
            }
        }

        private static void BeAggressive(HttpClient client, int amount)
        {
            var array = new int[amount];
            var i = 0;
            foreach (var j in array)
            {
                array[i] = i++;
            }
            var list = array.ToList();
            Parallel.ForEach(array, x =>
            {
                RequestAppInfoAsync(client, x);
            });
            
        }

        private static async Task RequestAppInfoAsync(HttpClient httpClient, int index)
        {
            try
            {
                string fmt = "00000000.##";
                string formatString = " {0,15:" + fmt + "}";

                var now = DateTime.Now;
                var response = await httpClient.GetAsync("http://localhost:50150/api/appinfo");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.Write(formatString, index);
                Console.WriteLine($"requested: {now.ToLongTimeString()}, returned: {DateTime.Now.ToLongTimeString()} : result: {responseBody}");
                

            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception: {e}");
                Debug.WriteLine($"Inner: {e.InnerException}");
            }
        }

        private static void WriteColorCoded()
        {

        }
    }
}
