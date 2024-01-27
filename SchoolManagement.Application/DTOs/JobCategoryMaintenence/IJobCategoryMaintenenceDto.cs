namespace SchoolManagement.Application.DTOs.JobCategoryMaintenences
{
    public interface IJobCategoryMaintenenceDto
    {
        public int JobCategoryMaintenenceId { get; set; }
        public string? CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
