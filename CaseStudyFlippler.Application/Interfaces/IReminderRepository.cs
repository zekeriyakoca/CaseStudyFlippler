using CaseStudyFlippler.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyFlippler.Application.Interfaces
{
    public interface IReminderRepository : IRepository<Reminder>
    {
        IEnumerable<Reminder> GetAllLazy();
    }
}
