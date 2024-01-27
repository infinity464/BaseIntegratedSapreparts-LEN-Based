using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class MaintenanceAuthority : BaseDomainEntity
    {
        public MaintenanceAuthority()
        {
            CorrectiveMaintenences = new HashSet<CorrectiveMaintenence>();
        } 
        public int MaintenanceAuthorityId { get; set; }
        public string? Name { get; set; } 
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CorrectiveMaintenence> CorrectiveMaintenences { get; set; }
    }
}
