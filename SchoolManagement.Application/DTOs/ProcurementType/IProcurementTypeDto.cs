namespace SchoolManagement.Application.DTOs.ProcurementType
{
    public interface IProcurementTypeDto
    {
        public int ProcurementTypeId { get; set; }
        public string? TypeName { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
