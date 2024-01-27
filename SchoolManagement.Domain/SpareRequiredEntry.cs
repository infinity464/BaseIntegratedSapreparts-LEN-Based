using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public partial class SpareRequiredEntry : BaseDomainEntity
    {
        public int SpareRequiredEntryId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? JobDescriptionEntryPmsId { get; set; }
        public int? ItemDetailId { get; set; }
        public int? MaintenanceTypeId { get; set; }
        public int? MaintenanceCategoryId { get; set; }
        public int? ByWhomId { get; set; }
        public int? PmsNumberEntryId { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName? DepartmentName { get; set; }
        public virtual EquipmentName? EquipmentName { get; set; }
        public virtual ItemDetail? ItemDetail { get; set; }
        public virtual MaintenanceType? MaintenanceType { get; set; }
        public virtual MaintenanceCategory? MaintenanceCategory { get; set; }
        public virtual ByWhom? ByWhom { get; set; }
        public virtual PmsNumberEntry? PmsNumberEntry { get; set; }
        public virtual JobDescriptionEntryPms? JobDescriptionEntryPms { get; set; }
    }
}
