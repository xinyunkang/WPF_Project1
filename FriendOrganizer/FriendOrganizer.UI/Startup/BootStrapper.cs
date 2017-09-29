using Autofac;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.ViewModel;
using FriendOrganizerDataAccess;

namespace FriendOrganizer.UI.Startup
{
    public class BootStrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FriendOrganizerDbContext>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<FriendDataService>().As<IFriendDataService>();   //Want to use FriendDataService, whenever an IFriendDataService is Required.
                                                                                  //When an IFriendDataService is required somewhere, it would just create of the FriendDataService Class.
            return builder.Build();
        }
    }
}
