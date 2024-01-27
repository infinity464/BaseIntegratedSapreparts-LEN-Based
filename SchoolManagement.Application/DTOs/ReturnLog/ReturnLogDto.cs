namespace SchoolManagement.Application.DTOs.ReturnLogs
{
    public class ReturnLogDto : IReturnLogDto
    {
        public int ReturnLogId { get; set; }
        public int? IssueRegisterId { get; set; }
        public int? ReturnStatusId { get; set; }
        public int? IssueQty { get; set; }
        public int? ReceiveQty { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? ReturnBy { get; set; }
        public bool IsActive { get; set; }
    }
}
