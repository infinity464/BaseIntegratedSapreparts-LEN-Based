using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class ScheduleMaintenenceType : BaseDomainEntity
    {
        public ScheduleMaintenenceType()
        {
            JobDescriptionScheduleMaintenences = new HashSet<JobDescriptionScheduleMaintenence>();
            ScheduleMaintenances = new HashSet<ScheduleMaintenance>();
        } 
        public int ScheduleMaintenenceTypeId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<JobDescriptionScheduleMaintenence> JobDescriptionScheduleMaintenences { get; set; }
        public virtual ICollection<ScheduleMaintenance> ScheduleMaintenances { get; set; }
    }
}
