using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyFlippler.Application.Entities
{
    public abstract class Entity
    {
        private int _Id;

        [Key]
        public virtual int Id
        {
            get => _Id;
            protected set => _Id = value;
        }

    }
    public class EntityTimeStamped : Entity, ITimeStamped
    {
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; } 
    }
}
