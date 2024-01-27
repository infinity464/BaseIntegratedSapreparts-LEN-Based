using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public class SpareRequisition : BaseDomainEntity
    {
        public SpareRequisition()
        {
            CorrectiveMaintenences = new HashSet<CorrectiveMaintenence>();
        }

        public int SpareRequisitionId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CorrectiveMaintenence> CorrectiveMaintenences { get; set; }
    }
}
