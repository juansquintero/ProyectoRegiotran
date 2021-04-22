using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace Regiotran.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LectorCodeQR : ContentView
    {
        public LectorCodeQR()
        {
            InitializeComponent();
        }

        private async void btnScannerQR_Clicked(object sender, EventArgs e)
        {
            try
            {
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                scanner.TopText = "Lector codigo QR \n Regiotran";
                scanner.BottomText = "Lea su codigo ahora";
                var result = await scanner.Scan();
                if (result != null)
                {
                    txtResultado.Text = result.Text;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }

        }
    }
}