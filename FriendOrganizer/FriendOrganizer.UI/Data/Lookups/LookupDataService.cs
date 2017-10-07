﻿using FriendOrganizer.Model;
using FriendOrganizerDataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Lookups
{
    public class LookupDataService : IFriendLookupDataService, IProgrammingLanguageLookupDataService
    {
        private Func<FriendOrganizerDbContext> _contextCreator;

        public LookupDataService(Func<FriendOrganizerDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<IEnumerable<LookupItem>> GetFriendLookupAsync()
        {
            using(var ctx = _contextCreator())
            {
                return await ctx.Friends.AsNoTracking().Select( //asNoTracking: no cache in dbcontext.
                        f => new LookupItem
                        {
                            Id = f.Id,
                            DisplayMember = f.FirstName + " " + f.LastName
                        }
                    ).ToListAsync();
            }
        }

        public async Task<IEnumerable<LookupItem>> GetProgrammingLanguageLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.ProgrammingLanguages.AsNoTracking().Select( //asNoTracking: no cache in dbcontext.
                        f => new LookupItem
                        {
                            Id = f.Id,
                            DisplayMember = f.Name
                        }
                    ).ToListAsync();
            }
        }
    }
}
