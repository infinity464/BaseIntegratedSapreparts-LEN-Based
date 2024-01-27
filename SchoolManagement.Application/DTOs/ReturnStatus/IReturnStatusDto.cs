namespace SchoolManagement.Application.DTOs.ReturnStatus
{
    public interface IReturnStatusDto
    {
        public int ReturnStatusId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}
