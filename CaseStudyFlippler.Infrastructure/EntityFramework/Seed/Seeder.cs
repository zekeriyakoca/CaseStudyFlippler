using CaseStudyFlippler.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyFlippler.Infrastructure.EntityFramework
{
    public class Seeder
    {
        public Seeder(DataContext context)
        {
            this.context = context;
        }

        public DataContext context { get; }

        public bool Seed()
        {
            try
            {
                context.Database.EnsureCreated();

                //TODO : Gather seeding operations into single transaction if entity count gets crazy.
                if (!context.Users.Any()) //Users come first
                {
                    AddUsers(context);
                }
                if (!context.Reminders.Any())
                {
                    AddReminders(context);
                }

            }
            catch
            {
                return false;
            }
            return true;
        }

        private void AddUsers(DataContext context)
        {
            context.Users.AddRange(new List<User> {
                new User()
                {
                    FullName = "Test User" 
                }
            });
            context.SaveChanges();
        }
        private void AddReminders(DataContext context)
        {
            var firstUser = context.Users.First();
            context.Reminders.AddRange(new List<Reminder> {
                new Reminder()
                {
                    Description = "Test Reminder",
                    RemindAt = DateTime.Now,
                    UserId = firstUser.Id
                }
            });
            context.SaveChanges();
        }
    }
}
