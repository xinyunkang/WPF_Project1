using FriendOrganizer.UI.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationItemViewModel:ViewModelBase
    {
        

        public NavigationItemViewModel(int id, string displayMember,
            IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            OpenFriendDetailViewCommand = new DelegateCommand(OnOpenFriendDetailView);
            _eventAggregator = eventAggregator;
        }

        private void OnOpenFriendDetailView()
        {
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                       .Publish(Id); //call the event with pass in selected friendId
        }

        public int Id { get; }

        private string _displayMember;

        public string DisplayMember
        {
            get { return _displayMember; }
            set {
                _displayMember = value;
                OnPropertyChanged();  //inherited from ViewModelBase class
            }
        }

        public ICommand OpenFriendDetailViewCommand { get; }

        private IEventAggregator _eventAggregator;
    }
}
