namespace SchoolManagement.Application.DTOs.Priority
{
    public interface IPriorityDto
    {
        public int PriorityId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
