using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class ScheduleMaintenenceType
    {
        public ScheduleMaintenenceType()
        {
            JobDescriptionScheduleMaintenences = new HashSet<JobDescriptionScheduleMaintenence>();
            ScheduleMaintenances = new HashSet<ScheduleMaintenance>();
        }

        public int ScheduleMaintenenceTypeId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<JobDescriptionScheduleMaintenence> JobDescriptionScheduleMaintenences { get; set; }
        public virtual ICollection<ScheduleMaintenance> ScheduleMaintenances { get; set; }
    }
}
