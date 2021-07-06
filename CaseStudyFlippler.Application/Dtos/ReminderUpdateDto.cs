using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyFlippler.Application.Dtos
{
    public class ReminderUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [MinLength(3), Required]
        public string Description { get; set; }

        [Required]
        public DateTime RemindAt { get; set; }

        //UserId cannot be updated
    }
}
