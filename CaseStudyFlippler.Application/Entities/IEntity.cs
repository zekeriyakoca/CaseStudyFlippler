using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyFlippler.Application.Entities
{
    public interface IEntity
    {
        public int Id { get; set; }
    }

    public interface IHasCreationTime
    {
        DateTime Created { get; set; }
    }

    public interface ICreationAudited : IHasCreationTime
    {
        int? LastCreaterUserId { get; set; }
    }
    public interface IHasModificationTime
    {
        DateTime Updated { get; set; }
    }

    public interface IModificationAudited : IHasModificationTime
    {
        int? LastModifierUserId { get; set; }
    }
    public interface IAudited : ICreationAudited, IModificationAudited
    {

    }
    public interface ITimeStamped : IHasCreationTime, IHasModificationTime
    {

    }
}
