using CaseStudyFlippler.Application;
using CaseStudyFlippler.Application.Entities;
using CaseStudyFlippler.Application.Interfaces;
using CaseStudyFlippler.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyFlippler.Infrastructure.Repositories
{
    public class ReminderRepository : Repository<Reminder>, IReminderRepository
    {
        public ReminderRepository(DataContext context) : base(context)
        {

        }
        public IEnumerable<Reminder> GetAllLazy()
        {
            return Context.Set<Reminder>().AsNoTracking();
        }
        public override void Add(Reminder entity)
        {
            // This approach is just for samples and is not appropriate for this kinda validation. 
            // This validation should be moved to controller action and return BadRequest in case of user absent
            if (!Context.Users.Any(u => u.Id == entity.UserId))
                throw new BusinessException("User not found");
            base.Add(entity);
        }

        public override async Task<Reminder> Get(int id)
        {
            return await Context.Set<Reminder>().Include(r => r.User).SingleOrDefaultAsync(r => r.Id == id);
        }
    }
}
