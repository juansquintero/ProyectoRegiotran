using Regiotran.Helpers;
using Regiotran.Services;
using Regiotran.Validators;
using Regiotran.Validators.Rules;
using Regiotran.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Regiotran.Models;

namespace Regiotran.ViewModels
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : LoginViewModel
    {
        #region Fields

        private ValidatableObject<string> name;
        private ValidatableObject<string> adminCode;
        private ValidatablePair<string> password;
        readonly FirebaseHelper fireBaseHelper = new FirebaseHelper();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.BackButtonCommand = new Command(this.BackButtonClicked);
        }
        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public ValidatableObject<string> Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.SetProperty(ref this.name, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
        public ValidatablePair<string> Password
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

        public ValidatableObject<string> AdminCode
        {
            get
            {
                return this.adminCode;
            }

            set
            {
                if (this.adminCode == value)
                {
                    return;
                }
                this.SetProperty(ref this.adminCode, value);
            }
        }
        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        public Command BackButtonCommand { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Initialize whether fieldsvalue are true or false.
        /// </summary>
        /// <returns>true or false </returns>
        public bool AreFieldsValid()
        {
            bool isNumber = this.Number.Validate();
            bool isNameValid = this.Name.Validate();
            bool isPasswordValid = this.Password.Validate();
            return isPasswordValid && isNameValid && isNumber;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.Name = new ValidatableObject<string>();
            this.Password = new ValidatablePair<string>();
            this.adminCode = new ValidatableObject<string>();
        }

        /// <summary>
        /// this method contains the validation rules
        /// </summary>
        private void AddValidationRules()
        {
            this.Name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Necesita un nombre" });
            this.Password.Item1.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Necesita una contraseña" });
            this.Password.Item2.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Re-ingrese la contraseña" });
        }

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoginClicked(object obj)
        {
            Application.Current.MainPage = new LoginPage();
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            if (this.AreFieldsValid())
            {
                // Do something
                //await DataBase.AddData(Name.Value,"Test",Number.Value,Password.Item1.ToString());
                //Application.Current.MainPage = new LoginPage();

                var user = await fireBaseHelper.GetNumber(Number.Value);
                if (user != null && Number.Value == user.Number)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ya existe este numero registrado", "OK");
                    return;
                }
                else
                {
                    if (AdminCode.Value == null || AdminCode.Value != "nimda")
                    {
                        await fireBaseHelper.AddPerson(Number.Value, Name.Value, Password.Item1.ToString(), 0, "1");
                        await Application.Current.MainPage.DisplayAlert("Exito", "El usuario ha sido registrado", "OK");
                        Application.Current.MainPage = new LoginPage();
                    }
                    else if (AdminCode.Value == "nimda")
                    {
                        await fireBaseHelper.AddPerson(Number.Value, Name.Value, Password.Item1.ToString(), 0, "0");
                        await Application.Current.MainPage.DisplayAlert("Exito", "El usuario ha sido registrado", "OK");
                        Application.Current.MainPage = new LoginPage();
                    }                                                
                }               
            }
        }

        private void BackButtonClicked(object obj)
        {
            Application.Current.MainPage = new LoginPage();
        }

        #endregion
    }
}