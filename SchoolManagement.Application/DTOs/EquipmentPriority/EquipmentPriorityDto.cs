namespace SchoolManagement.Application.DTOs.EquipmentPrioritys
{
    public class EquipmentPriorityDto : IEquipmentPriorityDto
    {
        public int EquipmentPriorityId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
