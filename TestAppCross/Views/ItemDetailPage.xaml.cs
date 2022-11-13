using System.ComponentModel;
using TestAppCross.ViewModels;
using Xamarin.Forms;

namespace TestAppCross.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}