using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class JobCategoryMaintenence
    {
        public JobCategoryMaintenence()
        {
            JobDescriptionScheduleMaintenences = new HashSet<JobDescriptionScheduleMaintenence>();
        }

        public int JobCategoryMaintenenceId { get; set; }
        public string CategoryName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<JobDescriptionScheduleMaintenence> JobDescriptionScheduleMaintenences { get; set; }
    }
}
