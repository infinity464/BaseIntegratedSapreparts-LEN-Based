using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{ 
    public class ScheduleMaintenance : BaseDomainEntity
    {
        public int ScheduleMaintenanceId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? IssueRegisterId { get; set; }
        public int? ItemDetailId { get; set; }
        public int? ScheduleMaintenenceTypeId { get; set; }
        public int? FiscalYearId { get; set; }
        public int? JobDescriptionScheduleMaintenenceId { get; set; }
        public int? CompletedStatus { get; set; }
        public int? ByWhomId { get; set; }
        public int? ProgresId { get; set; } 
        public string? DrNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ByWhom? ByWhom { get; set; }
        public virtual BaseSchoolName? DepartmentName { get; set; }
        public virtual FiscalYear? FiscalYear { get; set; }
        public virtual IssueRegister? IssueRegister { get; set; }
        public virtual ItemDetail? ItemDetail { get; set; }
        public virtual JobDescriptionScheduleMaintenence? JobDescriptionScheduleMaintenence { get; set; }
        public virtual ScheduleMaintenenceType? ScheduleMaintenenceType { get; set; }
        public virtual Progres? Progres { get; set; }
    }
}
