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
            Console.Write("Enter IP address: ");
            string ip = Console.ReadLine();
            string url = $"https://ipinfo.io/{ip}/json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine("[ + ] Request successfully made.");

                    string ResponseData = await response.Content.ReadAsStringAsync();
                    Data IpInfo = JsonConvert.DeserializeObject<Data>(ResponseData);

                    Console.Clear();
                    Console.WriteLine($"[ / ] Country: {IpInfo.country}");
                    Console.WriteLine($"[ / ] City: {IpInfo.city}");
                    Console.WriteLine($"[ / ] Coordinates: {IpInfo.loc}");
                    Console.WriteLine($"[ / ] Postal code: {IpInfo.postal}");
                    Console.WriteLine($"[ / ] ASN: {IpInfo.org}");
                    Console.WriteLine($"[ / ] Time Zone: {IpInfo.timezone}");

                    await Task.Delay(8000); // I had trouble making it to NOT quit program instantly, I think you can remove "await Task.Delay(8000)" if you want.
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
