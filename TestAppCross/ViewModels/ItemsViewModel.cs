using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestAppCross.Models;
using TestAppCross.Services;
using TestAppCross.Views;
using Xamarin.Forms;

namespace TestAppCross.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private ScreeningDay _selectedItem;
        public ObservableCollection<ScreeningDay> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<ScreeningDay> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Najbliższe seanse";
            Items = new ObservableCollection<ScreeningDay>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<ScreeningDay>(OnItemSelected);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                var items = new List<ScreeningDay>();
                Items.Clear();
                var cinemaFromDb = await DataService.GetCinemaDTO();
                if (cinemaFromDb.ScreeningDays != null)
                {
                    await CinemaDataStore.SetItems(cinemaFromDb);
                    items = cinemaFromDb.ScreeningDays;
                }
                if (cinemaFromDb.ScreeningDays.Count == 0)
                {
                    await CinemaDataStore.SetItems();
                    var mockItems = await CinemaDataStore.GetItemsAsync(true);
                    items = mockItems.ToList();
                }

                foreach (var item in items)
                {
                    Items.Add(item);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public ScreeningDay SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(ScreeningDay item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}