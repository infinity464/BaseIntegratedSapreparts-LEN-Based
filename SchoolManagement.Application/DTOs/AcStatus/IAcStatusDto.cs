namespace SchoolManagement.Application.DTOs.AcStatuss
{
    public interface IAcStatusDto
    {
        public int AcStatusId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool? Status { get; set; }
        public bool IsActive { get; set; }
    }
}
