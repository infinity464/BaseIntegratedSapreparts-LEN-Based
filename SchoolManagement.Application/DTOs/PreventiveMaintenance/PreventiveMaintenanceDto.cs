namespace SchoolManagement.Application.DTOs.PreventiveMaintenance
{
    public class PreventiveMaintenanceDto : IPreventiveMaintenanceDto
    {
        public int PreventiveMaintenanceId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? IssueRegisterId { get; set; }
        public int? ItemDetailId { get; set; }
        public string? Pmsno { get; set; }
        public int? MaintenanceSubCategoryId { get; set; }
        public int? MaintenanceCategoryId { get; set; }
        public int? JobDescriptionEntryPmsId { get; set; }
        public int? MaintenanceTypeId { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int? CompletedStatus { get; set; }
        public string? Remarks { get; set; } 
        public bool IsActive { get; set; }

        public string? PattNo { get; set; }
        public string? ItemName { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public string? MaintenanceType { get; set; }
        public string? MaintenenceCategory { get; set; } 
        public string? Jic { get; set; } 
        public string? JobDescription { get; set; }
        public string? ByWhome { get; set; }
    }
}
