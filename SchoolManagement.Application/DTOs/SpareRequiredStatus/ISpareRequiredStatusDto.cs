namespace SchoolManagement.Application.DTOs.SpareRequiredStatus
{
    public interface ISpareRequiredStatusDto
    {
        public int SpareRequiredStatusId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}
