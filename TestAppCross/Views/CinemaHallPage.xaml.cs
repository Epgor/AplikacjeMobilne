using System.ComponentModel;
using TestAppCross.ViewModels;
using Xamarin.Forms;

namespace TestAppCross.Views
{
	public partial class CinemaHallPage : ContentPage
    {
		public CinemaHallPage ()
		{
			InitializeComponent ();
			BindingContext = new CinemaHallViewModel();
		}
	}
}