using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class Opinion : BaseDomainEntity
    {
        public Opinion()
        {
            Acceptances = new HashSet<Acceptance>();
        }
        public int OpinionId { get; set; }
        public string? Name { get; set; } 
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Acceptance> Acceptances { get; set; }
    }
}
