using FriendOrganizer.Model;
using FriendOrganizerDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        private Func<FriendOrganizerDbContext> _contextCreator;

        public FriendDataService(Func<FriendOrganizerDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public IEnumerable<Friend> GetAll()
        {
            //// TODO: Load data from real database
            //yield return new Friend { FirstName = "Thomas", LastName = "Huber" };
            //yield return new Friend { FirstName = "Andreas", LastName = "Boehler" };
            //yield return new Friend { FirstName = "Julia", LastName = "Huber" };
            //yield return new Friend { FirstName = "Chrissi", LastName = "Egin" };

           using(var ctx= _contextCreator())
            {
               return ctx.Friends.AsNoTracking().ToList();
            }
        }
    }
}
