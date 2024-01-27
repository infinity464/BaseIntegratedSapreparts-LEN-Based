using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class SpareRequiredStatus : BaseDomainEntity
    {
        public SpareRequiredStatus()
        {
            JobDescriptionEntryPms = new HashSet<JobDescriptionEntryPms>();
        }

        public int SpareRequiredStatusId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<JobDescriptionEntryPms> JobDescriptionEntryPms { get; set; }
    }
}
