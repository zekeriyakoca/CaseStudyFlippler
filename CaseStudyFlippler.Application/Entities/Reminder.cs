using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyFlippler.Application.Entities
{
    public class Reminder : EntityTimeStamped
    {
        public string Description { get; set; }

        [Required]
        public DateTime RemindAt { get; set; }

        [ForeignKey(nameof(User))]
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
