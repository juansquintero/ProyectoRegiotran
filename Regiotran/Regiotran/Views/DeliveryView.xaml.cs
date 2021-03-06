using Regiotran.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Regiotran.Views
{
    /// <summary>
    /// The Delivery view. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeliveryView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryView" /> class.
        /// </summary>
        public DeliveryView()
        {
            this.InitializeComponent();
            BindingContext = new CheckoutPageViewModel();
        }
    }
}