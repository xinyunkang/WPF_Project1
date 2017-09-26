using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FriendOrganizer.UI.ViewModel
{

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;  //Interface created by INotifyPropertyChanged to notify the changes of property.

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)  //[CallerMemberName] Allows you to obtain the method or property name of the caller to the method.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); //'this' is MainViewModel, 
        }
    }


}
