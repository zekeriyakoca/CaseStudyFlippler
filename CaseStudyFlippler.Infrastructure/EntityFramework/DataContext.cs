using CaseStudyFlippler.Application.Entities;
using CaseStudyFlippler.Infrastructure.Configs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaseStudyFlippler.Infrastructure.EntityFramework
{
    public class DataContext : DbContext
    {
        private readonly DbContextOptions options;

        public DataContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
            this.options = options;
        }

        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ReminderConfiguration)));
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnSaveChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            OnSaveChanges();
            return base.SaveChanges();
        }

        private void OnSaveChanges()
        {
            var updatedEntities = ChangeTracker
                .Entries<IHasModificationTime>()
                .Where(e => e.State == EntityState.Modified 
                || e.State == EntityState.Added 
                || e.State == EntityState.Deleted /*This behaviour can be replaced with Deleted column*/);

            foreach (var item in updatedEntities)
            {
                (item.Entity as IHasModificationTime).Updated = DateTime.Now;
            }
            

            var insertedEntities = ChangeTracker
                .Entries<IHasCreationTime>()
                .Where(e => e.State == EntityState.Added);

            foreach (var item in insertedEntities)
            {
                (item.Entity as IHasCreationTime).Created = DateTime.Now;
            }
            
        }
    }
}
