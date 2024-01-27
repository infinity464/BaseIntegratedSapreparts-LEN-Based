using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class ByWhom
    {
        public ByWhom()
        {
            CorrectiveMaintenences = new HashSet<CorrectiveMaintenence>();
            JobDescriptionEntryPms = new HashSet<JobDescriptionEntryPm>();
            ScheduleMaintenances = new HashSet<ScheduleMaintenance>();
            SpareRequiredEntries = new HashSet<SpareRequiredEntry>();
        }

        public int ByWhomId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CorrectiveMaintenence> CorrectiveMaintenences { get; set; }
        public virtual ICollection<JobDescriptionEntryPm> JobDescriptionEntryPms { get; set; }
        public virtual ICollection<ScheduleMaintenance> ScheduleMaintenances { get; set; }
        public virtual ICollection<SpareRequiredEntry> SpareRequiredEntries { get; set; }
    }
}
