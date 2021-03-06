using Newtonsoft.Json;
using Regiotran.Helpers;
using Regiotran.Models;
using Regiotran.Services;
using Regiotran.Validators;
using Regiotran.Validators.Rules;
using Regiotran.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Regiotran.ViewModels
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : LoginViewModel
    {
        #region Fields

        private ValidatableObject<string> password;
        readonly FirebaseHelper fireBaseHelper = new FirebaseHelper();

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
        }

        #endregion Constructor

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public ValidatableObject<string> Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.SetProperty(ref this.password, value);
            }
        }

        #endregion property

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public Command SocialMediaLoginCommand { get; set; }

        #endregion Command

        #region methods

        /// <summary>
        /// Check the password is null or empty
        /// </summary>
        /// <returns>Returns the fields are valid or not</returns>
        public bool AreFieldsValid()
        {
            bool isNumberValid = this.Number.Validate();
            bool isPasswordValid = this.Password.Validate();
            return isNumberValid && isPasswordValid;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.Password = new ValidatableObject<string>();
        }

        /// <summary>
        /// Validation rule for password
        /// </summary>
        private void AddValidationRules()
        {
            this.Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });
        }

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj)
        {
            if (this.AreFieldsValid())
            {
                var user = await fireBaseHelper.Login(Number.Value, Password.Value); 
                if (user != null && user.Rol == "1")
                {
                    //await DisplayAlert("Error", "Ya existe este numero registrado", "OK");
                    await Application.Current.MainPage.DisplayAlert("Bienvenido", "Usuario" , "OK");
                    
                    Login data = new Login
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Number = user.Number,
                        Password = user.Password,
                        Tickets = user.Tickets,
                        Rol = user.Rol
                    };
                    string stringData = JsonConvert.SerializeObject(data);
                    Settings.GeneralSettings = stringData;
                    Application.Current.MainPage = new ProfilePage();
                }
                else if (user != null && user.Rol == "0" )
                {
                    await Application.Current.MainPage.DisplayAlert("Bienvenido", "Administrador", "OK");

                    Login data = new Login
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Number = user.Number,
                        Password = user.Password,
                        Tickets = user.Tickets,
                        Rol = user.Rol
                    };
                    string stringData = JsonConvert.SerializeObject(data);
                    Settings.GeneralSettings = stringData;
                    Application.Current.MainPage = new AdminPage();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Este ususario no esta registrado", "OK");
                    Application.Current.MainPage = new LoginPage();
                }
                //await fireBaseHelper.Login(Number.Value, Password.Value);                
            }
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            Application.Current.MainPage = new SignUpPage();
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ForgotPasswordClicked(object obj)
        {
            
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SocialLoggedIn(object obj)
        {
            // Do something
        }

        #endregion methods
    }
}