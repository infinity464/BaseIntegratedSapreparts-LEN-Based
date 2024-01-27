namespace SchoolManagement.Application.DTOs.Group
{
    public class CreateGroupDto : IGroupDto
    {
        public int GroupId { get; set; }
        public string? GroupName { get; set; } 
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
 