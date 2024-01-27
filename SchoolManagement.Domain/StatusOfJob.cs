using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class StatusOfJob : BaseDomainEntity
    {
        public StatusOfJob()
        {
            CorrectiveMaintenences = new HashSet<CorrectiveMaintenence>();
        } 
        public int StatusOfJobId { get; set; }
        public string? Name { get; set; } 
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CorrectiveMaintenence> CorrectiveMaintenences { get; set; }
    }
}
