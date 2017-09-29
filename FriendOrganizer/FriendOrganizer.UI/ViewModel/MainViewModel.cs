using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        private IFriendDataService _friendDataService;
        private Friend _selectedFriend;


        public MainViewModel(IFriendDataService friendDataService)   //pass in the interface service
        {
            Friends = new ObservableCollection<Friend>();
            _friendDataService = friendDataService;

        }

        public async Task LoadAsync()  //naming rule, the function must start with upper case.
        {
            var friends = await _friendDataService.GetAllAsync();
            Friends.Clear();
            foreach(var friend in friends)
            {
                Friends.Add(friend);
            }
        }
        //ObservableCollection：  Represents a dynamic data collection that provides notifications when items get added, removed, or when the whole list is refreshed.
        public ObservableCollection<Friend> Friends { get; set; }



        public Friend SelectedFriend
        {
            get { return _selectedFriend; }
            set {
                _selectedFriend = value;
                OnPropertyChanged(); //the property is default to SelectedFriend
            }
        }

    }
}
