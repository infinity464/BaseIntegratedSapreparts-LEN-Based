namespace SchoolManagement.Application.DTOs.JobCategoryMaintenences
{
    public class CreateJobCategoryMaintenenceDto : IJobCategoryMaintenenceDto
    {
        public int JobCategoryMaintenenceId { get; set; }
        public string? CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
 