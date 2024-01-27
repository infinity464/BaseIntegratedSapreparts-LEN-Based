using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class IssueType : BaseDomainEntity
    {
        public IssueType()
        {
            IssueRegisters = new HashSet<IssueRegister>();
          //  CorrectiveMaintenences = new HashSet<CorrectiveMaintenence>();
        }

       
        public int IssueTypeId { get; set; }
        public string? Name { get; set; } 
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<IssueRegister> IssueRegisters { get; set; }
       // public virtual ICollection<CorrectiveMaintenence> CorrectiveMaintenences { get; set; }
    }
}
