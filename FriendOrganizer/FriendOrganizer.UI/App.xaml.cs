using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.ViewModel;
using FriendOrganizerDataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FriendOrganizerDataAccess;
using System;
using FriendOrganizer.UI.Startup;
using Autofac;

namespace FriendOrganizer.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var container = new BootStrapper().BootStrap();
            var mainWindow = container.Resolve<MainWindow>();
            //var mainWindow = new MainWindow(
            //    new MainViewModel(
            //        new FriendDataService()));
            mainWindow.Show();
        }
    }
}
