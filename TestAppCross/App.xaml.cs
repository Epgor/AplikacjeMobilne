using MonkeyCache.FileStore;
using System;
using TestAppCross.Services;
using TestAppCross.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAppCross
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Barrel.ApplicationId = AppInfo.PackageName;
            DependencyService.Register<CinemaMockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
