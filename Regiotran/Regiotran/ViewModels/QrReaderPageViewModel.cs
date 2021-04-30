using Regiotran.Services;
using Regiotran.Views;
using System;
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
    public class QrReaderPageViewModel : BaseViewModel
    {
        #region Fields

        private static QrReaderPageViewModel paymentViewModel;

        private Command trackOrderCommand;

        private Command makePaymentCommand;
        private Command backButtonCommand;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="QrReaderPageViewModel"/> class.
        /// </summary>
        public QrReaderPageViewModel()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of payment view model.
        /// </summary>
        public static QrReaderPageViewModel BindingContext =>
            paymentViewModel = PopulateData<QrReaderPageViewModel>("transaction.json");

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

        public Command BackButtonCommand
        {
            get
            {
                return this.backButtonCommand ?? (this.backButtonCommand = new Command(this.BackButtonClicked));
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
            Application.Current.MainPage = new AdminPage();            
        }

        /// <summary>
        /// Invoked when make payment button is clicked.
        /// </summary>
        private void MakePaymentClicked(object obj)
        {
            // Do something
            Application.Current.MainPage = new AdminPage();
        }

        private void BackButtonClicked(object obj)
        {
            Application.Current.MainPage = new AdminPage();
        }
        #endregion
    }
}
