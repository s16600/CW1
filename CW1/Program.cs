using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cwiczenia1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var result = await client.GetAsync("https://www.pja.edu.pl");
            //var result = await client.GetAsync(args[0]);

            if (result.IsSuccessStatusCode) //2xx
            {
                string html = await result.Content.ReadAsStringAsync();
                var regex = new Regex("[a-z0-9]+@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);

                var matches = regex.Matches(html); //=MatchCollecion matches = regex.Matches(html);
                foreach (var m in matches)
                {
                    Console.WriteLine(m);
                }
            }


            Console.WriteLine("Koniec!");
        }
    }
}