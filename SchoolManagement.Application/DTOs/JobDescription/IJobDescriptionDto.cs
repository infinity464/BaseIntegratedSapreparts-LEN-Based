namespace SchoolManagement.Application.DTOs.JobDescription
{
    public interface IJobDescriptionDto
    {
        public int JobDescriptionId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
