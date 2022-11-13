using Android.Content.Res;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestAppCross.Models;

namespace TestAppCross.Services
{

    class CinemaMockDataStore : ICinemaDataStore<ScreeningDay>
    {
        private List<ScreeningDay> items;
        private static CinemaDTO _cinema;
        private string _filePath = "exampleSeedData.json";
        public CinemaMockDataStore()
        {
            ReadFromFile();
        }
        public async Task SetItems()
        {
            items = _cinema.ScreeningDays;
        }
        public async Task SetItems(CinemaDTO data)
        {
            items = data.ScreeningDays;
        }
        public async Task<MovieSession> GetMovieSessionAsync(string id)
        {
            var screeningDays = await Task.FromResult(items);
            var cinemaHalls = GetAllHalls(screeningDays);
            var movieSessions = GetAllSessions(cinemaHalls);

            return movieSessions.First(r => r.Id == id);
        }

        public async Task<CinemaHall> GetCinemaHallAsync(string id)
        {
            var screeningDays = await Task.FromResult(items);
            var cinemaHalls = GetAllHalls(screeningDays);

            return cinemaHalls.First(r => r.Id == id);
        }
        public async Task<ScreeningDay> GetItemAsync(string id)
        {
            var screeningDay = await Task.FromResult(items);
            return screeningDay.First(r => r.Id == id);
        }

        public async Task<IEnumerable<ScreeningDay>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
        private List<MovieSession> GetAllSessions(List<CinemaHall> cinemaHalls)
        {
            var movieSessions = new List<MovieSession>();
            foreach(var hall in cinemaHalls)
            {
                movieSessions.AddRange(hall.MovieSessions);
            }
            return movieSessions;
        }
        private List<CinemaHall> GetAllHalls(List<ScreeningDay> screeningDays)
        {
            var cinemaHalls = new List<CinemaHall>();
            foreach (var day in screeningDays)
            {
                foreach (var item in day.CinemaHalls)
                {
                    cinemaHalls.Add(item);
                }
            }
            return cinemaHalls;
        }

        private async Task<int> ReadFromFile()
        {
            try
            {            
                AssetManager assets = Android.App.Application.Context.Assets;

                using (var reader = new StreamReader(assets.Open(_filePath)))
                {
                    string json = reader.ReadToEnd();
                    _cinema = JsonConvert.DeserializeObject<CinemaDTO>(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
            return 0;
        }
    }

}
