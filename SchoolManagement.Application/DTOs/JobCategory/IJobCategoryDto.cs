namespace SchoolManagement.Application.DTOs.JobCategory
{
    public interface IJobCategoryDto
    {
        public int JobCategoryId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
