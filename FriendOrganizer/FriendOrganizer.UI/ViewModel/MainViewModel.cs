using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using static FriendOrganizer.UI.View.Services.MessageDialogService;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        
        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IFriendDetailViewModel> friendDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService
            )   //pass in the interface service
        {
            NavigationViewModel = navigationViewModel;
            _FriendDetailViewModelCreator = friendDetailViewModelCreator; //assign to a property
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
              .Subscribe(OnOpenFriendDetailView);   //To subscribe the event published by NavigationViewModel.

            CreateNewFriendCommand = new DelegateCommand(OnCreateNewFriendExecute);

        }

        

        private async void OnOpenFriendDetailView(int? friendId)
        {
            if (FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
               var result= _messageDialogService.ShowOkCancelDailog("You have made changes. Navigate away?", "Question");
                if(result==MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            FriendDetailViewModel = _FriendDetailViewModelCreator();
            await FriendDetailViewModel.LoadAsync(friendId);
        }

        public INavigationViewModel NavigationViewModel { get; }

        private Func<IFriendDetailViewModel> _FriendDetailViewModelCreator;

        public async Task LoadAsync()  //naming rule, the function must start with upper case.
        {
            NavigationViewModel.LoadAsync();
        }

        private IFriendDetailViewModel _friendDetailViewModel;

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return _friendDetailViewModel; }
            set {
                _friendDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        private void OnCreateNewFriendExecute()
        {
            OnOpenFriendDetailView(null);
        }

        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        public ICommand CreateNewFriendCommand { get; }
    }
}
