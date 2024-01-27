using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class Group : BaseDomainEntity
    {
        public Group()
        {
            Demands = new HashSet<Demand>();
        }
        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Demand> Demands { get; set; }
    }
}
