using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyFlippler.Application.Dtos
{
    public class ReminderDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        //public int UserId { get; set; }

        public DateTime RemindAt { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
