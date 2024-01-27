using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.Models
{
    public partial class MaintenanceSubCategory
    {
        public MaintenanceSubCategory()
        {
            MaintenancePlannings = new HashSet<MaintenancePlanning>();
            MaintenanceSchedules = new HashSet<MaintenanceSchedule>();
        }

        public int MaintenanceSubCategoryId { get; set; }
        public int? MaintenanceCategoryId { get; set; }
        public int? DepartmentNameId { get; set; }
        public string? SubCategoryName { get; set; }
        public string? AllowedExtension { get; set; }
        public string? Remarks { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName? DepartmentName { get; set; }
        public virtual MaintenanceCategory? MaintenanceCategory { get; set; }
        public virtual ICollection<MaintenancePlanning> MaintenancePlannings { get; set; }
        public virtual ICollection<MaintenanceSchedule> MaintenanceSchedules { get; set; }
    }
}
