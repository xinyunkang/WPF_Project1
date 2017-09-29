using FriendOrganizer.UI.ViewModel;
using System.Threading.Tasks;
using System.Windows;

namespace FriendOrganizer.UI
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel; //get or set the data context of an element when it participates in data binding.
            Loaded += MainWindow_Loaded; //add the load event, do not write dataloading call in constructor directly.
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)//Event handlers naturally return void, so async methods return void so that you can have an asynchronous event handler. 
        {
            await _viewModel.LoadAsync();
        }
    }
}
