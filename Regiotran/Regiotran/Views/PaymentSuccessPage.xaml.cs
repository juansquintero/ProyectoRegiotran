using Newtonsoft.Json;
using Regiotran.Helpers;
using Regiotran.Models;
using Regiotran.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Regiotran.Views
{
    /// <summary>
    /// Page to show the payment success.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentSuccessPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentSuccessPage" /> class.
        /// </summary>
        readonly FirebaseHelper fireBaseHelper = new FirebaseHelper();
        public Login Login { get; set; }

        public PaymentSuccessPage()
        {
            this.InitializeComponent();
            this.BindingContext = PaymentViewModel.BindingContext;
            Recarga();
        }

        public async void Recarga()
        {
            Login data = JsonConvert.DeserializeObject<Login>(Settings.GeneralSettings);
            //finder = data.Id;
            var user = await fireBaseHelper.GetById(data.Id);
            var sum = user.Tickets + 1;
            await fireBaseHelper.AddTicket(user.Id, user.Name, user.Number, user.Password, user.Rol, sum);
            await Navigation.PushModalAsync(new ProfilePage());
        }
    }
}