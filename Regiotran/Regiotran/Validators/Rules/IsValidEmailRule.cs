using System.Text.RegularExpressions;
using Xamarin.Forms.Internals;

namespace Regiotran.Validators.Rules
{
    /// <summary>
    /// Validation Rule to check the email is valid or not.
    /// </summary>
    /// <typeparam name="T">Valid email rule parameter</typeparam>
    [Preserve(AllMembers = true)]
    public class IsValidEmailRule<T> : IValidationRule<T>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Validation Message.
        /// </summary>
        public string ValidationMessage { get; set; }

        #endregion

        #region Method

        /// <summary>
        /// Check the email has valid or not
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>returns bool value</returns>
        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var str = value as string;
            Regex regex = new Regex(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$");
            Match match = regex.Match(str);
            return match.Success;
        }
        #endregion
    }
}
