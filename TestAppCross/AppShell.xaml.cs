using System;
using System.Collections.Generic;
using TestAppCross.Models;
using TestAppCross.ViewModels;
using TestAppCross.Views;
using Xamarin.Forms;

namespace TestAppCross
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(CinemaHallPage), typeof(CinemaHallPage));
            Routing.RegisterRoute(nameof(MovieSessionPage), typeof(MovieSessionPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
