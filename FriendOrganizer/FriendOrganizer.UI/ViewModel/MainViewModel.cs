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

            _eventAggregator.GetEvent<OpenDetailViewEvent>()
              .Subscribe(OnOpenDetailView);   //To subscribe the event published by NavigationViewModel.
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
                .Subscribe(AfterDetailDeleted);
            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);

        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            DetailViewModel = null;
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            if (DetailViewModel != null && DetailViewModel.HasChanges)
            {
               var result= _messageDialogService.ShowOkCancelDialog("You have made changes. Navigate away?", "Question");
                if(result==MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    DetailViewModel = _FriendDetailViewModelCreator();
                    break;
            }
           
            await DetailViewModel.LoadAsync(args.Id);
        }

        public INavigationViewModel NavigationViewModel { get; }

        private Func<IFriendDetailViewModel> _FriendDetailViewModelCreator;

        public async Task LoadAsync()  //naming rule, the function must start with upper case.
        {
            NavigationViewModel.LoadAsync();
        }

        private IDetailViewModel _detailViewModel;

        public IDetailViewModel DetailViewModel
        {
            get { return _detailViewModel; }
            set {
                _detailViewModel = value;
                OnPropertyChanged();
            }
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArgs {
                ViewModelName= viewModelType.Name
            });
        }

        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        public ICommand CreateNewDetailCommand { get; }
    }
}
