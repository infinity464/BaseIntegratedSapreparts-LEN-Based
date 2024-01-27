using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class JobDescription : BaseDomainEntity
    {
        public int JobDescriptionId { get; set; }
        public string? Name { get; set; } 
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        
    }
}
