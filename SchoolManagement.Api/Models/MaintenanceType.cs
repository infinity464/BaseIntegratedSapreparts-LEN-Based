using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class MaintenanceType
    {
        public MaintenanceType()
        {
            JobDescriptionEntryPms = new HashSet<JobDescriptionEntryPm>();
            MaintenanceCategories = new HashSet<MaintenanceCategory>();
            MaintenancePlannings = new HashSet<MaintenancePlanning>();
            MaintenanceSchedules = new HashSet<MaintenanceSchedule>();
            PreventiveMaintenances = new HashSet<PreventiveMaintenance>();
            SpareRequiredEntries = new HashSet<SpareRequiredEntry>();
        }

        public int MaintenanceTypeId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public int? DepartmentNameId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName DepartmentName { get; set; }
        public virtual ICollection<JobDescriptionEntryPm> JobDescriptionEntryPms { get; set; }
        public virtual ICollection<MaintenanceCategory> MaintenanceCategories { get; set; }
        public virtual ICollection<MaintenancePlanning> MaintenancePlannings { get; set; }
        public virtual ICollection<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        public virtual ICollection<PreventiveMaintenance> PreventiveMaintenances { get; set; }
        public virtual ICollection<SpareRequiredEntry> SpareRequiredEntries { get; set; }
    }
}
