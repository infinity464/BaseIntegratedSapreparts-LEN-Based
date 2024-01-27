using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class Priority : BaseDomainEntity
    {
        public Priority()
        {
            CorrectiveMaintenences = new HashSet<CorrectiveMaintenence>();
        }
        public int PriorityId { get; set; }
        public string? Name { get; set; } 
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CorrectiveMaintenence> CorrectiveMaintenences { get; set; }
    }
}
