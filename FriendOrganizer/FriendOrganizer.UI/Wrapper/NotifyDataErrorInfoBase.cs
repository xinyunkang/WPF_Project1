using FriendOrganizer.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Collections;

namespace FriendOrganizer.UI.Wrapper
{
    
        public class NotifyDataErrorInfoBase:ViewModelBase,INotifyDataErrorInfo
        {
            private Dictionary<string, List<string>> _errorsByPropertyName
            = new Dictionary<string, List<string>>();

            public bool HasErrors => _errorsByPropertyName.Any(); //return true if there is an entry in the _errorsByPropertyName dictionary.

            public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

            public IEnumerable GetErrors(string propertyName)
            {
                return _errorsByPropertyName.ContainsKey(propertyName)
                    ? _errorsByPropertyName[propertyName] : null;

            }

            //helper method to raise the ErrorsChanged event.
            protected void OnErrorsChanged(string propertyName)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName)); //if ErrorsChanged is not null...
                base.OnPropertyChanged(nameof(HasErrors));
            }

            //add two helper method to maintain the Errors dictionary
            protected void AddError(string propertyName, string error)
            {
                if (!_errorsByPropertyName.ContainsKey(propertyName))
                {
                    _errorsByPropertyName[propertyName] = new List<string>();
                }
                if (!_errorsByPropertyName[propertyName].Contains(error))
                {
                    _errorsByPropertyName[propertyName].Add(error);
                    OnErrorsChanged(propertyName);
                }
            }

            protected void ClearErrors(string propertyName)
            {
                if (_errorsByPropertyName.ContainsKey(propertyName))
                {
                    _errorsByPropertyName.Remove(propertyName);
                    OnErrorsChanged(propertyName);
                }
            }
        }
    
}
