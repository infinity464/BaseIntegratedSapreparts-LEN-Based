namespace SchoolManagement.Application.DTOs.SpareRequisition
{
    public class CreateSpareRequisitionDto : ISpareRequisitionDto
    {
        public int SpareRequisitionId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}
 