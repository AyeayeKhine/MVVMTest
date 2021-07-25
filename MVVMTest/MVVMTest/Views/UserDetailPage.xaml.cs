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
  
    public partial class UserDetailPage : ContentPage
    {
        UserDetailViewModel userDetailViewModel = new UserDetailViewModel();
        public UserDetailPage()
        {
            InitializeComponent();
            this.BindingContext = userDetailViewModel;
        }
    }
}