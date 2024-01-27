
namespace SchoolManagement.Application.DTOs.ReturnStatus
{
    public class ReturnStatusDto : IReturnStatusDto
    {
        public int ReturnStatusId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}
