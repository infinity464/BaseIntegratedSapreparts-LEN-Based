using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class JobDescriptionScheduleMaintenence : BaseDomainEntity
    {
        public JobDescriptionScheduleMaintenence()
        {
            ScheduleMaintenances = new HashSet<ScheduleMaintenance>();
        }
        public int JobDescriptionScheduleMaintenenceId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? ItemDetailId { get; set; }
        public int? ScheduleMaintenenceTypeId { get; set; }
        public int? JobCategoryMaintenenceId { get; set; }
        public int? FiscalYearId { get; set; }
        public int? IssueRegisterId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public string? JobDescription { get; set; }

        public virtual BaseSchoolName? DepartmentName { get; set; }
        public virtual EquipmentName? EquipmentName { get; set; }
        public virtual FiscalYear? FiscalYear { get; set; }
        public virtual IssueRegister? IssueRegister { get; set; }
        public virtual ItemDetail? ItemDetail { get; set; }
        public virtual JobCategoryMaintenence? JobCategoryMaintenence { get; set; }
        public virtual ScheduleMaintenenceType? ScheduleMaintenenceType { get; set; }
        public virtual ICollection<ScheduleMaintenance> ScheduleMaintenances { get; set; }
    }
}
