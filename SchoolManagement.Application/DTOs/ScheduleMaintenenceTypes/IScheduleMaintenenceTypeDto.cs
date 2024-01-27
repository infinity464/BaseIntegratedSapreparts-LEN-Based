namespace SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes
{
    public interface IScheduleMaintenenceTypeDto
    {
        public int ScheduleMaintenenceTypeId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}
