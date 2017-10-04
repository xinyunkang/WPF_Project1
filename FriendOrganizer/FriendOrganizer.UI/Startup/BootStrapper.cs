using Autofac;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.View.Services;
using FriendOrganizer.UI.ViewModel;
using FriendOrganizerDataAccess;
using Prism.Events;

namespace FriendOrganizer.UI.Startup
{
    public class BootStrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance(); //Prism.core

            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<FriendOrganizerDbContext>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<FriendDetailViewModel>().As<IFriendDetailViewModel>();




            // builder.RegisterType<LookupDataService>().As<IFriendLookupDataService>();  
            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();  //Do not want to use single interface.
            builder.RegisterType<FriendRepository>().As<IFriendRepository>();   //Want to use FriendDataService, whenever an IFriendDataService is Required.
                                                                                  //When an IFriendDataService is required somewhere, it would just create of the FriendDataService Class.
            return builder.Build();
        }
    }
}
