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
            string detailViewModelName,
            IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            _detailViewModelName = detailViewModelName;
            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailViewExecute);
            _eventAggregator = eventAggregator;
        }

        private void OnOpenDetailViewExecute()
        {
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                       .Publish(
                new OpenDetailViewEventArgs
                {
                    Id = Id,
                    ViewModelName = _detailViewModelName
                }
                ); //call the event with pass in selected friendId
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

        private string _detailViewModelName;

        public ICommand OpenDetailViewCommand { get; }

        private IEventAggregator _eventAggregator;
    }
}
