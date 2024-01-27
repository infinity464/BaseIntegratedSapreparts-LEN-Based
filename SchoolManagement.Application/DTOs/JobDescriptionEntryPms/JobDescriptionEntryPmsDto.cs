namespace SchoolManagement.Application.DTOs.JobDescriptionEntryPms
{
    public class JobDescriptionEntryPmsDto : IJobDescriptionEntryPmsDto
    {
        public int JobDescriptionEntryPmsId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? MaintenanceTypeId { get; set; }
        public int? MaintenanceCategoryId { get; set; }
        public int? ByWhomId { get; set; }
        public int? ItemDetailId { get; set; }
        public int? PmsNumberEntryId { get; set; }
        public string? JobDescription { get; set; }
        public string? Jic { get; set; }
        public bool? SpareRequiredStatus { get; set; }
        public int? SpareRequiredStatusId { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        public string? DepartmentName { get; set; }
        public string? ItemName { get; set; }
        public string? MaintenanceType { get; set; }
        public string? MaintenanceCategory { get; set; }
        public string? SpareRequiredStatusNavigation { get; set; }
        public string? ByWhom { get; set; }
    }
}
