using Regiotran.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Regiotran.Views
{
    /// <summary>
    /// Page Displaying saved cards.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavedCardPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SavedCardPage" /> class.
        /// </summary>
        public SavedCardPage()
        {
            this.InitializeComponent();
            this.BindingContext = MyCardsViewModel.BindingContext;
        }
    }
}