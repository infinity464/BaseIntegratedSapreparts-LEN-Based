using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class MaintenanceCategory
    {
        public MaintenanceCategory()
        {
            JobDescriptionEntryPms = new HashSet<JobDescriptionEntryPm>();
            LifeLimitItemRunningHours = new HashSet<LifeLimitItemRunningHour>();
            MaintenancePlannings = new HashSet<MaintenancePlanning>();
            MaintenanceSchedules = new HashSet<MaintenanceSchedule>();
            MaintenanceSubCategories = new HashSet<MaintenanceSubCategory>();
            PreventiveMaintenances = new HashSet<PreventiveMaintenance>();
            SpareRequiredEntries = new HashSet<SpareRequiredEntry>();
        }

        public int MaintenanceCategoryId { get; set; }
        public int? MaintenanceTypeId { get; set; }
        public string CategoryName { get; set; }
        public string Remarks { get; set; }
        public int? DepartmentNameId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName DepartmentName { get; set; }
        public virtual MaintenanceType MaintenanceType { get; set; }
        public virtual ICollection<JobDescriptionEntryPm> JobDescriptionEntryPms { get; set; }
        public virtual ICollection<LifeLimitItemRunningHour> LifeLimitItemRunningHours { get; set; }
        public virtual ICollection<MaintenancePlanning> MaintenancePlannings { get; set; }
        public virtual ICollection<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        public virtual ICollection<MaintenanceSubCategory> MaintenanceSubCategories { get; set; }
        public virtual ICollection<PreventiveMaintenance> PreventiveMaintenances { get; set; }
        public virtual ICollection<SpareRequiredEntry> SpareRequiredEntries { get; set; }
    }
}
