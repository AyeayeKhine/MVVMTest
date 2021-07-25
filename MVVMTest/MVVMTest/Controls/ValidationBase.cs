using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MVVMTest.Controls
{
    public class ValidationBase : BindableBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public ValidationBase()
        {
            ErrorsChanged += ValidationBase_ErrorsChanged;
        }

        private void ValidationBase_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            //OnPropertyChanged("HasErrors");
            //OnPropertyChanged("ErrorsList");
        }

        #region INotifyDataErrorInfo Members

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (_errors.ContainsKey(propertyName) && (_errors[propertyName].Any()))
                {
                    return _errors[propertyName].ToList();
                }
                else
                {
                    return new List<string>();
                }
            }
            else
            {
                return _errors.SelectMany(err => err.Value.ToList()).ToList();
            }
        }

        public bool HasErrors
        {
            get
            {
                return _errors.Any(propErrors => propErrors.Value.Any());
            }
        }

        #endregion
    }
}
