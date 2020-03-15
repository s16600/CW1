using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cwiczenia1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var client = new HttpClient();

            try
            {
                
                //var result = await client.GetAsync("https://www.pja.edu.pl");

                if (args.Length != 1) throw new System.ArgumentNullException();
                if (!Uri.IsWellFormedUriString(args[0], UriKind.Absolute)) throw new System.ArgumentException();

                var result = await client.GetAsync(args[0]);


                if (result.IsSuccessStatusCode) //2xx
                {
                    string html = await result.Content.ReadAsStringAsync();
                    var regex = new Regex("[a-z0-9]+@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);

                    HashSet<string> zbiorAdresow = new HashSet<string>();
                    var matches = regex.Matches(html); //=MatchCollecion matches = regex.Matches(html);
                    foreach (var m in matches)
                    {
                        //Console.WriteLine(m);
                        zbiorAdresow.Add(m.ToString());
                    }

                    if (zbiorAdresow.Count>0)
                    {
                        foreach (string s in zbiorAdresow) Console.WriteLine(s);
                    } else
                    {
                        Console.WriteLine("Nie znaleziono adresów email");
                    }
                }
                else Console.WriteLine("Błąd w czasie pobierania strony");


            }

            catch (System.ArgumentNullException e)
            {
                Console.WriteLine("Zbyt mała liczba argumentów");
            }

            catch (System.ArgumentException e)
            {
                Console.WriteLine("Błędny format adresu URL");
            }

            finally
            {
                client.Dispose();
                //Console.WriteLine("Zwolniono zasoby");
            }
            
            
            Console.WriteLine("Koniec!");
        }
    }
}