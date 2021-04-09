using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Regiotran.Views
{
    /// <summary>
    /// View used to show the email entry with validation status.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleNumberEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleNumberEntry" /> class.
        /// </summary>
        public SimpleNumberEntry()
        {
            this.InitializeComponent();
        }
    }
}