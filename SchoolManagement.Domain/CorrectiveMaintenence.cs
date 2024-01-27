using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class CorrectiveMaintenence:BaseDomainEntity
    {
        public int CorrectiveMaintenenceId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? PriorityId { get; set; }
        public int? MaintenanceAuthorityId { get; set; }
        public int? StatusOfJobId { get; set; }
        public int? ByWhomId { get; set; }
        public DateTime? DateOfDefect { get; set; }
        public string? DescriptionOfDefect { get; set; }
        public string? WorkOrderNo { get; set; }
        public DateTime? WorkOrderDate { get; set; } 
        public DateTime? WorkStartDate { get; set; }
        public string? RefDocument { get; set; }
        public int? SpareRequisitionId { get; set; }
        public DateTime? WorkCompletedDate { get; set; }
        public int? CompletedStatus { get; set; }
        public bool IsActive { get; set; }
        public int? IssueRegisterId { get; set; }
        //public int? IssueTypeId { get; set; }
        //public int? ItemCategoryId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? ItemDetailId { get; set; }

        public virtual SpareRequisition? SpareRequisition { get; set; }
        public virtual ByWhom? ByWhom { get; set; }
        public virtual BaseSchoolName? DepartmentName { get; set; }
        public virtual EquipmentName? EquipmentName { get; set; }
        public virtual IssueRegister? IssueRegister { get; set; }
        public virtual ItemCategory? ItemCategory { get; set; }
        public virtual ItemDetail? ItemDetail { get; set; }
        public virtual MaintenanceAuthority? MaintenanceAuthority { get; set; }
        public virtual Priority? Priority { get; set; }
        public virtual StatusOfJob? StatusOfJob { get; set; }
    }
}
