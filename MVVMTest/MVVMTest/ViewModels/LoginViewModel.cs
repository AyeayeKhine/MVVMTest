using MVVMTest.ClsPublic;
using MVVMTest.ViewModels.Base;
using MVVMTest.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMTest.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);

            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            AddValidations();
            UserName = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
        }

        public void OnAppearing()
        {
            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            AddValidations();
            UserName = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();

        }
        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                //_userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                //_password = value;
                RaisePropertyChanged(() => Password);
            }
        }
        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());

        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());

        private bool Validate()
        {
            bool isEmailValidUser = false;
            bool isValidUser = ValidateUserName();
            if (isValidUser)
                isEmailValidUser = EmailValidateUserName();
            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword && isEmailValidUser;
        }

        private bool ValidateUserName()
        {
            if (UserName.Validate())
                return EmailValidateUserName();
            else
                return UserName.Validate();


        }

        private bool EmailValidateUserName()
        {
            return UserName.EmailFormatValidate();
        }

        private bool ValidatePassword()
        {
            return Password.Validate();
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Username is required." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password is required." });
        }
        public Command LoginCommand { get; }



        private async void OnLoginClicked(object obj)
        {

            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
            if (Validate())
            {
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}?{nameof(UserName)}={UserName.Value}");
            }
                
        }
    }
}
