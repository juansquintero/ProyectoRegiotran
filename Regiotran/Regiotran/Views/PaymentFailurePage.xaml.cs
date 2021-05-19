﻿using Regiotran.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Regiotran.Views
{
    /// <summary>
    /// Page to show the payment failure.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentFailurePage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentFailurePage" /> class.
        /// </summary>
        public PaymentFailurePage()
        {
            this.InitializeComponent();
            this.BindingContext = PaymentViewModel.BindingContext;
        }
    }
}