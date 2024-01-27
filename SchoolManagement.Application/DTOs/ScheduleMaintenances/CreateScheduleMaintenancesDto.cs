namespace SchoolManagement.Application.DTOs.ScheduleMaintenances
{
    public class CreateScheduleMaintenanceDto : IScheduleMaintenanceDto
    {
        public int ScheduleMaintenanceId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? IssueRegisterId { get; set; }
        public int? ItemDetailId { get; set; }
        public int? ScheduleMaintenenceTypeId { get; set; }
        public int? FiscalYearId { get; set; }
        public int? JobDescriptionScheduleMaintenenceId { get; set; }
        public int? ByWhomId { get; set; }
        public int? ProgresId { get; set; }
        public int? CompletedStatus { get; set; }
        public string? DrNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
 