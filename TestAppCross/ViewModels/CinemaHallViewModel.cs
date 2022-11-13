using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TestAppCross.Models;
using TestAppCross.Views;
using Xamarin.Forms;

namespace TestAppCross.ViewModels
{
	[QueryProperty(nameof(ItemId), nameof(ItemId))]
	public class CinemaHallViewModel : BaseViewModel
	{
		//privates
        private string itemId;
        private string name;
        private string seats;
        private string premiumSeats;
        private string isOpen;
        private MovieSession movieSession;
        //commands
        public Command<MovieSession> ItemTapped { get; }
        public ObservableCollection<MovieSession> MovieSessions { get; }
        public CinemaHallViewModel ()
		{
            MovieSessions = new ObservableCollection<MovieSession>();
            ItemTapped = new Command<MovieSession>(OnItemSelected);
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
        public string Name { get => name; 
            set
            {
                SetProperty(ref name, value);
            } 
        }
        public string Seats 
        { 
            get => seats;
            set
            {
                SetProperty(ref seats, value);
            }
        }
        public string PremiumSeats
        {
            get => premiumSeats;
            set
            {
                SetProperty(ref premiumSeats, value);
            }
        }
        public string IsOpen 
        { 
            get => isOpen;
            set
            {
                SetProperty(ref isOpen, value);
            }
        }
        public MovieSession MovieSession
        {
            get => movieSession;
            set
            {
                SetProperty(ref movieSession, value);
                OnItemSelected(value);
            }
        }
        public async void LoadItemId(string itemId)
        {
            try
            {
                MovieSessions.Clear();
                var item = await CinemaDataStore.GetCinemaHallAsync(itemId);
                Id = item.Id;
                Name = item.Name;
                Seats = $"Miejsc: {item.Seats}";
                PremiumSeats = $"Miejsc VIP: {item.PremiumSeats}";
                IsOpen = item.IsOpen ? "Open that day." : "Closed that day";

                foreach (var session in item.MovieSessions)
                {
                    MovieSessions.Add(session);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        async void OnItemSelected(MovieSession item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(MovieSessionPage)}?{nameof(MovieSessionViewModel.ItemId)}={item.Id}");
        }
    }
}