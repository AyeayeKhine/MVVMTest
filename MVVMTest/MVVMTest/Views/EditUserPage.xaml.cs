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
  
    public partial class EditUserPage : ContentPage
    {
        EditUserViewModel registerViewModel = new EditUserViewModel();
        public EditUserPage()
        {
            InitializeComponent();
            this.BindingContext = registerViewModel;
        }

    }
}