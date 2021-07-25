using GalaSoft.MvvmLight.Ioc;
using MVVMTest.ClsPublic;
using MVVMTest.Interfaces;
using MVVMTest.Models;
using MVVMTest.ViewModels.Base;
using MVVMTest.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMTest.ViewModels
{
    [QueryProperty(nameof(UserId), nameof(UserId))]
    public class UserDetailViewModel : ViewModelBase
    {
        public string userId;
        private string userName;
        private string email;
        public string password;
        public string position;
       
        public Command EditCommand { get; }
        public Command DeleteCommand { get; }
        protected readonly IRepository<User> _repository;

        public UserDetailViewModel()
        {
            EditCommand = new Command(RegisterClick);
            DeleteCommand = new Command(() => DeleteClick(UserId));
            _repository = SimpleIoc.Default.GetInstance<IRepository<User>>();
            Title = "User Detail Page";
            
        }

        private async void DeleteClick(string userId)
        {
            await _repository.DeleteItemAsync(Guid.Parse(UserId));
            await Shell.Current.GoToAsync($"//UsersPage");
        }

       
        private async void RegisterClick()
        {
            //await Shell.Current.GoToAsync($"//UsersPage");  
            await Shell.Current.GoToAsync($"{nameof(EditUserPage)}?{nameof(EditUserViewModel.UserId)}={UserId}");
            //await Shell.Current.GoToAsync($"//RegistrationPage?UserId={ UserId }");
        }

        private  async Task UserById(string UserId)
        {
            //Task.Run(async () => await _repository.GetItemAsync(Guid.Parse(UserId)));
            User userModel =  await _repository.GetItemAsync(Guid.Parse(UserId));
            UserName = userModel.UserName;
            Email = userModel.Email;
            Password = userModel.Password;
            Position = userModel.Position;
        }

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public string Position
        {
            get => position;
            set => SetProperty(ref position, value);
        }
        public string UserId
        {
            get => userId;
            set {
                SetProperty(ref userId, Uri.UnescapeDataString(value));
                Task.Run(async () => await UserById(userId));
              }
        }
        
    }
}
