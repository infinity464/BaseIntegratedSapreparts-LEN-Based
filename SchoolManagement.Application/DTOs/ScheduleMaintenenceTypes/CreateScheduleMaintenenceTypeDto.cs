namespace SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes
{
    public class CreateScheduleMaintenenceTypeDto : IScheduleMaintenenceTypeDto
    {
        public int ScheduleMaintenenceTypeId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}
 