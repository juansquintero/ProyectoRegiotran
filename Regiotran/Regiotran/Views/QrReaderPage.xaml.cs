using Newtonsoft.Json;
using Regiotran.Helpers;
using Regiotran.Models;
using Regiotran.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using ZXing;

namespace Regiotran.Views
{
    /// <summary>
    /// Page to show the payment success.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QrReaderPage : ContentPage
    {
        readonly FirebaseHelper fireBaseHelper = new FirebaseHelper();
        public Login Login { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QrReaderPage" /> class.
        /// </summary>
        public QrReaderPage()
        {
            this.InitializeComponent();
            this.BindingContext = QrReaderPageViewModel.BindingContext;
            BindingContext = new QrReaderPageViewModel();
        }

        public void scanView_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Resultado", "The barcode's text is " + result.Text, "OK");
                var ticketData = JsonConvert.DeserializeObject<Login>(result.Text);
                var userData = await fireBaseHelper.GetNumber(ticketData.Number);
                await fireBaseHelper.AddTicket(userData.Id, userData.Name, userData.Number, userData.Password, userData.Rol, userData.Tickets);
            });                        
        }
    }
}