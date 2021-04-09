using Regiotran.Validators;
using Regiotran.Validators.Rules;
using Xamarin.Forms.Internals;

namespace Regiotran.ViewModels
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginViewModel : BaseViewModel
    {
        #region Fields

        private ValidatableObject<string> number;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginViewModel" /> class.
        /// </summary>
        public LoginViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the email ID from user in the login page.
        /// </summary>
        public ValidatableObject<string> Number
        {
            get
            {
                return this.number;
            }

            set
            {
                if (this.number == value)
                {
                    return;
                }

                this.SetProperty(ref this.number, value);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// This method to validate the email
        /// </summary>
        /// <returns>returns bool value</returns>
        public bool IsNumberFieldValid()
        {
            bool isNumberValid = this.Number.Validate();
            return isNumberValid;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.Number = new ValidatableObject<string>();
        }

        /// <summary>
        /// This method contains the validation rules
        /// </summary>
        private void AddValidationRules()
        {
            this.Number.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Numero requerido" });
            this.Number.Validations.Add(new IsValidEmailRule<string> { ValidationMessage = "Numero invalido" });
        }

        #endregion
    }
}
