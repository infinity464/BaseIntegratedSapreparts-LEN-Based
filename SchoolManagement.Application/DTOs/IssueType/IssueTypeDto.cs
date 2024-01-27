namespace SchoolManagement.Application.DTOs.IssueType
{
    public class IssueTypeDto : IIssueTypeDto
    {
        public int IssueTypeId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
