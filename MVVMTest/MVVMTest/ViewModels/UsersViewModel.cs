using GalaSoft.MvvmLight.Ioc;
using MVVMTest.Interfaces;
using MVVMTest.Models;
using MVVMTest.ViewModels.Base;
using MVVMTest.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MVVMTest.ViewModels
{
   public class UsersViewModel : ViewModelBase
    {
        private User _selectedItem;
        public ObservableCollection<User> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<User> ItemTapped { get; }

        protected readonly IRepository<User> _repository;
        public UsersViewModel()
        {
            Title = "User List";
            Items = new ObservableCollection<User>();
            _repository = SimpleIoc.Default.GetInstance<IRepository<User>>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<User>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await _repository.GetItemsAsync(true);
                foreach (var item in items)
                {
                    item.Position = "Position is " + item.Position;
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public User SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync("//RegistrationPage");
        }

        async void OnItemSelected(User item)
        {
            if (item == null)
                return;
            string UserId = item.UserId.ToString();
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(UserDetailPage)}?{nameof(UserDetailViewModel.UserId)}={UserId}");
            //await Shell.Current.GoToAsync($"//UserDetailPage?UserId={ UserId }");
        }
    }
}
