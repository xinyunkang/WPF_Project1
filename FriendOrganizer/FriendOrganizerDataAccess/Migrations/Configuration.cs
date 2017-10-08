namespace FriendOrganizerDataAccess.Migrations
{
    using FriendOrganizer.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizerDataAccess.FriendOrganizerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendOrganizerDataAccess.FriendOrganizerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Friends.AddOrUpdate(
                f => f.FirstName,     //if FirstName exists in the db, update. else add.
                 new Friend { FirstName = "Thomas", LastName = "Huber" },
                new Friend { FirstName = "Andreas", LastName = "Boehler" },
                new Friend { FirstName = "Julia", LastName = "Huber" },
                new Friend { FirstName = "Chrissi", LastName = "Egin" }
                );

            context.ProgrammingLanguages.AddOrUpdate(
       pl => pl.Name,
       new ProgrammingLanguage { Name = "C#" },
       new ProgrammingLanguage { Name = "TypeScript" },
       new ProgrammingLanguage { Name = "F#" },
       new ProgrammingLanguage { Name = "Swift" },
       new ProgrammingLanguage { Name = "Java" });

            context.SaveChanges(); //save first to ensure that the friend id exists when save the phone number.

            context.FriendPhoneNumbers.AddOrUpdate(pn => pn.Number,
            new FriendPhoneNumber { Number = "+49 12345678", FriendId = context.Friends.First().Id });


        }
    }
}
