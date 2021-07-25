using GalaSoft.MvvmLight.Ioc;
using MVVMTest.ClsPublic;
using MVVMTest.Interfaces;
using MVVMTest.Models;
using MVVMTest.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMTest.ViewModels
{
    [QueryProperty(nameof(UserId), nameof(UserId))]
    public class RegisterViewModel : ViewModelBase
    {
        public string userId;
        public string userName;
        public ValidatableObject<string> email;
        public ValidatableObject<string> password;
        public string positioin;
        public ICommand ValidateEmailCommand => new Command(() => ValidateEmail());
        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());
        public ICommand RegisterCommand { get; }
        public ICommand DeleteCommand { get; }

        protected readonly IRepository<User> _repository;

        public RegisterViewModel()
        {
            email = new ValidatableObject<string>();
            password = new ValidatableObject<string>();
            RegisterCommand = new Command(RegisterClick);
            _repository = SimpleIoc.Default.GetInstance<IRepository<User>>();
            Title = "Registration Page";
            AddValidations();
        }

        public void OnAppearing()
        {
            UserName = "";
            Position = "";
            if (string.IsNullOrEmpty(UserId))
            {
                email = new ValidatableObject<string>();
                password = new ValidatableObject<string>();
                AddValidations();
                Email = new ValidatableObject<string>();
                Password = new ValidatableObject<string>();

            }
            
        }
        private bool Validate()
        {
            bool isEmailValidUser = false;
            bool isValidUser = ValidateEmail();
            if (isValidUser)
                isEmailValidUser = EmailFormatValidate();
            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword && isEmailValidUser;
        }

        private bool ValidateEmail()
        {
            if (Email.Validate())
                return EmailFormatValidate();
            else
                return Email.Validate();


        }

        private bool EmailFormatValidate()
        {
            return email.EmailFormatValidate();
        }

        private bool ValidatePassword()
        {
            return password.Validate();
        }

        private void AddValidations()
        {
            email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email is required." });
            password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password is required." });
        }
        
        private async void RegisterClick()
        {
            if (Validate())
            {
                User userMoedl = new User();
                userMoedl.UserName = UserName;
                userMoedl.Email = Email.Value;
                userMoedl.Password = Password.Value;
                userMoedl.Position = Position;
                
                await _repository.AddItemAsync(userMoedl);
                await Shell.Current.GoToAsync($"//UsersPage");
                await Shell.Current.GoToAsync("..");
                UserId = null;
            }
            

        }

        private  async Task UserById(string UserId)
        {
            //Task.Run(async () => await _repository.GetItemAsync(Guid.Parse(UserId)));
            User userModel =  await _repository.GetItemAsync(Guid.Parse(UserId));
            UserName = userModel.UserName;
            Email.Value = userModel.Email;
            password.Value = userModel.Password;
            email.Value = userModel.Email;
            Password.Value = userModel.Password;
            Position = userModel.Position;

        }

        public string UserId
        {
            get => userId;
            set {
                SetProperty(ref userId, value);
                if (!string.IsNullOrEmpty(UserId))
                {
                    Task.Run(async () => await UserById(userId));
                }
            }
        }
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }
        public string Position
        {
            get => positioin;
            set => SetProperty(ref positioin, value);
        }

        public ValidatableObject<string> Email
        {
            get => email;
            set => RaisePropertyChanged(() => Email);
            //set => SetProperty(ref email, value);
        }

        public ValidatableObject<string> Password
        {
            get => password;
            set => RaisePropertyChanged(() => Password);
            //set => SetProperty(ref password, value);
        }

    }
}
