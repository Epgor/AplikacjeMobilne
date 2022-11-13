using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TestAppCross.Models;
using TestAppCross.Views;
using Xamarin.Forms;

namespace TestAppCross.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        //private fields
        private string itemId;
        private string date;
        private string description;
        private string isOpen;
        private string isExtra;
        private CinemaHall cinemaHall;

        //command
        public Command<CinemaHall> ItemTapped { get; }

        public ObservableCollection<CinemaHall> CinemaHalls { get; }
        
        public ItemDetailViewModel()
        {
            CinemaHalls = new ObservableCollection<CinemaHall>();
            ItemTapped = new Command<CinemaHall>(OnItemSelected);
        }
        
        //setter from query
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

        //public fields set/get
        public string Id { get; set; }
        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string IsOpen
        {
            get => isOpen;
            set => SetProperty(ref isOpen, value);
        }
        public string IsExtra
        {
            get => isExtra;
            set => SetProperty(ref isExtra, value);
        }
        public CinemaHall CinemaHall
        {
            get => cinemaHall;
            set
            {
                SetProperty(ref cinemaHall, value);
                OnItemSelected(value);
            }
        }
        //item getting
        public async void LoadItemId(string itemId)
        {
            try
            {
                CinemaHalls.Clear();
                var item = await CinemaDataStore.GetItemAsync(itemId);
                Id = item.Id;
                Date = item.Date.ToShortDateString();
                IsOpen = item.IsOpen ? "Open that day." : "Closed that day";
                IsExtra = item.IsExtraPaid ? "Extra paid" : "Regular price";
                foreach(var hall in item.CinemaHalls)
                {
                    CinemaHalls.Add(hall);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        async void OnItemSelected(CinemaHall item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(CinemaHallPage)}?{nameof(CinemaHallViewModel.ItemId)}={item.Id}");
        }
    }
}
