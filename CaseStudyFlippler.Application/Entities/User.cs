using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyFlippler.Application.Entities
{
    public class User : EntityTimeStamped
    {
        public string FullName { get; set; }

        public virtual IEnumerable<Reminder> Reminders { get; set; }
    }
}
