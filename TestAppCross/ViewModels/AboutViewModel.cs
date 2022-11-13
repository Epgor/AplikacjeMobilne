using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TestAppCross.Services;
using Xamarin.Forms;

namespace TestAppCross.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private string textPing;
        private string textAuth;
        public string Ping
        {
            get { return textPing; }
            set { SetProperty(ref textPing, value); }
        }
        public string Auth
        {
            get { return textAuth; }
            set { SetProperty(ref textAuth, value); }
        }
        public Command LoadItemsCommand { get; }
        public Command AuthenticateCommand { get; }
        public AboutViewModel()
        {
            Title = "KinoAPI";
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AuthenticateCommand = new Command(async () => await ExecuteAuthenticationCheck());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Ping = "Waiting";
                Ping = await DataService.GetHealth();

            }
            catch (Exception ex)
            {
                Ping = "Redis is Dead";
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteAuthenticationCheck()
        {
            IsBusy = true;

            try
            {
                Auth = "Waiting";
                Auth = await DataService.GetAuthentication();

            }
            catch (Exception ex)
            {
                Auth = "Authentication Failed!";
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
    
}