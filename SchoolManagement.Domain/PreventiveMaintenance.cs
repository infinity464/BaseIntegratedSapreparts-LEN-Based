using System;
using System.Collections.Generic;
using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public class PreventiveMaintenance : BaseDomainEntity
    {
        public int PreventiveMaintenanceId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? IssueRegisterId { get; set; }
        public int? ItemDetailId { get; set; }
        public string? Pmsno { get; set; }
        public int? MaintenanceSubCategoryId { get; set; }
        public int? JobDescriptionEntryPmsId { get; set; }
        public int? MaintenanceCategoryId { get; set; }
        public int? MaintenanceTypeId { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int? CompletedStatus { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
        //public int? CompletedStatus { get; set; }

        public virtual BaseSchoolName? DepartmentName { get; set; }
        public virtual JobDescriptionEntryPms? JobDescriptionEntryPms { get; set; }
        public virtual IssueRegister? IssueRegister { get; set; }
        public virtual ItemDetail? ItemDetail { get; set; }
        public virtual MaintenanceCategory? MaintenanceCategory { get; set; }
        public virtual MaintenanceSubCategory? MaintenanceSubCategory { get; set; }
        public virtual MaintenanceType? MaintenanceType { get; set; }
    }
}
