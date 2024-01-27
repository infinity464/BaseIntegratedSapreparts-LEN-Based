using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class PresentBillet : BaseDomainEntity
    {
        public int PresentBilletId { get; set; }
        public string PresentBilletName { get; set; } = null!;
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
