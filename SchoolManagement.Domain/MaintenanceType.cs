using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class MaintenanceType : BaseDomainEntity
    {
        public MaintenanceType()
        {
            MaintenancePlannings = new HashSet<MaintenancePlanning>();
            MaintenanceCategories = new HashSet<MaintenanceCategory>();
            MaintenanceSchedules = new HashSet<MaintenanceSchedule>();
            JobDescriptionEntryPms = new HashSet<JobDescriptionEntryPms>();
            PreventiveMaintenances = new HashSet<PreventiveMaintenance>();
            SpareRequiredEntries = new HashSet<SpareRequiredEntry>();
        }

        public int MaintenanceTypeId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public int? DepartmentNameId { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName? DepartmentName { get; set; }
        public virtual ICollection<SpareRequiredEntry> SpareRequiredEntries { get; set; }
        public virtual ICollection<JobDescriptionEntryPms> JobDescriptionEntryPms { get; set; }
        public virtual ICollection<MaintenancePlanning> MaintenancePlannings { get; set; }
        public virtual ICollection<MaintenanceCategory> MaintenanceCategories { get; set; }
        public virtual ICollection<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        public virtual ICollection<PreventiveMaintenance> PreventiveMaintenances { get; set; }
    }
}
