using MVVMTest.ClsPublic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        //protected readonly IDialogService DialogService;
        //protected readonly INavigationService NavigationService;

        private bool _isBusy;
        public string title;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
        public ViewModelBase()
        {
            //DialogService = ViewModelLocator.Resolve<IDialogService>();
            //NavigationService = ViewModelLocator.Resolve<INavigationService>();
            //GlobalSetting.Instance.BaseEndpoint = ViewModelLocator.Resolve<ISettingsService>().UrlBase;
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
