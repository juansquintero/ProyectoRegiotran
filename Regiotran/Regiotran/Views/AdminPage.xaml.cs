using Regiotran.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Regiotran.Views
{
    /// <summary>
    /// Page to show article master page
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminPage" /> class.
        /// </summary>
        public AdminPage()
        {
            this.InitializeComponent();            
            this.BindingContext = AdminPageViewModel.BindingContext;
            BindingContext = new AdminPageViewModel();
        }
    }
}