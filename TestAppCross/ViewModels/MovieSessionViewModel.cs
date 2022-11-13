
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace TestAppCross.ViewModels
{
	[QueryProperty(nameof(ItemId), nameof(ItemId))]
	public class MovieSessionViewModel : BaseViewModel
	{
		//privates
        private string itemId;
        private string occupiedSeats;
        private string occupiedPremiumSeats;
        private string startDateTime;
        private string name;
        private string genre;
        private string length;
        private string ticketPrice;
        private string premiumTicketPrice;
        private string imageUrl;
        private UriImageSource webImage;
        //commands

        public MovieSessionViewModel()
		{

		}
        //public setter from query
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        //public set/get
        public string Id { get; set; }
        public UriImageSource WebImage
        {
            get => webImage;
            set => SetProperty(ref webImage, value);
        }
        public string OccupiedSeats
        {
            get => occupiedSeats;
            set => SetProperty(ref occupiedSeats, value);
        }
        public string OccupiedPremiumSeats
        {
            get => occupiedPremiumSeats;
            set => SetProperty(ref occupiedPremiumSeats, value);
        }
        public string StartDateTime
        {
            get => startDateTime;
            set => SetProperty(ref startDateTime, value);
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Genre
        {
            get => genre;
            set => SetProperty(ref genre, value);
        }
        public string Length
        {
            get => length;
            set => SetProperty(ref length, value);
        }
        public string TicketPrice
        {
            get => ticketPrice;
            set => SetProperty(ref ticketPrice, value);
        }
        public string PremiumTicketPrice
        {
            get => premiumTicketPrice;
            set => SetProperty(ref premiumTicketPrice, value);
        }
        public string ImageUrl
        {
            get => imageUrl;
            set => SetProperty(ref imageUrl, value);
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await CinemaDataStore.GetMovieSessionAsync(itemId);
                Id = item.MoviePlayed.Id.ToString();
                OccupiedSeats = $"Zajęte miejsca: {item.OccupiedSeats}";
                OccupiedPremiumSeats = $"Zajęte miejsca VIP: {item.OccupiedPremiumSeats}";
                StartDateTime = $"Data: {item.StartDateTime.ToShortDateString()}";
                Name = item.MoviePlayed.Name.ToString();
                Genre = $"Gatunek: {item.MoviePlayed.Genre}";
                var tempLength = item.MoviePlayed.Length;
                Length = $"Czas trwania: {tempLength.Hours}{ tempLength.Minutes}:{tempLength.Seconds}";
                TicketPrice = $"Cena: {item.MoviePlayed.TicketPrice} zł";
                PremiumTicketPrice = $"Cena za VIP: {item.MoviePlayed.PremiumTicketPrice} zł";
                ImageUrl = item.MoviePlayed.ImageUrl;
                WebImage = new UriImageSource
                {
                    Uri = new Uri(ImageUrl),
                    CachingEnabled = true,
                    CacheValidity = TimeSpan.FromMinutes(30),
                };           
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}