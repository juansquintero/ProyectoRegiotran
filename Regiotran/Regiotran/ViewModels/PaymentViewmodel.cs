using Newtonsoft.Json;
using Regiotran.Helpers;
using Regiotran.Models;
using Regiotran.Views;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Regiotran.ViewModels
{
    /// <summary>
    /// ViewModel for Payment page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class PaymentViewModel : BaseViewModel
    {
        #region Fields

        private static PaymentViewModel paymentViewModel;

        private Command trackOrderCommand;

        private Command makePaymentCommand;
        public Login login { get; set; }

        Login data = JsonConvert.DeserializeObject<Login>(Settings.GeneralSettings);
        readonly FirebaseHelper fireBaseHelper = new FirebaseHelper();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="PaymentViewModel"/> class.
        /// </summary>
        public PaymentViewModel()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of payment view model.
        /// </summary>
        public static PaymentViewModel BindingContext =>
            paymentViewModel = PopulateData<PaymentViewModel>("transaction.json");

        /// <summary>
        /// Gets or sets the payment success icon.
        /// </summary>
        [DataMember(Name = "paymentSuccessIcon")]
        public string PaymentSuccessIcon { get; set; }

        /// <summary>
        /// Gets or sets the payment failure icon.
        /// </summary>
        [DataMember(Name = "paymentFailureIcon")]
        public string PaymentFailureIcon { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the command that will be executed when track order button is clicked.
        /// </summary>
        public Command TrackOrderCommand
        {
            get
            {
                return this.trackOrderCommand ?? (this.trackOrderCommand = new Command(this.TrackOrderClicked));
            }
        }

        /// <summary>
        /// Gets the command that will be executed when make payment button is clicked.
        /// </summary>
        public Command MakePaymentCommand
        {
            get
            {
                return this.makePaymentCommand ?? (this.makePaymentCommand = new Command(this.MakePaymentClicked));
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
        /// Invoked when track order button is clicked.
        /// </summary>
        private void TrackOrderClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when make payment button is clicked.
        /// </summary>
        private async void MakePaymentClicked(object obj)
        {
            Login data = JsonConvert.DeserializeObject<Login>(Settings.GeneralSettings);
            //var user = await fireBaseHelper.GetById(data.Id);
            var sum = data.Tickets + 1;
            await fireBaseHelper.AddTicket(data.Id, data.Name, data.Number, data.Password, data.Rol, sum);
            Application.Current.MainPage = new ProfilePage();
        }

        #endregion
    }
}
