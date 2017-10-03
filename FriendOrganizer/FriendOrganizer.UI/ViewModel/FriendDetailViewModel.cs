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
using FriendOrganizer.UI.Wrapper;

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
            //To check condition if friend is changed.
            return Friend!=null&&!Friend.HasErrors;
        }

        private async void OnSaveExecute()
        {
            await _friendDataService.SaveAsync(Friend.Model); //pass in the Friend property
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
            //Friend = await _friendDataService.GetByIdAsync(friendId);  use friendwrapper instead.
            var friend = await _friendDataService.GetByIdAsync(friendId);
            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();//Raises CanExecuteChanged on the UI thread so every command invoker can requery to check if the command can execute.

        }

        //propfull tab tab
        private FriendWrapper _friend;

        public FriendWrapper Friend
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
