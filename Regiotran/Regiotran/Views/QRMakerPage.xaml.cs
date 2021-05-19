using ConceptDevelopment.Net.Cryptography;
using CryptoForms;
using Newtonsoft.Json;
using Regiotran.Helpers;
using Regiotran.Models;
using Regiotran.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;

namespace Regiotran.Views
{
    /// <summary>
    /// Page to show the something went wrong
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRMakerPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QRMakerPage" /> class.
        /// </summary>
        /// 
        public Login Login { get; set; }
        public string qrData {get; set; }
        public string qrDataCrypt { get; set; }
        public struct bytes { };
        

        
        public QRMakerPage()
        {
            this.InitializeComponent();
            this.BindingContext = QRMakerPageViewModel.BindingContext;

            Login data = JsonConvert.DeserializeObject<Login>(Settings.GeneralSettings);
            Login qr = new Login
            {
                Id = data.Id,
                Name = data.Name,
                Number = data.Number,
                Tickets = data.Tickets,
            };

            qrData = JsonConvert.SerializeObject(qr);

            var ps = new PontifexSolitaire("patitofeo");
            qrDataCrypt = ps.Encrypt(data.Id).Pad5();

        }

        protected override void OnAppearing()
        {
            
            base.OnAppearing();            

            this.QRCodeView.BarcodeOptions = new ZXing.Common.EncodingOptions()
            {
                Height = 300,
                Width = 300
            };
            this.QRCodeView.BarcodeValue = qrDataCrypt;
        }
    }
}