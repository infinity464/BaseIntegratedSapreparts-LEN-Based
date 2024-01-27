namespace SchoolManagement.Application.DTOs.StatusOfJob
{
    public class StatusOfJobDto : IStatusOfJobDto
    {
        public int StatusOfJobId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
