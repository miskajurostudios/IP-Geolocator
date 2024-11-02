# IP-Geolocator
Geolocate IP address using C# and ipinfo.io

Source Code for this is a Visual Studio!

This Geolocator is using ipinfo.io to get information about IP address!
(Made in Visual Studio 2022)

# Source Code (C#)
This is the code that was written in VS

```csharp
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Geolocator
{
    public class Data
    {
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string org { get; set; }
        public string postal { get; set; }
        public string timezone { get; set; }
        public string loc { get; set; }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "GeoLocator";
            Console.Write("Zadejte IP Adresu: ");
            string ip = Console.ReadLine();
            string url = $"https://ipinfo.io/{ip}/json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine("[ + ] Požadavek Úspěšně Udělán");

                    string ResponseData = await response.Content.ReadAsStringAsync();
                    Data IpInfo = JsonConvert.DeserializeObject<Data>(ResponseData);

                    Console.Clear();
                    Console.WriteLine($"[ / ] Země: {IpInfo.country}");
                    Console.WriteLine($"[ / ] Město: {IpInfo.city}");
                    Console.WriteLine($"[ / ] Souřadnice: {IpInfo.loc}");
                    Console.WriteLine($"[ / ] Poštovní směrovací číslo: {IpInfo.postal}");
                    Console.WriteLine($"[ / ] ASN: {IpInfo.org}");
                    Console.WriteLine($"[ / ] Časová zóna: {IpInfo.timezone}");

                    await Task.Delay(8000); // Počká 8 sekund
                    Console.ReadKey();
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"[ - ] ERROR: {ex.Message}");
                }
            }
        }
    }
}
```
