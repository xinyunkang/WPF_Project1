﻿using System.Threading.Tasks;
using FriendOrganizer.Model;
using System.Data.Entity;
using FriendOrganizerDataAccess;

namespace FriendOrganizer.UI.Data.Repositories
{
    public class MeetingRepository : GenericRepository<Meeting, FriendOrganizerDbContext>, IMeetingRepository
    {
        public MeetingRepository(FriendOrganizerDbContext context) : base(context)
        {
        }

        public async override Task<Meeting> GetByIdAsync(int id)
        {
            return await Context.Meetings
              .Include(m => m.Friends)
              .SingleAsync(m => m.Id == id);
        }
    }
}
