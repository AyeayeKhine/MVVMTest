using MVVMTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVMTest.Views
{
  
    public partial class RegistrationPage : ContentPage
    {
        RegisterViewModel registerViewModel = new RegisterViewModel();
        public RegistrationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = new RegisterViewModel();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await System.Threading.Tasks.Task.Delay(250);
                txtUserName.Focus();
            });
        }
    }
}