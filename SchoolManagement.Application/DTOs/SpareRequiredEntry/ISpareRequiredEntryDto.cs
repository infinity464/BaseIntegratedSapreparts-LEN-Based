namespace SchoolManagement.Application.DTOs.SpareRequiredEntry
{
    public interface ISpareRequiredEntryDto
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
    }
}
