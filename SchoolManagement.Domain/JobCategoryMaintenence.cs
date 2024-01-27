using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class JobCategoryMaintenence: BaseDomainEntity
    {
        public JobCategoryMaintenence()
        {
           JobDescriptionScheduleMaintenences = new HashSet<JobDescriptionScheduleMaintenence>();
        }

        public int JobCategoryMaintenenceId { get; set; }
        public string? CategoryName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<JobDescriptionScheduleMaintenence> JobDescriptionScheduleMaintenences { get; set; }
    }
}
