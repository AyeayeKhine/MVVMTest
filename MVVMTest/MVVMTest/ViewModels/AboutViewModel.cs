using MVVMTest.ViewModels.Base;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MVVMTest.ViewModels
{
    [QueryProperty(nameof(UserName), nameof(UserName))]
    public class AboutViewModel : BaseViewModel
    {

        private string userName;

        public string UserName
        {
            get { return userName; }
            set 
            { 
                SetProperty(ref userName, value);
            }
        }
        public AboutViewModel()
        {
            UserName = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}