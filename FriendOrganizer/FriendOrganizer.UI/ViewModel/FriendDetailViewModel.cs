using FriendOrganizer.UI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendOrganizer.Model;
using Prism.Events;
using FriendOrganizer.UI.Event;
using System.Windows.Input;
using Prism.Commands;

namespace FriendOrganizer.UI.ViewModel
{
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private IFriendDataService _friendDataService;
        private IEventAggregator _eventAggregator;

        public FriendDetailViewModel(IFriendDataService friendDataService,
            IEventAggregator eventAggregator)
        {
            _friendDataService = friendDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                .Subscribe(OnOpenFriendDetailView);   //To subscribe the event published by NavigationViewModel.

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private bool OnSaveCanExecute()
        {
            return true;
        }

        private async void OnSaveExecute()
        {
            await _friendDataService.SaveAsync(Friend); //pass in the Friend property
            _eventAggregator.GetEvent<AfterFriendSavedEvent>().Publish(
                new AfterFriendSavedEventArgs
                {
                    Id = Friend.Id,
                    DisplayMember=$"{Friend.FirstName} {Friend.LastName}"
                }
                );
        }

        private async void OnOpenFriendDetailView(int friendId)
        {
            await LoadAsync(friendId);
        }

        public async Task LoadAsync(int friendId)
        {
            Friend = await _friendDataService.GetByIdAsync(friendId);
        }

        //propfull tab tab
        private Friend _friend;

        public Friend Friend
        {
            get { return _friend; }
            set {
                _friend = value;
                OnPropertyChanged(); //In ViewModelBase
            }
        }

        public ICommand SaveCommand { get; } //do not need set, which is initialize directly in the constructor.

    }
}
