using MVVMTest.Services;
using MVVMTest.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVMTest
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            SetupApp.Instance.Setup();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
