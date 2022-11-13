using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using TestAppCross.Models;
using Xamarin.Essentials;
using MonkeyCache.FileStore;
using System.Net.Http.Headers;

namespace TestAppCross.Services
{
    public static class DataService
    {
        static string HealthCheckUrl = "http://10.0.2.2:5295/api/Redis/HealthCheck";
        static string CinemaUrl = "http://10.0.2.2:5295/api/cinema";
        static string AuthCkeckUrl = "http://10.0.2.2:5295/User/AuthenticationHealthCheck";
        static HttpClient client;
        static DataService()
        {
            try
            {
                client = new HttpClient()
                {
                    Timeout = TimeSpan.FromSeconds(5)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YWRtaW46YWRtaW4=");
            }
            catch
            {

            }
        }
        public static async Task<string> GetHealth()
        {
            Uri uri = new Uri(HealthCheckUrl);

            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            return "Failed connecting to database";
        }

        public static async Task<string> GetAuthentication()
        {
            Uri uri = new Uri(AuthCkeckUrl);

            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            return "Failed to authenticate";
        }

        public static Task<CinemaDTO> GetCinemaDTO() =>
            GetAsync<CinemaDTO>(CinemaUrl, "get-cinema");

        static async Task<T> GetAsync<T>(string url, string key, int mins = 30, bool forceRefresh = false)
        {
            var json = string.Empty;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                json = Barrel.Current.Get<string>(key);
            else if (!forceRefresh && !Barrel.Current.IsExpired(key))
                json = Barrel.Current.Get<string>(key);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync(url);

                    Barrel.Current.Add(key, json, TimeSpan.FromMinutes(mins));
                }
                return JsonConvert.DeserializeObject<T>(json);
            }


            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw ex;
            }


        }
    }
}