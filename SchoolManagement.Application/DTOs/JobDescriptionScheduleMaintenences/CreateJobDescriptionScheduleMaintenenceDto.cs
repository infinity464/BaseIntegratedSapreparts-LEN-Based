namespace SchoolManagement.Application.DTOs.JobDescriptionScheduleMaintenences
{
    public class CreateJobDescriptionScheduleMaintenenceDto : IJobDescriptionScheduleMaintenenceDto
    {
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

    }
}
 