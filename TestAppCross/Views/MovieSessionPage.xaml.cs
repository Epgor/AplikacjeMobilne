using System;
using System.ComponentModel;
using TestAppCross.ViewModels;
using Xamarin.Forms;

namespace TestAppCross.Views
{
	public partial class MovieSessionPage : ContentPage
    {
		public MovieSessionPage ()
		{
			InitializeComponent ();
			BindingContext = new MovieSessionViewModel();
        }
	}
}