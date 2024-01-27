using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class MaintenanceSubCategory : BaseDomainEntity
    {
        public MaintenanceSubCategory()
        {
            MaintenancePlannings = new HashSet<MaintenancePlanning>();
            MaintenanceSchedules = new HashSet<MaintenanceSchedule>();
            PreventiveMaintenances = new HashSet<PreventiveMaintenance>();
        }

        public int MaintenanceSubCategoryId { get; set; }
        public int? MaintenanceCategoryId { get; set; }
        public int? DepartmentNameId { get; set; }
        public string? SubCategoryName { get; set; }
        public string? AllowedExtension { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName? DepartmentName { get; set; }
        public virtual MaintenanceCategory? MaintenanceCategory { get; set; }
        public virtual ICollection<MaintenancePlanning> MaintenancePlannings { get; set; }
        public virtual ICollection<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        public virtual ICollection<PreventiveMaintenance> PreventiveMaintenances { get; set; }
    }
}
