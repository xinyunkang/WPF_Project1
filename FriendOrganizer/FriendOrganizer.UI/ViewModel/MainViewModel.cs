using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        
        public MainViewModel(INavigationViewModel navigationViewModel,
            IFriendDetailViewModel friendDetailViewModel
            )   //pass in the interface service
        {
            NavigationViewModel = navigationViewModel;
            FriendDetailViewModel = friendDetailViewModel; //assign to a property
        }

        public INavigationViewModel NavigationViewModel { get; }

        public async Task LoadAsync()  //naming rule, the function must start with upper case.
        {
            NavigationViewModel.LoadAsync();
        }

        public IFriendDetailViewModel FriendDetailViewModel { get;  }  //create property. We don't need setter because we set the property directly in the constructor 


    }
}
