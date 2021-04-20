using Newtonsoft.Json;
using Regiotran.Helpers;
using Regiotran.Models;
using Regiotran.Views;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;

namespace Regiotran.ViewModels
{
    /// <summary>
    /// ViewModel for something went wrong page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class QRMakerPageViewModel : BaseViewModel
    {
        #region Fields

        private static QRMakerPageViewModel somethingWentWrongPageViewModel;

        private string imagePath;

        private string header;

        private string content;

        private Command tryAgainCommand;
        public Login Login { get; set; }

        Login data = JsonConvert.DeserializeObject<Login>(Settings.GeneralSettings);
        

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance for the <see cref="QRMakerPageViewModel" /> class.
    /// </summary>
    public QRMakerPageViewModel()
        {
                       
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of something went wrong page view model.
        /// </summary>
        public static QRMakerPageViewModel BindingContext =>
            somethingWentWrongPageViewModel = PopulateData<QRMakerPageViewModel>("errorAndEmpty.json");

        /// <summary>
        /// Gets or sets the ImagePath.
        /// </summary>
        [DataMember(Name = "somethingWentWrongImagePath")]
        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }

            set
            {
                this.SetProperty(ref this.imagePath, value);
            }
        }

        /// <summary>
        /// Gets or sets the Header.
        /// </summary>
        [DataMember(Name = "somethingWentWrongHeader")]
        public string Header
        {
            get
            {
                return this.header;
            }

            set
            {
                this.SetProperty(ref this.header, value);
            }
        }

        /// <summary>
        /// Gets or sets the Content.
        /// </summary>
        [DataMember(Name = "somethingWentWrongContent")]
        public string Content
        {
            get
            {
                return this.content;
            }

            set
            {
                this.SetProperty(ref this.content, value);
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the command that is executed when the TryAgain button is clicked.
        /// </summary>
        public Command TryAgainCommand
        {
            get
            {
                return this.tryAgainCommand ?? (this.tryAgainCommand = new Command(this.TryAgain));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populates the data for view model from json file.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="fileName">Json file to fetch data.</param>
        /// <returns>Returns the view model object.</returns>
        private static T PopulateData<T>(string fileName)
        {
            var file = "Regiotran.Data." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T data;

            using (var stream = assembly.GetManifestResourceStream(file))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                data = (T)serializer.ReadObject(stream);
            }

            return data;
        }

        /// <summary>
        /// Invoked when the TryAgain button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void TryAgain(object obj)
        {
            Application.Current.MainPage = new ProfilePage();
        }

        #endregion
    }
}
