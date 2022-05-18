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
    /// Page to show the cart list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage
    {
        FirebaseHelper services;

        public CartPage(Login user)
        {
            this.InitializeComponent();
            BindingContext = user;
            services = new FirebaseHelper();
        }

        public async void BtnDelete(object sender, EventArgs e)
        {
            await services.DeletePerson(Id.Text);
            Application.Current.MainPage = new WishlistPage();
        }
        public async void BtnUpdate(object sender, EventArgs e)
        {
            await services.UpdatePerson(Id.Text, Name.Text, Number.Text, Rol.Text, Password.Text, int.Parse(Tickets.Text));
            Application.Current.MainPage = new WishlistPage();
        }
    }
}