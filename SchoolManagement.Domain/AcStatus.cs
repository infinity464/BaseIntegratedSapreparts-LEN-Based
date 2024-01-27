using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class AcStatus : BaseDomainEntity
    {
        

        public int AcStatusId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool? Status { get; set; }
        public bool IsActive { get; set; }

        
    }
}
