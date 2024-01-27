namespace SchoolManagement.Application.DTOs.MaintenanceAuthority
{
    public class CreateMaintenanceAuthorityDto : IMaintenanceAuthorityDto
    {
        public int MaintenanceAuthorityId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
 