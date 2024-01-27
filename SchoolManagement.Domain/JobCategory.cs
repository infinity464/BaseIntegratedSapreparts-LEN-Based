using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class JobCategory : BaseDomainEntity
    {
        public int JobCategoryId { get; set; }
        public string? Name { get; set; } 
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        
    }
}
