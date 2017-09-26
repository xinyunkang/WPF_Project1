using FriendOrganizer.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FriendOrganizerDataAccess
{
    public class FriendOrganizerDbContext: DbContext
    {
        public FriendOrganizerDbContext():base("FriendOrganizerDb") //FriendOrganizerDb is the connectionstring name will be config in app.config later.
        {

        }
        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //use singlar table name, instead of Plural
        }
    }
}
