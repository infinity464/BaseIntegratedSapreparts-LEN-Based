using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class ByWhom : BaseDomainEntity
    {
        public ByWhom()
        {
            CorrectiveMaintenences = new HashSet<CorrectiveMaintenence>();
            JobDescriptionEntryPms = new HashSet<JobDescriptionEntryPms>();
            ScheduleMaintenances = new HashSet<ScheduleMaintenance>();
            SpareRequiredEntries = new HashSet<SpareRequiredEntry>();
        }

        public int ByWhomId { get; set; }
        public string? Name { get; set; } 
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<SpareRequiredEntry> SpareRequiredEntries { get; set; }
        public virtual ICollection<CorrectiveMaintenence> CorrectiveMaintenences { get; set; }
        public virtual ICollection<JobDescriptionEntryPms> JobDescriptionEntryPms { get; set; }
        public virtual ICollection<ScheduleMaintenance> ScheduleMaintenances { get; set; }
    }
}
