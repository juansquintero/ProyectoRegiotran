using Newtonsoft.Json;
using Regiotran.Helpers;
using Regiotran.Models;
using Regiotran.Models;
using Regiotran.Views;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Regiotran.ViewModels
{
    /// <summary>
    /// ViewModel for Checkout page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CheckoutPageViewModel : BaseViewModel
    {
        #region Fields

        private static CheckoutPageViewModel checkoutPageViewModel;

        private ObservableCollection<Payment> paymentModes;

        private Command editCommand;

        private Command addAddressCommand;

        private Command placeOrderCommand;

        private Command paymentOptionCommand;

        private Command applyCouponCommand;
        
        readonly FirebaseHelper fireBaseHelper = new FirebaseHelper();

        #endregion

        #region Constructor

        public Login login { get; set; }
        Login data = JsonConvert.DeserializeObject<Login>(Settings.GeneralSettings);
        public string finder { get; set; }       

        /// <summary>
        /// Initializes a new instance for the <see cref="CheckoutPageViewModel" /> class.
        /// </summary>
        public CheckoutPageViewModel()
        {
            login = new Login
            {
                Name = data.Name,
                Number = data.Number,
                Tickets = data.Tickets,
            };
            finder = data.Id;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the value of my cards page view model.
        /// </summary>
        public static CheckoutPageViewModel BindingContext =>
            checkoutPageViewModel = PopulateData<CheckoutPageViewModel>("transaction.json"); 
      
        /// <summary>
        /// Gets or sets the property that has been bound with SfListView, which displays the payment modes.
        /// </summary>
        [DataMember(Name = "paymentModes")]
        public ObservableCollection<Payment> PaymentModes
        {
            get { return this.paymentModes; }

            set
            {
                if (this.paymentModes == value)
                {
                    return;
                }

                this.SetProperty(ref this.paymentModes, value);
            }
        }       
        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public Command EditCommand
        {
            get
            {
                return this.editCommand ?? (this.editCommand = new Command(this.EditClicked));
            }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the Add new address button is clicked.
        /// </summary>
        public Command AddAddressCommand
        {
            get
            {
                return this.addAddressCommand ?? (this.addAddressCommand = new Command(this.AddAddressClicked));
            }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public Command PlaceOrderCommand
        {
            get
            {
                return this.placeOrderCommand ?? (this.placeOrderCommand = new Command(this.PlaceOrderClicked));
            }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the payment option button is clicked.
        /// </summary>
        public Command PaymentOptionCommand
        {
            get
            {
                return this.paymentOptionCommand ?? (this.paymentOptionCommand = new Command(this.PaymentOptionClicked));
            }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the apply coupon button is clicked.
        /// </summary>
        public Command ApplyCouponCommand
        {
            get
            {
                return this.applyCouponCommand ?? (this.applyCouponCommand = new Command(this.ApplyCouponClicked));
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
        /// Invoked when the Edit button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void EditClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Add address button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void AddAddressClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Place order button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void PlaceOrderClicked(object obj)
        {
            //Login data = JsonConvert.DeserializeObject<Login>(Settings.GeneralSettings);
            //finder = data.Id;
            //var user = await fireBaseHelper.GetById(finder);
            //var sum = user.Tickets + 1;
            //await fireBaseHelper.AddTicket(user.Id, user.Name, user.Number, user.Password, user.Rol, sum);
            Application.Current.MainPage = new PaymentSuccessPage();
        }

        /// <summary>
        /// Invoked when the Payment option is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void PaymentOptionClicked(object obj)
        {
            if (obj is RowDefinition rowDefinition && rowDefinition.Height.Value == 0)
            {
                rowDefinition.Height = GridLength.Auto;
            }
        }

        /// <summary>
        /// Invoked when the Apply coupon button is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ApplyCouponClicked(object obj)
        {
            // Do something
        }

        #endregion
    }
}