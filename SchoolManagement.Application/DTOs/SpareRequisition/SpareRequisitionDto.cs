namespace SchoolManagement.Application.DTOs.SpareRequisition
{
    public class SpareRequisitionDto : ISpareRequisitionDto
    {
        public int SpareRequisitionId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}
