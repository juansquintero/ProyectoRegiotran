using Newtonsoft.Json;
using Regiotran.Helpers;
using Regiotran.Models;
using Regiotran.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using ZXing;
using ConceptDevelopment.Net.Cryptography;

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
        public string Res {get; set; }

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
                
                var ps = new PontifexSolitaire("patitofeo");
                var Ras = ps.Decrypt(result.Text).Pad5();
                //await DisplayAlert("Resultado", "The barcode's text is " + Ras, "OK");
                var user = await fireBaseHelper.GetById(Ras);

                if (user != null && user.Tickets >= 1)
                {
                    var sumTickets = user.Tickets - 1;
                    await fireBaseHelper.AddTicket(user.Id, user.Name, user.Number, user.Password, user.Rol, sumTickets);
                    //await DisplayAlert("Exito", "El tiquete ha sido cobrado ", "OK");
                    //Application.Current.MainPage = new AdminPage();
                    await Navigation.PushModalAsync(new AdminPage());
                }
                else
                {
                    await DisplayAlert("Error", "El usuario no existe o el codigo esta corrupto o no tiene tiquetes disponibles", "OK");
                    await Navigation.PushModalAsync(new AdminPage());
                }
            });            
        }
    }
}