namespace SchoolManagement.Application.DTOs.PmsNumberEntry
{
    public class CreatePmsNumberEntryDto : IPmsNumberEntryDto
    {
        public int PmsNumberEntryId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? ItemDetailId { get; set; }
        public string? PmsNo { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
 