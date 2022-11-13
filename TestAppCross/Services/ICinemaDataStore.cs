using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAppCross.Models;

namespace TestAppCross.Services
{
    public interface ICinemaDataStore<T>
    {
        Task<MovieSession> GetMovieSessionAsync(string id);
        Task<CinemaHall> GetCinemaHallAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task SetItems();
        Task SetItems(CinemaDTO dto);
    }
}
