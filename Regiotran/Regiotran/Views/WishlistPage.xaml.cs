using Regiotran.DataService;
using Regiotran.Helpers;
using Regiotran.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Regiotran.Views
{
    /// <summary>
    /// Page to show the wishlist. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WishlistPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public WishlistPage()
        {
            this.InitializeComponent();
            this.BindingContext = WishlistDataService.Instance.WishlistPageViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var allPersons = await firebaseHelper.GetAllPersons();
            lstPersons.ItemsSource = allPersons;
        }
        public void OnItemSelected(object sender, ItemTappedEventArgs args)
        {
            var user = args.Item as Login;
            if (user == null) return;

            Application.Current.MainPage = new CartPage(user);
            lstPersons.SelectedItem = null;
        }

    }
}