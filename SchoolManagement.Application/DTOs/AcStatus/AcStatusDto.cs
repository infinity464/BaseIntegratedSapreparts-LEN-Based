using SchoolManagement.Application.DTOs.AcStatuss;

namespace SchoolManagement.Application.DTOs.AcStatus
{
    public class AcStatusDto : IAcStatusDto
    {
        public int AcStatusId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool? Status { get; set; }
        public bool IsActive { get; set; }
    }
}
